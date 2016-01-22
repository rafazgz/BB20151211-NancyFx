﻿using System.Linq;
using App.Models;
using Common.Interfaces;
using Nancy;
using Nancy.ModelBinding;

namespace App.Modules
{
    public class RoomModule : NancyModule
    {
        protected IRoomService Service { get; private set; }

        public RoomModule(IRoomService service) : base("/api/rooms")
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
        }
    }
}