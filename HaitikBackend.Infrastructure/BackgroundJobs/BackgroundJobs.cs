using HaitikBackend.Application.Common.Interfaces.BackgroundJobs;
using HaitikBackend.Application.Features.OrderDriverAssignments.Commands.AutoAssignment;
using HaitikBackend.Domain.Enums;
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
        
    }

    public Task EnqueueSendOrderDeliveryOtp(int ordrId)
    {
        throw new NotImplementedException();
    }

    public Task ScheduleFallbackCheck(int orderId, TimeSpan Delay)
    {
        throw new NotImplementedException();
    }
}
