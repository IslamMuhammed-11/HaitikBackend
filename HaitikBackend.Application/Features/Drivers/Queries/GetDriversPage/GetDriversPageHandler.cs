using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.Drivers.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.Drivers.Queries.GetDriversPage;

public class GetDriversPageHandler : IRequestHandler<GetDriversPageQuery, Result<DriversPageResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDriversPageHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<DriversPageResponse>> Handle(GetDriversPageQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Drivers.Query();

        if (request.Status is not null)
            query = query.Where(e => e.Status == request.Status);

        if (request.GeoZoneId is not null)
            query = query.Where(e => e.GeoZoneId == request.GeoZoneId.Value);

        int skip = (request.PageNumber - 1) * request.PageSize;

        int totalCount = await query.CountAsync(cancellationToken);

        List<DriverDetails> page = await query
            .AsNoTracking()
            .OrderByDescending(e => e.Id)
            .ProjectTo<DriverDetails>(_mapper.ConfigurationProvider)
            .Skip(skip)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var response = new DriversPageResponse(page, request.PageSize, request.PageNumber, totalCount);

        return Result<DriversPageResponse>.Success(response);
    }
}
