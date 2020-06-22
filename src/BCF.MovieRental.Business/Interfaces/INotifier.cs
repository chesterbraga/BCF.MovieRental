using System.Collections.Generic;
using BCF.MovieRental.Business.Notifications;

namespace BCF.MovieRental.Business.Interfaces
{
    public interface INotifier
    {
        bool HasErrors();
        IEnumerable<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}