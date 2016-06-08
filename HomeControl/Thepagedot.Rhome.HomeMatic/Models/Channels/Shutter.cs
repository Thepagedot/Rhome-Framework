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
        private float _Level;
        public float Level
        {
            get { return _Level; }
            set { _Level = value; RaisePropertyChanged(); }
        }

        public int StopIseId { get; set; }

        [JsonConstructor]
        public Shutter(string name, int type, int iseId, string address, bool isVisible, HomeMaticXmlApiAdapter homeMaticXmlApi) : base(name, type, iseId, address, isVisible, homeMaticXmlApi)
        {
        }

        //public Shutter(string name, int type, int iseId, string address, bool isVisible) : base(name, type, iseId, address, isVisible)
        //{
        //}

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

        public async Task UpAsync()
        {
            await _HomeMaticXmlApi.SendChannelUpdateAsync(IseId, 1);
        }

        public async Task DownAsync()
        {
            await _HomeMaticXmlApi.SendChannelUpdateAsync(IseId, 0);
        }

        public async Task StopAsync()
        {
            await _HomeMaticXmlApi.SendChannelUpdateAsync(StopIseId, 0);
        }
    }
}
