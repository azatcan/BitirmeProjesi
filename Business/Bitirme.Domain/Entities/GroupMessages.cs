namespace Bitirme.Domain.Entities
{
    public class GroupMessages
    {
        public Guid Id { get; set; }
        public Guid SenderUserID { get; set; }
        public Guid GroupID { get; set; }
        public string MessageContent { get; set; }
        public DateTime SendDate { get; set; }

        public Users SenderUser { get; set; }
        public Groups Group { get; set; }
    }
}
