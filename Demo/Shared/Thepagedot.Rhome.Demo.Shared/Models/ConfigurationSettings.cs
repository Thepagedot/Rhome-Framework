using System.Collections.Generic;
using Thepagedot.Rhome.Base.Models;
using Thepagedot.Rhome.HomeMatic.Models;
using Newtonsoft.Json;

namespace Thepagedot.Rhome.Demo.Shared.Models
{
    public class Configuration
    {
        public List<CentralUnit> CentralUnits { get; set; }
        public List<Room> Rooms { get; set; }

        public Configuration()
        {
            CentralUnits = new List<CentralUnit>();
            Rooms = new List<Room>();
        }

        [JsonConstructor]
        public Configuration(List<CentralUnit> centralUnits, List<Room> rooms)
        {
            this.CentralUnits = centralUnits;
            this.Rooms = rooms;
        }
    }
}
