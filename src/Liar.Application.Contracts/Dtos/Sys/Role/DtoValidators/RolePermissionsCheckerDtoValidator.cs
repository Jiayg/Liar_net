using System.Linq;
using FluentValidation;

namespace Liar.Application.Contracts.Dtos.Sys.Role.DtoValidators
{
    public class RolePermissionsCheckerDtoValidator : AbstractValidator<RolePermissionsCheckerDto>
    {
        public RolePermissionsCheckerDtoValidator()
        {
            RuleFor(x => x.RoleIds).NotNull().Must(x => x.Count() > 0);
            RuleFor(x => x.Permissions).NotNull().Must(x => x.Count() > 0);
        }
    }
}
