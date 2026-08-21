using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.ValueObjects;
using MediatR;

namespace HaitikBackend.Application.Features.Orders.Command.ChangeLocation;

public sealed record ChangeLocationCommand(int OrderId, double Latitiude , double Longitude) : IRequest<Result<ChangeLocationResponse>>;
