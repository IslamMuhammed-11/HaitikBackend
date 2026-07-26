using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Errors;

public static class DeliveryErrors
{
    public static Error DeliveryProofNotFound(int id) => Error.Create("Delivery.ProofNotFound", $"Delivery proof with id {id} was not found.", enErrorTypes.NotFound);

    public static Error DeliveryAlreadyMarked => Error.Create("Delivery.AlreadyMarked", "Delivery has already been marked as delivered.", enErrorTypes.Conflict);

    public static Error InvalidDeliveryData => Error.Create("Delivery.InvalidData", "Provided delivery data is invalid.", enErrorTypes.Validation);
}
