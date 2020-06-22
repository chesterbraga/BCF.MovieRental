using System.Collections.Generic;
using System.Linq;
using BCF.MovieRental.Business.Interfaces;
using BCF.MovieRental.Business.Enums;

namespace BCF.MovieRental.Business.Notifications
{
    public class Notifier : INotifier
    {
        private readonly List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public IEnumerable<Notification> GetNotifications()
        {
            return _notifications;
        }

        public bool HasErrors()
        {
            return _notifications.Where(p => p.Type == MessageType.Error).Any();
        }
    }
}