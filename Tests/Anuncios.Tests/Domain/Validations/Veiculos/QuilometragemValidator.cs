using FluentValidation;

namespace Anuncios.Tests.Domain.Validations.Veiculos
{
    public class QuilometragemValidator : AbstractValidator<int>
    {
        public QuilometragemValidator()
        {
            RuleFor(c => c)
                .GreaterThan(0)
               .WithMessage("Informe uma quilometragem maior que 0")
               .LessThan(1000000)
               .WithMessage($"Informe uma quilometragem menor que 1000000");
        }
    }
}
