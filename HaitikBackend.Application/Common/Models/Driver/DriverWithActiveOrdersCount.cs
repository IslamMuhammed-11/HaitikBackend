using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Application.Common.Models.Driver;

public sealed record DriverWithActiveOrdersCount(HaitikBackend.Domain.Entities.Driver Driver, int ActiveOrdersCount);
