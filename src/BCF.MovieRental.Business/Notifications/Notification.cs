using BCF.MovieRental.Business.Enums;

namespace BCF.MovieRental.Business.Notifications
{
    public class Notification
    {
        public Notification(string message, MessageType type = MessageType.Error)
        {
            Message = message;
            Type = type;
        }

        public string Message { get; }
        public MessageType Type { get; }
    }
}