using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anuncios.Domain.Validations.Veiculos
{
    public class AnoValidator : AbstractValidator<int>
    {
        public AnoValidator()
        {
            RuleFor(c => c)
                .GreaterThan(1900)
               .WithMessage("Informe um ano maior que 1900")
               .LessThanOrEqualTo(DateTime.Now.Year)
               .WithMessage($"Informe um ano maior que {DateTime.Now.Year}");
        }
    }
}
