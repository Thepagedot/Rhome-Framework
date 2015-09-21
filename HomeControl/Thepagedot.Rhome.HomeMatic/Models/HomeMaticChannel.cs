using System.Collections.Generic;
using Thepagedot.Rhome.Base.Models;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public abstract class HomeMaticChannel : Channel<IEnumerable<Datapoint>>
    {
        public int Type { get; set; }
		public int IseId { get; set; }
		public string Address { get; set; }

        protected HomeMaticChannel(string name, int type, int iseId, string address) : base(name)
        {
            this.Type = type;
            this.IseId = iseId;
            this.Address = address;
        }

        public abstract override void SetState(IEnumerable<Datapoint> values);
    }
}