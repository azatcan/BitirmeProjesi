using Bitirme.Domain.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bitirme.UI.Controllers
{
    public class UsersController : Controller
    {
        private readonly DataContext dataContext;

        public UsersController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public ActionResult GetUserList()
        {
            var users = dataContext.Users.Select(u => new {
                id = u.Id,
                name = u.Name,
                
            }).ToList();

            return Json(users);
        }
    }
}
