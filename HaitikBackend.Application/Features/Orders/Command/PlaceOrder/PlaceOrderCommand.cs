using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Application.Features.Orders.Command.PlaceOrder;

public sealed record PlaceOrderCommand(string CustomerPhoneNumber , GeoLocation PickupLocation , int agencyId) : IRequest<Result<int>>;

