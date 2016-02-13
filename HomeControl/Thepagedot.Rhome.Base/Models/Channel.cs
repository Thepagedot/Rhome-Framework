using GalaSoft.MvvmLight;
using System.ComponentModel;

namespace Thepagedot.Rhome.Base.Models
{
    public abstract class Channel : ViewModelBase
    {
        public string Name { get; set; }

        protected Channel(string name)
        {
            this.Name = name;
        }
    }

    /// <summary>
    /// Acts like Subdevice due to a device can have multiple channels
    /// </summary>
    /// <typeparam name="T">Type of the element(s) that contains the channel values like state, temperature, ...</typeparam>
    public abstract class Channel<T> : Channel
    {
        protected Channel(string name) : base(name)
        {
        }

        public abstract void SetState(T values);
    }
}
