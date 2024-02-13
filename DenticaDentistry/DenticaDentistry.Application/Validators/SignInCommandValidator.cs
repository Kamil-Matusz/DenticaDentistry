using DenticaDentistry.Application.Commands;
using FluentValidation;

namespace DenticaDentistry.Application.Validators;

public class SignInCommandValidator : AbstractValidator<SignIn>
{
    public SignInCommandValidator()
    {
        RuleFor(x => x.Email).NotNull().EmailAddress().WithMessage("Email Address is required");
        RuleFor(x => x.Password).NotNull().WithMessage("Password is required");
    }
}