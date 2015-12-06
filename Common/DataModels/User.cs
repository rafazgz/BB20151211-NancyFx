using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Common.DataModels
{
    public class User
    {
        [JsonProperty("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } 
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}