using Anuncios.Domain.Entities;
using Anuncios.Domain.Repositories.Write;
using Anuncios.Infrastructure.SqlServer.Write.DbContexts;
using System.Linq;
using System.Threading.Tasks;

namespace Anuncios.Infrastructure.SqlServer.Write.Repositories
{
    public class AnuncioRepository : Repository<Anuncio> , IAnuncioRepository
    {
        public AnuncioRepository(AnunciosDbContext bancoAnuncios) : base(bancoAnuncios)
        {
        }

        public bool Existe(string marca, string modelo, string versao, int ano)
        {
            return Db.Anuncios.Where(c => c.Marca.ToLower() == marca.ToLower() &&
                                          c.Modelo.ToLower() == modelo.ToLower() &&
                                          c.Versao.ToLower() == versao.ToLower() &&
                                          c.Ano == ano).Any();
        }

        public bool Existe(string marca, string modelo, string versao,int ano, int id)
        {
            return Db.Anuncios.Where(c => c.Marca.ToLower() == marca.ToLower() &&
                                          c.Modelo.ToLower() == modelo.ToLower() &&
                                          c.Versao.ToLower() == versao.ToLower() &&
                                          c.Ano == ano &&
                                          c.Id != id).Any();
        }
    }
}
