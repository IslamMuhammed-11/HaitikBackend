using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Errors;

public static class OrderErrors
{
    public static Error OrderNotFound(int id) => Error.Create("Order.NotFound", $"Order with id {id} was not found.", enErrorTypes.NotFound);

    public static Error InvalidStatusTransition => Error.Create("Order.InvalidStatusTransition", "The requested order status transition is not allowed.", enErrorTypes.Validation);

    public static Error DriverAlreadyAssigned => Error.Create("Order.DriverAlreadyAssigned", "Order already has a driver assigned.", enErrorTypes.Conflict);

    public static Error OrderAlreadyDelivered => Error.Create("Order.AlreadyDelivered", "Order has already been delivered and cannot be modified.", enErrorTypes.Conflict);

    public static Error CannotUpdateLocation => Error.Create("Order.CannotUpdateLocation", "Order location cannot be updated in its current status.", enErrorTypes.Validation);

    public static Error ConcurrecyConflict => Error.Create("Order.ConcurrencyConflict", "The resource was modified by another user. Please reload and try again.", enErrorTypes.Conflict);

    public static Error CustomerPhoneNumberIsNotValid(string phoneNumber) => Error.Create("Order.InvalidCustomerPhoneNumber", $"The provided phone number is not valid: {phoneNumber}", enErrorTypes.Validation);

    public static Error OrderAlreadyHasReturnRequest => Error.Create("Order.AlreadyHaveReturnRequest", $"Order Already Have Return Request", enErrorTypes.Conflict);

    public static Error OrderAlreadyHasDeliveryProof => Error.Create("Order.AlreadyHaveDeliveryProof", $"Order Already Have Delivery Proof", enErrorTypes.Conflict);

    public static Error InvalidTrackingToken => Error.Create("Order.InvalidTrackingToken", "The tracking token is invalid.", enErrorTypes.NotFound);
}
