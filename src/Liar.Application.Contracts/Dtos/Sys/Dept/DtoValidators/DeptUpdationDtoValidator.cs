using FluentValidation;

namespace Liar.Application.Contracts.Dtos.Sys.Dept.DtoValidators
{
    public class DeptUpdationDtoValidator : AbstractValidator<DeptUpdationDto>
    {
        public DeptUpdationDtoValidator()
        {
            Include(new DeptCreationDtoValidator());
        }
    }
}
