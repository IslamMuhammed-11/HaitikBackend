using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.ValueObjects;
using MediatR;

namespace HaitikBackend.Application.Features.GovernmentAgencies.Commands.AddAgency;

public sealed record RegisterAgencyCommand(string Name , GeoLocation Location , string Username , string Password) : IRequest<Result<int>>;
