using Microsoft.AspNetCore.SignalR;

namespace pimi_connect_api.Hubs
{
    public class UserHub:Hub
    {
        // example implementation
        public static int TotalUsers { get; set; } = 0;
        public UserHub() 
        { 
        
        }
        public override Task OnConnectedAsync()
        {
            TotalUsers++;
            Clients.All.SendAsync("showMessageUserConnected", TotalUsers).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUsers--;
            Clients.All.SendAsync("showMessageUserDisconnected", TotalUsers).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }

    }
}
