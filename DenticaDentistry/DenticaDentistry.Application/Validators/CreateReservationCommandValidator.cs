using DenticaDentistry.Application.Commands;
using FluentValidation;

namespace DenticaDentistry.Application.Validators;

public class CreateReservationCommandValidator : AbstractValidator<CreateReservation>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(x => x.DentistIndustryId).NotNull().WithMessage("Dentist Industry Id is required");
        RuleFor(x => x.BookerName).NotNull().WithMessage("Booker Name is required");
        RuleFor(x => x.BookerName).MinimumLength(3).MaximumLength(500);
        RuleFor(x => x.ReservationDate).NotNull().WithMessage("Reservation Date is required");
        RuleFor(x => x.DentistId).NotNull().WithMessage("Dentist Id is required");
    }
}