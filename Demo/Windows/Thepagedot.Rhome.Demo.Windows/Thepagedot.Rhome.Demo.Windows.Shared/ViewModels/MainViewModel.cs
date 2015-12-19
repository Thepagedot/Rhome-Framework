using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Thepagedot.Rhome.Base.Models;
using Thepagedot.Rhome.Demo.Win.Common;
using Thepagedot.Rhome.HomeMatic.Models;

namespace Thepagedot.Rhome.Demo.Win.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private static MainViewModel _Current;
        public static MainViewModel Current
        {
            get { return _Current ?? (_Current = new MainViewModel()); }
        }

        private ObservableCollection<Room> _RoomList;
        public ObservableCollection<Room> RoomList
        {
            get { return _RoomList ?? (_RoomList = new ObservableCollection<Room>()); }
            set { SetProperty(ref _RoomList, value); }
        }

        public MainViewModel()
        {
            _Current = this;

            if (DesignMode.DesignModeEnabled)
                LoadDemoData();
        }

        private void LoadDemoData()
        {
            var livingRoom = new HomeMaticRoom("Wohnzimmer", new Uri("ms-appx:///Assets/Header/House.jpg"), 1);
            var lightSwitch = new HomeMaticDevice("Light Switch", 0, "", new List<HomeMaticChannel> {new Switcher("Lamp", 12, 0, "")});
            livingRoom.Devices.Add(lightSwitch);

            RoomList.Add(livingRoom);
            RoomList.Add(new HomeMaticRoom("Küche", new Uri("ms-appx:///Assets/Header/House.jpg"), 1));
            RoomList.Add(new HomeMaticRoom("Bad", new Uri("ms-appx:///Assets/Header/House.jpg"), 1));
            RoomList.Add(new HomeMaticRoom("Schlafzimmer", new Uri("ms-appx:///Assets/Header/House.jpg"), 1));
            RoomList.Add(new HomeMaticRoom("Flur", new Uri("ms-appx:///Assets/Header/House.jpg"), 1));
        }

        public async Task LoadDataAsync()
        {
            // Download rooms
            if (!RoomList.Any())
            {
                var roomList = await App.HomeMaticXmlApi.GetRoomsWidthDevicesAsync();
                if (roomList != null)
                {
                    foreach (var room in roomList)
                    {
                        RoomList.Add(room);
                    }
                }
            }
        }
    }
}
