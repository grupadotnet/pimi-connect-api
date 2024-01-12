using FluentValidation;

namespace pimi_connect_app.Data.Models.Validators;

public class UserDtoValidator: AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(u => u.UserName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(255);
    }
}