using Anuncios.Domain.Validations.Anuncios;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace Anuncios.Tests.Domain.Validations.Anuncios
{
    public class ObservacaoAnuncioValidatorTests 
    {
        ObservacaoAnuncioValidator _validator = null;
        public ObservacaoAnuncioValidatorTests()
        {
            _validator = new ObservacaoAnuncioValidator();
        }

        [Fact]
        public void Sut_Validator()
        {
            _validator
                .Should()
                .BeAssignableTo<AbstractValidator<string>>();
        }

        [Fact]
        public void Ao_Informar_Uma_Descricao_Nula_O_Resultado_Deve_Ser_Falso()
        {
            string descricaoNula = null;
            var result = _validator.Validate(descricaoNula);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Ao_Informar_Uma_Descricao_Vazia_O_Resultado_Deve_Ser_Falso()
        {
            var result = _validator.Validate("");
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Ao_Informar_Uma_Descricao_Maior_Que_1000_Caracteres_O_Resultado_Deve_Ser_Falso()
        {
            var result = _validator.Validate(new string('A', 1001));
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Ao_Informar_Uma_Descricao_Valida_O_Resultado_Deve_Ser_Verdadeiro()
        {
            var result = _validator.Validate("Observação");
            result.IsValid.Should().BeTrue();
        }
    }
}
