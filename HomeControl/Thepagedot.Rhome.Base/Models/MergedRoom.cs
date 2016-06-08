using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thepagedot.Rhome.Base.Models
{
    public class MergedRoom
    {
        public string Name { get; set; }
        public int Floor { get; set; }
        public List<Room> Rooms { get; set; }

        public MergedRoom()
        {
            Rooms = new List<Room>();
        }

        public MergedRoom(Room room)
        {
            Name = room.Name.Trim();
            Floor = room.Floor;
            Rooms = new List<Room>();
            Rooms.Add(room);
        }

        internal void AddRoom(Room room)
        {
            Rooms.Add(room);
        }
    }
}
