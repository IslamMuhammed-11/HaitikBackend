

using NetTopologySuite.Geometries;

namespace HaitikBackend.Application.Common.Models;

public class OrderNotificationModel
{
    public int OrderID { get; init; }

    public int DriverID { get; init; }

    public string DriverEmail { get; init; } = string.Empty;

    public double Latitude { get; init; }

    public double Longitude { get; init; }


    private OrderNotificationModel(int orderId, int driverId, string email, Point location)
    {
        OrderID = orderId;
        DriverID = driverId;
        DriverEmail = email;
        Latitude = location.Y;
        Longitude = location.X;
    }


    public static OrderNotificationModel Create(int orderId, int driverId, string email, Point location)
    {
        return new OrderNotificationModel(orderId, driverId, email, location);
    }


}
