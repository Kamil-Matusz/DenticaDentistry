using DenticaDentistry.Application.Commands;
using FluentValidation;

namespace DenticaDentistry.Application.Validators;

public class SignUpCommandValidator : AbstractValidator<SignUp>
{
    public SignUpCommandValidator()
    {
        RuleFor(x => x.Email).NotNull().EmailAddress().WithMessage("Email Address is required");
        RuleFor(x => x.Username).NotNull().WithMessage("Username is required");
        RuleFor(x => x.Fullname).NotNull().WithMessage("Fullname is required");
        RuleFor(x => x.PhoneNumber).NotNull().WithMessage("Phone Number is required");
    }
}