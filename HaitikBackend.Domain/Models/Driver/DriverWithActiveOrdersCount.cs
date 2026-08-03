namespace HaitikBackend.Domain.Models.Driver;

public sealed record DriverWithActiveOrdersCount(Entities.Driver Driver, int ActiveOrdersCount);
