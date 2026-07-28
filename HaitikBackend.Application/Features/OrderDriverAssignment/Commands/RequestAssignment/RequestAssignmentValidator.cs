using FluentValidation;

namespace HaitikBackend.Application.Features.OrderDriverAssignment.Commands.RequestAssignment;

public class RequestAssignmentValidator : AbstractValidator<RequestAssignmentCommand>
{
    public RequestAssignmentValidator()
    {
        RuleFor(x => x.OrderId).GreaterThan(0);
        RuleFor(x => x.DriverId).GreaterThan(0);
    }
}
