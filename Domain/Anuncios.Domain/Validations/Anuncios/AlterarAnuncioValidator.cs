using Anuncios.Domain.Commands;
using Anuncios.Domain.Validations.Veiculos;
using FluentValidation;
using SharedKernel.Domain.Validations;

namespace Anuncios.Domain.Validations.Anuncios
{
    public class AlterarAnuncioValidator : AbstractValidator<AlterarAnuncioCommand>
    {
        public AlterarAnuncioValidator()
        {
            RuleFor(c => c.Id)
                .SetValidator(c => new IdValidator());

            RuleFor(c => c.Marca)
                .SetValidator(c => new MarcaValidator());

            RuleFor(c => c.Modelo)
                .SetValidator(c => new ModeloValidator());

            RuleFor(c => c.Versao)
                .SetValidator(c => new VersaoValidator());

            RuleFor(c => c.Ano)
               .SetValidator(c => new AnoValidator());

            RuleFor(c => c.Quilometragem)
               .SetValidator(c => new QuilometragemValidator());

            RuleFor(c => c.Observacao)
            .SetValidator(c => new ObservacaoAnuncioValidator());
        }
    }
}
