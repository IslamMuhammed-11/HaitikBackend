using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Common.Validations;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Errors;

namespace HaitikBackend.Domain.Entities;

public partial class OrderStatusHistory : BaseEntity
{
    public int Id { get; private set; }

    public int OrderId { get; private set; }

    public enOrderStatus LastStatus { get; private set; }

    public enOrderStatus CurrentStatus { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public virtual Order Order { get; private set; } = null!;

    private OrderStatusHistory()
    {
    }

    private OrderStatusHistory(int orderId, enOrderStatus lastStatus, enOrderStatus currentStatus, DateTime updatedAt)
    {
        OrderId = orderId;
        LastStatus = lastStatus;
        CurrentStatus = currentStatus;
        UpdatedAt = updatedAt;
    }

    public static Result<OrderStatusHistory> Create(int orderId, enOrderStatus lastStatus, enOrderStatus currentStatus, DateTime updatedAt)
    {
        if (!CheckOrderTransitionsEligibility.Check(lastStatus, currentStatus))
            return Result<OrderStatusHistory>.Failed(OrderErrors.InvalidStatusTransition);

        return Result<OrderStatusHistory>.Success(new OrderStatusHistory(orderId, lastStatus, currentStatus, updatedAt));
    }



}
