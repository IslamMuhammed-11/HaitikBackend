using HaitikBackend.API.Requests.Orders;
using HaitikBackend.Application.Abstractions;
using HaitikBackend.Application.Common.Models.FileModels;
using HaitikBackend.Application.Features.Orders.Command.BulkUpload;
using HaitikBackend.Application.Features.Orders.Command.ChangeLocation;
using HaitikBackend.Application.Features.Orders.Command.MarkAsDelivering;
using HaitikBackend.Application.Features.Orders.Command.PlaceOrder;
using HaitikBackend.Application.Features.Orders.Queries.GetAgencyOrdersPage;
using HaitikBackend.Application.Features.Orders.Queries.GetAllOrdersPage;
using HaitikBackend.Application.Features.Orders.Queries.GetOrderDetails;
using HaitikBackend.Application.Features.Orders.Queries.GetOrdersByDriverPage;
using HaitikBackend.Application.Features.Orders.Queries.Responses;
using HaitikBackend.Application.Features.OrderStatusHistories.Queries.GetOrderStatusHistoriesPage;
using HaitikBackend.Application.Features.OrderStatusHistories.Queries.GetOrderStatusHistory;
using HaitikBackend.Authorization;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace HaitikBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IOrderOwnershipService _orderOwnership;

    public OrdersController(IMediator mediator, IOrderOwnershipService orderOwnership)
    {
        _mediator = mediator;
        _orderOwnership = orderOwnership;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.Agency)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {

        if(!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id))
            return Unauthorized();

        var command = new PlaceOrderCommand(request.CustomerPhoneNumber, request.CustomerEmail, request.Longitude, request.Latitude, id);

        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("bulk-upload")]
    [Authorize(Policy = AuthorizationPolicies.Agency)]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> BulkUpload(IFormFile file)
    {
        if (file is null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var fileUpload = new FileUpload
        {
            Content = file.OpenReadStream(),
            FileName = file.FileName,
            ContentType = file.ContentType,
            Extension = Path.GetExtension(file.FileName),
            Length = file.Length
        };

        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var agencyId))
            return Unauthorized();

        var command = new BulkUploadCommand(fileUpload, agencyId);
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpGet("bulk-upload/{batchId}/report")]
    [Authorize(Policy = AuthorizationPolicies.Agency)]
    public Task<IActionResult> GetBulkUploadReport(int batchId)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.Agency)]
    public async Task<IActionResult> GetOrders([FromQuery] int? agencyId, [FromQuery] enOrderStatus? status, IAuthorizationService authorization, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
    {


        var authResult = await authorization.AuthorizeAsync(User, agencyId, AuthorizationPolicies.AgencyOwnership);

        if (!authResult.Succeeded)
            return Forbid();


        var result = Result<OrdersPageResponse>.Failed(OrderErrors.OrderNotFound(0));

        if (agencyId.HasValue)
        {
            var query = new GetAgencyOrdersPageQuery(agencyId.Value, status, pageSize, pageNumber);
            result = await _mediator.Send(query);


        }
        else
        {
            var query = new GetAllOrdersPageQuery(status, pageSize, pageNumber);
            result = await _mediator.Send(query);
        }

        return result.ToActionResult();

    }

    [HttpGet("driver/{driverId:int}")]
    [Authorize(Policy = AuthorizationPolicies.Driver)]
    public async Task<IActionResult> GetDriverOrders(int driverId, [FromQuery] enOrderStatus? status, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
    {
        // This is a self-access check (driver accessing their own order list), not Order resource ownership.
        // Admin bypass is handled by the Driver policy (RequireRole("admin","driver")).
        if (!User.IsInRole("admin") &&
            (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var currentDriverId) ||
             currentDriverId != driverId))
        {
            return Forbid();
        }

        var query = new GetOrdersByDriverPageQuery(driverId, status, pageSize, pageNumber);
        var result = await _mediator.Send(query);
        return result.ToActionResult();
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin,driver,agency")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var query = new GetOrderDetailsQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess || result.Value is null)
            return result.ToActionResult();

        // Ownership check reuses the already-loaded DTO — no extra database query.
        if (!_orderOwnership.CanAccess(result.Value, User))
            return Forbid();

        return result.ToActionResult();
    }

    [HttpPut("{id}/address")]
    [Authorize(Roles = "agency")]
    public async Task<IActionResult> UpdateAddress(int id, [FromBody] ChangeLocationCommand command)
    {
        if (!await _orderOwnership.CanAccessAsync(id, User))
            return Forbid();

        var cmd = command with { OrderId = id };
        var result = await _mediator.Send(cmd);
        return result.ToActionResult();
    }


    [HttpPatch("{id}/marks-as-delivering")]
    [Authorize(Roles = "driver")]
    public async Task<IActionResult> MarkOrderAsDelivering(int id)
    {
        if (!await _orderOwnership.CanAccessAsync(id, User))
            return Forbid();

        var cmd = new MarkAsDeliveringCommand(id);
        var result = await _mediator.Send(cmd);
        return result.ToActionResult();
    }

    [HttpGet("history")]
    [Authorize(Policy = AuthorizationPolicies.Admin)]
    public async Task<IActionResult> GetOrderHistory([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
    {
        var query = new GetOrderStatusHistoriesPageQuery(pageSize, pageNumber);
        var result = await _mediator.Send(query);
        return result.ToActionResult();
    }



    [HttpGet("{id}/history")]
    [Authorize(Policy = AuthorizationPolicies.Agency)]

    public async Task<IActionResult> GetOrderHistoryById(int id)
    {
        if (!await _orderOwnership.CanAccessAsync(id, User))
            return Forbid();

        var query = new GetOrderStatusHistoryQuery(id);
        var result = await _mediator.Send(query);

        return result.ToActionResult();
    }

}
