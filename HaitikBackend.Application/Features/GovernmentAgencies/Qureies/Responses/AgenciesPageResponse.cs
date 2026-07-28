using System.Collections.Generic;

namespace HaitikBackend.Application.Features.GovernmentAgencies.Qureies.Responses;

public sealed record AgenciesPageResponse(IReadOnlyCollection<AgencyDetails> Agencies, int PageSize, int PageNumber, int TotalCount);
