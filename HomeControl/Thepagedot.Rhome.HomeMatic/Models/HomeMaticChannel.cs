using System.Collections.Generic;
using Thepagedot.Rhome.Base.Models;
using System.Linq;
using System;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public abstract class HomeMaticChannel : Channel<IEnumerable<Datapoint>>
    {
        public int Type { get; set; }
		public int IseId { get; set; }
		public string Address { get; set; }
		public bool IsLowBattery { get; set; }

        protected HomeMaticChannel(string name, int type, int iseId, string address) : base(name)
        {
            this.Type = type;
            this.IseId = iseId;
            this.Address = address;
        }

		public override void SetState(IEnumerable<Datapoint> datapoints)
		{
			// Set IsLowbattery
			var lowBatPoint = datapoints.FirstOrDefault(d => d.Type == DatapointType.LOWBAT);
			if (lowBatPoint != null)
				IsLowBattery = Convert.ToBoolean(lowBatPoint.Value);
		}
    }
}