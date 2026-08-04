using HaitikBackend.API.Requests.Drivers;
using HaitikBackend.Application.Common.Models.FileModels;
using HaitikBackend.Application.Features.DeliveryProofs.Commands.ProofDelivery;
using HaitikBackend.Application.Features.DriverLocationPings.Commands.PingLocation;
using HaitikBackend.Application.Features.OrderDriverAssignment.Commands.AcceptAssignment;
using HaitikBackend.Application.Features.OrderDriverAssignment.Commands.RejectAssignment;
using HaitikBackend.Application.Features.OrderDriverAssignments.Queries.GetDriverOffersPage;
using HaitikBackend.Application.Features.Otp.CreateOtp;
using HaitikBackend.Application.Features.Otp.VerifyOtp;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HaitikBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriverController : ControllerBase
{
    private readonly IMediator _mediator;

    public DriverController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("orders/offered")]
    public async Task<IActionResult> GetOfferedOrders([FromQuery] int driverId, [FromQuery] enOrderDriverAssignmentStatus? status, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
    {
        var query = new GetDriverOffersPageQuery(status, driverId, pageSize, pageNumber);
        var result = await _mediator.Send(query);
        return result.ToActionResult();
    }

    [HttpPost("orders/{id}/accept")]
    public async Task<IActionResult> AcceptOrder(int id, [FromBody] AcceptAssignmentCommand command)
    {
        var cmd = command with { OrderId = id };
        var result = await _mediator.Send(cmd);
        return result.ToActionResult();
    }

    [HttpPost("orders/{id}/reject")]
    public async Task<IActionResult> RejectOrder(int id, [FromBody] RejectAssignmentCommand command)
    {
        var cmd = command with { OrderId = id };
        var result = await _mediator.Send(cmd);
        return result.ToActionResult();
    }

    [HttpPost("location")]
    public async Task<IActionResult> UpdateLocation([FromBody] PingLocationCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("orders/{id}/pod")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> ProofOfDelivery(int id, DeliveryProofRequest request)
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

        var command = new ProofDeliveryCommand(id, fileUpload, request.ReciverName, request.deliveryNotes);
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("orders/{id}/delivery/request-otp")]
    public async Task<IActionResult> RequestDeliveryOtp(int id)
    {
        var command = new CreateOtpCommand(id, enOtpPurpose.Delivery);
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("orders/{id}/delivery/verify-otp")]
    public async Task<IActionResult> VerifyDeliveryOtp(int id, [FromBody] VerifyOtpCommand command)
    {
        var cmd = command with { OrderId = id, Purpose = enOtpPurpose.Delivery };
        var result = await _mediator.Send(cmd);
        return result.ToActionResult();
    }

    [HttpPost("orders/{id}/return/request-otp")]
    public async Task<IActionResult> RequestReturnOtp(int id)
    {
        var command = new CreateOtpCommand(id, enOtpPurpose.Return);
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("orders/{id}/return/verify-otp")]
    public async Task<IActionResult> VerifyReturnOtp(int id, [FromBody] VerifyOtpCommand command)
    {
        var cmd = command with { OrderId = id, Purpose = enOtpPurpose.Return };
        var result = await _mediator.Send(cmd);
        return result.ToActionResult();
    }
}
