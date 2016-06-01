using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thepagedot.Rhome.Base.Models
{
    public abstract class SystemVariable
    {
        public string Name { get; set; }
        public dynamic Value { get; set; }
        public string ValueString { get; set; }
    }
}
