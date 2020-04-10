using SharedKernel.Domain.Repositories;
using System.Threading.Tasks;

namespace Anuncios.Infrastructure.SqlServer.Write.DbContexts
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AnunciosDbContext _context;

        public UnitOfWork(AnunciosDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
