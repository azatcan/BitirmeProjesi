using Bitirme.Domain.Entities;

namespace Bitirme.UI.Areas.Admin.Models
{
    public class EventModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ImageUrl { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
