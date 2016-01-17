using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using AutoMapper;
using Common.ViewModels;
using Newtonsoft.Json;

namespace Common.DataModels
{
    public sealed class Room
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        private string _name;
        [StringLength(128)]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RawName = value?.ToLower();
            }
        }
        
        [StringLength(128), Index(IsUnique = true)]
        public string RawName { get; private set; }

        public ICollection<User> Users { get; set; }

        public Room()
        {
            Users = new List<User>();
        }

        public RoomViewModel ViewModel => Mapper.Map<RoomViewModel>(this);
    }
}