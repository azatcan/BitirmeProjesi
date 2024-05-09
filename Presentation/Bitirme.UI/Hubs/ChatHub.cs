using Bitirme.Domain.Data;
using Bitirme.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace Bitirme.UI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly DataContext _context;
        private readonly UserManager<Users> userManager;

        public ChatHub(DataContext context, UserManager<Users> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        public async Task JoinGroup(Guid groupId)
        {
            var userId = Guid.Parse(Context.UserIdentifier);

            if (userId != null)
            {
                var group = await _context.Groups.FindAsync(groupId);
                if (group != null)
                {
                    var connection = new Connections()
                    {
                        UserID = userId,
                        GroupID = groupId,
                        ConnectionType = "Group"
                    };

                    var members = new GroupMembers()
                    {
                        UserID = userId,
                        GroupID = groupId,
                    };
                    _context.GroupMembers.Add(members);
                    await _context.SaveChangesAsync();
                    _context.Connections.Add(connection);
                    await _context.SaveChangesAsync();

                    await Groups.AddToGroupAsync(Context.ConnectionId, groupId.ToString());
                }
            }
        }

        public async Task SendMessageToGroup(Guid groupId, string messageContent)
        {
            var userId = Guid.Parse(Context.UserIdentifier);
            if (userId != null)
            {
                var group = await _context.Groups.FindAsync(groupId);
                if (group != null)
                {
                    var message = new GroupMessages
                    {
                        SenderUserID = userId,
                        GroupID = groupId,
                        MessageContent = messageContent,
                        SendDate = DateTime.Now
                    };
                    _context.GroupMessages.Add(message);
                    await _context.SaveChangesAsync();

 
                    var groupMembers = _context.GroupMembers.Where(gm => gm.GroupID == groupId).Select(gm => gm.UserID).ToList();
                    var groupMemberIds = groupMembers.Where(id => id != userId).Select(id => id.ToString()).ToList();
                    await Clients.Users(groupMemberIds).SendAsync("ReceiveGroupMessage", messageContent);

                }
            }
        }

        public async Task SendPersonalMessage(Guid receiverUserId, string messageContent)
        {
            var senderUserId = Guid.Parse(Context.UserIdentifier);
            if (senderUserId != null)
            {
                var message = new Messages
                {
                    SenderUserID = senderUserId,
                    ReceiverUserID = receiverUserId,
                    MessageContent = messageContent,
                    SendDate = DateTime.Now
                };
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                await Clients.User(receiverUserId.ToString()).SendAsync("ReceivePersonalMessage", messageContent);
            }
        }
    }
}
