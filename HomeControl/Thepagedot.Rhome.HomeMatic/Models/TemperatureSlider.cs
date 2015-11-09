using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class TemperatureSlider : HomeMaticChannel
    {
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }

        [JsonConstructor]
        public TemperatureSlider(string name, int type, int iseId, string address) : base(name, type, iseId, address)
        {
            MinValue = 6.0;
            MaxValue = 30.0;
        }

        public override void SetState(IEnumerable<Datapoint> datapoints)
        {
			base.SetState(datapoints);

			var statePoint = datapoints.FirstOrDefault(d => d.Type == DatapointType.STATE);
        }

        //public override void ChangeState(object state)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
