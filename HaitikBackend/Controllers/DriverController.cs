using HaitikBackend.Application.Abstractions;
using HaitikBackend.Application.Common.Models.FileModels;
using HaitikBackend.Application.Features.DeliveryProofs.Commands.ProofDelivery;
using HaitikBackend.Application.Features.OrderDriverAssignment.Commands.AcceptAssignment;
using HaitikBackend.Application.Features.OrderDriverAssignment.Commands.RejectAssignment;
using HaitikBackend.Application.Features.OrderDriverAssignments.Queries.GetDriverOffersPage;
using HaitikBackend.Application.Features.Otp.CreateOtp;
using HaitikBackend.Application.Features.Otp.VerifyOtp;
using HaitikBackend.Authorization;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HaitikBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = AuthorizationPolicies.Driver)]
public class DriverController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IOrderOwnershipService _orderOwnership;

    public DriverController(IMediator mediator, IOrderOwnershipService orderOwnership)
    {
        _mediator = mediator;
        _orderOwnership = orderOwnership;
    }

    [HttpGet("orders/offered")]
    public async Task<IActionResult> GetOfferedOrders([FromQuery] int driverId, [FromQuery] enOrderDriverAssignmentStatus? status, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
    {
        if (!User.IsInRole("admin"))
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var currentDriverId) || currentDriverId != driverId)
                return Forbid();
        }

        var query = new GetDriverOffersPageQuery(status, driverId, pageSize, pageNumber);
        var result = await _mediator.Send(query);
        return result.ToActionResult();
    }

    [HttpPost("orders/{id}/accept")]
    public async Task<IActionResult> AcceptOrder(int id, [FromBody] AcceptAssignmentCommand command)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var driverId))
            return Unauthorized();

        if (!await _orderOwnership.CanAccessAsync(id, User))
            return Forbid();

        var cmd = command with { OrderId = id, DriverId = driverId };
        var result = await _mediator.Send(cmd);
        return result.ToActionResult();
    }

    [HttpPost("orders/{id}/reject")]
    public async Task<IActionResult> RejectOrder(int id, [FromBody] RejectAssignmentCommand command)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var driverId))
            return Unauthorized();

        if (!await _orderOwnership.CanAccessAsync(id, User))
            return Forbid();

        var cmd = command with { OrderId = id, DriverId = driverId };
        var result = await _mediator.Send(cmd);
        return result.ToActionResult();
    }

    [HttpPost("orders/{id}/pod")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> ProofOfDelivery(int id, IFormFile File, [FromQuery] string ReciverName, [FromQuery] string? deliveryNotes)
    {
        if (!await _orderOwnership.CanAccessAsync(id, User))
            return Forbid();

        if (File is null || File.Length == 0)
            return BadRequest("No file uploaded.");

        var fileUpload = new FileUpload
        {
            Content = File.OpenReadStream(),
            FileName = File.FileName,
            ContentType = File.ContentType,
            Extension = Path.GetExtension(File.FileName),
            Length = File.Length
        };

        var command = new ProofDeliveryCommand(id, fileUpload, ReciverName, deliveryNotes);
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("orders/{id}/delivery/request-otp")]
    public async Task<IActionResult> RequestDeliveryOtp(int id)
    {
        if (!await _orderOwnership.CanAccessAsync(id, User))
            return Forbid();

        var command = new CreateOtpCommand(id, enOtpPurpose.Delivery);
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("orders/{id}/delivery/verify-otp")]
    public async Task<IActionResult> VerifyDeliveryOtp(int id, [FromBody] VerifyOtpCommand command)
    {
        if (!await _orderOwnership.CanAccessAsync(id, User))
            return Forbid();

        var cmd = command with { OrderId = id, Purpose = enOtpPurpose.Delivery };
        var result = await _mediator.Send(cmd);
        return result.ToActionResult();
    }

    [HttpPost("orders/{id}/return/request-otp")]
    public async Task<IActionResult> RequestReturnOtp(int id)
    {
        if (!await _orderOwnership.CanAccessAsync(id, User))
            return Forbid();

        var command = new CreateOtpCommand(id, enOtpPurpose.Return);
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("orders/{id}/return/verify-otp")]
    public async Task<IActionResult> VerifyReturnOtp(int id, [FromBody] VerifyOtpCommand command)
    {
        if (!await _orderOwnership.CanAccessAsync(id, User))
            return Forbid();

        var cmd = command with { OrderId = id, Purpose = enOtpPurpose.Return };
        var result = await _mediator.Send(cmd);
        return result.ToActionResult();
    }
}

