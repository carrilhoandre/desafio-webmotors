using Anuncios.Domain.Commands;
using Anuncios.Domain.Queries;
using SharedKernel.Domain.Queries;
using System;
using System.Threading.Tasks;

namespace Anuncios.Domain.Repositories.Read
{
    public interface IAnuncioRepository
    {
        Task<PaginatedResult<AnuncioQuery>> ConsultarAsync(ConsultarCommand command);
        Task<AnuncioQuery> ObterPeloIdAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}
