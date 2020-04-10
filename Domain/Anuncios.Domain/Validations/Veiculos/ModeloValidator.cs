using FluentValidation;
using FluentValidation.Results;

namespace Anuncios.Domain.Validations.Veiculos
{
    public class ModeloValidator : AbstractValidator<string>
    {
        public ModeloValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Informe o modelo")
                .MaximumLength(45)
                .WithMessage("O modelo deve conter no máximo 45 caracteres");
        }

        public override ValidationResult Validate(ValidationContext<string> context)
        {
            if (context.InstanceToValidate == null)
                context = new ValidationContext<string>("");
            return base.Validate(context);
        }
    }
}
