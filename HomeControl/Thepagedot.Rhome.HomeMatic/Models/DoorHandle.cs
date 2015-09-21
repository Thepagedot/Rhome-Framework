using System;
using System.Collections.Generic;
using System.Linq;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class DoorHandle : HomeMaticChannel
    {
		public DoorHandleState State { get; set; }
        public DoorHandle(string name, int type, int iseId, string address) : base(name, type, iseId, address)
        {
            this.State = DoorHandleState.Closed;
        }

        public DoorHandle(string name, int type, int iseId, string address, DoorHandleState state) : base(name, type, iseId, address)
        {
            this.State = state;
        }

        public override void SetState(IEnumerable<Datapoint> datapoints)
        {
            var value = datapoints.First().Value;

            switch (Convert.ToInt32(value))
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

        //public override async void ChangeState(object state)
        //{
        //    if ((DoorHandleState)state != State)
        //    {
        //        await HomeMaticXml.ChangeState(IseId, state);
        //        State = (DoorHandleState)state;
        //    }
        //}
    }

    public enum DoorHandleState
    {
        Closed, // 0
        Tilted, // 1
        Open    // 2
    }
}
