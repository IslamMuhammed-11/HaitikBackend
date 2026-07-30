using System.Collections.Generic;

namespace HaitikBackend.Application.Features.Roles.Queries.Responses;

public sealed record RolesPageResponse(IReadOnlyCollection<RoleDetails> Roles , int TotalCount);

public sealed record RoleDetails(int Id, string Name);
