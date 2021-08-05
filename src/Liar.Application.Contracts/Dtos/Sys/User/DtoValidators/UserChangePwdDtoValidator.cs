using FluentValidation;

namespace Liar.Application.Contracts.Dtos.Sys.User.DtoValidators
{
    public class UserChangePwdDtoValidator : AbstractValidator<UserChangePwdDto>
    {
        public UserChangePwdDtoValidator()
        {
            RuleFor(x => x.Password).NotEmpty().Length(5, 32);
            RuleFor(x => x.RePassword).NotEmpty().Length(5, 32)
                                      .Must((dto, rePassword) =>
                                      {
                                          return dto.Password == rePassword;
                                      })
                                      .WithMessage("重复密码必须跟新密码一样");
        }
    }
}
