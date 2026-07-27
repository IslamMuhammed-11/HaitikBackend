using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.Repositories;
using MediatR;

namespace HaitikBackend.Application.Features.GovernmentEmployees.Commands.AssignUserAsEmployee;

public class AssignUserAsEmployeeHandler : IRequestHandler<AssignUserAsEmployeeCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IGovernmentAgencyRepository _governmentAgencyRepository;
    private readonly IGovernmentEmployeeRepository _governmentEmployeeRepository;

    public AssignUserAsEmployeeHandler(IUserRepository userRepository, IGovernmentAgencyRepository governmentAgencyRepository, IGovernmentEmployeeRepository governmentEmployeeRepository)
    {
        _userRepository = userRepository;
        _governmentAgencyRepository = governmentAgencyRepository;
        _governmentEmployeeRepository = governmentEmployeeRepository;
    }

    public async Task<Result> Handle(AssignUserAsEmployeeCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            return Result.Failed(UserErrors.UserNotFound(request.UserId));

        var agencyExists = await _governmentAgencyRepository.DoesExistAsync(request.AgencyId);

        if (!agencyExists)
            return Result.Failed(GovernmentAgencyErrors.AgencyNotFound(request.AgencyId));

        var governmentEmployee = user.AssignAsGovernmentEmployee(request.AgencyId);

        _governmentEmployeeRepository.Add(governmentEmployee);

        await _governmentEmployeeRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
