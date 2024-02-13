using DenticaDentistry.Application.Commands;
using FluentValidation;

namespace DenticaDentistry.Application.Validators;

public class DeleteReservationCommandValidator : AbstractValidator<DeleteReservation>
{
    public DeleteReservationCommandValidator()
    {
        RuleFor(x => x.ReservationId).NotNull().WithMessage("Reservation Id is required for the delete");
    }   
}