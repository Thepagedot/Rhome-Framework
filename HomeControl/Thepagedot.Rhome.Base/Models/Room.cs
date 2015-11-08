using System;
using System.Collections.Generic;

namespace Thepagedot.Rhome.Base.Models
{
    public abstract class Room
    {
        public string Name { get; set; }
        public int Floor { get; set; }
        public Uri ImageUrl { get; set; }
		public List<Device> DeviceList { get; set; }

        protected Room(string name)
        {
            Name = name;
            Floor = 0;
            DeviceList = new List<Device>();
        }

		protected Room(string name, Uri imageUrl)
        {
            Name = name;
		    ImageUrl = imageUrl;
            Floor = 0;
			DeviceList = new List<Device> ();
        }

        protected Room(string name, Uri imageUrl, List<Device> deviceList)
        {
            Name = name;
            ImageUrl = imageUrl;
            Floor = 0;
            DeviceList = deviceList;
        }
    }
}
