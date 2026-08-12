using HaitikBackend.Application.Features.Users.Command.RegisterUser;
using HaitikBackend.Application.Features.Users.Queries.GetUserDetails;
using HaitikBackend.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaitikBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST: api/admin/users
    [HttpPost("users")]
    public async Task<IActionResult> AddUserAdmin([FromBody] RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    // GET: api/admin/users/{id}
    [HttpGet("users/{id}")]
    public async Task<IActionResult> GetUserAdminById(int id)
    {
        var query = new GetUserDetailsQuery(id);
        var result = await _mediator.Send(query);
        return result.ToActionResult();
    }

}
