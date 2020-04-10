using Anuncios.Domain.Commands;
using Anuncios.Domain.Validations.Anuncios;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace Anuncios.Tests.Domain.Validations.Anuncios
{
    public class AlterarAnuncioValidatorTests 
    {
        AlterarAnuncioValidator _validator = null;
        AlterarAnuncioCommand command = new AlterarAnuncioCommand()
        {
           Marca = "Ford",
           Modelo = "Fussion",
           Versao = "1.5 LX 16V FLEX 4P MANUAL",
           Ano = 2019,
           Quilometragem = 10000,
           Id = 1,
           Observacao = "Observação"
        };
        public AlterarAnuncioValidatorTests()
        {
            _validator = new AlterarAnuncioValidator();
        }

        [Fact]
        public void Sut_Validator()
        {
            _validator
                .Should()
                .BeAssignableTo<AbstractValidator<AlterarAnuncioCommand>>();
        }

        [Fact]
        public void Ao_Informar_Um_Comando_Invalido_O_Resultado_Da_Validacao_Deve_Ser_Falso()
        {
            command.Marca = "";
            var result = _validator.Validate(command);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Ao_Informar_Um_Comando_Valido_O_Resultado_Da_Validacao_Deve_Ser_Verdadeiro()
        {
            var result = _validator.Validate(command);
            
            result.IsValid.Should().BeTrue();
        }
    }
}
