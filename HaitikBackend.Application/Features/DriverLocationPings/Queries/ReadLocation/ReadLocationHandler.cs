using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.DriverLocationPings.Queries.Responses;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.DriverLocationPings.Queries.ReadLocation;

public class ReadLocationHandler : IRequestHandler<ReadLocationQuery, Result<LocationPing>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ReadLocationHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<LocationPing>> Handle(ReadLocationQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.DriverLocationPings.Query().Where(e => e.DriverId == request.DriverId);

        var location = await query
            .AsNoTracking()
            .ProjectTo<LocationPing>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (location is null)
            return Result<LocationPing>.Failed(DriveLocationPingErrors.NoProvidedLocationForThisDriver(request.DriverId));

        return Result<LocationPing>.Success(location);

    }
}
