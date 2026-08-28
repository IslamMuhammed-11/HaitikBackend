using HaitikBackend.Application.Abstractions;
using HaitikBackend.Application.Features.OrderDriverAssignments.Commands.AutoAssignment;
using HaitikBackend.Application.Features.OrderDriverAssignments.Commands.FallBackCheck;
using HaitikBackend.Application.Features.Otp.CreateOtp;
using HaitikBackend.Application.Features.PublicTracking.CreateTrackingAccess;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Models.Driver;
using Hangfire;
using MediatR;

namespace HaitikBackend.Infrastructure.BackgroundJobs;

public class BackgroundJobs : IBackgroundJobs
{

    private readonly IMediator _mediator;

    public BackgroundJobs(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task EnqueueAutoAssignment(int orderId)
    {
        BackgroundJob.Enqueue<IMediator>(e => e.Send(new AutoAssignmentCommand(orderId)));

        return Task.CompletedTask;
    }

    public Task EnqueueOrderStatusNotification(int orderId, enOrderStatus currentStatus, DateTime updatedAt)
    {
        throw new NotImplementedException();

    }

    public Task EnqueueCreateTrackingAccess(int orderId, string? email)
    {
        BackgroundJob.Enqueue<IMediator>(e => e.Send(new CreateTrackingAccessCommand(orderId, email)));

        return Task.CompletedTask;
    }

    public Task EnqueueSendOrderDeliveryOtp(int orderId, enOtpPurpose purpose)
    {
        BackgroundJob.Enqueue<IMediator>(e => e.Send(new CreateOtpCommand(orderId, purpose)));

        return Task.CompletedTask;
    }

    public Task ScheduleFallbackCheck(int orderId, List<DriverIdWithActiveOrdersCount> drivers, TimeSpan Delay)
    {
        BackgroundJob.Schedule<IMediator>(e => e.Send(new FallbackCheckCommand(orderId, drivers)), Delay);
        return Task.CompletedTask;
    }
}
