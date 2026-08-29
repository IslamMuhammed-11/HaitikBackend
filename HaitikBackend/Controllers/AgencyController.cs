using HaitikBackend.Application.Features.GovernmentAgencies.Commands.AddAgency;
using HaitikBackend.Application.Features.GovernmentAgencies.Qureies.GetAgenciesPage;
using HaitikBackend.Application.Features.GovernmentAgencies.Qureies.GetAgencyDetails;
using HaitikBackend.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HaitikBackend.Authorization;

namespace HaitikBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgencyController : ControllerBase
{
    private readonly IMediator _mediator;

    public AgencyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST: api/agency
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.Admin)]
    public async Task<IActionResult> AddAgency([FromBody] RegisterAgencyCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    // GET: api/agency/{id}
    [HttpGet("{id}")]
    [Authorize(Policy = AuthorizationPolicies.Agency)]
    [Authorize(Policy = AuthorizationPolicies.AgencyOwnership)]
    public async Task<IActionResult> GetAgencyById(int id)
    {
        var query = new GetAgencyDetailsQuery(id);
        var result = await _mediator.Send(query);
        return result.ToActionResult();
    }

    // GET: api/agency?pageSize=10&pageNumber=1
    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.Admin)]
    public async Task<IActionResult> GetAllAgencies([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
    {
        var query = new GetAgenciesPageQuery(pageSize, pageNumber);
        var result = await _mediator.Send(query);
        return result.ToActionResult();
    }
}
