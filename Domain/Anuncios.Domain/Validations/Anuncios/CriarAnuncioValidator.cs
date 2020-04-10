using Anuncios.Domain.Commands;
using Anuncios.Domain.Validations.Veiculos;
using FluentValidation;

namespace Anuncios.Domain.Validations.Anuncios
{
    public class CriarAnuncioValidator : AbstractValidator<CriarAnuncioCommand>
    {
        public CriarAnuncioValidator()
        {
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
