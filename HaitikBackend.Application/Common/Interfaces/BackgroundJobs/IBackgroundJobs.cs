using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Models.Driver;

namespace HaitikBackend.Application.Common.Interfaces.BackgroundJobs;

public interface IBackgroundJobs
{
    Task EnqueueAutoAssignment(int orderId);

    Task ScheduleFallbackCheck(int orderId, ICollection<DriverWithActiveOrdersCount> drivers, TimeSpan Delay);

    Task EnqueueOrderStatusNotification(int orderId, enOrderStatus currentStatus, DateTime updatedAt);

    Task EnqueueSendOrderDeliveryOtp(int ordrId);
}
