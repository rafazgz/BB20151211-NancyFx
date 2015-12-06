using System.Collections.Generic;
using Common.DataModels;

namespace Common.Interfaces
{
    public interface IRoomService
    {
        IList<Room> GetRooms();

        Room CreateRoom(string name);
    }
}