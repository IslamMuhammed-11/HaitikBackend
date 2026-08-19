using HaitikBackend.Application.Features.Return.Commands.AcceptReturn;
using HaitikBackend.Application.Features.Return.Commands.RejectReturn;
using HaitikBackend.Application.Features.Return.Commands.RequestReturn;
using HaitikBackend.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HaitikBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReturnsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReturnsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{id}/approve")]
    public async Task<IActionResult> ApproveReturn(int id, [FromBody] AcceptReturnCommand command)
    {
        var cmd = command with { orderId = id };
        var result = await _mediator.Send(cmd);
        return result.ToActionResult();
    }

    [HttpPost("{id}/reject")]
    public async Task<IActionResult> RejectReturn(int id, [FromBody] RejectReturnCommand command)
    {
        var cmd = command with { orderId = id };
        var result = await _mediator.Send(cmd);
        return result.ToActionResult();
    }


    [HttpPost("{id}/request")]
    public async Task<IActionResult> RequestReturn(int id, [FromBody] RequestReturnCommand command)
    {
        var cmd = command with { orderId = id };
        var result = await _mediator.Send(cmd);
        return result.ToActionResult();
    }

}
