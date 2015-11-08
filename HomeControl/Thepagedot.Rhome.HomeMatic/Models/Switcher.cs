﻿using System;
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
        public Switcher(string name, int type, int iseId, string address, bool state) : base(name, type, iseId, address)
        {
            this.State = state;
        }

        public Switcher(string name, int type, int iseId, string address) : base(name, type, iseId, address)
        {
            this.State = false;
        }        

        public override void SetState(IEnumerable<Datapoint> datapoints)
        {
            var value = datapoints.First().Value;            
            State = Convert.ToBoolean(value);            
        }

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
