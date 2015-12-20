using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thepagedot.Rhome.Demo.Shared.Models;
using Thepagedot.Rhome.HomeMatic.Models;

namespace Thepagedot.Rhome.Demo.Shared.Services
{
    public class SettingsService
    {
        private ILocalStorageService _LocalStorageService;
        private HomeControlService _HomeControlService;

        public Configuration Configuration { get; set; }

        public SettingsService(ILocalStorageService localStorageService, HomeControlService homeControlService)
        {
            _LocalStorageService = localStorageService;
            _HomeControlService = homeControlService;
        }

        public void Refresh()
        {
            // Reset
            _HomeControlService.HomeMatic = null;

            // Load central units
            if (Configuration.CentralUnits != null)
            {
                // HomeMatic
                var homeMaticCentral = Configuration.CentralUnits.FirstOrDefault(c => c.Brand == Base.Models.CentralUnitBrand.HomeMatic);
                if (homeMaticCentral != null && homeMaticCentral is Ccu)
                    _HomeControlService.HomeMatic = new HomeMatic.Services.HomeMaticXmlApi(homeMaticCentral as Ccu);
            }
        }

        public async Task LoadSettingsAsync()
        {
            var configuration = await _LocalStorageService.LoadSettingsAsync();
            if (configuration != null)
            {
                Configuration = configuration;
                Refresh();
            }
            else
            {
                Configuration = new Configuration();
            }
        }

        public async Task SaveSettingsAsync()
        {
            if (Configuration != null)
                await _LocalStorageService.SaveSettingsAsync(Configuration);
        }
    }
}
