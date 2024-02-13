using DenticaDentistry.Application.Commands;
using FluentValidation;

namespace DenticaDentistry.Application.Validators;

public class ChangeDentistServiceNameCommandValidator : AbstractValidator<ChangeDentistServiceName>
{
    public ChangeDentistServiceNameCommandValidator()
    {
        RuleFor(x => x.DentistIndustryId).NotNull().WithMessage("Dentist Industry Id is required");
        RuleFor(x => x.Name).NotNull().WithMessage("Service Name is required");
        RuleFor(x => x.Name).MinimumLength(3);
    }
}