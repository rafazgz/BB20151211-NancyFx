using System.Collections.Generic;
using Common.DataModels;

namespace Common.Interfaces
{
    public interface IRoomService
    {
        IList<Room> GetRooms();

        IList<User> GetUsers(string name);

        Room CreateRoom(string name);

        User JoinRequest(string username, string roomname);
    }
}