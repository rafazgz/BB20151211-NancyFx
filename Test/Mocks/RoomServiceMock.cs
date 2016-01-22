using System.Collections.Generic;
using System.Linq;
using Common.DataModels;
using Common.Interfaces;
using Nancy.Testing;

namespace Test.Mocks
{
    public class RoomServiceMock : IRoomService
    {
        private readonly IList<Room> _rooms;

        public RoomServiceMock()
        {
            _rooms = new List<Room>();
            var room = new Room { Name = "DotNetters" };
            _rooms.Add(room);
        }

        public IList<Room> GetRooms()
        {
            return _rooms;
        }

        public Room CreateRoom(string name)
        {
            var room = new Room {Name = name};

            if (_rooms.FirstOrDefault(x => x.Name == name) != null)
            {
                _rooms.Add(room);   
            }

            return room;
        }
    }
}