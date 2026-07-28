using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Drivers.Commands.UpdateTotalOrdersPerDay;

public sealed record UpdateTotalOrdersPerDayCommand(int DriverId, short? MaximumOrdersPerDay) : IRequest<Result>;
