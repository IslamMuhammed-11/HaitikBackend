using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Common.Validations;
using HaitikBackend.Domain.DomainEvents.OrderEvents;
using HaitikBackend.Domain.DomainEvents.ReturnEvents;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.ValueObjects;

namespace HaitikBackend.Domain.Entities;

public partial class Order : BaseEntity
{
    public int Id { get; private set; }

    public enOrderStatus Status { get; private set; }

    public string CustomerPhoneNumber { get; private set; } = null!;

    public string? CustomerEmail { get; private set; }

    public int? AssignedDriver { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public GeoLocation DeliveryLocation { get; private set; } = null!;

    public int AgencyId { get; private set; }

    public byte[] RowVersion { get; private set; } = default!;

    public string? TrackingTokenHash { get; private set; }

    private Order()
    {
    }

    private Order(enOrderStatus status, string customerPhoneNumber, string? email, int? assignedDriver, DateTime createdAt, GeoLocation pickupLocation, int employeeId, string? trackingTokenHash)
    {
        Status = status;
        CustomerPhoneNumber = customerPhoneNumber;
        CustomerEmail = email;
        AssignedDriver = assignedDriver;
        CreatedAt = createdAt;
        DeliveryLocation = pickupLocation;
        AgencyId = employeeId;
        TrackingTokenHash = trackingTokenHash;

    }


    public static Order Create(string customerPhoneNumber, string? email, DateTime createdAt, GeoLocation pickupLocation, int agencyId, string? trackingTokenHash,
                                                 enOrderStatus status = enOrderStatus.Pending, int? assignedDriver = null)
    {
        return
            new Order(status, customerPhoneNumber, email, assignedDriver, createdAt, pickupLocation, agencyId, trackingTokenHash);


    }




    public Result SetStatusAsReceivedPackage() =>
        _ChangeOrderStatus(enOrderStatus.ReceivedPackage);

    public Result SetStatusAsDelivering() =>
        _ChangeOrderStatus(enOrderStatus.Delivering);

    public Result SetStatusAsDelivered() =>
        _ChangeOrderStatus(enOrderStatus.Delivered);

    public Result AssignDriver(int driverId)
    {
        if (AssignedDriver is not null)
            return Result.Failed(OrderErrors.DriverAlreadyAssigned);

        AssignedDriver = driverId;

        Raise(new DriverAssignedEvent(Id, driverId));

        return Result.Success();
    }

    public OrderDriverAssignment RequestToAssignDriver(int driverId)
    {
        return OrderDriverAssignment.RequestAssign(driverId, Id, DateTime.UtcNow);
    }

    public Result<Return> RequestToReturn(string reason)
    {
        if (this.Return is not null)
            return Result<Return>.Failed(OrderErrors.OrderAlreadyHasReturnRequest);

        var request = Return.ReturnRequest(Id, AgencyId, reason);

        this.Return = request;

        Raise(new ReturnRequestCreatedEvent(Id, AgencyId, reason));

        return Result<Return>.Success(request);

    }

    public void ExpirePendingAssignments()
    {
        foreach (var assignment in OrderDriverAssignments.Where(e => e.Status == enOrderDriverAssignmentStatus.Pending))
            assignment.MarkAsExpired();
    }

    public void SetTrackingToken(string hashedToken)
    {
        TrackingTokenHash = hashedToken;
    }

    public Result ProofDelivery(string imageUrl, string reciverName, string? deliveryNotes, DateTime deliveredAt)
    {

        if (DeliveryProof is not null)
            return Result.Failed(OrderErrors.OrderAlreadyHasDeliveryProof);

        var proof = DeliveryProof.Create(Id, imageUrl, reciverName, deliveryNotes, deliveredAt);

        DeliveryProof = proof;

        Raise(new DeliveryProofWasUploadedEvent(Id));

        return Result.Success();
    }

    public Result UpdateLocation(GeoLocation newPickupLocation)
    {
        if (Status != enOrderStatus.Pending)
            return Result.Failed(OrderErrors.CannotUpdateLocation);

        var oldLocation = DeliveryLocation;

        DeliveryLocation = newPickupLocation;

        Raise(new OrderLoactionChanged(Id, oldLocation, newPickupLocation));

        return Result.Success();
    }

    private Result _ChangeOrderStatus(enOrderStatus status)
    {

        if (!CheckOrderTransitionsEligibility.Check(Status, status))
            return Result.Failed(OrderErrors.InvalidStatusTransition);

        OrderStatusHistories.Add(OrderStatusHistory.Log(Id, Status, status, DateTime.Now).Value!);

        Status = status;

        Raise(new OrderStatusChangedEvent(Id, CustomerEmail, status, DateTime.Now));

        return Result.Success();
    }

    public virtual GovernmentAgency Agency { get; private set; } = null!;

    public virtual Return? Return { get; private set; }

    public virtual DeliveryProof? DeliveryProof { get; private set; }

    public virtual ICollection<OtpCode> OtpCodes { get; private set; } = new List<OtpCode>();

    public virtual ICollection<OrderDriverAssignment> OrderDriverAssignments { get; private set; } = new List<OrderDriverAssignment>();

    public virtual Driver? Driver { get; private set; }


    public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; private set; } = new List<OrderStatusHistory>();
}
