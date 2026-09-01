using HaitikBackend.Application.Features.DriverLocationPings.Commands.PingLocation;
using HaitikBackend.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace HaitikBackend.API.Hubs;


[AllowAnonymous] // for now
public class DriverTrackingHub : Hub
{

    private readonly IMediator _mediator;


    public DriverTrackingHub(IMediator mediator)
    {
        _mediator = mediator;
    }


    public override Task OnConnectedAsync()
    {

        //Auth


        return base.OnConnectedAsync();
        
        
    }

    public async Task SendLocation(double latitude, double longitude , int driverId)
    {

        GeoLocation currentLocation = GeoLocation.Create(latitude, longitude);

        //int driverId = int.TryParse(Context.UserIdentifier, out int id) ? id : 0;

        if (driverId == 0)
            return;

        await _mediator.Send(new PingLocationCommand(driverId, latitude , longitude, DateTime.UtcNow));

        await Clients.Group("admins").SendAsync("recieveLocation", latitude, longitude, driverId);

    }

    public async Task JoinAdminGroup() =>
        await Groups.AddToGroupAsync(Context.ConnectionId, "admins");


    public async Task LeaveAdminGroup() =>
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "admins");


    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }

}
