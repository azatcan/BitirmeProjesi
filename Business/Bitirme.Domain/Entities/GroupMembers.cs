namespace Bitirme.Domain.Entities
{
    public class GroupMembers
    {
        public Guid Id { get; set; }
        public Guid UserID { get; set; }
        public Guid GroupID { get; set; }

        public Users User { get; set; }
        public Groups Group { get; set; }
    }
}
