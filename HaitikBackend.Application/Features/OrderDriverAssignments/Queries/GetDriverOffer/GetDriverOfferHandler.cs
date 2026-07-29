using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.OrderDriverAssignment.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.OrderDriverAssignments.Queries.GetDriverOffer;

public class GetDriverOfferHandler : IRequestHandler<GetDriverOfferQuery, Result<DriverOffer>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetDriverOfferHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<DriverOffer>> Handle(GetDriverOfferQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.OrderDriverAssignments.Query().Where(e => e.OrderId == request.OrderId && e.DriverId == request.DrvierId);

        var offer = await query
            .ProjectTo<DriverOffer>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (offer is null)
            return Result<DriverOffer>.Failed(OrderDriverAssignmentErrors.AssignmentNotFound(request.OrderId, request.DrvierId));

        return Result<DriverOffer>.Success(offer);

    }
}
