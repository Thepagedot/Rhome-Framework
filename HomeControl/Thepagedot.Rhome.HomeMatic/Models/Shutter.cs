using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thepagedot.Rhome.HomeMatic.Services;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class Shutter : HomeMaticChannel
    {
        public float Level { get; set; }
        public int StopIseId { get; set; }

        [JsonConstructor]
        public Shutter(string name, int type, int iseId, string address, float level, int stopIseId, bool isVisible) : base(name, type, iseId, address, isVisible)
        {
            this.Level = level;
            this.StopIseId = stopIseId;
        }

        public Shutter(string name, int type, int iseId, string address, bool isVisible) : base(name, type, iseId, address, isVisible)
        {
        }        

        public override void SetState(IEnumerable<Datapoint> datapoints)
        {
			base.SetState(datapoints);

			var levelPoint = datapoints.FirstOrDefault(d => d.Type == DatapointType.LEVEL);
			if (levelPoint != null)
				Level = float.Parse(levelPoint.Value, CultureInfo.InvariantCulture.NumberFormat);

			var stopPoint = datapoints.FirstOrDefault(d => d.Type == DatapointType.STOP);
			if (stopPoint != null)
			{
				StopIseId = stopPoint.IseId;
			}            
        }

        public async Task Up(HomeMaticXmlApi homeMaticXmlApi)
        {
            await homeMaticXmlApi.SendChannelUpdateAsync(IseId, 1);
        }

        public async Task Down(HomeMaticXmlApi homeMaticXmlApi)
        {
            await homeMaticXmlApi.SendChannelUpdateAsync(IseId, 0);
        }

        public async Task Stop(HomeMaticXmlApi homeMaticXmlApi)
        {
            await homeMaticXmlApi.SendChannelUpdateAsync(StopIseId, 0);
        }
    }
}
