using Microsoft.AspNetCore.SignalR;

namespace HaitikBackend.API.Hubs;

public class OrderTrackingHub : Hub
{
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }






    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }

}
