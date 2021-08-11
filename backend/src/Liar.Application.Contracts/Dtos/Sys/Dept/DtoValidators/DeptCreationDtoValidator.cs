using FluentValidation;

namespace Liar.Application.Contracts.Dtos.Sys.Dept.DtoValidators
{
    public class DeptCreationDtoValidator : AbstractValidator<DeptCreationDto>
    {
        public DeptCreationDtoValidator()
        {
            RuleFor(x => x.SimpleName).NotEmpty().Length(2, 16);
            RuleFor(x => x.FullName).NotEmpty().Length(2, 32);
            RuleFor(x => x.Pid).GreaterThan(1).WithMessage("{PropertyName} 不能为空");
        }
    }
}
