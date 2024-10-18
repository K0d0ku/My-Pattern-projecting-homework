using System;
public class program
{   /*FORGOT TO UPLOAD , my apologies*/
    public static void Main(string[] args)
    {
        Order order = new Order();
        order.AddProduct("bike", 4200, 1);
        order.AddProduct("helmet", 50, 2);

        IPayment payment = new CreditCardPayment();
        payment.ProcessPayment(order.CalculateTotalCost());
        IDelivery delivery = new CourierDelivery();
        delivery.DeliverOrder(order);
        INotification notification = new EmailNotification();
        notification.SendNotification("your order has been placed.");
    }
}
public class Order
{
    private List<Product> products = new List<Product>();
    public void AddProduct(string name, double price, int quantity)
    {
        products.Add(new Product(name, price, quantity));
    }
    public double CalculateTotalCost()
    {
        double total = 0;
        foreach (var product in products)
        {
            total += product.Price * product.Quantity;
        }
        DiscountCalculator discountCalculator = new DiscountCalculator();
        return discountCalculator.CalculateDiscount(total);
    }
}
public class Product /*for SRP*/
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public Product(string name, double price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }
}
public interface IPayment
{
    public void ProcessPayment(double amount);
}
public class CreditCardPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"paid with Credit Card: {amount:C}.");
    }
}
public class PayPalPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"paid with PayPal: {amount:C}.");
    }
}
public class BankTransferPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"paid thru Bank Transfer: {amount:C}.");
    }
}

public interface IDelivery
{
    public void DeliverOrder(Order order);
}
public class CourierDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine("delivering order by Courier.");
    }
}
public class PostDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine("delivering order by Post.");
    }
}
public class PickUpPointDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine("delivering order to Pick-Up Point.");
    }
}

public interface INotification
{
    public void SendNotification(string message);
}
public class EmailNotification : INotification
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"dending email notification: {message}");
    }
}
public class SmsNotification : INotification
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"dending SMS notification: {message}");
    }
}
public class DiscountCalculator
{
    public double CalculateDiscount(double totalAmount)
    {
        if (totalAmount > 1000)
        {
            return totalAmount * 0.9;
        }
        return totalAmount;
    }
}
