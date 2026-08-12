using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.ValueObjects;
using MediatR;

namespace HaitikBackend.Application.Features.GovernmentAgencies.Commands.AddAgency;

public sealed record RegisterAgencyCommand(string Name , double Longitude , double Latitude , string Username , string Password) : IRequest<Result<int>>;
