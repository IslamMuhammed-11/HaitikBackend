using HaitikBackend.Application.Features.PublicTracking.TrackOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace HaitikBackend.API.Hubs;

[AllowAnonymous] // for now
public class OrderTrackingHub : Hub
{
    private readonly IMediator _mediator;
    private int? _orderId;

    public OrderTrackingHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public static string GetGroupName(int orderId) => $"order:{orderId}";

    public override async Task OnConnectedAsync()
    {
        var token = Context.GetHttpContext()?.Request.Query["tracking_token"].ToString();

        if (string.IsNullOrWhiteSpace(token))
        {
            Context.Abort();
            return;
        }

        var result = await _mediator.Send(new TrackOrderQuery(token));

        if (!result.IsSuccess || result.Value is null)
        {
            Context.Abort();
            return;
        }

        _orderId = result.Value.OrderId;
        await Groups.AddToGroupAsync(Context.ConnectionId, GetGroupName(_orderId.Value));
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (_orderId.HasValue)
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetGroupName(_orderId.Value));

        await base.OnDisconnectedAsync(exception);
    }
}
