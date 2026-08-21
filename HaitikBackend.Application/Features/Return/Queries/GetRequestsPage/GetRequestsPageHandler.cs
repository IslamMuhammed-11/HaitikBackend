using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.Return.Queries.Responses;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.Return.Queries.GetRequestsPage;

public class GetRequestsPageHandler : IRequestHandler<GetRequestsPageQuery, Result<ReturnsPageResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRequestsPageHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ReturnsPageResponse>> Handle(GetRequestsPageQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.PageNumber - 1) * request.PageSize;

        var query = _unitOfWork.Returns.Query();

        if (request.Status is not null)
            query = query.Where(r => r.Status == request.Status.Value);

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .AsNoTracking()
            .OrderByDescending(r => r.OrderId)
            .ProjectTo<ReturnRequest>(_mapper.ConfigurationProvider)
            .Skip(skip)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var response = new ReturnsPageResponse(items, request.PageSize, request.PageNumber, totalCount);

        return Result<ReturnsPageResponse>.Success(response);
    }
}
