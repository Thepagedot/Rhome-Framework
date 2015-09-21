using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class Shutter : HomeMaticChannel
    {
        public float Level { get; set; }
        public int StopIseId { get; set; }

        public Shutter(string name, int type, int iseId, string address) : base(name, type, iseId, address)
        {
        }

        public Shutter(string name, int type, int iseId, string address, float level, int stopIseId) : base(name, type, iseId, address)
        {
            this.Level = level;
            this.StopIseId = stopIseId;
        }

        public override void SetState(IEnumerable<Datapoint> datapoints)
        {
            var level = datapoints.First().Value;
            var stopIseId = datapoints.ElementAt(1).IseId;
            this.Level = float.Parse(level, CultureInfo.InvariantCulture.NumberFormat);
            this.StopIseId = Convert.ToInt32(stopIseId);            
        }
    }
}
