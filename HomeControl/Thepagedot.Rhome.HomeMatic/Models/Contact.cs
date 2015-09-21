using System;
using System.Collections.Generic;
using System.Linq;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class Contact : HomeMaticChannel
    {
		public bool State { get; set; }

        public Contact(string name, int type, int iseId, string address) : base(name, type, iseId, address)
        {
            this.State = false;
        }

        public Contact(string name, int type, int iseId, string address, bool state) : base(name, type, iseId, address)
        {
            this.State = state;
        }

        public override void SetState(IEnumerable<Datapoint> values)
        {
            State = Convert.ToBoolean(values.First().Value);
        }

        //public override async void ChangeState(object state)
        //{
        //    if ((bool)state != State)
        //    {
        //        await HomeMaticXml.ChangeState(IseId, state);
        //        State = (bool)state;
        //    }
        //}
    }
}
