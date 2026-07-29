using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.OrderDriverAssignment.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.OrderDriverAssignments.Queries.GetOrderOffersPage;

public class GetOrderOffersPageHandler : IRequestHandler<GetOrderOffersPageQuery, Result<OffersPageResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderOffersPageHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<OffersPageResponse>> Handle(GetOrderOffersPageQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.PageNumber - 1) * request.PageSize;

        var query = _unitOfWork.OrderDriverAssignments.Query().Where(a => a.OrderId == request.OrderId);

        if (request.Status is not null)
            query = query.Where(a => a.Status == request.Status.Value);

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .AsNoTracking()
            .OrderByDescending(a => a.CreatedAt)
            .ProjectTo<DriverOffer>(_mapper.ConfigurationProvider)
            .Skip(skip)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var response = new OffersPageResponse(items, request.PageSize, request.PageNumber, totalCount);

        return Result<OffersPageResponse>.Success(response);
    }
}
