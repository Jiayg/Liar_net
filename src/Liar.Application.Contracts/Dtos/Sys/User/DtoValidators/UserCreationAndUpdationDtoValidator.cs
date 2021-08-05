using FluentValidation;
using Liar.Core.DtoValidator;

namespace Liar.Application.Contracts.Dtos.Sys.User.DtoValidators
{
    public class UserCreationAndUpdationDtoValidator : AbstractValidator<UserCreationAndUpdationDto>
    {
        public UserCreationAndUpdationDtoValidator()
        {
            RuleFor(x => x.Account).NotEmpty().Matches(@"^[a-zA-Z][a-zA-Z0-9_]{4," + (15).ToString() + "}$").WithMessage("{PropertyName} 不合法,账号必须是5～16个字符,以字母开头,可包包含字母、数字、下划线。");
            RuleFor(x => x.Name).NotEmpty().Length(2, 16);
            RuleFor(x => x.Email).NotEmpty().MaximumLength(16).EmailAddress();
            RuleFor(x => x.Phone).NotEmpty().MaximumLength(11).Phone();
            RuleFor(x => x.Birthday).NotEmpty();
        }
    }
}
