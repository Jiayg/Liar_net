using FluentValidation;

namespace Liar.Application.Contracts.Dtos.Sys.User.DtoValidators
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(x => x.Account).NotEmpty().Length(5, 16);
            RuleFor(x => x.Password).NotEmpty().Length(5, 32);
        }
    }
}
