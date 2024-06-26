using Bitirme.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Bitirme.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminUserAprovedController : Controller
    {
        private readonly UserManager<Users> userManager;

        public AdminUserAprovedController(UserManager<Users> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> PendingApprovals()
        {
            var users = userManager.Users.Where(u => !u.IsApproved).ToList();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.IsApproved = true;
                await userManager.UpdateAsync(user);
            }
            return RedirectToAction("PendingApprovals");
        }
    }
}

