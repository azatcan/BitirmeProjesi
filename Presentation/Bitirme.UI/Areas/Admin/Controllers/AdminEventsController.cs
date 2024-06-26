using Bitirme.Domain.Data;
using Bitirme.Domain.Entities;
using Bitirme.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bitirme.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminEventsController : Controller
    {
        private readonly DataContext dataContext;
        private readonly UserManager<Users> _userManager;

        public AdminEventsController(DataContext dataContext, UserManager<Users> userManager)
        {
            this.dataContext = dataContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var value = await dataContext.Events.Include(g => g.Users).ToListAsync(); ;
            return View(value);
        }

        public IActionResult Create()
        {
            return View(new EventModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventModel model)
        {          
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                Events events = new Events();
                if (model.ImageUrl != null)
                {
                    var extension = Path.GetExtension(model.ImageUrl.FileName);
                    var newimagename = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/resimler/event", newimagename);
                    using (var stream = new FileStream(location, FileMode.Create))
                    {
                        await model.ImageUrl.CopyToAsync(stream);
                    }
                    events.ImageUrl = newimagename;
                }
                events.Title = model.Title;
                events.Description = model.Description;
                events.CreateDate = DateTime.Now;
                events.UserId = currentUser.Id;
                events.Users = currentUser;

                dataContext.Events.Add(events);
                dataContext.SaveChanges();


                return View(model);

            }
            return NoContent();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var events = await dataContext.Events.FindAsync(id);
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/resimler/event", events.ImageUrl);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            dataContext.Events.Remove(events);
            await dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
