using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thepagedot.Rhome.Base.Models;

namespace Thepagedot.Rhome.Base.Services
{
    public class RoomService
    {
        public List<MergedRoom> MergeRooms(IEnumerable<Room> rooms)
        {
            var mergedRooms = new List<MergedRoom>();

            foreach (var room in rooms)
            {
                var mergedRoom = mergedRooms.FirstOrDefault(r => r.Name.Equals(room.Name) && r.Floor == room.Floor);
                if (mergedRoom == null)
                {
                    // No existent merged room found. First room of this kind. Create a merged room
                    mergedRoom = new MergedRoom(room);
                    mergedRooms.Add(mergedRoom);
                }
                else
                {
                    // Merged room of this name and floor is already existent. Add this one to it
                    mergedRoom.AddRoom(room);
                }
            }

            return mergedRooms;
        }
    }
}
