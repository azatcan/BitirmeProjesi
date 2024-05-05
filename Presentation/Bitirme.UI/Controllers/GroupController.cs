using Bitirme.Domain.Data;
using Bitirme.Domain.Entities;
using Bitirme.Infrastructure.Abstract;
using Bitirme.Infrastructure.Concrete;
using Bitirme.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bitirme.UI.Controllers
{
    public class GroupController : Controller
    {
        private readonly DataContext dataContext;
        private readonly GroupsService groupsService;
        private readonly UserManager<Users> userManager;

        public GroupController(DataContext dataContext, GroupsService groupsService, UserManager<Users> userManager)
        {
            this.dataContext = dataContext;
            this.groupsService = groupsService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = groupsService.List();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> JoinGroup(GroupMembersModel model)
        { 
            return View();  
        }
    }
}
