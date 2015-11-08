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
        public static ConfigurationSettings Configuration { get; set; }

        private static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private static string configurationFileName = "configuration.json";


        public static async Task SaveSettings()
        {
            var json = JsonConvert.SerializeObject(Configuration);
            var filePath = Path.Combine(folderPath, configurationFileName);
            using (var file = File.Open(filePath, FileMode.Create, FileAccess.Write))
            {
                using (var stream = new StreamWriter(file))
                {
                    await stream.WriteAsync(json);
                }
            }            
        }

        public static async Task LoadSettings()
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

                var conf = JsonConvert.DeserializeObject<ConfigurationSettings>(json);
                if (conf != null)
                    Configuration = conf;
            }         
        }
    }
}

