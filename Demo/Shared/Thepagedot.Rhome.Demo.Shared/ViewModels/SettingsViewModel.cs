using System;
using System.Collections.Generic;
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

        private List<CentralUnit> _CentralUnits;
        public List<CentralUnit> CentralUnits
        {
            get { return _CentralUnits; }
            set { _CentralUnits = value; }
        }

        public SettingsViewModel(SettingsService settingsService)
        {
            _SettingsService = settingsService;

            CentralUnits = new List<CentralUnit>();
            CentralUnits.Add(new Ccu("Robby Demo", "192.168.0.14"));
        }

        public async Task InitializeAsync()
        {
            if (_SettingsService.Configuration != null)
                await _SettingsService.LoadSettingsAsync();

            CentralUnits = _SettingsService.Configuration.CentralUnits;
        }

        public async Task SaveSettingsAsync()
        {
            await _SettingsService.SaveSettingsAsync();
        }
    }
}