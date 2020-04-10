using Anuncios.Domain.Entities;
using SharedKernel.Domain.Repositories;

namespace Anuncios.Domain.Repositories.Write
{
    public interface IAnuncioRepository : IRepository<Anuncio>
    {
        bool Existe(string marca, string modelo, string versao, int ano);
        bool Existe(string marca, string modelo, string versao, int ano, int id);
    }
}
