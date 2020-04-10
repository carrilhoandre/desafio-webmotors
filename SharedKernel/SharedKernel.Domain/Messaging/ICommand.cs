using SharedKernel.Domain.Messaging;
using SharedKernel.Domain.Notifications;
using System.Collections.Generic;

namespace SharedKernel.Domain.Commands
{
    public interface ICommand : IMessage
    {
        IReadOnlyCollection<Notification> Errors { get; }
        bool IsValid();
    }
}
