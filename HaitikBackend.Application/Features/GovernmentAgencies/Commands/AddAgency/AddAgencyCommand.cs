using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.GovernmentAgencies.Commands.AddAgency;

public sealed record AddAgencyCommand(string Name) : IRequest<Result<int>>;
