using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class Contact : HomeMaticChannel
    {
		public bool State { get; set; }        

        [JsonConstructor]
        public Contact(string name, int type, int iseId, string address, bool state) : base(name, type, iseId, address)
        {
            this.State = state;
        }

        public Contact(string name, int type, int iseId, string address) : base(name, type, iseId, address)
        {
            this.State = false;
        }

        public override void SetState(IEnumerable<Datapoint> values)
        {
            State = Convert.ToBoolean(values.First().Value);
        }           
    }
}
