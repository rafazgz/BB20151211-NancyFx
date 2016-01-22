using System.Data.Entity.Migrations;
using Common.DataModels;
using Data.Context;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService()
        {
            _context = new DataContext();
        }

        public User CreateUser(string username)
        {
            var user = new User {Name = username};

            _context.Users.AddOrUpdate(user);
            _context.SaveChanges();

            return user;
        }
    }
}