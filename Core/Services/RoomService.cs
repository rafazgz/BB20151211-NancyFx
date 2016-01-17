using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Common.DataModels;
using Common.Interfaces;
using Data.Context;

namespace Core.Services
{
    public class RoomService : IRoomService
    {
        private readonly DataContext _context;

        public RoomService()
        {
            _context = new DataContext();
        }

        public IList<Room> GetRooms()
        {
            return _context.Rooms.ToList();
        }

        public Room CreateRoom(string name)
        {
            var room = GetRoomByName(name) ?? new Room {Name = name.ToLower()};

            _context.Rooms.AddOrUpdate(room);
            _context.SaveChanges();

            return room;
        }

        private Room GetRoomByName(string name)
        {
            return _context.Rooms.Find(new {name = name.ToLower()});
        }
    }
}