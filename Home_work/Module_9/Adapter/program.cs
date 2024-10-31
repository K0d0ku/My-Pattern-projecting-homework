/*output:
processing payment 100 via PayPal.

refunding payment 50 via PayPal.

making transaction 200 via Stripe.

refunding transaction 100 via Stripe.

executing payment 300 via External Payment Service.

executing refund 150 via External Payment Service.*/

using System;
/*Adapter*/
public class program
{
    public static void Main(string[] args)
    {
        IPaymentProcessor paypalProcessor = new PayPalPaymentProcessor();
        paypalProcessor.ProcessPayment(100.0);
        paypalProcessor.RefundPayment(50.0);

        StripePaymentService stripeService = new StripePaymentService();
        IPaymentProcessor stripeProcessor = new StripePaymentAdapter(stripeService);
        stripeProcessor.ProcessPayment(200.0);
        stripeProcessor.RefundPayment(100.0);

        ExternalPaymentService externalService = new ExternalPaymentService();
        IPaymentProcessor externalProcessor = new ExternalPaymentAdapter(externalService);
        externalProcessor.ProcessPayment(300.0);
        externalProcessor.RefundPayment(150.0);
    }
}

public interface IPaymentProcessor
{
    void ProcessPayment(double amount);
    void RefundPayment(double amount);
}
public class PayPalPaymentProcessor : IPaymentProcessor
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"processing payment {amount} via PayPal.\n");
    }
    public void RefundPayment(double amount)
    {
        Console.WriteLine($"refunding payment {amount} via PayPal.\n");
    }
}
public class StripePaymentService
{
    public void MakeTransaction(double totalAmount)
    {
        Console.WriteLine($"making transaction {totalAmount} via Stripe.\n");
    }
    public void MakeRefund(double totalAmount)
    {
        Console.WriteLine($"refunding transaction {totalAmount} via Stripe.\n");
    }
}
public class StripePaymentAdapter : IPaymentProcessor
{
    private StripePaymentService _stripePaymentService;
    public StripePaymentAdapter(StripePaymentService stripePaymentService)
    {
        _stripePaymentService = stripePaymentService;
    }
    public void ProcessPayment(double amount)
    {
        _stripePaymentService.MakeTransaction(amount);
    }
    public void RefundPayment(double amount)
    {
        _stripePaymentService.MakeRefund(amount);
    }
}
public class ExternalPaymentService
{
    public void ExecutePayment(double amount)
    {
        Console.WriteLine($"executing payment {amount} via External Payment Service.\n");
    }

    public void ExecuteRefund(double amount)
    {
        Console.WriteLine($"executing refund {amount} via External Payment Service.\n");
    }
}
public class ExternalPaymentAdapter : IPaymentProcessor
{
    private ExternalPaymentService _externalPaymentService;
    public ExternalPaymentAdapter(ExternalPaymentService externalPaymentService)
    {
        _externalPaymentService = externalPaymentService;
    }
    public void ProcessPayment(double amount)
    {
        _externalPaymentService.ExecutePayment(amount);
    }
    public void RefundPayment(double amount)
    {
        _externalPaymentService.ExecuteRefund(amount);
    }
}
