using Anuncios.Domain.Commands;
using Anuncios.Domain.Entities;
using Anuncios.Domain.Handlers;
using Anuncios.Domain.Repositories.Write;
using Bogus;
using FluentAssertions;
using Moq;
using SharedKernel.Domain.Commands;
using SharedKernel.Domain.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace Anuncios.Tests.Domain.Handlers
{
    public class AnuncioHandlerTests
    {
        private Mock<IUnitOfWork> _uow;
        private Mock<IAnuncioRepository> _anuncioRepository;
        private CriarAnuncioCommand _criarAnuncioCommand;
        private AlterarAnuncioCommand _alterarAnuncioCommand;
        private ExcluirCommand _excluirCommand;
        public AnuncioHandlerTests()
        {
            _uow = new Mock<IUnitOfWork>();
            _anuncioRepository = new Mock<IAnuncioRepository>();
            SetupMocks();
            
            
        }

        public void SetupMocks()
        {
            _anuncioRepository.Setup(c => c.ObterPeloIdAsync(It.IsAny<int>())).ReturnsAsync(It.IsAny<Anuncio>());
            _anuncioRepository.Setup(c => c.Existe(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            _anuncioRepository.Setup(c => c.Existe(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(false);
            _uow.Setup(c => c.Commit()).ReturnsAsync(true);
            _criarAnuncioCommand = new Faker<CriarAnuncioCommand>("pt_BR")
                                 .RuleFor(c => c.Marca, f => f.Random.String())
                                 .RuleFor(c => c.Modelo, f => f.Random.String())
                                 .RuleFor(c => c.Versao, f => f.Random.String())
                                 .RuleFor(c => c.Ano, f => 2019)
                                 .RuleFor(c => c.Quilometragem, f => 10000)
                                 .RuleFor(c => c.Observacao, f => f.Random.String())
                                 .Generate();
            _alterarAnuncioCommand = new Faker<AlterarAnuncioCommand>("pt_BR")
                                .RuleFor(c => c.Id, f => 1)
                                .RuleFor(c => c.Marca, f => f.Random.String())
                                .RuleFor(c => c.Modelo, f => f.Random.String())
                                .RuleFor(c => c.Versao, f => f.Random.String())
                                .RuleFor(c => c.Ano, f => 2019)
                                .RuleFor(c => c.Quilometragem, f => 10000)
                                .RuleFor(c => c.Observacao, f => f.Random.String())
                                .Generate();
        }

        [Fact]
        public async Task Ao_Criar_Anuncio_Com_Um_Comando_Invalido_O_Resultado_Deve_Ser_Falso()
        {
            
            var handler = new AnuncioHandler(_uow.Object, _anuncioRepository.Object);
            var result = await handler.Handle<CriarAnuncioCommand>(_criarAnuncioCommand);
            result.Should().BeNull();
            handler.Valid.Should().BeFalse();
        }

        [Fact]
        public async Task Ao_Criar_Anuncio_Ja_Existente_O_Resultado_Deve_Ser_Falso()
        {
            var anuncioRepository = new Mock<IAnuncioRepository>();
            anuncioRepository.Setup(c => c.Existe(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(true);
            var handler = new AnuncioHandler(_uow.Object, _anuncioRepository.Object);
            var result = await handler.Handle<CriarAnuncioCommand>(_criarAnuncioCommand);
            result.Should().BeNull();
            handler.Valid.Should().BeFalse();
        }

        [Fact]
        public async Task Ao_Criar_Anuncio_Com_Erro_No_Commit_O_Resultado_Deve_Ser_Falso()
        {
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(c => c.Commit()).ReturnsAsync(false);
            var handler = new AnuncioHandler(uow.Object, _anuncioRepository.Object);
            var result = await handler.Handle<CriarAnuncioCommand>(_criarAnuncioCommand);
            result.Should().BeNull();
            handler.Valid.Should().BeFalse();
        }

        [Fact]
        public async Task Ao_Alterar_Anuncio_Com_Um_Comando_Invalido_O_Resultado_Deve_Ser_Falso()
        {

            var handler = new AnuncioHandler(_uow.Object, _anuncioRepository.Object);
            var result = await handler.Handle<AlterarAnuncioCommand>(_alterarAnuncioCommand);
            result.Should().BeNull();
            handler.Valid.Should().BeFalse();
        }

        [Fact]
        public async Task Ao_Alterar_Anuncio_Com_Anuncio_Nao_Encontrado_O_Resultado_Deve_Ser_Falso()
        {
            var anuncioRepository = new Mock<IAnuncioRepository>();
            Anuncio anuncio = null;
            anuncioRepository.Setup(c => c.ObterPeloIdAsync(It.IsAny<int>())).ReturnsAsync(anuncio);
            var handler = new AnuncioHandler(_uow.Object, anuncioRepository.Object);
            var result = await handler.Handle<AlterarAnuncioCommand>(_criarAnuncioCommand);
            result.Should().BeNull();
            handler.Valid.Should().BeFalse();
        }

        [Fact]
        public async Task Ao_Alterar_Anuncio_Ja_Existente_O_Resultado_Deve_Ser_Falso()
        {
            var anuncioRepository = new Mock<IAnuncioRepository>();
            anuncioRepository.Setup(c => c.Existe(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            var handler = new AnuncioHandler(_uow.Object, anuncioRepository.Object);
            var result = await handler.Handle<AlterarAnuncioCommand>(_alterarAnuncioCommand);
            result.Should().BeNull();
            handler.Valid.Should().BeFalse();
        }

        [Fact]
        public async Task Ao_Alterar_Anuncio_Com_Erro_No_Commit_O_Resultado_Deve_Ser_Falso()
        {
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(c => c.Commit()).ReturnsAsync(false);
            var handler = new AnuncioHandler(uow.Object, _anuncioRepository.Object);
            var result = await handler.Handle<AlterarAnuncioCommand>(_criarAnuncioCommand);
            result.Should().BeNull();
            handler.Valid.Should().BeFalse();
        }
    }
}
