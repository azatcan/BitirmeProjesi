using Azure.Messaging;
using Bitirme.Domain.Data;
using Bitirme.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bitirme.UI.Controllers
{
    public class MessagesController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<Users> userManager;

        public MessagesController(DataContext dataContext, UserManager<Users> userManager)
        {
            _dataContext = dataContext;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetMessages()
        {
            var mesages = _dataContext.Messages.Select(u => new {
                id = u.Id,
                SenderUserId = u.SenderUserID,
                ReceiverUserID = u.ReceiverUserID,
                MessageContent = u.MessageContent,

            }).ToList();

            return Json(mesages);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesBetweenUsers(Guid userId1,Guid userId2)
        {
            var currentUser = await userManager.GetUserAsync(User);
            userId1 = currentUser.Id;
            try
            {
                var messages = _dataContext.Messages
                    .Include(m => m.SenderUser)
                    .Include(m => m.ReceiverUser)
                    .Where(m => (m.SenderUserID == userId1 && m.ReceiverUserID == userId2) || (m.SenderUserID == userId2 && m.ReceiverUserID == userId1))
                    .OrderBy(m => m.SendDate)
                    .ToList();

                
                var formattedMessages = messages.Select(m => new
                {
                    Id = m.Id,
                    SenderUserID = m.SenderUserID,
                    SenderUserName = m.SenderUser.Name, 
                    ReceiverUserID = m.ReceiverUserID,
                    ReceiverUserName = m.ReceiverUser.Name, 
                    MessageContent = m.MessageContent,
                    SendDate = m.SendDate,
                    IsGroupMessage = m.IsGroupMessage,
                    FileID = m.FileID,
                    
                });

                
                return Json(formattedMessages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

