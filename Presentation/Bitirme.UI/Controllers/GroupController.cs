using Bitirme.Domain.Data;
using Bitirme.Domain.Entities;
using Bitirme.Infrastructure.Abstract;
using Bitirme.Infrastructure.Concrete;
using Bitirme.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

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
        public async Task<IActionResult> Index()
        {
            var groups = await dataContext.Groups.Include(g => g.CreatorUser).ToListAsync();
            return View(groups);
        }

        [HttpGet]
        public async Task<IActionResult> GroupMemberCheck(Guid groupId)
        {
            var currentUser = await userManager.GetUserAsync(User);
            var groupMembers = dataContext.GroupMembers
                .Where(x => x.GroupID == groupId)
                .Select(x => new { x.UserID})
                .ToList();
            foreach (var item in groupMembers)
            {
                if (currentUser.Id == item.UserID)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            return View();
            
        }

        [HttpGet]
        public async Task<IActionResult> GetUserGroups()
        {
            var currentUser = await userManager.GetUserAsync(User);
            var userId = currentUser.Id;
            var userGroups = dataContext.GroupMembers
                .Where(gm => gm.UserID == userId)
                .Select(gm => new { id = gm.GroupID, name = gm.Group.GroupName }) 
                .ToList();

            return Ok(userGroups);
        }

        [HttpGet]
        public IActionResult GetGroupMessages(Guid groupId)
        {
            try
            {
                var groupMessages = dataContext.GroupMessages
                    .Where(msg => msg.GroupID == groupId)
                    .Select(msg => new { msg.Id, msg.SenderUserID, msg.MessageContent, msg.SendDate }) 
                    .ToList();

                return Ok(groupMessages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
