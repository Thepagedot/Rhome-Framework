using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Thepagedot.Rhome.Base.Interfaces;
using Thepagedot.Rhome.Base.Models;
using Thepagedot.Rhome.HomeMatic.Misc;
using Thepagedot.Rhome.HomeMatic.Models;
using Thepagedot.Tools;
using Thepagedot.Tools.Services;

namespace Thepagedot.Rhome.HomeMatic.Services
{
	public class HomeMaticXmlApi : IHomeControlApi<HomeMaticRoom, HomeMaticDevice, HomeMaticChannel>
	{
		public readonly Ccu Ccu;
		public bool IsDemoMode = false;

		private IHttpService _HttpService;

		public HomeMaticXmlApi(Ccu ccu, bool isDemoMode = false, string owner = null)
		{
			this.Ccu = ccu;
			this.IsDemoMode = isDemoMode;

			_HttpService = new HttpService();

			if (IsDemoMode)
				_HttpService = new MockedHttpService(owner);
		}

		public async Task<IEnumerable<HomeMaticRoom>> GetRoomsAsync()
		{
			var roomList = new List<HomeMaticRoom>();
			string xmlResponse;

			// Get xml response from API
			var url = $"http://{Ccu.Address}/config/xmlapi/roomlist.cgi";
			xmlResponse = await _HttpService.GetStringAsync(url, new TimeSpan(0, 0, 0, 60));
			if (xmlResponse == null)
				return null;

			// Parse xml
			var xmlRoomList = XDocument.Parse(xmlResponse);
			foreach (var xmlRoom in xmlRoomList.Descendants("room"))
			{
				var roomName = RoomNameResolver.Resolve(xmlRoom.Attribute("name").Value);
				var roomIseId = Convert.ToInt32(xmlRoom.Attribute("ise_id").Value);
				var channelIdsForRoomList = new List<int>();

				foreach (var xmlChannel in xmlRoom.Descendants("channel"))
				{
					var iseId = Convert.ToInt32(xmlChannel.Attribute("ise_id").Value);
					channelIdsForRoomList.Add(iseId);
				}

				roomList.Add(new HomeMaticRoom(roomName, roomIseId, channelIdsForRoomList));
			}

			return roomList;
		}

		public async Task GetDevicesForRoomAsync(Room room)
		{
			var stateList = await GetAllStatesAsync();

			var homeMaticRoom = room as HomeMaticRoom;
			if (homeMaticRoom != null)
			{
				// Get List of all devices
				var allDevicesList = await GetDevicesAsync();
				foreach (var device in allDevicesList)
				{
					var addDeviceToRoom = false;

					var homeMaticDevice = device as HomeMaticDevice;
					if (homeMaticDevice != null)
					{
						foreach (var channel in homeMaticDevice.Channels)
						{
							// Compare devices channels ISE_IDs with the one from the room list
							if (!homeMaticRoom.ChannelIds.Contains(channel.IseId))
								continue;

							// Get state from state list
							var datapoints = stateList.Where(d => d.ChannelIseId == channel.IseId).ToList();
							if (datapoints.Any())
								channel.SetState(datapoints);

							// If a device has multiple channels only add it once
							addDeviceToRoom = true;
						}

						if (addDeviceToRoom)
							room.Devices.Add(device);
					}
					else
					{
						throw new FormatException("Wrong Format. Non homatic devices detected that cannot be handeld.");
					}
				}
			}
			else
			{
				throw new FormatException("Wrong Format. Please provide a HomeMatic room to this API.");
			}
		}

