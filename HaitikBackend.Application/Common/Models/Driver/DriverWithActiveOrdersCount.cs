namespace HaitikBackend.Application.Common.Models.Driver;

public sealed record DriverWithActiveOrdersCount(Domain.Entities.Driver Driver, int ActiveOrdersCount);
