﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Thepagedot.Rhome.HomeMatic.Services;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class Information : HomeMaticChannel
    {
        public List<Datapoint> Values { get; set; }

        private string _Content;
        public string Content
        {
            get { return _Content; }
            set { _Content = value; RaisePropertyChanged(); }
        }

        [JsonConstructor]
        public Information(string name, int type, int iseId, string address, bool isVisible, HomeMaticXmlApi homeMaticXmlApi) : base(name, type, iseId, address, isVisible, homeMaticXmlApi)
        {
        }

        //public Information(string name, int type, int iseId, string address, bool isVisible, HomeMaticXmlApi homeMaticXmlApi) : base(name, type, iseId, address, isVisible, homeMaticXmlApi)
        //{
        //    Values = new List<Datapoint>();
        //}

        public override void SetState(IEnumerable<Datapoint> datapoints)
        {
			base.SetState(datapoints);

			//TODO: This must be called by the UI to give the chance to provide custom strings for name
			// So the content property needs to be deleted and the ui has to build the string out of the list of datapoints and their ToFormattedString(string) methods

            Content = String.Empty;
            for (int i = 0; i < datapoints.Count(); i++)
            {
                var datapoint = datapoints.ElementAt(i);
                Values.Add(datapoint);
				Content += datapoint.ToFormattedString();

                if (i != datapoints.Count() - 1)
                    Content += "\n";
            }
        }
    }
}