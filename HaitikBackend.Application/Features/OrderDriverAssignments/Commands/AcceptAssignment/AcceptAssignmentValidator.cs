using FluentValidation;

namespace HaitikBackend.Application.Features.OrderDriverAssignment.Commands.AcceptAssignment;

public class AcceptAssignmentValidator : AbstractValidator<AcceptAssignmentCommand>
{
    public AcceptAssignmentValidator()
    {
        RuleFor(x => x.OrderId).GreaterThan(0);
        RuleFor(x => x.DriverId).GreaterThan(0);
    }
}
