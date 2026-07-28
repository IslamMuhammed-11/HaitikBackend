using FluentValidation;

namespace HaitikBackend.Application.Features.GovernmentAgencies.Commands.AddAgency;

public class AddAgencyValidator : AbstractValidator<AddAgencyCommand>
{
    public AddAgencyValidator()
    {
        RuleFor(e => e.Name).NotNull().NotEmpty().MinimumLength(2);
    }
}
