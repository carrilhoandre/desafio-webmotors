using SharedKernel.Domain.Commands;
using SharedKernel.Domain.Notifications;
using SharedKernel.Domain.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedKernel.Domain.Handlers.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        IReadOnlyCollection<Notification> Notifications { get; }
        bool Invalid { get; }
        bool Valid { get; }
        Task<ICommandResult> Handle<ICommand>(T command);
    }
}
