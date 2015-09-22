using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Thepagedot.Rhome.Base.Interfaces;
using Thepagedot.Rhome.Base.Models;
using Thepagedot.Rhome.Base.Tools;
using Thepagedot.Rhome.HomeMatic.Models;

namespace Thepagedot.Rhome.HomeMatic.Services
{
    public class HomeMaticXmlApi : IHomeControlApi
    {
        private readonly Ccu Ccu;

        public HomeMaticXmlApi(Ccu ccu)
        {
            this.Ccu = ccu;
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            var roomList = new List<Room>();

            // Get xml response from API
            var url = new Uri(String.Format("http://{0}/config/xmlapi/roomlist.cgi", Ccu.Address));
            var xmlResponse = await Downloader.DownloadWebResponse(url);

            // Parse xml
            var xmlRoomList = XDocument.Parse(xmlResponse);
            foreach (var xmlRoom in xmlRoomList.Descendants("room"))
            {
                var roomName = xmlRoom.Attribute("name").Value;
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
                        foreach (var channel in homeMaticDevice.ChannelList)
                        {
                            // Compare devices channels ISE_IDs with the one from the room list
                            if (!homeMaticRoom.ChannelIdList.Contains(channel.IseId))
                                continue;

                            // Get state from state list
                            var datapoints = stateList.Where(d => d.ChannelIseId == channel.IseId).ToList();
                            if (datapoints.Any())
                                channel.SetState(datapoints);

                            // If a device has multiple channels only add it once
                            addDeviceToRoom = true;
                        }

                        if (addDeviceToRoom)
                            room.DeviceList.Add(device);
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

        public async Task<IEnumerable<Device>> GetDevicesAsync()
        {
            var deviceList = new List<Device>();

            // Get xml response from API
            var url = new Uri(String.Format("http://{0}/config/xmlapi/devicelist.cgi", Ccu.Address));
            var xmlResponse = await Downloader.DownloadWebResponse(url);

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

                    switch (channelType)
                    {
                        case 17:
                            channelList.Add(new TemperatureSlider(channelName, channelType, channelIseId, channelAddress));
                            break;
                        case 22:
                            channelList.Add(new Information(channelName, channelType, channelIseId, channelAddress));
                            break;
                        case 26:
                            channelList.Add(new Switcher(channelName, channelType, channelIseId, channelAddress));
                            break;
                        case 37:
                            channelList.Add(new Contact(channelName, channelType, channelIseId, channelAddress));
                            break;
                        case 36:
                            channelList.Add(new Shutter(channelName, channelType, channelIseId, channelAddress));
                            break;
                        case 38:
                            channelList.Add(new DoorHandle(channelName, channelType, channelIseId, channelAddress));
                            break;
                    }
                }

                deviceList.Add(new HomeMaticDevice(deviceName, deviceIseId, deviceAddress, channelList));
            }

            return deviceList;
        }

        public async Task<IEnumerable<Room>> GetRoomsWidthDevicesAsync()
        {
            var roomList = await GetRoomsAsync();

            foreach (var room in roomList)
            {
                await GetDevicesForRoomAsync(room);
            }

            return roomList;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id">Ise ID of the channel to update</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task SendChannelUpdateAsync(int id, object value)
        {
            var url = new Uri(String.Format("http://{0}/config/xmlapi/statechange.cgi?ise_id={1}&new_value={2}", Ccu.Address, id, value.ToString().ToLower()));
            await Downloader.DownloadWebResponse(url);
        }

        public async Task<object> GetChannelStateAsync(Channel channel)
        {
            var homeMaticChannel = channel as HomeMaticChannel;
            if (homeMaticChannel == null)
                throw new FormatException("Wrong Format. Please provide a HomeMatic channel to this API");

            // Get xml response from API
            var url = new Uri(String.Format("http://{0}/config/xmlapi/state.cgi?channel_id={1}", Ccu.Address, homeMaticChannel.IseId));
            var xmlResponse = await Downloader.DownloadWebResponse(url);

            // Parse xml
            var xmlDatapointList = XDocument.Parse(xmlResponse);
            foreach (XElement xmlDatapoint in xmlDatapointList.Descendants("datapoint"))
            {
                var datapointType = xmlDatapoint.Attribute("type").Value;
                if (datapointType.Equals("STATE") || datapointType.Equals("SETPOINT"))
                {
                    string datapointValue = xmlDatapoint.Attribute("value").Value;
                    return datapointValue;
                }
            }

            return null;
        }

        private async Task<List<Datapoint>> GetAllStatesAsync()
        {
            var stateList = new List<Datapoint>();
            var url = new Uri(String.Format("http://{0}/config/xmlapi/statelist.cgi", Ccu.Address));

            var xmlResponse = await Downloader.DownloadWebResponse(url);
            var xmlDeviceList = XDocument.Parse(xmlResponse);
            foreach (XElement xmlDevice in xmlDeviceList.Descendants("device"))
            {
                foreach (XElement xmlChannel in xmlDevice.Descendants("channel"))
                {
                    int channelIdeId = Convert.ToInt32(xmlChannel.Attribute("ise_id").Value);
                    foreach (XElement xmlDatapoint in xmlChannel.Descendants("datapoint"))
                    {
                        var datapointType = xmlDatapoint.Attribute("type").Value;
                        if (datapointType.Equals("STATE") || datapointType.Equals("SETPOINT") || datapointType.Equals("LEVEL") || datapointType.Equals("STOP"))
                        {
                            var datapointIseId = Convert.ToInt32(xmlDatapoint.Attribute("ise_id").Value);
                            var datapointValue = xmlDatapoint.Attribute("value").Value;
                            var datapointUnit = xmlDatapoint.Attribute("valueunit").Value;
                            stateList.Add(new Datapoint(datapointType, datapointIseId, channelIdeId, datapointValue, datapointUnit));
                        }
                    }
                }
            }

            return stateList;
        }

        #region Helper

        public async Task<bool> CheckConnectionAsync()
        {
            try
            {
                await Downloader.DownloadWebResponse(new Uri(String.Format("http://{0}", Ccu.Address)));
                Debug.WriteLine("CCU found. IP-Address: " + Ccu.Address);
            }
            catch (WebException e)
            {
                Debug.WriteLine("ERROR: CCU has not been found. Exception: " + e.Message);
                return false;
            }

            try
            {
                XDocument xmlApiVersion = XDocument.Parse(await Downloader.DownloadWebResponse(new Uri(String.Format("http://{0}/addons/xmlapi/version.cgi", Ccu.Address))));
                string version = xmlApiVersion.Element("version").Value;
                Debug.WriteLine("XML API found. Version: " + version);
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
