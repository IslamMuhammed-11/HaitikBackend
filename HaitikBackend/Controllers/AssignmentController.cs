using HaitikBackend.Application.Features.Orders.Command.AssignDriver;
using HaitikBackend.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HaitikBackend.Authorization;

namespace HaitikBackend.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize(Policy = AuthorizationPolicies.Admin)]
public class AssignmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssignmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{id}/assign")]
    public async Task<IActionResult> AssignDriver(int id, [FromBody] AssignDriverCommand command)
    {
        var cmd = command with { OrderId = id };
        var result = await _mediator.Send(cmd);
        return result.ToActionResult();
    }

    [HttpPost("{id}/assignment/override")]
    public async Task<IActionResult> OverrideAssignment(int id)
    {
        throw new NotImplementedException();
    }
}
