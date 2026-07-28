using HaitikBackend.Domain.Common.Results;
using MediatR;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Application.Features.Drivers.Queries.Responses;

namespace HaitikBackend.Application.Features.Drivers.Queries.GetDriversPage;

public sealed record GetDriversPageQuery(int PageNumber, int PageSize, enDriverStatus? Status, int? GeoZoneId) : IRequest<Result<DriversPageResponse>>;
