using System.Collections.Generic;

namespace Thepagedot.Rhome.Base.Models
{
	public abstract class Device
	{
		public string Name { get; set; }

        protected Device(string name)
		{
			Name = name;
		}
	}

    public abstract class Device<ChannelType> : Device
    {
        public List<ChannelType> Channels { get; set; }

        protected Device(string name) : base(name)
        {
            Channels = new List<ChannelType>();
        }
    }
}