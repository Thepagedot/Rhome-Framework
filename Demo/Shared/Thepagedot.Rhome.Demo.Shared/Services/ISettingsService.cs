using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thepagedot.Rhome.Demo.Shared.Services
{
    public interface ISettingsService
    {
        Task SaveSettingsAsync();
        Task LoadSettingsAsync();
    }
}
