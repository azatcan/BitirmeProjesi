using Bitirme.Domain.Entities;

namespace Bitirme.UI.Models
{
    public class GroupMembersModel
    {
        public Guid Id { get; set; }
        public Guid UserID { get; set; }
        public Guid GroupID { get; set; }

        public Users User { get; set; }
        public Groups Group { get; set; }
    }
}
