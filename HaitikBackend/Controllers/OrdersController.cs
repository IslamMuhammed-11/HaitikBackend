using HaitikBackend.Application.Common.Models.FileModels;
using HaitikBackend.Application.Features.Orders.Command.BulkUpload;
using HaitikBackend.Application.Features.Orders.Command.ChangeLocation;
using HaitikBackend.Application.Features.Orders.Command.MarkAsDelivering;
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

    [HttpPost("bulk-upload/{agencyId}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> BulkUpload(int agencyId, IFormFile file)
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

        var command = new BulkUploadCommand(fileUpload, agencyId);
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


    [HttpPatch("{id}/marks-as-delivering")]
    public async Task<IActionResult> MarkOrderAsDelivering(int id)
    {
        var cmd = new MarkAsDeliveringCommand(id);

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
