using DenticaDentistry.Application.Commands;
using FluentValidation;

namespace DenticaDentistry.Application.Validators;

public class AddLicenseNumberCommandValidator : AbstractValidator<AddLicenseNumber>
{
    public AddLicenseNumberCommandValidator()
    {
        RuleFor(x => x.DentistId).NotNull().WithMessage("Dentist Id is required");
        RuleFor(x => x.LicenseNumber).NotNull().WithMessage("License Number is required");
        RuleFor(x => x.LicenseNumber).MaximumLength(15);
    }
}