using System;
using Android.Content;
using Thepagedot.Rhome.Demo.Shared.Models;
using Newtonsoft.Json;
using Thepagedot.Rhome.Demo.Droid.Helpers;
using System.IO;
using System.Threading.Tasks;

namespace Thepagedot.Rhome.Demo.Droid
{
    public static class Settings
    {
        public static Configuration Configuration { get; set; }

        private static readonly string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private static readonly string configurationFileName = "configuration.json";
        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };             

        public static async Task SaveSettingsAsync()
        {
            var json = JsonConvert.SerializeObject(Configuration, Formatting.Indented, jsonSerializerSettings);
            var filePath = Path.Combine(folderPath, configurationFileName);
            using (var file = File.Open(filePath, FileMode.Create, FileAccess.Write))
            {
                using (var stream = new StreamWriter(file))
                {
                    await stream.WriteAsync(json);
                }
            }
        }

        public static async Task LoadSettingsAsync()
        {
            string json;
            var filePath = Path.Combine(folderPath, configurationFileName);

            if (File.Exists(filePath))
            {
                using (var file = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var stream = new StreamReader(file))
                    {
                        json = await stream.ReadToEndAsync();
                    }
                }

                var conf = JsonConvert.DeserializeObject<Configuration>(json, jsonSerializerSettings);             
                if (conf != null)
                    Configuration = conf;
            }         
        }
    }
}

