using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Application.Features.Return.Queries.Responses;

public sealed record ReturnRequest(int OrderId, int AgencyId, string? AgencyName, int? ReviewedById, string? ReviewdByName, string Reason, enReturnStatus Status);
