using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace Common.DataModels
{
    public sealed class Room
    {
        [JsonProperty("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("users")]
        public ICollection<User> Users { get; set; }

        public Room()
        {
            Users = new List<User>();
        }
    }
}