		public async Task<IEnumerable<HomeMaticDevice>> GetDevicesAsync()
		{
			var deviceList = new List<HomeMaticDevice>();
			string xmlResponse;

			// Get xml response from API
			var url = $"http://{Ccu.Address}/config/xmlapi/devicelist.cgi";
			xmlResponse = await _HttpService.GetStringAsync(url, new TimeSpan(0, 0, 0, 60));
			if (xmlResponse == null)
				return null;

			// Parse xml
			var xmlDeviceList = XDocument.Parse(xmlResponse);
			foreach (var xmlDevice in xmlDeviceList.Descendants("device"))
			{
				var deviceName = xmlDevice.Attribute("name").Value;
				var deviceIseId = Convert.ToInt32(xmlDevice.Attribute("ise_id").Value);
				var deviceAddress = xmlDevice.Attribute("address").Value;

				// Get Channels
				var channelList = new List<HomeMaticChannel>();
				foreach (var xmlChannel in xmlDevice.Descendants("channel"))
				{
					var channelName = xmlChannel.Attribute("name").Value;
					var channelType = Convert.ToInt32(xmlChannel.Attribute("type").Value);
					var channelIseId = Convert.ToInt32(xmlChannel.Attribute("ise_id").Value);
					var channelAddress = xmlChannel.Attribute("address").Value;
					var channelIsVisible = Convert.ToBoolean(xmlChannel.Attribute("visible").Value);

					switch (channelType)
					{
						case 17:
							channelList.Add(new TemperatureSlider(channelName, channelType, channelIseId, channelAddress, channelIsVisible, this));
							break;
						case 22:
							channelList.Add(new Information(channelName, channelType, channelIseId, channelAddress, channelIsVisible, this));
							break;
						case 26:
							channelList.Add(new Switcher(channelName, channelType, channelIseId, channelAddress, channelIsVisible, this));
							break;
						case 37:
							channelList.Add(new Contact(channelName, channelType, channelIseId, channelAddress, channelIsVisible, this));
							break;
						case 36:
							channelList.Add(new Shutter(channelName, channelType, channelIseId, channelAddress, channelIsVisible, this));
							break;
						case 38:
							channelList.Add(new DoorHandle(channelName, channelType, channelIseId, channelAddress, channelIsVisible, this));
							break;
					}
				}

				deviceList.Add(new HomeMaticDevice(deviceName, deviceIseId, deviceAddress, channelList));
			}

			return deviceList;
		}

		public async Task<IEnumerable<Room>> GetRoomsWidthDevicesAsync()
		{
			var rooms = await GetRoomsAsync();
			var devices = await GetDevicesAsync();

			foreach (var device in devices)
			{
				foreach (var channel in device.Channels)
				{
					foreach (var room in rooms)
					{
						if (room.ChannelIds.Contains(channel.IseId))
						{
							if (!room.Devices.Contains(device))
							{
								room.Devices.Add(device);
							}

						}
					}
				}
			}

			return rooms;
		}

		#region Update

		public async Task UpdateStatesForRoomsAsync(IEnumerable<Room> rooms)
		{
			var allStates = await GetAllStatesAsync();

			var channels = new List<HomeMaticChannel>();
			foreach (var room in rooms)
				foreach (var device in room.Devices)
					foreach (var channel in ((HomeMaticDevice)device).Channels)
						channels.Add(channel);

			foreach (var channel in channels)
			{
				var datapoints = allStates.Where(s => s.ChannelIseId == channel.IseId);
				channel.SetState(datapoints);
			}
		}

		public async Task UpdateStatesForRoomAsync(Room room)
		{
			var allStates = await GetAllStatesAsync();
			var channels = new List<HomeMaticChannel>();

			foreach (var device in room.Devices)
				foreach (var channel in ((HomeMaticDevice)device).Channels)
					channels.Add(channel);

			foreach (var channel in channels)
			{
				var datapoints = allStates.Where(s => s.ChannelIseId == channel.IseId);
				if (datapoints.Any())
					channel.SetState(datapoints);
			}
		}

		#endregion

		/// <summary>
		///
		/// </summary>
		/// <param name="id">Ise ID of the channel to update</param>
		/// <param name="value"></param>
		/// <returns></returns>
		public async Task SendChannelUpdateAsync(int id, object value)
		{
			var url = $"http://{Ccu.Address}/config/xmlapi/statechange.cgi?ise_id={id}&new_value={value.ToString().ToLower()}";
			await _HttpService.GetStringAsync(url);
		}

		public async Task RunProgramAsync(int id)
		{
			var url = $"http://{Ccu.Address}/config/xmlapi/runprogram.cgi?program_id={id}";
			await _HttpService.GetStringAsync(url);
		}

		private async Task<List<Datapoint>> GetAllStatesAsync()
		{
			var stateList = new List<Datapoint>();
			string xmlResponse;

			var url = $"http://{Ccu.Address}/config/xmlapi/statelist.cgi";
			xmlResponse = await _HttpService.GetStringAsync(url, new TimeSpan(0, 0, 0, 20));

			var xmlDeviceList = XDocument.Parse(xmlResponse);
			foreach (XElement xmlDevice in xmlDeviceList.Descendants("device"))
			{
				foreach (XElement xmlChannel in xmlDevice.Descendants("channel"))
				{
					int channelIdeId = Convert.ToInt32(xmlChannel.Attribute("ise_id").Value);
					foreach (XElement xmlDatapoint in xmlChannel.Descendants("datapoint"))
					{
						var datapointType = xmlDatapoint.Attribute("type").Value;
						var datapointIseId = Convert.ToInt32(xmlDatapoint.Attribute("ise_id").Value);
						var datapointValue = xmlDatapoint.Attribute("value").Value;
						var datapointUnit = xmlDatapoint.Attribute("valueunit").Value;
						stateList.Add(new Datapoint(datapointType, datapointIseId, channelIdeId, datapointValue, datapointUnit));
					}
				}
			}

			return stateList;
		}

