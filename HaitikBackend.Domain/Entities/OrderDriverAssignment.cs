using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Entities;

public partial class OrderDriverAssignment
{
    public int DriverId { get; private set; }

    public int OrderId { get; private set; }

    public enOrderDriverAssignmentStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? RespondedAt { get; private set; }


    private OrderDriverAssignment()
    {
    }

    private OrderDriverAssignment(int driverId, int orderId, enOrderDriverAssignmentStatus status, DateTime createdAt, DateTime? respondedAt = null)
    {
        DriverId = driverId;
        OrderId = orderId;
        Status = status;
        CreatedAt = createdAt;
        RespondedAt = respondedAt;

    }

    public static OrderDriverAssignment AssignRequest(int driverId, int orderId, DateTime createdAt)
    {
        return new OrderDriverAssignment(driverId, orderId, enOrderDriverAssignmentStatus.Pending, createdAt, null);
    }

    public void MarkAsAccepted() => ChangeStatus(enOrderDriverAssignmentStatus.Accepted, DateTime.UtcNow);

    public void MarkAsRejected() => ChangeStatus(enOrderDriverAssignmentStatus.Rejected, DateTime.UtcNow);

    public void MarkAsExpired() => ChangeStatus(enOrderDriverAssignmentStatus.Expired, DateTime.UtcNow);


    public void MarkAsResponded(DateTime responseTime)
    {
        if (RespondedAt is not null)
            return;

        RespondedAt = responseTime;
    }

    public const int ExpireTimeBySeconds = 60;


    private void ChangeStatus(enOrderDriverAssignmentStatus status, DateTime respondedAt)
    {
        Status = status;

        MarkAsResponded(respondedAt);
    }


    public virtual Order Order { get; private set; } = null!;

    public virtual Driver Driver { get; private set; } = null!;

}
