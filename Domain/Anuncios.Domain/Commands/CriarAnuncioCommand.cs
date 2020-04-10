using Anuncios.Domain.Validations.Anuncios;
using SharedKernel.Domain.Commands;

namespace Anuncios.Domain.Commands
{
    public class CriarAnuncioCommand : Command, ICommand
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Versao { get; set; }
        public int Ano { get; set; }
        public int Quilometragem { get; set; }
        public string Observacao { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new CriarAnuncioValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
