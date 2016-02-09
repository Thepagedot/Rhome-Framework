using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
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
        private ILocalStorageService _SettingsService;
        private HomeControlService _HomeControlService;

        private List<Room> _Rooms;
        public List<Room> Rooms
        {
            get { return _Rooms; }
            set { _Rooms = value; RaisePropertyChanged(); }
        }

        private RelayCommand _RefreshCommand;
        public RelayCommand RefreshCommand
        {
            get
            {
                return _RefreshCommand ?? (_RefreshCommand = new RelayCommand(
                    async () =>
                    {
                        await RefreshAsync();
                    }));
            }
        }


        public MainViewModel(IResourceService resourceService, ILocalStorageService settingsService, HomeControlService homeControlService)
        {
            _ResourceService = resourceService;
            _SettingsService = settingsService;
            _HomeControlService = homeControlService;

            //var appTitle = _ResourceService.GetString("AppTitle");

            if (IsInDesignMode)
            {
                Rooms = new List<Room>
                {
                    new HomeMaticRoom("Bedroom", 0, new List<int>()),
                    new HomeMaticRoom("Living room", 0, new List<int>()),
                    new HomeMaticRoom("Kitchen", 0, new List<int>())
                };
            }
        }

        public async Task InitializeAsync()
        {
            // Prevent double loading
            if (IsLoading || IsLoaded)
                return;

            IsLoading = true;

            if (_HomeControlService.HomeMatic != null)
            {
                try
                {
                    Rooms = (await _HomeControlService.HomeMatic.GetRoomsWidthDevicesAsync()).ToList();
                }
                catch (HttpRequestException)
                {
                    //TODO: Load strings from ResourceService
                    RaiseConnectionError("Connection Error", "Failed to connect");
                }

            }

            IsLoaded = true;
            IsLoading = false;
        }

        public async Task RefreshAsync()
        {
            IsLoaded = false;
            await InitializeAsync();
        }
    }
}