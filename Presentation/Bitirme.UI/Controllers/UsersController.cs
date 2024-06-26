using Bitirme.Domain.Data;
using Bitirme.Domain.Entities;
using Bitirme.UI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bitirme.UI.Controllers
{
    public class UsersController : Controller
    {
        private readonly DataContext dataContext;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersController(DataContext dataContext, UserManager<Users> userManager, SignInManager<Users> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            this.dataContext = dataContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var model = new UsersModel
            {
                UserName = user.UserName,
                Email = user.Email,
                ImagePath = user.ImagePath,
                Name = user.Name,
                SurName = user.SurName,
                SentMessages = user.SentMessages,
                ReceivedMessages = user.ReceivedMessages,
                GroupMembers = user.GroupMemberships,
                Connections = user.Connections
            };

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var model = new UsersUpdateModel
            {
                UserName = user.UserName,
                Email = user.Email,
                ExistingImagePath = user.ImagePath,
                Name = user.Name,
                SurName = user.SurName
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UsersUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                // Mevcut resim yolunu yeniden ayarla
                model.ExistingImagePath = (await _userManager.GetUserAsync(User)).ImagePath;
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            if (model.ProfileImage != null)
            {
                var extension = Path.GetExtension(model.ProfileImage.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "resimler/user");
                var filePath = Path.Combine(uploadsFolder, newImageName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfileImage.CopyToAsync(fileStream);
                }

                if (!string.IsNullOrEmpty(user.ImagePath))
                {
                    var oldFilePath = Path.Combine(uploadsFolder, user.ImagePath);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                user.ImagePath = newImageName;
            }

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.Name = model.Name;
            user.SurName = model.SurName;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToAction("Profile");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
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
