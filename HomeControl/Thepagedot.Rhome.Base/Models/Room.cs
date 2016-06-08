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


        {
            Name = name;
            Floor = 0;
            Devices = new List<Device>();

            _HomeControlPlatform = homeControlPlatform;
        }

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
