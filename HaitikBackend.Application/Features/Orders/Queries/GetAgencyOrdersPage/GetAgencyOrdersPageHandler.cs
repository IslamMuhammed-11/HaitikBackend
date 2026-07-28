using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.Orders.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.Orders.Queries.GetAgencyOrdersPage;

public class GetAgencyOrdersPageHandler : IRequestHandler<GetAgencyOrdersPageQuery, Result<OrdersPageResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetAgencyOrdersPageHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<Result<OrdersPageResponse>> Handle(GetAgencyOrdersPageQuery request, CancellationToken cancellationToken)
    {

        var skip = (request.PageNumber - 1) * request.PageSize;

        var query = _orderRepository.Query().Where(e => e.EmployeeId == request.AgencyId);

        if (request.Status is not null)
            query = query.Where(e => e.Status == request.Status);

        int totalCount = await query.CountAsync();

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
