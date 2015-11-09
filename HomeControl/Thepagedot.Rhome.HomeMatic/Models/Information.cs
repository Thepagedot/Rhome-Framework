using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class Information : HomeMaticChannel
    {
        public List<Datapoint> Values { get; set; }
        public string Content { get; set; }

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
            for (int i = 0; i < values.Count(); i++)
            {
                var datapoint = values.ElementAt(i);
                Values.Add(datapoint);
                Content += String.Format("{0}: {1}{2}", datapoint.Type, datapoint.Value, datapoint.ValueUnit);

                if (i != values.Count() - 1)
                    Content += "\n";    
            }
        }
    }
}