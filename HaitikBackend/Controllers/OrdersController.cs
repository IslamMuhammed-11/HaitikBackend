using HaitikBackend.API.Requests.Orders;
using HaitikBackend.Application.Common.Models.FileModels;
using HaitikBackend.Application.Features.Orders.Command.BulkUpload;
using HaitikBackend.Application.Features.Orders.Command.ChangeLocation;
using HaitikBackend.Application.Features.Orders.Command.PlaceOrder;
using HaitikBackend.Application.Features.Orders.Queries.GetAgencyOrdersPage;
using HaitikBackend.Application.Features.Orders.Queries.GetAllOrdersPage;
using HaitikBackend.Application.Features.Orders.Queries.GetOrderDetails;
using HaitikBackend.Application.Features.Orders.Queries.GetOrdersByDriverPage;
using HaitikBackend.Application.Features.OrderStatusHistories.Queries.GetOrderStatusHistoriesPage;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace HaitikBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] PlaceOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("bulk-upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> BulkUpload(BulkUploadRequest request)
    {
        if (request.File is null || request.File.Length == 0)
            return BadRequest("No file uploaded.");

        var fileUpload = new FileUpload
        {
            Content = request.File.OpenReadStream(),
            FileName = request.File.FileName,
            ContentType = request.File.ContentType,
            Extension = Path.GetExtension(request.File.FileName),
            Length = request.File.Length
        };

        var command = new BulkUploadCommand(fileUpload);
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpGet("bulk-upload/{batchId}/report")]
    public Task<IActionResult> GetBulkUploadReport(int batchId)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders([FromQuery] int? agencyId, [FromQuery] enOrderStatus? status, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
    {
        if (agencyId.HasValue)
        {
            var query = new GetAgencyOrdersPageQuery(agencyId.Value, status, pageSize, pageNumber);
            var result = await _mediator.Send(query);
            return result.ToActionResult();
        }
        else
        {
            var query = new GetAllOrdersPageQuery(status, pageSize, pageNumber);
            var result = await _mediator.Send(query);
            return result.ToActionResult();
        }
    }

    [HttpGet("driver/{driverId:int}")]
    public async Task<IActionResult> GetDriverOrders(int driverId, [FromQuery] enOrderStatus? status, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
    {
        var query = new GetOrdersByDriverPageQuery(driverId, status, pageSize, pageNumber);
        var result = await _mediator.Send(query);
        return result.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var query = new GetOrderDetailsQuery(id);
        var result = await _mediator.Send(query);
        return result.ToActionResult();
    }

    [HttpPut("{id}/address")]
    public async Task<IActionResult> UpdateAddress(int id, [FromBody] ChangeLocationCommand command)
    {
        var cmd = command with { OrderId = id };
        var result = await _mediator.Send(cmd);
        return result.ToActionResult();
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetOrderHistory([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
    {
        var query = new GetOrderStatusHistoriesPageQuery(pageSize, pageNumber);
        var result = await _mediator.Send(query);
        return result.ToActionResult();
    }
}
