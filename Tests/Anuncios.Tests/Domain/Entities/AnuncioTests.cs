using Anuncios.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Anuncios.Tests.Domain.Entities
{
    public class AnuncioTests
    {
        public AnuncioTests()
        {

        }

        [Fact]
        public void Ao_Alterar_Um_Anuncio_Os_Campos_Devem_Estar_Identicos()
        {
            string marca = "Ford", 
                   modelo= "Fussion",
                   versao = "1.5 LX 16V FLEX 4P MANUAL",
                   observacao = "Observação";
            int ano = 2019, quilometragem = 10000;
            var anuncio = new Anuncio(marca, modelo, versao, ano, quilometragem, observacao);
            anuncio.Should().NotBeNull();
            anuncio.Marca.Should().Be(marca);
            anuncio.Modelo.Should().Be(modelo);
            anuncio.Versao.Should().Be(versao);
            anuncio.Ano.Should().Be(ano);
            anuncio.Quilometragem.Should().Be(quilometragem);
            anuncio.Observacao.Should().Be(observacao);
        }

        [Fact]
        public void Ao_Criar_Um_Anuncio_Os_Campos_Devem_Estar_Preenchidos()
        {
            var anuncio = new Anuncio("Ford", "Fussion", "1.5 LX 16V FLEX 4P MANUAL", 2019, 10000, "Observação");
            anuncio.Should().NotBeNull();
            anuncio.Marca.Should().NotBeNullOrEmpty();
            anuncio.Modelo.Should().NotBeNullOrEmpty();
            anuncio.Versao.Should().NotBeNullOrEmpty();
            anuncio.Ano.Should().BeGreaterThan(0);
            anuncio.Quilometragem.Should().BeGreaterThan(0);
            anuncio.Observacao.Should().NotBeNullOrEmpty();
        }

    }
}
