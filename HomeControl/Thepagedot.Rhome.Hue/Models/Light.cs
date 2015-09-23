using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thepagedot.Rhome.Base.Models;

namespace Thepagedot.Rhome.Hue.Models
{
    public class Light : Device
    {
        public State State { get; set; }
        public string Type { get; set; }
        public string ModelId { get; set; }
        public string ManufacturerName { get; set; }
        public string UniqueId { get; set; }
        public string SwVersion { get; set; }

        protected Light(string name) : base(name)
        {                        
        }
    }

    public class State
    {
        public bool On { get; set; }
        public int Brightness { get; set; }
        public int Hue { get; set; }
        public int Saturation { get; set; }
        public string Effect { get; set; }
        public float[] Xy { get; set; }
        public string Alert { get; set; }
        public string ColorMode { get; set; }
        public bool Reachable { get; set; }
    }
}