using System;
using System.Collections.Generic;
using System.Linq;
using Thepagedot.Rhome.HomeMatic.Services;
using System.Threading.Tasks;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class Switcher : HomeMaticChannel
    {
        public bool State { get; set; }        

        public Switcher(string name, int type, int iseId, string address) : base(name, type, iseId, address)
        {
            this.State = false;
        }

        public Switcher(string name, int type, int iseId, string address, bool state) : base(name, type, iseId, address)
        {
            this.State = state;
        }

        public override void SetState(IEnumerable<Datapoint> datapoints)
        {
            var value = datapoints.First().Value;            
            State = Convert.ToBoolean(value);            
        }

        //public override async void ChangeState(object state)
        //{
        //    if ((bool)state != State)
        //    {
        //        await HomeMaticXml.ChangeState(IseId, state);
        //        State = (bool)state;
        //    }
        //}

        public async Task On(HomeMaticXmlApi homeMaticXmlApi)
        {
            await homeMaticXmlApi.SendChannelUpdateAsync(IseId, true);
        }

        public async Task Off(HomeMaticXmlApi homeMaticXmlApi)
        {
            await homeMaticXmlApi.SendChannelUpdateAsync(IseId, false);
        }
    }
}
