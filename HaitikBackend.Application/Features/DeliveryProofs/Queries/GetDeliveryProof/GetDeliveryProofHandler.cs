using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.DeliveryProofs.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.DeliveryProofs.Queries.GetDeliveryProof;

public class GetDeliveryProofHandler : IRequestHandler<GetDeliveryProofQuery, Result<DeliveryProofResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDeliveryProofHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<DeliveryProofResponse>> Handle(GetDeliveryProofQuery request, CancellationToken cancellationToken)
    {
        var item = await _unitOfWork.DeliveryProofs.Query()
            .AsNoTracking()
            .Where(d => d.OrderId == request.OrderId)
            .ProjectTo<DeliveryProofResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (item is null)
            return Result<DeliveryProofResponse>.Failed(DeliveryProofErrors.DeliveryProofNotFound(request.OrderId));

        return Result<DeliveryProofResponse>.Success(item);
    }
}
