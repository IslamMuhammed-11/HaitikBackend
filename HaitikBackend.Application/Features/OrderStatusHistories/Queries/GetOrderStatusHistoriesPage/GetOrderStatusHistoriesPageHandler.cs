using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.OrderStatusHistories.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.OrderStatusHistories.Queries.GetOrderStatusHistoriesPage;

public class GetOrderStatusHistoriesPageHandler : IRequestHandler<GetOrderStatusHistoriesPageQuery, Result<OrderStatusHistoriesPageResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderStatusHistoriesPageHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<OrderStatusHistoriesPageResponse>> Handle(GetOrderStatusHistoriesPageQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.PageNumber - 1) * request.PageSize;

        var query = _unitOfWork.OrderStatusHistory.Query();

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .AsNoTracking()
            .OrderByDescending(e => e.OrderId)
            .ThenByDescending(e => e.UpdatedAt)
            .ProjectTo<OrderStatusHistoryResponse>(_mapper.ConfigurationProvider)
            .Skip(skip)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var response = new OrderStatusHistoriesPageResponse(items, request.PageSize, request.PageNumber, totalCount);

        return Result<OrderStatusHistoriesPageResponse>.Success(response);
    }
}
