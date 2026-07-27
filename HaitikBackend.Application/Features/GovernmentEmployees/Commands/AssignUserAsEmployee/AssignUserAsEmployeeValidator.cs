using FluentValidation;

namespace HaitikBackend.Application.Features.GovernmentEmployees.Commands.AssignUserAsEmployee;

public class AssignUserAsEmployeeValidator : AbstractValidator<AssignUserAsEmployeeCommand>
{
    public AssignUserAsEmployeeValidator()
    {
        RuleFor(e => e.UserId).NotNull().GreaterThan(0);
        RuleFor(e => e.AgencyId).NotNull().GreaterThan(0);
    }
}
