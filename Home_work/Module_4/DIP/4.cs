using System;
public interface INotificationsService
{
    public void SendNotification(string message);
}
public class EmailSender : INotificationsService
{
    public void SendNotification(string message)
    {
        Console.WriteLine("Email sent: " + message);
    }
}
public class SmsSender : INotificationsService
{
    public void SendNotification(string message)
    {
        Console.WriteLine("SMS sent: " + message);
    }
}
public class NotificationService
{
    private readonly IEnumerable<INotificationsService> _notificationServices;
    /*the IEnumerable is for injecting multiple implementations*/
    public NotificationService(IEnumerable<INotificationsService> notificationServices)
    {
        _notificationServices = notificationServices;
    }
    public void SendNotification(string message)
    {
        foreach (var service in _notificationServices) /*foreach in other hand to iterate over every item*/
        {
            service.SendNotification(message);
        }
    }
}
