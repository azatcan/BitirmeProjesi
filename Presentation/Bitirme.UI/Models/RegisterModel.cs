using System.ComponentModel.DataAnnotations;

namespace Bitirme.UI.Models
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string RePassword { get; set; }
        public IFormFile ImagePath { get; set; }
        [Required]
        public IFormFile StudentDocument { get; set; }
    }
}
