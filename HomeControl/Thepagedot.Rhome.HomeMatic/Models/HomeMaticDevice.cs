using System.Collections.Generic;
using Thepagedot.Rhome.Base.Models;
using Newtonsoft.Json;

namespace Thepagedot.Rhome.HomeMatic.Models
{
	public class HomeMaticDevice : Device<HomeMaticChannel>
    {
		public int IseId { get; set; }
		public string Address { get; set; }

		public HomeMaticDevice(string name, int iseId, string address) : base (name)
        {
            this.IseId = iseId;
            this.Address = address;
            this.ChannelList = new List<HomeMaticChannel>();
        }

        [JsonConstructor]
        public HomeMaticDevice(string name, int iseId, string address, List<HomeMaticChannel> channelList) : base(name)
        {
            this.IseId = iseId;
            this.Address = address;
            this.ChannelList = channelList;
        }
    }
}
