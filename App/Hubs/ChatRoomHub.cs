using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace App.Hubs
{
    [HubName("chatRoom")]
    public class ChatRoomHub : Hub
    {
        private static readonly Dictionary<string, dynamic> Users = new Dictionary<string, dynamic>();
        public Task Join(string username, string room)
        {
            var user = new {Name = username, Room = room};
            Users.Add(Context.ConnectionId, user);

            Clients.Group(room).hasJoined(username);

            return Groups.Add(Context.ConnectionId, room);
        }

        public Task Disconnect(string room)
        {
            Users.Remove(Context.ConnectionId);
            return Groups.Remove(Context.ConnectionId, room);
        }

        public Task Send(string message)
        {
            var user = Users[Context.ConnectionId];
            return Clients.Group(user.Room).addMessage(user.Name, message);
        }
    }
}