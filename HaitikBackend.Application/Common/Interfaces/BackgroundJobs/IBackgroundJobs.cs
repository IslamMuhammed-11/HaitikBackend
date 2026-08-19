using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Models.Driver;

namespace HaitikBackend.Application.Common.Interfaces.BackgroundJobs;

public interface IBackgroundJobs
{
    Task EnqueueAutoAssignment(int orderId);

    Task ScheduleFallbackCheck(int orderId, List<DriverIdWithActiveOrdersCount> drivers, TimeSpan Delay);

    Task EnqueueOrderStatusNotification(int orderId, enOrderStatus currentStatus, DateTime updatedAt);

    Task EnqueueSendOrderDeliveryOtp(int ordrId);
}
