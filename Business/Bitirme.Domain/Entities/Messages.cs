namespace Bitirme.Domain.Entities
{
    public class Messages
    {

        public Guid Id { get; set; }
        public Guid SenderUserID { get; set; }
        public Users SenderUser { get; set; }
        public Guid ReceiverUserID { get; set; }
        public Users ReceiverUser { get; set; }
        public string MessageContent { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsGroupMessage { get; set; }

        public Guid? FileID { get; set; }
        public Files? File { get; set; }
        public ICollection<Files> Files { get; set; }

    }
}
