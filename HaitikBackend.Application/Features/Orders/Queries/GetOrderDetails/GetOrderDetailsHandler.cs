using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.Users.Queries.GetUsersPage;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.Orders.Queries.GetOrderDetails;

public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetailsQuery, Result<UserDetails>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderDetailsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<UserDetails>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Orders.Query();

        var order = await query
            .AsNoTracking()
            .Where(e => e.Id == request.Id)
            .ProjectTo<UserDetails>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (order is null)
            return Result<UserDetails>.Failed(OrderErrors.OrderNotFound(request.Id));

        return Result<UserDetails>.Success(order);
    }
}
