using Common.DataModels;

namespace Core.Services
{
    public interface IUserService
    {
        User CreateUser(string username);
    }
}