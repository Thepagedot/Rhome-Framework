using System.Collections.Generic;
using System.Threading.Tasks;
using Thepagedot.Rhome.Base.Models;

namespace Thepagedot.Rhome.Base.Interfaces
{
	public interface IHomeControlApi
	{
        // Rooms
        Task<List<Room>> GetRoomsAsync();
        Task<List<Device>> GetDevicesForRoomAsync(Room room);

        // Devices
        Task<List<Device>> GetDevicesAsync();

        // Rooms and Devices
        Task<List<Room>> GetRoomsWidthDevicesAsync();

        // States
	    Task SendChannelUpdateAsync(int id, object value);
        Task<object> GetChannelStateAsync(Channel channel);

        // Helper methods
        Task<bool> CheckConnectionAsync();
	}
}

