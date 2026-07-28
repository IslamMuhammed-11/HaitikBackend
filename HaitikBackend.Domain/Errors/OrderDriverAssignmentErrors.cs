using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Errors;

public static class OrderDriverAssignmentErrors
{
    public static Error AssignmentNotFound(int orderId, int driverId) => Error.Create("OrderDriverAssignment.NotFound", $"Assignment for order {orderId} and driver {driverId} was not found.", enErrorTypes.NotFound);
}
