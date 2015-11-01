using System;
using Thepagedot.Rhome.Base.Interfaces;

namespace Thepagedot.Rhome.Demo.Droid
{
    public class DataHolder
    {
        public static DataHolder Current { get; set; }

        public IHomeControlApi CurrentHomeControl { get; set; }

        public DataHolder()
        {
            Current = this;
        }
    }
}