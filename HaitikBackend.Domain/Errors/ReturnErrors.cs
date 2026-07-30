namespace HaitikBackend.Domain.Errors;

public static class ReturnErrors
{

    public static Error ReturnRequestNotFound(int orderId) => 
        Error.Create("Return.NotFound", $"Return request with this order Id was not found {orderId}", Enums.enErrorTypes.NotFound);

   
}
