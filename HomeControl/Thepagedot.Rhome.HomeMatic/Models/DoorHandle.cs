using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class DoorHandle : HomeMaticChannel
    {
		public DoorHandleState State { get; set; }        

        [JsonConstructor]
        public DoorHandle(string name, int type, int iseId, string address, bool isVisible, DoorHandleState state) : base(name, type, iseId, address, isVisible)
        {
            this.State = state;
        }

        public DoorHandle(string name, int type, int iseId, string address, bool isVisible) : base(name, type, iseId, address, isVisible)
        {
            this.State = DoorHandleState.Closed;
        }

        public override void SetState(IEnumerable<Datapoint> datapoints)
        {
			base.SetState(datapoints);

			var statePoint = datapoints.FirstOrDefault(d => d.Type == DatapointType.STATE);
			if (statePoint != null)
			{
				switch (Convert.ToInt32(statePoint.Value))
				{
					case 0:
						State = DoorHandleState.Closed;
						break;
					case 1:
						State = DoorHandleState.Tilted;
						break;
					case 2:
						State = DoorHandleState.Open;
						break;
				}
			}
        }
    }

    public enum DoorHandleState
    {
        Closed, // 0
        Tilted, // 1
        Open    // 2
    }
}
