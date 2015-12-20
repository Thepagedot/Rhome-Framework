using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thepagedot.Rhome.Demo.Shared.Models;

namespace Thepagedot.Rhome.Demo.Shared.Services
{
    public interface ILocalStorageService
    {
        Task SaveSettingsAsync(Configuration configuration);
        Task<Configuration> LoadSettingsAsync();
    }
}
