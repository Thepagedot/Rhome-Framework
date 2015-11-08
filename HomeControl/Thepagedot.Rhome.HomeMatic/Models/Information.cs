using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class Information : HomeMaticChannel
    {
        public List<Datapoint> Values { get; set; }

        [JsonConstructor]
        public Information(string name, int type, int iseId, string address, List<Datapoint> values) : base(name, type, iseId, address)
        {
            Values = values;
        }

        public Information(string name, int type, int iseId, string address) : base(name, type, iseId, address)
        {
            Values = new List<Datapoint>();
        }        

        public override void SetState(IEnumerable<Datapoint> values)
        {
            Values = values.ToList();
        }
    }
}
