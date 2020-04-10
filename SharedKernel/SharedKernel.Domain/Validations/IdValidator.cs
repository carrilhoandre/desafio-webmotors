using FluentValidation;

namespace SharedKernel.Domain.Validations
{
    public class IdValidator : AbstractValidator<int>
    {
        public IdValidator()
        {
            RuleFor(c => c)
                .NotNull()
                .WithMessage("Informe o id")
                .GreaterThan(0)
                .WithMessage("Informe o id");
        }
    }
}
