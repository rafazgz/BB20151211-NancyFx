using System.Collections.Generic;
using System.Data.Entity;
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
            return _context.Rooms.Include(x => x.Users).ToList();
        }

        public Room CreateRoom(string name)
        {
            var room = GetRoomByName(name) ?? new Room {Name = name};

            _context.Rooms.AddOrUpdate(room);
            _context.SaveChanges();

            return room;
        }

        public User JoinRequest(string username, string roomname)
        {
            var room = GetRoomByName(roomname);

            var user = GetUserByName(username);

            if (room == null || user == null)
            {
                return null;
            }

            room.Users.Add(user);

            _context.Entry(room);
            _context.SaveChanges();

            return user;
        }

        public bool DisconnectUser(string username, string roomname)
        {
            var room = GetRoomByName(roomname);

            var user = GetUserByName(username);

            if (room == null || user == null)
            {
                return false;
            }

            room.Users.Remove(user);

            _context.Entry(room);
            _context.SaveChanges();

            return true;
        }

        public IList<User> GetUsers(string name)
        {
            var room = GetRoomByName(name);

            if (room == null)
            {
                return new List<User>();
            }

            return room.Users.ToList();
        }

        private Room GetRoomByName(string name)
        {
            return _context.Rooms.Include(x => x.Users).FirstOrDefault(r => r.RawName == name.ToLower());
        }

        private User GetUserByName(string name)
        {
            return _context.Users.FirstOrDefault(user => user.Name == name);
        }
    }
}