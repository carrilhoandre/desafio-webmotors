using Anuncios.Domain.Validations.Veiculos;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace Anuncios.Tests.Domain.Validations.Veiculos
{
    public class ModeloValidatorTests 
    {
        ModeloValidator _validator = null;
        public ModeloValidatorTests()
        {
            _validator = new ModeloValidator();
        }

        [Fact]
        public void Sut_Validator()
        {
            _validator
                .Should()
                .BeAssignableTo<AbstractValidator<string>>();
        }

        [Fact]
        public void Ao_Informar_Um_Modelo_Nulo_O_Resultado_Deve_Ser_Falso()
        {
            string marcaNula = null;
            var result = _validator.Validate(marcaNula);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Ao_Informar_Um_Modelo_Vazio_O_Resultado_Deve_Ser_Falso()
        {
            var result = _validator.Validate("");
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Ao_Informar_Um_Modelo_Maior_Que_46_Caracteres_O_Resultado_Deve_Ser_Falso()
        {
            var result = _validator.Validate(new string('A', 46));
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Ao_Informar_Um_Modelo_Valido_O_Resultado_Deve_Ser_Verdadeiro()
        {
            var result = _validator.Validate("Ford");
            result.IsValid.Should().BeTrue();
        }
    }
}
