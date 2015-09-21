using System.Collections.Generic;

namespace Thepagedot.Rhome.Base.Models
{
	public abstract class Device
	{
		public string Name { get; set; }
	    public List<Channel> ChannelList { get; set; }

	    protected Device(string name)
		{
			Name = name;
		}
	}
}