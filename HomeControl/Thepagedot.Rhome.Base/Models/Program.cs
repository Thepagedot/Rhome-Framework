using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thepagedot.Rhome.Base.Models
{
    public abstract class Program
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public abstract Task RunAsync();
    }
}
