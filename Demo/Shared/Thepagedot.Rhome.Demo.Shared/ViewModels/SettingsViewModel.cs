using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thepagedot.Rhome.Base.Models;
using Thepagedot.Rhome.Demo.Shared.Services;
using Thepagedot.Rhome.HomeMatic.Models;

namespace Thepagedot.Rhome.Demo.Shared.ViewModels
{
    public class SettingsViewModel : AsyncViewModelBase
    {
        private SettingsService _SettingsService;

        private ObservableCollection<CentralUnit> _CentralUnits;
        public ObservableCollection<CentralUnit> CentralUnits
        {
            get { return _CentralUnits; }
            set { _CentralUnits = value; RaisePropertyChanged();  }
        }

        public SettingsViewModel(SettingsService settingsService)
        {
            _SettingsService = settingsService;

            CentralUnits = new ObservableCollection<CentralUnit>();
            //CentralUnits.Add(new Ccu("Robby Demo", "192.168.0.14"));
        }

        public async Task InitializeAsync()
        {
            if (_SettingsService.Configuration != null)
                await _SettingsService.LoadSettingsAsync();

            CentralUnits = new ObservableCollection<CentralUnit>(_SettingsService.Configuration.CentralUnits);
        }

        public async Task SaveSettingsAsync()
        {
            await _SettingsService.SaveSettingsAsync();
        }

        public async Task AddCentralUnitAsync(CentralUnit centralUnit)
        {
            // Add central unit to list and configuration
            CentralUnits.Add(centralUnit);
            _SettingsService.Configuration.CentralUnits.Add(centralUnit);
            _SettingsService.Refresh();

            // Save settings
            await _SettingsService.SaveSettingsAsync();
        }

        public async Task DeleteCentralUnitAsync(CentralUnit centralUnit)
        {
            // Delete central unit from list and configuration
            CentralUnits.Remove(centralUnit);
            _SettingsService.Configuration.CentralUnits.Remove(centralUnit);
            _SettingsService.Refresh();

            // Save settings
            await _SettingsService.SaveSettingsAsync();
        }
    }
}