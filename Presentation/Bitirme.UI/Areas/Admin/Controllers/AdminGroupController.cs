using Bitirme.Domain.Data;
using Bitirme.Domain.Entities;
using Bitirme.Infrastructure.Abstract;
using Bitirme.UI.Areas.Admin.Models;
using Bitirme.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bitirme.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminGroupController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<Users> _userManager;
        private readonly GroupsService _groupsService;

        public AdminGroupController(DataContext dataContext, UserManager<Users> userManager, GroupsService groupsService)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _groupsService = groupsService;
        }

        public IActionResult ListGroup()
        {
            var result = _groupsService.List();
            return View(result);
        }

        [HttpGet]
        public IActionResult AddGroup()
        {
            return View(new GroupModel());
        }
        [HttpPost]
        public async Task<IActionResult> AddGroup(GroupModel model)
        {
            if (ModelState.IsValid)
            {
                
                var currentUser = await _userManager.GetUserAsync(User);
                var groups = new Groups()
                {
                    GroupName = model.GroupName,
                    CreatorUserID = model.CreatorUserID = currentUser.Id,
                    CreationDate = model.CreationDate = DateTime.Now,
                    GroupDescription = model.GroupDescription,
                    CreatorUser = currentUser
                };
                _dataContext.Groups.Add(groups);
                _dataContext.SaveChanges();

                return RedirectToAction("ListGroup", "AdminGroup");
            }
            return View(model);
        } 

        public IActionResult Delete(Guid id)
        {
            var result = _groupsService.GetById(id);
            _groupsService.Delete(result);
            return RedirectToAction("ListGroup", "AdminGroup");
        }

    }
}

