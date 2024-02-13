using DenticaDentistry.Application.Commands;
using FluentValidation;

namespace DenticaDentistry.Application.Validators;

public class ChangeDentistServicePriceCommandValidator : AbstractValidator<ChangeDentistServicePrice>
{
    public ChangeDentistServicePriceCommandValidator()
    {
        RuleFor(x => x.DentistIndustryId).NotNull().WithMessage("Dentist Industry Id is required");
        RuleFor(x => x.Price).NotNull().WithMessage("Service Price is required");
    }
}