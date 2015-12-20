using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thepagedot.Rhome.Base.Models;
using Thepagedot.Rhome.HomeMatic.Models;

namespace Thepagedot.Rhome.Demo.Shared.ViewModels
{
    public class RoomViewModel : AsyncViewModelBase
    {
        private Room _CurrentRoom;
        public Room CurrentRoom
        {
            get { return _CurrentRoom; }
            set { _CurrentRoom = value; RaisePropertyChanged(); }
        }

        public RoomViewModel()
        {
            var room = new HomeMaticRoom("Living room", 0, new List<int>());
            room.Devices = new List<Device>
            {
                new HomeMaticDevice("Testdevice 1", 0, "")
                {
                    Channels = new List<HomeMaticChannel>
                    {
                        new Switcher("Testswitcher", 1, 1, "", true)
                    }
                },

                new HomeMaticDevice("Testdevice 2", 0, "")
                {
                    Channels = new List<HomeMaticChannel>
                    {
                        new Shutter("Testshutter", 1, 1, "", true)
                    }
                },

                new HomeMaticDevice("Testdevice 3", 0, "")
                {
                    Channels = new List<HomeMaticChannel>
                    {
                        new TemperatureSlider("Testslider", 1, 1, "", true)
                    }
                }
            };

            CurrentRoom = room;
        }

    }
}
