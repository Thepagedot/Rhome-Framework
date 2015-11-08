using System;
using Thepagedot.Rhome.Base.Interfaces;
using System.Collections.Generic;
using Thepagedot.Rhome.HomeMatic.Services;
using Thepagedot.Rhome.HomeMatic.Models;
using Thepagedot.Rhome.Base.Models;
using System.Threading.Tasks;
using System.Linq;
using Android.Content;

namespace Thepagedot.Rhome.Demo.Droid
{
    public class DataHolder
    {
        // Currents
        public static DataHolder Current { get; set; }
        public Room CurrentRoom { get; set; }

        public HomeMaticXmlApi HomeMaticApi { get; set; }

        public List<CentralUnit> CentralUnits { get; set; }
        public List<Room> Rooms { get; set;}

        public DataHolder()
        {
            Current = this;
            CentralUnits = new List<CentralUnit>();
            Rooms = new List<Room>();

            //CreateDemoData();
        }

        public async Task Init(Context context)
        {
            // Load Settings
            await Settings.LoadSettings();
            if (Settings.Configuration != null)
            {
                CentralUnits = Settings.Configuration.CentralUnits;
                if (CentralUnits.Any())
                {
                    HomeMaticApi = new HomeMaticXmlApi(new Ccu(CentralUnits.First()));
                    Rooms = (await HomeMaticApi.GetRoomsWidthDevicesAsync()).ToList();
                }                
            }
            else
            {
                Settings.Configuration = new Thepagedot.Rhome.Demo.Shared.Models.ConfigurationSettings();
            }
        }

        private void CreateDemoData()
        {
            var ccu = new Ccu("HomeMatic Robby", "192.168.0.14");
            HomeMaticApi = new HomeMaticXmlApi(ccu);

            var room1 = new HomeMaticRoom("Living Room", 0, null);
            var room2 = new HomeMaticRoom("Kitchen", 0, null);
            var room3 = new HomeMaticRoom("Bathroom", 0, null);

            var device1 = new HomeMaticDevice("Device 1", 0, "");
            device1.ChannelList.Add(new Switcher("Lamp 1", 0, 0, ""));
            var device2 = new HomeMaticDevice("Device 2", 0, "");
            device2.ChannelList.Add(new Shutter("Shutters 1", 0, 0, ""));

            room1.DeviceList.Add(device1);
            room1.DeviceList.Add(device2);

            Rooms.Add(room1);
            Rooms.Add(room2);
            Rooms.Add(room3);
            Rooms.Add(room3);
        }
    }
}