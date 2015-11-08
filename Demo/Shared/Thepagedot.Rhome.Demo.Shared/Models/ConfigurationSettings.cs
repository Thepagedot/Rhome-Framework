using System.Collections.Generic;
using Thepagedot.Rhome.Base.Models;
using Thepagedot.Rhome.HomeMatic.Models;
using Newtonsoft.Json;

namespace Thepagedot.Rhome.Demo.Shared.Models
{
    public class ConfigurationSettings
    {
        public List<CentralUnit> CentralUnits { get; set; }

        public ConfigurationSettings()
        {
            
        }

        [JsonConstructor]
        public ConfigurationSettings(List<CentralUnit> centralUnits)
        {
            this.CentralUnits = centralUnits;
        }
       
    }
}
