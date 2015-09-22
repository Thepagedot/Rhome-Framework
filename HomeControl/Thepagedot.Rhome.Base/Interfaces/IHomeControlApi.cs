using System.Collections.Generic;
using System.Threading.Tasks;
using Thepagedot.Rhome.Base.Models;

namespace Thepagedot.Rhome.Base.Interfaces
{
	public interface IHomeControlApi
	{
        // Rooms
        Task<IEnumerable<Room>> GetRoomsAsync();
        Task GetDevicesForRoomAsync(Room room);

        // Devices
        Task<IEnumerable<Device>> GetDevicesAsync();

        // Rooms and Devices
        Task<IEnumerable<Room>> GetRoomsWidthDevicesAsync();

        // States
	    Task SendChannelUpdateAsync(int id, object value);
        Task<object> GetChannelStateAsync(Channel channel);

        // Helper methods
        Task<bool> CheckConnectionAsync();
	}
}

