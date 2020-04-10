using FluentValidation;
using FluentValidation.Results;

namespace Anuncios.Domain.Validations.Veiculos
{
    public class MarcaValidator : AbstractValidator<string>
    {
        public MarcaValidator()
        {
            RuleFor(c=>c)
                .NotEmpty()
                .WithMessage("Informe a marca")
                .MaximumLength(45)
                .WithMessage("A marca deve conter no máximo 45 caracteres");
        }

        public override ValidationResult Validate(ValidationContext<string> context)
        {
            if (context.InstanceToValidate == null)
                context = new ValidationContext<string>("");
            return base.Validate(context);
        }
    }
}
