using System.Collections.Generic;

namespace Common.ViewModels
{
    public class RoomViewModel
    {
        public long Id { get; set; } 
        public string Name { get; set; }
        public ICollection<UserViewModel> Users { get; set; }
    }
}