using System;
using System.Collections.Generic;
using System.Linq;
using Thepagedot.Rhome.HomeMatic.Services;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class Switcher : HomeMaticChannel
    {
        public bool State { get; set; }

        [JsonConstructor]
        public Switcher(string name, int type, int iseId, string address, bool isVisible, bool state) : base(name, type, iseId, address, isVisible)
        {
            this.State = state;
        }

        public Switcher(string name, int type, int iseId, string address, bool isVisible) : base(name, type, iseId, address, isVisible)
        {
            this.State = false;
        }        

        public override void SetState(IEnumerable<Datapoint> datapoints)
        {
			base.SetState(datapoints);

			var statePoint = datapoints.FirstOrDefault(d => d.Type == DatapointType.STATE);
			if (statePoint != null)
				State = Convert.ToBoolean(statePoint.Value);            
        }

        public async Task OnAsync(HomeMaticXmlApi homeMaticXmlApi)
        {
            await homeMaticXmlApi.SendChannelUpdateAsync(IseId, true);
        }

        public async Task OffAsync(HomeMaticXmlApi homeMaticXmlApi)
        {
            await homeMaticXmlApi.SendChannelUpdateAsync(IseId, false);
        }
    }
}
