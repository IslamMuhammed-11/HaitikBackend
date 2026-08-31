using HaitikBackend.Application.Abstractions;
using HaitikBackend.Application.Features.Return.Commands.AcceptReturn;
using HaitikBackend.Application.Features.Return.Commands.RejectReturn;
using HaitikBackend.Application.Features.Return.Commands.RequestReturn;
using HaitikBackend.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HaitikBackend.Authorization;

namespace HaitikBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReturnsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IOrderOwnershipService _orderOwnership;

    public ReturnsController(IMediator mediator, IOrderOwnershipService orderOwnership)
    {
        _mediator = mediator;
        _orderOwnership = orderOwnership;
    }

    [HttpPost("{id}/approve")]
    [Authorize(Policy = AuthorizationPolicies.Admin)]
    public async Task<IActionResult> ApproveReturn(int id, [FromBody] AcceptReturnCommand command)
    {
        var cmd = command with { orderId = id };
        var result = await _mediator.Send(cmd);
        return result.ToActionResult();
    }

    [HttpPost("{id}/reject")]
    [Authorize(Policy = AuthorizationPolicies.Admin)]
    public async Task<IActionResult> RejectReturn(int id, [FromBody] RejectReturnCommand command)
    {
        var cmd = command with { orderId = id };
        var result = await _mediator.Send(cmd);
        return result.ToActionResult();
    }


    [HttpPost("{id}/request")]
    [Authorize(Policy = AuthorizationPolicies.Agency)]
    public async Task<IActionResult> RequestReturn(int id, [FromBody] RequestReturnCommand command)
    {
        if (!await _orderOwnership.CanAccessAsync(id, User))
            return Forbid();

        var cmd = command with { orderId = id };
        var result = await _mediator.Send(cmd);
        return result.ToActionResult();
    }

}
