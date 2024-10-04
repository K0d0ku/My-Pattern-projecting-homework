using System;
public class Order
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }

    public double CalculateTotalPrice()
    {
        // Рассчет стоимости с учетом скидок
        return Quantity * Price * 0.9;
    }
}
public class PaymentProccessing
{
    public void ProcessPayment(string paymentDetails)
    {
        // Логика обработки платежа
        Console.WriteLine("Payment processed using: " + paymentDetails);
    }
}
public class NotificationEmail
{
    public void SendConfirmationEmail(string email)
    {
        // Логика отправки уведомления
        Console.WriteLine("Confirmation email sent to: " + email);
    }
}
