using DenticaDentistry.Application.DTO;
using FluentValidation;

namespace DenticaDentistry.Application.Validators;

public class DentistDtoValidator : AbstractValidator<DentistDto>
{
    public DentistDtoValidator()
    {
        RuleFor(dto => dto.Fullname).NotEmpty().WithMessage("Full Name is required");
        RuleFor(dto => dto.Email).NotEmpty().EmailAddress().WithMessage("Email Address is required");
        RuleFor(dto => dto.PhoneNumber).NotEmpty().WithMessage("Phone Number is required");
    }
}