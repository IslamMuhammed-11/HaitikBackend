using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.GovernmentEmployees.Commands.AssignUserAsEmployee;

public sealed record AssignUserAsEmployeeCommand(int UserId, int AgencyId) : IRequest<Result>;
