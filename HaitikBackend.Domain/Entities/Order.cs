using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Common.Validations;
using HaitikBackend.Domain.DomainEvents.OrderEvents;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.ValueObjects;

namespace HaitikBackend.Domain.Entities;

public partial class Order : BaseEntity
{
    public int Id { get; private set; }

    public enOrderStatus Status { get; private set; }

    public string CustomerPhoneNumber { get; private set; } = null!;

    public int? AssignedDriver { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public GeoLocation PickupLocation { get; private set; } = null!;

    public int? GeoZone { get; private set; }

    public int AgencyId { get; private set; }


    private Order()
    {
    }

    private Order(enOrderStatus status, string customerPhoneNumber, int? assignedDriver, DateTime createdAt, GeoLocation pickupLocation, int? geoZone, int agencyId)
    {
        Status = status;
        CustomerPhoneNumber = customerPhoneNumber;
        AssignedDriver = assignedDriver;
        CreatedAt = createdAt;
        PickupLocation = pickupLocation;
        GeoZone = geoZone;
        AgencyId = agencyId;

    }


    public static Order Create(string customerPhoneNumber, DateTime createdAt, GeoLocation pickupLocation, int? geoZone, int agencyId,
                                                 enOrderStatus status = enOrderStatus.Pending, int? assignedDriver = null)
    {
        return 
            new Order(status, customerPhoneNumber, assignedDriver, createdAt, pickupLocation, geoZone, agencyId);


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

    public Result UpdateLocation(GeoLocation newPickupLocation)
    {
        if (Status != enOrderStatus.Pending)
            return Result.Failed(OrderErrors.CannotUpdateLocation);

        var oldLocation = PickupLocation;

        PickupLocation = newPickupLocation;

        //UpdateGeoZone()

        Raise(new OrderLoactionChanged(Id, oldLocation, newPickupLocation));

        return Result.Success();
    }

    private Result _ChangeOrderStatus(enOrderStatus status)
    {

        if (!CheckOrderTransitionsEligibility.Check(Status, status))
            return Result.Failed(OrderErrors.InvalidStatusTransition);

        Status = status;

        Raise(new OrderStatusChangedEvent(Id, status, DateTime.Now));

        return Result.Success();
    }

    public virtual GovernmentAgency Agency { get; private set; } = null!;

    public virtual ICollection<DeliveryProof> DeliveryProofs { get; private set; } = new List<DeliveryProof>();

    public virtual ICollection<OtpCode> OtpCodes { get; private set; } = new List<OtpCode>();

    public virtual GeoZone? GeoZoneNavigation { get; private set; }

    public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; private set; } = new List<OrderStatusHistory>();
}
