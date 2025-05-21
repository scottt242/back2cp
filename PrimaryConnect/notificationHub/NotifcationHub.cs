using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace PrimaryConnect.notificationHub
{
    public class NotificationHub:Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userType = Context.User?.FindFirst("UserType")?.Value;
            if (userType == "Teacher")
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Teachers");
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Teacher_{userId}");
            }
            else if (userType == "Parent")
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Parents");
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Parent_{userId}");
            }

            await base.OnConnectedAsync();
        }
    }
}
