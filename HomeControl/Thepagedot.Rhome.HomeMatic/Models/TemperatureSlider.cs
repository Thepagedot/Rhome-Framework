using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Thepagedot.Rhome.HomeMatic.Services;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class TemperatureSlider : HomeMaticChannel
    {
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }

        [JsonConstructor]
        public TemperatureSlider(string name, int type, int iseId, string address, bool isVisible) : base(name, type, iseId, address, isVisible)
        {
            MinValue = 6.0;
            MaxValue = 30.0;
        }

        public override void SetState(IEnumerable<Datapoint> datapoints)
        {
			base.SetState(datapoints);

            var statePoint = datapoints.FirstOrDefault(d => d.Type == DatapointType.SETPOINT || d.Type == DatapointType.SET_TEMPERATURE);
            if (statePoint != null)
            {
                Value = double.Parse(statePoint.Value, CultureInfo.InvariantCulture);
                Unit = statePoint.ValueUnit;
            }
        }

        public async Task ChangeTemperatureAsync(int temperature, HomeMaticXmlApi homeMaticXmlApi)
        {
            await homeMaticXmlApi.SendChannelUpdateAsync(IseId, temperature);
        }

        //public override void ChangeState(object state)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
