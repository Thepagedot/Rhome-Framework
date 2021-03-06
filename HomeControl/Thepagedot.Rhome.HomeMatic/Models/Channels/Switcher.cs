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
        private bool _State;
        public bool State
        {
            get { return _State; }
            set { _State = value; RaisePropertyChanged(); }
        }

        [JsonConstructor]
        public Switcher(string name, int type, int iseId, string address, bool isVisible, HomeMaticXmlApi homeMaticXmlApi) : base(name, type, iseId, address, isVisible, homeMaticXmlApi)
        {
        }

        //public Switcher(string name, int type, int iseId, string address, bool isVisible) : base(name, type, iseId, address, isVisible)
        //{
        //    this.State = false;
        //}

        public override void SetState(IEnumerable<Datapoint> datapoints)
        {
			base.SetState(datapoints);

			var statePoint = datapoints.FirstOrDefault(d => d.Type == DatapointType.STATE);
			if (statePoint != null)
				State = Convert.ToBoolean(statePoint.Value);
        }

        public async Task OnAsync()
        {
            await _HomeMaticXmlApi.SendChannelUpdateAsync(IseId, true);
        }

        public async Task OffAsync()
        {
            await _HomeMaticXmlApi.SendChannelUpdateAsync(IseId, false);
        }

        public async Task SetStateAsync(bool state)
        {
            await _HomeMaticXmlApi.SendChannelUpdateAsync(IseId, state);
        }
    }
}
