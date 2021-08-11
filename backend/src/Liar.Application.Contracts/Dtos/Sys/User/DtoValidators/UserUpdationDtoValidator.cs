using FluentValidation;

namespace Liar.Application.Contracts.Dtos.Sys.User.DtoValidators
{
    public class UserUpdationDtoValidator : AbstractValidator<UserUpdationDto>
    {
        public UserUpdationDtoValidator()
        {
            Include(new UserCreationAndUpdationDtoValidator());
        }
    }
}
