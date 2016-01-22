using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Models;
using Common.DataModels;
using Common.Interfaces;
using Nancy;
using Nancy.ModelBinding;

namespace App.Modules
{
    public class RoomModule : NancyModule
    {
        protected IRoomService Service { get; private set; }

        public RoomModule(IRoomService service)
            : base("/api/rooms")
        {
            Service = service;

            Get["/list"] = p =>
            {
                var rooms = Service.GetRooms();

                return Negotiate.WithModel(rooms.Select(e => e.ViewModel));
            };

            Post["/new"] = p =>
            {
                var model = this.Bind<NewRoomModel>();

                var room = Service.CreateRoom(model.Name);

                return Negotiate.WithModel(room.ViewModel);
            };

            Get["/{room}/listUsers"] = p =>
            {
                string room = p.room;

                var users = Service.GetUsers(room);

                return Negotiate.WithModel(users.Select(user => user.ViewModel.Name));
            };

            Post["/{room}/join"] = p =>
            {
                string room = p.room;
                var model = this.Bind<JoinRequest>();

                var user = Service.JoinRequest(model.User, room);

                return Negotiate.WithModel(user.ViewModel);
            };
        }
    }
}