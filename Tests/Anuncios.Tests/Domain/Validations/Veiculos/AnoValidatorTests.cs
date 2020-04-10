using Anuncios.Domain.Validations.Veiculos;
using FluentAssertions;
using FluentValidation;
using System;
using Xunit;

namespace Anuncios.Tests.Domain.Validations.Veiculos
{
    public class AnoValidatorTests 
    {
        AnoValidator _validator = null;
        public AnoValidatorTests()
        {
            _validator = new AnoValidator();
        }

        [Fact]
        public void Sut_Validator()
        {
            _validator
                .Should()
                .BeAssignableTo<AbstractValidator<int>>();
        }

        [Fact]
        public void Ao_Informar_Um_Ano_Zerado_O_Resultado_Deve_Ser_Falso()
        {
            var result = _validator.Validate(0);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Ao_Informar_Um_Ano_Maior_Que_O_Atual_Resultado_Deve_Ser_Falso()
        {
            var ano = DateTime.Now.Year + 1;
            var result = _validator.Validate(ano);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Ao_Informar_Um_Ano_Menor_Que_1900_O_Atual_Resultado_Deve_Ser_Falso()
        {
            var result = _validator.Validate(1899);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Ao_Informar_Um_Ano_Valido_O_Resultado_Deve_Ser_Verdadeiro()
        {
            var result = _validator.Validate(DateTime.Now.Year);
            result.IsValid.Should().BeTrue();
        }
    }
}
