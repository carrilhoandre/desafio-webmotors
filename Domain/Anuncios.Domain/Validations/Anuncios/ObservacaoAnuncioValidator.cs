using FluentValidation;
using FluentValidation.Results;

namespace Anuncios.Domain.Validations.Anuncios
{
    public class ObservacaoAnuncioValidator : AbstractValidator<string>
    {
        public ObservacaoAnuncioValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Informe a Observação")
                .MaximumLength(1000)
                .WithMessage("A observação deve conter no máximo 1000 caracteres");
        }

        public override ValidationResult Validate(ValidationContext<string> context)
        {
            if (context.InstanceToValidate == null)
                context = new ValidationContext<string>("");
            return base.Validate(context);
        }
    }
}
