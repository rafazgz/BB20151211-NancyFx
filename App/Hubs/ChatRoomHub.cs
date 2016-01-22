using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Interfaces;
using Core.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace App.Hubs
{
    [HubName("chatRoom")]
    public class ChatRoomHub : Hub
    {
        private static readonly Dictionary<string, User> Users = new Dictionary<string, User>();
        private readonly IRoomService _roomService;

        public ChatRoomHub()
        {
            _roomService = new RoomService();
        }


        public Task Join(string username, string room)
        {
            var user = new User { Name = username, Room = room };
            Users.Add(Context.ConnectionId, user);

            Clients.Group(room).hasJoined(username);

            return Groups.Add(Context.ConnectionId, room);
        }

        public Task Disconnect()
        {
            var user = Users[Context.ConnectionId];

            Clients.Group(user.Room).disconnectUser(user.Name);

            Users.Remove(Context.ConnectionId);

            Clients.Caller.disconnectedSelf();

            return Groups.Remove(Context.ConnectionId, user.Room);
        }

        public Task Send(string message)
        {
            var user = Users[Context.ConnectionId];
            return Clients.Group(user.Room).addMessage(user.Name, message);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            User user;

            if (Users.TryGetValue(Context.ConnectionId, out user))
            {
                Disconnect();
                _roomService.DisconnectUser(user.Name, user.Room);
            }

            return base.OnDisconnected(stopCalled);
        }

        private class User
        {
            public string Name { get; set; }
            public string Room { get; set; }
        }
    }
}