namespace Bitirme.Domain.Entities
{
    public class Groups
    {
        public Guid Id { get; set; }
        public string? GroupName { get; set; }
        public Guid CreatorUserID { get; set; }
        public DateTime CreationDate { get; set; }
        public string GroupDescription { get; set; }

        public Users? CreatorUser { get; set; }
        public ICollection<GroupMembers>? GroupMembers { get; set; }
        public ICollection<GroupMessages>? GroupMessages { get; set; }
        public ICollection<Connections>? Connections { get; set; }
    }
}
