using HaitikBackend.Application.Interfaces.Security;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace HaitikBackend.Application.Features.GovernmentAgencies.Commands.AddAgency;

public class RegisterAgencyHandler : IRequestHandler<RegisterAgencyCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    public RegisterAgencyHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<int>> Handle(RegisterAgencyCommand request, CancellationToken cancellationToken)
    {
        var exists = await _unitOfWork.Agencies.DoesExistAsync(request.Name);

        if (exists)
            return Result<int>.Failed(GovernmentAgencyErrors.AgencyNameExists);


        var hashedPassword = _passwordHasher.HashPassword(request.Password);

        var agency = GovernmentAgency.Create(request.Name, request.Location, request.Username, hashedPassword);

        _unitOfWork.Agencies.Add(agency);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(agency.Id);
    }

}
