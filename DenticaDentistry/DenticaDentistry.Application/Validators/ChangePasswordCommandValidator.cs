using DenticaDentistry.Application.Commands;
using FluentValidation;

namespace DenticaDentistry.Application.Validators;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePassword>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.UserId).NotNull().WithMessage("User Id is required");
        RuleFor(x => x.NewPassword).NotNull().WithMessage("New Password is required");
    }
}