using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.Drivers.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.Drivers.Queries.GetDriverDetails;

public class GetDriverDetailsHandler : IRequestHandler<GetDriverDetailsQuery, Result<DriverDetails>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDriverDetailsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<DriverDetails>> Handle(GetDriverDetailsQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Drivers.Query();


        var driver = await query
            .AsNoTracking()
            .Where(e => e.Id == request.Id)
            .ProjectTo<DriverDetails>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (driver is null)
            return Result<DriverDetails>.Failed(DriverErrors.DriverNotFound(request.Id));


        return Result<DriverDetails>.Success(driver);
    }
}
