using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.Orders.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.Orders.Queries.GetAllOrdersPage;

public class GetAllOrdersPageHandler : IRequestHandler<GetAllOrdersPageQuery, Result<OrdersPageResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllOrdersPageHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<OrdersPageResponse>> Handle(GetAllOrdersPageQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.PageNumber - 1) * request.PageSize;

        var query = _unitOfWork.Orders.Query();

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
