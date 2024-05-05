using Bitirme.Domain.Entities;
using Bitirme.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bitirme.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Users> userManager;
        private readonly RoleManager<Roles> roleManager;
        private readonly SignInManager<Users> signInManager;

        public AccountController(UserManager<Users> userManager, RoleManager<Roles> roleManager, SignInManager<Users> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName,model.Password,isPersistent:false,lockoutOnFailure:false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = new Users();
                if (model.ImagePath != null)
                {
                    var extension = Path.GetExtension(model.ImagePath.FileName);
                    var newimagename = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/resimler/user", newimagename);
                    using (var stream = new FileStream(location, FileMode.Create))
                    {
                        await model.ImagePath.CopyToAsync(stream);
                    }
                    user.ImagePath = newimagename;
                }              
                user.Name = model.Name;
                user.SurName = model.SurName;
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.Password = model.Password;
                user.RePassword = model.RePassword;
                if (model.Password == model.RePassword)
                {
                    var result = await userManager.CreateAsync(user,model.Password);
                    await userManager.AddToRoleAsync(user, "User");
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                return View(model);
                
            }
            return NoContent(); 
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
