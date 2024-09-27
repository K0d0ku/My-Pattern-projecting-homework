/*Произведите корректную (правильную) по вашему мнению реализацию с применением принципа Single-Responsibility Principle (SRP):
В этом примере класс Order отвечает за несколько вещей: хранение данных о заказе, расчет стоимости заказа с учетом скидок, обработку платежа и отправку уведомления пользователю.
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

    public void ProcessPayment(string paymentDetails)
    {
        // Логика обработки платежа
        Console.WriteLine("Payment processed using: " + paymentDetails);
    }

    public void SendConfirmationEmail(string email)
    {
        // Логика отправки уведомления
        Console.WriteLine("Confirmation email sent to: " + email);
    }
}

Проблемы:
•	Класс Order нарушает принцип SRP, так как он отвечает за несколько вещей: расчет цены, обработку платежа и отправку уведомлений. Это усложняет код и делает его менее гибким.
В этом примере Вам необходимо разделить ответственность между несколькими классами. Класс Order должен отвечает только за хранение данных о заказе. Другие задачи, такие как расчет цены, обработка платежа и отправка уведомлений, должны быть делегированы другим классам.
*/
