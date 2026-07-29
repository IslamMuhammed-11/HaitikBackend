using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace HaitikBackend.Application.Features.GovernmentEmployees.Commands.AssignUserAsEmployee;

public class AssignUserAsEmployeeHandler : IRequestHandler<AssignUserAsEmployeeCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignUserAsEmployeeHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AssignUserAsEmployeeCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            return Result.Failed(UserErrors.UserNotFound(request.UserId));

        var agencyExists = await _unitOfWork.Agencies.DoesExistAsync(request.AgencyId);

        if (!agencyExists)
            return Result.Failed(GovernmentAgencyErrors.AgencyNotFound(request.AgencyId));

        var governmentEmployee = user.AssignAsGovernmentEmployee(request.AgencyId);

        _unitOfWork.Employees.Add(governmentEmployee);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
