using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thepagedot.Rhome.Base.Interfaces;
using Thepagedot.Rhome.Base.Models;

namespace Thepagedot.Rhome.Hue.Services
{
    public class HueJsonApi : IHomeControlApi
    {
        public Task<bool> CheckConnectionAsync()
        {
            throw new NotImplementedException();
        }

        public Task<object> GetChannelStateAsync(Channel channel)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Device>> GetDevicesAsync()
        {
            throw new NotImplementedException();
        }

        public Task GetDevicesForRoomAsync(Room room)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Room>> GetRoomsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Room>> GetRoomsWidthDevicesAsync()
        {
            throw new NotImplementedException();
        }

        public Task SendChannelUpdateAsync(int id, object value)
        {
            throw new NotImplementedException();
        }
    }
}
