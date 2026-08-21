using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.OrderStatusHistories.Queries.Responses;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.OrderStatusHistories.Queries.GetOrderStatusHistory;

public class GetOrderStatusHistoryHandler : IRequestHandler<GetOrderStatusHistoryQuery, Result<OrderStatusHistoryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderStatusHistoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<OrderStatusHistoryResponse>> Handle(GetOrderStatusHistoryQuery request, CancellationToken cancellationToken)
    {
        var item = await _unitOfWork.OrderStatusHistory.Query()
            .AsNoTracking()
            .Where(e => e.Id == request.OrderId)
            .ProjectTo<OrderStatusHistoryResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (item is null)
            return Result<OrderStatusHistoryResponse>.Failed(OrderErrors.OrderNotFound(request.OrderId));

        return Result<OrderStatusHistoryResponse>.Success(item);
    }
}
