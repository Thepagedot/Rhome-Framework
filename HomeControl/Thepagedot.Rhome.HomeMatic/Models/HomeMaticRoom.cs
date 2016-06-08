using System;
using System.Collections.Generic;
using Thepagedot.Rhome.Base.Models;
using Newtonsoft.Json;
using Thepagedot.Rhome.HomeMatic.Services;

namespace Thepagedot.Rhome.HomeMatic.Models
{
	public class HomeMaticRoom : Room
	{
		public int IseId { get; set; }
	    public List<int> ChannelIds { get; set; }

        [JsonConstructor]
        public HomeMaticRoom(int iseId, List<int> channelIdList, string name, int floor, Uri imageUrl, List<Device> deviceList, HomeMaticXmlApiAdapter homeMaticXmlApi) : base(name, imageUrl, deviceList, homeMaticXmlApi)
        {
            IseId = iseId;
            ChannelIds = new List<int>();
        }

        public HomeMaticRoom(string name, int iseId, List<int> channelIdList, HomeMaticXmlApiAdapter homeMaticXmlApi) : base(name, homeMaticXmlApi)
        {
            IseId = iseId;
            ChannelIds = channelIdList;
            Devices = new List<Device>();
        }
    }
}