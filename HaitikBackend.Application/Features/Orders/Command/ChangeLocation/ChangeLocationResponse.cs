using HaitikBackend.Domain.ValueObjects;
using System;

namespace HaitikBackend.Application.Features.Orders.Command.ChangeLocation;

public sealed record ChangeLocationResponse(int OrderId, GeoLocation NewLocation, DateTime ChangedAt);
