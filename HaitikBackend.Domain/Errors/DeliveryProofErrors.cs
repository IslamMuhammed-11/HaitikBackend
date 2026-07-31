namespace HaitikBackend.Domain.Errors;

public static class DeliveryProofErrors
{
    public static Error DeliveryProofNotFound(int orderId) => Error.Create("DeliveryProof.NotFound", $"Delivery proof for order {orderId} was not found", Enums.enErrorTypes.NotFound);
}
