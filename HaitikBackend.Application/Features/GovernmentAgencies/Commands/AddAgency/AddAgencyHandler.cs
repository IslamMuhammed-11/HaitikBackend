using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.Repositories;
using MediatR;

namespace HaitikBackend.Application.Features.GovernmentAgencies.Commands.AddAgency;

public class AddAgencyHandler : IRequestHandler<AddAgencyCommand, Result<int>>
{
    private readonly IGovernmentAgencyRepository _governmentAgencyRepository;

    public AddAgencyHandler(IGovernmentAgencyRepository governmentAgencyRepository)
    {
        _governmentAgencyRepository = governmentAgencyRepository;
    }

    public async Task<Result<int>> Handle(AddAgencyCommand request, CancellationToken cancellationToken)
    {
        var exists = await _governmentAgencyRepository.DoesExistAsync(request.Name);

        if (exists)
            return Result<int>.Failed(GovernmentAgencyErrors.AgencyNameExists);


        var agency = GovernmentAgency.Create(request.Name); 

        _governmentAgencyRepository.Add(agency);

        await _governmentAgencyRepository.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(agency.Id);
    }
}
