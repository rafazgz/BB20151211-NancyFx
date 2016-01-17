using AutoMapper;
using Common.DataModels;
using Common.ViewModels;

namespace Common.Setup
{
    public class AutoMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<User, UserViewModel>();
                c.CreateMap<Room, RoomViewModel>();
            });
        }
         
    }
}