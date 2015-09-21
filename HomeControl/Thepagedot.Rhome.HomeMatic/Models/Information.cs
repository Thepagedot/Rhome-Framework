using System.Collections.Generic;
using System.Linq;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class Information : HomeMaticChannel
    {
        public List<Datapoint> Values { get; set; }

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
