using FluentValidation;

namespace Liar.Application.Contracts.Dtos.Sys.Role.DtoValidators
{
    public class RoleUpdationDtoValidator : AbstractValidator<RoleUpdationDto>
    {
        public RoleUpdationDtoValidator()
        {
            Include(new RoleCreationDtoValidator());
        }
    }
}
