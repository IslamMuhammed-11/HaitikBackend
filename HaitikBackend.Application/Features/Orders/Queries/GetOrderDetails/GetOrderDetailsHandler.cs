using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.Orders.Queries.Responses;
using HaitikBackend.Application.Features.Users.Queries.GetUsersPage;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.Orders.Queries.GetOrderDetails;

public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetailsQuery, Result<OrderDetails>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderDetailsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<OrderDetails>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Orders.Query();

        var order = await query
            .AsNoTracking()
            .Where(e => e.Id == request.Id)
            .ProjectTo<OrderDetails>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (order is null)
            return Result<OrderDetails>.Failed(OrderErrors.OrderNotFound(request.Id));

        return Result<OrderDetails>.Success(order);
    }
}
