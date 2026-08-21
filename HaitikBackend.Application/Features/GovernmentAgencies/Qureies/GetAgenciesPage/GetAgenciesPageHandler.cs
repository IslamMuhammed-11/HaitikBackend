using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.GovernmentAgencies.Qureies.Responses;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.GovernmentAgencies.Qureies.GetAgenciesPage;

public class GetAgenciesPageHandler : IRequestHandler<GetAgenciesPageQuery, Result<AgenciesPageResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAgenciesPageHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<AgenciesPageResponse>> Handle(GetAgenciesPageQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.PageNumber - 1) * request.PageSize;

        var query = _unitOfWork.Agencies.Query();

        int totalCount = await query.CountAsync(cancellationToken);

        List<AgencyDetails> items = await query
            .AsNoTracking()
            .OrderByDescending(e => e.Id)
            .ProjectTo<AgencyDetails>(_mapper.ConfigurationProvider)
            .Skip(skip)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var response = new AgenciesPageResponse(items, request.PageSize, request.PageNumber, totalCount);

        return Result<AgenciesPageResponse>.Success(response);
    }
}
