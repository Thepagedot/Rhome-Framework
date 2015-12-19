using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thepagedot.Rhome.Base.Models;
using Thepagedot.Rhome.Demo.Shared.Services;
using Thepagedot.Rhome.HomeMatic.Models;

namespace Thepagedot.Rhome.Demo.Shared.ViewModels
{
    public class MainViewModel : AsyncViewModelBase
    {
        private IResourceService _ResourceService;
        private ISettingsService _SettingsService;

        private HomeControlService _HomeControlService;

        private List<Room> _Rooms;
        public List<Room> Rooms
        {
            get
            {
                return _Rooms;
            }
            set
            {
                _Rooms = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel(IResourceService resourceService, ISettingsService settingsService, HomeControlService homeControlService)
        {
            _ResourceService = resourceService;
            _SettingsService = settingsService;
            _HomeControlService = homeControlService;

            //var appTitle = _ResourceService.GetString("AppTitle");

            Rooms = new List<Room>
            {
                new HomeMaticRoom("Bedroom", 0, new List<int>()),
                new HomeMaticRoom("Living room", 0, new List<int>()),
                new HomeMaticRoom("Kitchen", 0, new List<int>())
            };
        }

        public async Task Initialize()
        {
            _HomeControlService.HomeMatic = new HomeMatic.Services.HomeMaticXmlApi(new Ccu("Demo", "192.168.0.14"));

            if (_HomeControlService.HomeMatic != null)
            {
                Rooms = (await _HomeControlService.HomeMatic.GetRoomsWidthDevicesAsync()).ToList();
            }
        }
    }
}