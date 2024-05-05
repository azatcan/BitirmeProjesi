using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bitirme.Domain.Entities
{
    public class Users : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string? ImagePath { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }

        [InverseProperty("SenderUser")]
        public ICollection<Messages> SentMessages { get; set; }
        [InverseProperty("ReceiverUser")]
        public ICollection<Messages> ReceivedMessages { get; set; }
        public ICollection<GroupMembers> GroupMemberships { get; set; }
        public ICollection<Connections> Connections { get; set; }
    }
}
