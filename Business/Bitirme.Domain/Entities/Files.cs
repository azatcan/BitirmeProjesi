namespace Bitirme.Domain.Entities
{
    public class Files
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }

        public Guid MessageID { get; set; } 
        public Messages Message { get; set; }
    }
}
