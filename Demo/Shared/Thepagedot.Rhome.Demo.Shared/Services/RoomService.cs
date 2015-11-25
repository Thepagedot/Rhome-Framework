using System;
using System.Linq;
using System.Collections.Generic;
using Thepagedot.Rhome.Base.Models;
using Thepagedot.Rhome.Base.Interfaces;
using System.Threading.Tasks;

namespace Thepagedot.Rhome.Demo.Shared
{
    public static class RoomService
    {
        //public static async Task<IEnumerable<Room>> MergeRooms(IEnumerable<IHomeControlApi> homeControlApis)
        //{
        //    // Fill rooms initially with the ones from the first home control system
        //    var allRooms = await homeControlApis.First().GetRoomsAsync();

        //    // Iterate through the rooms of the other systems and check if the names are equal
        //    for (var i = 1; i < homeControlApis.Count(); i++)
        //    {
        //        var rooms = await homeControlApis.ElementAt(i).GetRoomsAsync();
        //        foreach (var room in rooms)
        //        {
        //            // TODO: Merge rooms
        //        }
        //    }

        //    return allRooms;
        //}
    }
}

