using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Application.Common.Interfaces.BackgroundJobs;

public interface IBackgroundJobs
{
    Task EnqueueAutoAssignment(int orderId);

    Task ScheduleFallbackCheck(int orderId, TimeSpan Delay);

    Task EnqueueOrderStatusNotification(int orderId, enOrderStatus currentStatus, DateTime updatedAt);

    Task EnqueueSendOrderDeliveryOtp(int ordrId);
}
