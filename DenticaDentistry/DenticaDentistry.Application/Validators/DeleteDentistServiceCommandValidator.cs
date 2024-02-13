using DenticaDentistry.Application.Commands;
using FluentValidation;

namespace DenticaDentistry.Application.Validators;

public class DeleteDentistServiceCommandValidator : AbstractValidator<DeleteDentistService>
{
    public DeleteDentistServiceCommandValidator()
    {
        RuleFor(x => x.DentistIndustryId).NotNull().WithMessage("Dentist Industry Id is required for the delete");
    }
}