using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.DeliveryProofs.Queries.Responses;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.DeliveryProofs.Queries.GetDeliveryProofsPage;

public class GetDeliveryProofsPageHandler : IRequestHandler<GetDeliveryProofsPageQuery, Result<DeliveryProofsPageResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDeliveryProofsPageHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<DeliveryProofsPageResponse>> Handle(GetDeliveryProofsPageQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.PageNumber - 1) * request.PageSize;

        var query = _unitOfWork.DeliveryProofs.Query();

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .AsNoTracking()
            .OrderByDescending(d => d.DeliverdAt)
            .ProjectTo<DeliveryProofResponse>(_mapper.ConfigurationProvider)
            .Skip(skip)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var response = new DeliveryProofsPageResponse(items, request.PageSize, request.PageNumber, totalCount);

        return Result<DeliveryProofsPageResponse>.Success(response);
    }
}
