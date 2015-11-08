﻿using System;
using System.Collections.Generic;
using Thepagedot.Rhome.Base.Models;
using Newtonsoft.Json;

namespace Thepagedot.Rhome.HomeMatic.Models
{
	public class HomeMaticRoom : Room
	{
		public int IseId { get; set; }
	    public List<int> ChannelIdList { get; set; }

        [JsonConstructor]
        public HomeMaticRoom(int iseId, List<int> channelIdList, string name, int floor, Uri imageUrl, List<Device> deviceList) : base(name, imageUrl, deviceList)
        {
            IseId = iseId;
            ChannelIdList = new List<int>();
        }

        public HomeMaticRoom(string name, int iseId, List<int> channelIdList ) : base(name)
        {
            IseId = iseId;
            ChannelIdList = channelIdList;
            DeviceList = new List<Device>();
        }

		public HomeMaticRoom (string name, Uri imageUrl, int iseId) : base(name, imageUrl)
		{
			IseId = iseId;
            ChannelIdList = new List<int>();
			DeviceList = new List<Device>();
		}        
    }
}