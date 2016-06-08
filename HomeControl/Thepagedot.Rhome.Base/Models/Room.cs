using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thepagedot.Rhome.Base.Interfaces;

namespace Thepagedot.Rhome.Base.Models
{
    public abstract class Room
    {
        public string Name { get; set; }
        public int Floor { get; set; }
		public List<Device> Devices { get; set; }

        protected IHomeControlAdapter _HomeControlPlatform;

        protected Room(string name, IHomeControlAdapter homeControlPlatform)
        {
            Name = name;
            Floor = 0;
            Devices = new List<Device>();

            _HomeControlPlatform = homeControlPlatform;
        }

        protected Room(string name, Uri imageUrl, List<Device> deviceList, IHomeControlAdapter homeControlPlatform)
        {
            Name = name;
            Floor = 0;
            Devices = deviceList;

            _HomeControlPlatform = homeControlPlatform;
        }

        public async Task UpdateStatesAsync()
        {
            await _HomeControlPlatform.UpdateStatesForRoomAsync(this);
        }
    }
}
