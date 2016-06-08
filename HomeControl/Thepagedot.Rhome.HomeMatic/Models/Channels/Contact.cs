using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Thepagedot.Rhome.HomeMatic.Services;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class Contact : HomeMaticChannel
    {
        private bool _State;
        public bool State
        {
            get { return _State; }
            set { _State = value; RaisePropertyChanged(); }
        }

        [JsonConstructor]
        public Contact(string name, int type, int iseId, string address, bool isVisible, HomeMaticXmlApiAdapter homeMaticXmlApi) : base(name, type, iseId, address, isVisible, homeMaticXmlApi)
        {
        }

        //public Contact(string name, int type, int iseId, string address, bool isVisible, HomeMaticXmlApi homeMaticXmlApi) : base(name, type, iseId, address, isVisible, homeMaticXmlApi)
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
    }
}
