using FluentValidation;
using FluentValidation.Results;

namespace Anuncios.Domain.Validations.Veiculos
{
    public class VersaoValidator : AbstractValidator<string>
    {
        public VersaoValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Informe a versão")
                .MaximumLength(60)
                .WithMessage("A versão deve conter no máximo 60 caracteres");
        }

        public override ValidationResult Validate(ValidationContext<string> context)
        {
            if (context.InstanceToValidate == null)
                context = new ValidationContext<string>("");
            return base.Validate(context);
        }
    }
}
