using FluentValidation;

namespace Liar.Application.Contracts.Dtos.Sys.User.DtoValidators
{
    public class UserSetRoleDtoValidator : AbstractValidator<UserSetRoleDto>
    {
        public UserSetRoleDtoValidator()
        { 
            RuleFor(x => x.RoleIds).NotEmpty(); 
        }
    }
}
