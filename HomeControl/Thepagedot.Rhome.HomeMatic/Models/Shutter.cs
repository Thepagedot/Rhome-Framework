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
        public Shutter(string name, int type, int iseId, string address, float level, int stopIseId) : base(name, type, iseId, address)
        {
            this.Level = level;
            this.StopIseId = stopIseId;
        }

        public Shutter(string name, int type, int iseId, string address) : base(name, type, iseId, address)
        {
        }        

        public override void SetState(IEnumerable<Datapoint> datapoints)
        {
            var level = datapoints.First().Value;
            var stopIseId = datapoints.ElementAt(1).IseId;
            this.Level = float.Parse(level, CultureInfo.InvariantCulture.NumberFormat);
            this.StopIseId = Convert.ToInt32(stopIseId);            
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
