using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.Orders.Queries.Responses;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.Orders.Queries.GetOrdersByDriverPage;

public class GetOrdersByDriverPageHandler : IRequestHandler<GetOrdersByDriverPageQuery, Result<OrdersPageResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrdersByDriverPageHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<OrdersPageResponse>> Handle(GetOrdersByDriverPageQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.PageNumber - 1) * request.PageSize;

        var query = _unitOfWork.Orders.Query()
            .Where(e => e.AssignedDriver == request.DriverId);

        if (request.Status is not null)
            query = query.Where(e => e.Status == request.Status);

        int totalCount = await query.CountAsync(cancellationToken);

        List<OrderDetails> orders = await query
            .AsNoTracking()
            .OrderByDescending(e => e.CreatedAt)
            .ProjectTo<OrderDetails>(_mapper.ConfigurationProvider)
            .Skip(skip)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var response = new OrdersPageResponse(orders, request.PageSize, request.PageNumber, totalCount);

        return Result<OrdersPageResponse>.Success(response);
    }
}
