using FluentValidation;
using Shop.Application.DTOs.Auth;

namespace Shop.Application.Validators.Auth
{
    public class RegisterValidator
        : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}