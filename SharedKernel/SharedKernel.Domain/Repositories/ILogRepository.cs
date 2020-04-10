using SharedKernel.Domain.Commands.Logs;
using System.Threading.Tasks;

namespace SharedKernel.Domain.Repositories
{
    public interface ILogRepository
    {
        Task AddLog(LogCommand command);
    }
}
