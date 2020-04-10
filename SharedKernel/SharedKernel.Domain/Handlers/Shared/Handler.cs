using SharedKernel.Domain.Notifications;
using SharedKernel.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedKernel.Domain.Handlers.Shared
{
    public class Handler
    {
        private readonly List<Notification> _notifications;
        private readonly IUnitOfWork _uow;

        public Handler(IUnitOfWork uow)
        {
            _uow = uow;
            _notifications = new List<Notification>();
        }

        public IReadOnlyCollection<Notification> Notifications => _notifications;

        public void AddNotification(string property, string message)
        {
            _notifications.Add(new Notification(property, message));
        }

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotifications(IReadOnlyCollection<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(IList<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(ICollection<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public bool Invalid => _notifications.Any();
        public bool Valid => !Invalid;

        public async Task<bool> Commit()
        {
            if (Invalid) return false;
            if (await _uow.Commit()) return true;

            AddNotification("Error", "Falha ao salvar as informações");
            return false;
        }

        
    }
}
