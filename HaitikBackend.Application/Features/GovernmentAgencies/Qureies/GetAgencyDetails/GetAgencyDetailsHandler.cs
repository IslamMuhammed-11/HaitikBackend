using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.GovernmentAgencies.Qureies.Responses;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.GovernmentAgencies.Qureies.GetAgencyDetails;

public class GetAgencyDetailsHandler : IRequestHandler<GetAgencyDetailsQuery, Result<AgencyDetails>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAgencyDetailsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<AgencyDetails>> Handle(GetAgencyDetailsQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Agencies.Query();

        var agency = await query
            .AsNoTracking()
            .Where(e => e.Id == request.Id)
            .ProjectTo<AgencyDetails>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (agency is null)
            return Result<AgencyDetails>.Failed(GovernmentAgencyErrors.AgencyNotFound(request.Id));

        return Result<AgencyDetails>.Success(agency);
    }
}