		public Task<object> GetChannelStateAsync(Channel channel)
		{
			throw new NotImplementedException();
		}

		public async Task<List<SystemVariable>> GetSystemVariablesAsync()
		{
			var varList = new List<SystemVariable>();

			// Get xml response from API
			var url = $"http://{Ccu.Address}/config/xmlapi/sysvarlist.cgi";
			var xmlResponse = await _HttpService.GetStringAsync(url, new TimeSpan(0, 0, 0, 60));

			// Parse xml
			var xmlVarList = XDocument.Parse(xmlResponse);
			foreach (var xmlVar in xmlVarList.Descendants("systemVariable"))
			{
				var iseId = Convert.ToInt32(xmlVar.Attribute("ise_id").Value);
				var name = xmlVar.Attribute("name").Value;
				var value = xmlVar.Attribute("value").Value;
				var valueList = xmlVar.Attribute("value_list").Value;
				var min = xmlVar.Attribute("min").Value;
				var max = xmlVar.Attribute("max").Value;
				var unit = xmlVar.Attribute("unit").Value;
				var type = Convert.ToInt32(xmlVar.Attribute("type").Value);
				var subType = Convert.ToInt32(xmlVar.Attribute("subtype").Value);
				var visible = Convert.ToBoolean(xmlVar.Attribute("visible").Value);
				var timeStamp = xmlVar.Attribute("timestamp").Value;
				var valueName0 = xmlVar.Attribute("value_name_0").Value;
				var valueName1 = xmlVar.Attribute("value_name_1").Value;

				var variable = new SystemVariable(iseId, name, value, valueList, min, max, unit, type, subType, visible, timeStamp, valueName0, valueName1);
				varList.Add(variable);
			}

			return varList;
		}

		public async Task<List<Program>> GetProgramsAsync()
		{
			var programList = new List<Program>();

			// Get xml response from API
			var url = $"http://{Ccu.Address}/config/xmlapi/programlist.cgi";
			var xmlResponse = await _HttpService.GetStringAsync(url, new TimeSpan(0, 0, 0, 60));

			// Parse xml
			var xmlProgramList = XDocument.Parse(xmlResponse);
			foreach (var xmlProgram in xmlProgramList.Descendants("program"))
			{
				var id = Convert.ToInt32(xmlProgram.Attribute("id").Value);
				var active = Convert.ToBoolean(xmlProgram.Attribute("active").Value);
				var timeStamp = xmlProgram.Attribute("timestamp").Value;
				var name = xmlProgram.Attribute("name").Value;
				var description = xmlProgram.Attribute("description").Value;
				var visible = Convert.ToBoolean(xmlProgram.Attribute("visible").Value);
				var operate = Convert.ToBoolean(xmlProgram.Attribute("operate").Value);

				var program = new Program(id, active, timeStamp, name, description, visible, operate, this);
				programList.Add(program);
			}

			return programList;
		}

		public async Task<List<SystemNotification>> GetSystemNotificationsAsync()
		{




			return null;
		}

		#region Helper

		public async Task<bool> CheckConnectionAsync()
		{
			var result = await _HttpService.GetStringAsync($"http://{Ccu.Address}", null, new TimeSpan(0, 0, 10));
			if (result == null)
				return false;

			return true;
		}

		public async Task<bool> CheckAvailabilityAsync()
		{
			try
			{
				XDocument xmlApiVersion = XDocument.Parse(await _HttpService.GetStringAsync($"http://{Ccu.Address}/addons/xmlapi/version.cgi", null, new TimeSpan(0, 0, 10)));
				var xElement = xmlApiVersion.Element("version");
				if (xElement != null)
				{
					var version = xElement.Value;
					Debug.WriteLine("XML API found. Version: " + version);
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine("ERROR: XML API of the CCU has not been found. Exception: " + e.Message);
				return false;
			}

			return true;
		}

		#endregion
	}
}
