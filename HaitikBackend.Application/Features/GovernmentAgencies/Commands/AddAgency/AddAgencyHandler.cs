using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace HaitikBackend.Application.Features.GovernmentAgencies.Commands.AddAgency;

public class AddAgencyHandler : IRequestHandler<AddAgencyCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    public AddAgencyHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(AddAgencyCommand request, CancellationToken cancellationToken)
    {
        var exists = await _unitOfWork.Agencies.DoesExistAsync(request.Name);

        if (exists)
            return Result<int>.Failed(GovernmentAgencyErrors.AgencyNameExists);


        var agency = GovernmentAgency.Create(request.Name);

        _unitOfWork.Agencies.Add(agency);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(agency.Id);
    }
}
