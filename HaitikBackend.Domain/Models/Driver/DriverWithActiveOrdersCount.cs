using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.Models.Driver;

public sealed record DriverWithActiveOrdersCount(HaitikBackend.Domain.Entities.Driver Driver,  int ActiveOrdersCount);
