using System;
using System.Collections.Generic;

namespace Thepagedot.Rhome.Base.Models
{
    public abstract class Room
    {
        public string Name { get; set; }
        public int Floor { get; set; }
		public List<Device> Devices { get; set; }

        protected Room(string name)
        {
            Name = name;
            Floor = 0;
            Devices = new List<Device>();
        }

        protected Room(string name, Uri imageUrl, List<Device> deviceList)
        {
            Name = name;
            Floor = 0;
            Devices = deviceList;
        }
    }
}
