using System;
using System.Threading.Tasks;

namespace SharedKernel.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
