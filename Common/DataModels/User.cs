using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using Common.ViewModels;

namespace Common.DataModels
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } 
        public string Name { get; set; }
        public UserViewModel ViewModel
        {
            get { return Mapper.Map<UserViewModel>(this); }
        }
    }
}