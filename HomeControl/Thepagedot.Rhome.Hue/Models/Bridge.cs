using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thepagedot.Rhome.Base.Models;

namespace Thepagedot.Rhome.Hue.Models
{
    public class Bridge : CentralUnit
    {
        public string UserName { get; set; }

        protected Bridge(string name, string address, string userName) : base(name, address, CentralUnitBrand.PhilipsHue)
        {
            this.UserName = userName;
        }
    }
}