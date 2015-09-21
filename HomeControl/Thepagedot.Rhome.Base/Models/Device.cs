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
        public List<ChannelType> ChannelList { get; set; }

        protected Device(string name) : base(name)
        {
            ChannelList = new List<ChannelType>();
        }
    }
}