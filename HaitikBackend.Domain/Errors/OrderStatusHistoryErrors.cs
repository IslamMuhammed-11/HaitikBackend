namespace HaitikBackend.Domain.Errors;

public static class OrderStatusHistoryErrors
{
    public static Error HistoryNotFoundOrderStillPending => Error.Create("Order.StillPending", "Order History Not Found Order Still Pending.", Enums.enErrorTypes.NotFound);
}
