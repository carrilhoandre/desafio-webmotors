using FluentValidation;
using SharedKernel.Domain.Commands;

namespace SharedKernel.Domain.Validations
{
    public class ExcluirCommandValidator : AbstractValidator<ExcluirCommand>
    {
        public ExcluirCommandValidator()
        {
            RuleFor(c => c.Id).SetValidator(c => new IdValidator());
        }
    }
}
