using Anuncios.Domain.Commands;
using Anuncios.Domain.Entities;
using Anuncios.Domain.Queries;
using Anuncios.Domain.Repositories.Write;
using SharedKernel.Domain.Commands;
using SharedKernel.Domain.Handlers.Contracts;
using SharedKernel.Domain.Handlers.Shared;
using SharedKernel.Domain.Queries;
using SharedKernel.Domain.Repositories;
using System.Threading.Tasks;

namespace Anuncios.Domain.Handlers
{
    public class AnuncioHandler : Handler,
          IHandler<CriarAnuncioCommand>,
          IHandler<AlterarAnuncioCommand>,
          IHandler<ExcluirCommand>
    {
        private readonly IAnuncioRepository _anuncioRepository;

        public AnuncioHandler(IUnitOfWork uow,
                              IAnuncioRepository anuncioRepository) : base(uow)
        {
            _anuncioRepository = anuncioRepository;
        }

        public async Task<ICommandResult> Handle<ICommand>(CriarAnuncioCommand command)
        {
            if (!command.IsValid())
            {
                AddNotifications(command.Errors);
                return null;
            }

            var existe = _anuncioRepository.Existe(command.Marca,
                                                   command.Modelo,
                                                   command.Versao,
                                                   command.Ano);
            if (existe)
            {
                AddNotification("Error", $"Anúncio já existe");
                return null;
            }

            var anuncio = CreateAnuncio(command);
            _anuncioRepository.Adicionar(anuncio);
            if (!await Commit())
                return null;

            return new CadastroQuery()
            {
                Mensagem = "Anuncio cadastrado com sucesso"
            };
        }

        public async Task<ICommandResult> Handle<ICommand>(AlterarAnuncioCommand command)
        {
            if (!command.IsValid())
            {
                AddNotifications(command.Errors);
                return null;
            }
            var anuncio = await _anuncioRepository.ObterPeloIdAsync(command.Id);
            if (anuncio == null)
            {
                AddNotification("Error", $"Anúncio não encontrado");
                return null;
            }

            var existe = _anuncioRepository.Existe(command.Marca, 
                                                   command.Modelo, 
                                                   command.Versao,
                                                   command.Ano,
                                                   command.Id);
            if (existe)
            {
                AddNotification("Error", $"Anúncio já existe");
                return null;
            }

            anuncio.Alterar(command.Marca,
                               command.Modelo,
                               command.Versao,
                               command.Ano,
                               command.Quilometragem,
                               command.Observacao);

            _anuncioRepository.Alterar(anuncio);

            if (!await Commit())
                return null;

            return new CadastroQuery()
            {
                Mensagem = "Anuncio alterado com sucesso"
            };
        }

        public async Task<ICommandResult> Handle<ICommand>(ExcluirCommand command)
        {
            if (!command.IsValid())
            {
                AddNotifications(command.Errors);
                return null;
            }

            var anuncio = await _anuncioRepository.ObterPeloIdAsync(command.Id);
            if (anuncio == null)
            {
                AddNotification("Error", $"Anúncio não encontrado");
                return null;
            }

             _anuncioRepository.Excluir(command.Id);
            if (!await Commit())
                return null;
            return new CadastroQuery();
        }


        private Anuncio CreateAnuncio(CriarAnuncioCommand command)
        {
            return new Anuncio(command.Marca,
                               command.Modelo,
                               command.Versao,
                               command.Ano,
                               command.Quilometragem,
                               command.Observacao);
        }
    }
}
