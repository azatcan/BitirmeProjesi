using Bitirme.Domain.Entities;

namespace Bitirme.UI.Areas.Admin.Models
{
    public class GroupModel
    {
        public string? GroupName { get; set; }
        public Guid CreatorUserID { get; set; }
        public DateTime CreationDate { get; set; }
        public string GroupDescription { get; set; }

    }
}
