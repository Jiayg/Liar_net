using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Liar.Application.Contracts.Inputs
{
    /// <summary>
    /// 集成 Volo.Abp.FluentValidation
    /// </summary>
    public class CreateBookDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

    }

    public class CreateUpdateBookDtoValidator : AbstractValidator<CreateBookDto>
    {
        public CreateUpdateBookDtoValidator()
        {
            RuleFor(x => x.Name).Length(3, 10);
        }
    }

}
