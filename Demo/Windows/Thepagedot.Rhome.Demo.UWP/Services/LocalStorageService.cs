using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thepagedot.Rhome.Demo.Shared.Models;
using Thepagedot.Rhome.Demo.Shared.Services;

namespace Thepagedot.Rhome.Demo.UWP.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        public Task SaveSettingsAsync(Configuration configuration)
        {
            throw new NotImplementedException();
        }

        public Task<Configuration> LoadSettingsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
