using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Orders.Command.PlaceOrder;

public sealed record PlaceOrderCommand(string CustomerPhoneNumber,string? CustomerEmail, double Longitude, double Latitude, int AgencyId) : IRequest<Result<PlaceOrderResponse>>;

