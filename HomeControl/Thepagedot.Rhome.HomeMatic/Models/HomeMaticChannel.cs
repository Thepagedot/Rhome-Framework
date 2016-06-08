using System.Collections.Generic;
using Thepagedot.Rhome.Base.Models;
using System.Linq;
using System;
using Thepagedot.Rhome.HomeMatic.Services;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public abstract class HomeMaticChannel : Channel<IEnumerable<Datapoint>>
    {
        public int Type { get; set; }
		public int IseId { get; set; }
		public string Address { get; set; }
		public bool IsLowBattery { get; set; }
        public bool IsVisible { get; set; }

        protected HomeMaticXmlApiAdapter _HomeMaticXmlApi;

        protected HomeMaticChannel(string name, int type, int iseId, string address, bool isVisible, HomeMaticXmlApiAdapter homeMaticXmlApi) : base(name)
        {
            this.Type = type;
            this.IseId = iseId;
            this.Address = address;
            this.IsVisible = isVisible;

            this._HomeMaticXmlApi = homeMaticXmlApi;
        }

		public override void SetState(IEnumerable<Datapoint> datapoints)
		{
            // Set invisible when no datapoints available
            IsVisible = datapoints.Any();

            // Set IsLowbattery
            var lowBatPoint = datapoints.FirstOrDefault(d => d.Type == DatapointType.LOWBAT);
			if (lowBatPoint != null)
				IsLowBattery = Convert.ToBoolean(lowBatPoint.Value);
		}
    }
}