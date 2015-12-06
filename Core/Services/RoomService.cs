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
            var room = new Room {Name = name};
            _context.Rooms.AddOrUpdate(room);
            _context.SaveChanges();

            return room;
        }
    }
}