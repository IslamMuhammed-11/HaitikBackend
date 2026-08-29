using HaitikBackend.Application.Features.PublicTracking.TrackOrder;
using HaitikBackend.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HaitikBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
[EnableRateLimiting("public")]
public class PublicController : ControllerBase
{
    private readonly IMediator _mediator;

    public PublicController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Track/{token}")]
    public async Task<IActionResult> TrackOrder(string token)
    {
        var result = await _mediator.Send(new TrackOrderQuery(token));
        return result.ToActionResult();
    }


}
