using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thepagedot.Rhome.Demo.Shared.Models;

namespace Thepagedot.Rhome.Demo.Shared.Services
{
    public class SettingsService
    {
        private ILocalStorageService _LocalStorageService;

        public Configuration Configuration { get; set; }

        public SettingsService(ILocalStorageService localStorageService)
        {
            _LocalStorageService = localStorageService;
        }

        public async Task LoadSettingsAsync()
        {
            var configuration = await _LocalStorageService.LoadSettingsAsync();
            if (configuration != null)
                Configuration = configuration;
        }

        public async Task SaveSettingsAsync()
        {
            if (Configuration != null)
                await _LocalStorageService.SaveSettingsAsync(Configuration);
        }
    }
}
