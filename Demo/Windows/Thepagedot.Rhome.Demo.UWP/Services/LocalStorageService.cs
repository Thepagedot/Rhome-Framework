using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thepagedot.Rhome.Demo.Shared.Models;
using Thepagedot.Rhome.Demo.Shared.Services;
using Windows.Storage;

namespace Thepagedot.Rhome.Demo.UWP.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly JsonSerializerSettings _JsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
        private readonly string _FileName = "configuration.json";

        public async Task SaveSettingsAsync(Configuration configuration)
        {
            var json = JsonConvert.SerializeObject(configuration, Formatting.Indented, _JsonSerializerSettings);
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(_FileName, CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(file, json);
        }

        public async Task<Configuration> LoadSettingsAsync()
        {
            Configuration configuration;

            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(_FileName);
                var json = await FileIO.ReadTextAsync(file);

                configuration = JsonConvert.DeserializeObject<Configuration>(json, _JsonSerializerSettings);
            }
            catch (FileNotFoundException)
            {
                configuration = null;
            }

            return configuration;
        }
    }
}
