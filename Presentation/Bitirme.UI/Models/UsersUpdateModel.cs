namespace Bitirme.UI.Models
{
    public class UsersUpdateModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string? ExistingImagePath { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}
