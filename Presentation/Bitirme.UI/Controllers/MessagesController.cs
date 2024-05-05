using Microsoft.AspNetCore.Mvc;

namespace Bitirme.UI.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
