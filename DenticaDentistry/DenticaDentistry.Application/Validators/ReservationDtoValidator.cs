using DenticaDentistry.Application.DTO;
using FluentValidation;

namespace DenticaDentistry.Application.Validators;

public class ReservationDtoValidator : AbstractValidator<ReservationDto>
{
    public ReservationDtoValidator()
    {
        RuleFor(dto => dto.DentistIndustryId).NotEmpty().WithMessage("Dentist Industry Id is required");
        RuleFor(dto => dto.BookerName).NotEmpty().WithMessage("Booker Name is required");
        RuleFor(dto => dto.ReservationDate).NotEmpty().WithMessage("Reservation Date is required");
    }
}