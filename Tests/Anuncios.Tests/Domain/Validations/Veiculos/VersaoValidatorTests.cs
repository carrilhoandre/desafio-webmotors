using Anuncios.Domain.Validations.Veiculos;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace Anuncios.Tests.Domain.Validations.Veiculos
{
    public class VersaoValidatorTests : AbstractValidator<string>
    {
        VersaoValidator _validator = null;
        public VersaoValidatorTests()
        {
            _validator = new VersaoValidator();
        }

        [Fact]
        public void Sut_Validator()
        {
            _validator
                .Should()
                .BeAssignableTo<AbstractValidator<string>>();
        }

        [Fact]
        public void Ao_Informar_Uma_Versao_Nula_O_Resultado_Deve_Ser_Falso()
        {
            string marcaNula = null;
            var result = _validator.Validate(marcaNula);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Ao_Informar_Uma_Versao_Vazia_O_Resultado_Deve_Ser_Falso()
        {
            var result = _validator.Validate("");
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Ao_Informar_Uma_Versao_Maior_Que_60_Caracteres_O_Resultado_Deve_Ser_Falso()
        {
            var result = _validator.Validate(new string('A', 61));
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Ao_Informar_Uma_Versao_Valida_O_Resultado_Deve_Ser_Verdadeiro()
        {
            var result = _validator.Validate("1.5 LX 16V FLEX 4P MANUAL");
            result.IsValid.Should().BeTrue();
        }
    }
}
