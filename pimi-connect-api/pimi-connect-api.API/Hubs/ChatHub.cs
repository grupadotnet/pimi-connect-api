using Microsoft.AspNetCore.SignalR;
using pimi_connect_app.Data.AppDbContext;
using pimi_connect_app.Data.Models.ConnectionModels;

namespace pimi_connect_api.Hubs
{
    public class ChatHub: Hub
    {
        public static int TotalUsers { get; set; } = 0;
        
        private readonly AppDbContext _dbContext;
        
        public ChatHub(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        // public override Task OnConnectedAsync()
        // {
        //     TotalUsers++;
        //     Clients.All.SendAsync("showMessageUserConnected", TotalUsers).GetAwaiter().GetResult();
        //     return base.OnConnectedAsync();
        // }
        //
        // public override Task OnDisconnectedAsync(Exception? exception)
        // {
        //     TotalUsers--;
        //     Clients.All.SendAsync("showMessageUserDisconnected", TotalUsers).GetAwaiter().GetResult();
        //     return base.OnDisconnectedAsync(exception);
        // }

        public async Task JoinChat(UserConnectionDto conn)
        {
            TotalUsers++;
            await Clients.All
                .SendAsync("ReceiveMessage", "admin", $"{conn.Username} has joined!");
        }

        public async Task JoinSpecificChatroom(UserConnectionDto conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.Chatroom);
            
            await Clients.Group(conn.Chatroom)
                .SendAsync("ReceiveMessage", "admin", $"{conn.Username} has joined {conn.Chatroom}");
        }

        // public async Task SendMessage(string msg)
        // {
        //     await Clients.Groups(U)
        // }
    }
}
