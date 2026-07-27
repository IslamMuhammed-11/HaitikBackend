using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.Orders.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.Orders.Queries.GetAllOrdersPage;

public class GetAllOrdersPageHandler : IRequestHandler<GetAllOrdersPageQuery, Result<OrdersPageResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetAllOrdersPageHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<Result<OrdersPageResponse>> Handle(GetAllOrdersPageQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.PageNumber - 1) * request.PageSize;

        var query = _orderRepository.Query();

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
