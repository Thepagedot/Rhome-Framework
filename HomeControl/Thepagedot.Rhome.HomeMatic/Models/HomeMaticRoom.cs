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
        {
            IseId = iseId;
            ChannelIds = new List<int>();
        }

        {
            IseId = iseId;
            ChannelIds = channelIdList;
            Devices = new List<Device>();
        }
    }
}