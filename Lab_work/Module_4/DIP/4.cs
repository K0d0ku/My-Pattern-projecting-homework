/*Произведите корректную (правильную) по вашему мнению реализацию с применением принципа Dependency-Inversion Principle, DIP:
Рассмотрим пример, где класс Notification зависит от конкретной реализации класса EmailService:
public class EmailService
{
    public void SendEmail(string message)
    {
        Console.WriteLine($"Отправка Email: {message}");
    }
}

public class Notification
{
    private EmailService _emailService;

    public Notification()
    {
        _emailService = new EmailService();
    }

    public void Send(string message)
    {
        _emailService.SendEmail(message);
    }
}

В этом примере класс Notification жестко связан с конкретной реализацией EmailService. Если в будущем нужно будет изменить способ отправки уведомлений (например, добавить SMS или push-уведомления), придется изменять класс Notification, что нарушает DIP.
Чтобы соблюдать DIP, вам необходимо использовать абстракцию в виде интерфейса для отделения высокоуровневого модуля от низкоуровневого.*/
/*мой ответ*/
using System;
class Program
{
    static void Main(string[] args)
    {
        INotificationService emailService = new EmailService();
        Notification notification = new Notification(emailService);
        notification.Send("Привет это был тестовым email!");
    }
}
public interface INotificationService
{
    void Send(string message);
}
public class EmailService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"Отправка Email: {message}");
    }
}
public class Notification
{
    private readonly INotificationService _notificationService;
    public Notification(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
    public void Send(string message)
    {
        _notificationService.Send(message);
    }
}
