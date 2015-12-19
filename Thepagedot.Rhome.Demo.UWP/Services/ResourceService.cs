using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thepagedot.Rhome.Demo.Shared.Services;
using Windows.ApplicationModel.Resources;

namespace Thepagedot.Rhome.Demo.UWP.Services
{
    public class ResourceService : IResourceService
    {
        public string GetString(string key)
        {
            var resLoader = new ResourceLoader();
            return resLoader.GetString(key);
        }
    }
}
