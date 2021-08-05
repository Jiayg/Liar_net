using FluentValidation;

namespace Liar.Application.Contracts.Dtos.Sys.Menu.DtoValidators
{
    public class MenuUpdationDtoValidator : AbstractValidator<MenuUpdationDto>
    {
        public MenuUpdationDtoValidator()
        {
            Include(new MenuCreationDtoValidator());
        }
    }
}
