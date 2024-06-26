using Bitirme.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bitirme.UI.Models
{
    public class UsersModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }


        public string ImagePath { get; set; }
        public ICollection<Messages> SentMessages { get; set; }
        public ICollection<Messages> ReceivedMessages { get; set; }
        public ICollection<GroupMembers> GroupMembers { get; set; }
        public ICollection<Connections> Connections { get; set; }
    }
}
