namespace HaitikBackend.API.Requests.Orders;

public sealed record CreateOrderRequest(string CustomerPhoneNumber, string? CustomerEmail, double Longitude, double Latitude);
