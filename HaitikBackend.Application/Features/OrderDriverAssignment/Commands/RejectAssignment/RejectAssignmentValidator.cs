using FluentValidation;

namespace HaitikBackend.Application.Features.OrderDriverAssignment.Commands.RejectAssignment;

public class RejectAssignmentValidator : AbstractValidator<RejectAssignmentCommand>
{
    public RejectAssignmentValidator()
    {
        RuleFor(x => x.OrderId).GreaterThan(0);
        RuleFor(x => x.DriverId).GreaterThan(0);
    }
}
