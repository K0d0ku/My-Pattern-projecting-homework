/*output:
choose a currency for payment:
1. USD
2. EUR
3. KZT
enter your choice (1-3):
1
Processing payment of 100 via internal system.
Refunding payment of 50 via internal system.

choose a currency for payment:
1. USD
2. EUR
3. KZT
enter your choice (1-3):
2
Making payment of 100 via External Payment System A.
Making refund of 50 via External Payment System A.

choose a currency for payment:
1. USD
2. EUR
3. KZT
enter your choice (1-3):
3
Sending payment of 100 via External Payment System B.
Processing refund of 50 via External Payment System B.*/

using System;
/*Adapter*/
public class program
{
    public static void Main(string[] args)
    {
        /*// Используем внутреннюю платежную систему
        IPaymentProcessor internalProcessor = new InternalPaymentProcessor();
        internalProcessor.ProcessPayment(100.0);
        internalProcessor.RefundPayment(50.0);

        // Используем внешнюю платежную систему A через адаптер
        ExternalPaymentSystemA externalSystemA = new ExternalPaymentSystemA();
        IPaymentProcessor adapterA = new PaymentAdapterA(externalSystemA);
        adapterA.ProcessPayment(200.0);
        adapterA.RefundPayment(100.0);

        // Используем внешнюю платежную систему B через адаптер
        ExternalPaymentSystemB externalSystemB = new ExternalPaymentSystemB();
        IPaymentProcessor adapterB = new PaymentAdapterB(externalSystemB);
        adapterB.ProcessPayment(300.0);
        adapterB.RefundPayment(150.0);*/

        /*my code*/
        Console.WriteLine("choose a currency for payment:");
        Console.WriteLine("1. USD");
        Console.WriteLine("2. EUR");
        Console.WriteLine("3. KZT");
        Console.WriteLine("enter your choice (1-3): ");
        string input = Console.ReadLine();
        IPaymentProcessor paymentProcessor = null;

        if (int.TryParse(input, out int choice))
        {
            switch (choice)
            {
                case 1:
                    paymentProcessor = new InternalPaymentProcessor();
                    break;
                case 2:
                    paymentProcessor = new PaymentAdapterA(new ExternalPaymentSystemA());
                    break;
                case 3:
                    paymentProcessor = new PaymentAdapterB(new ExternalPaymentSystemB());
                    break;
                default:
                    Console.WriteLine("non existing choice, restart");
                    return;
            }
        }
        else
        {
            Console.WriteLine("invalid input, enter a number.");
            return;
        }
        paymentProcessor.ProcessPayment(100.0);
        paymentProcessor.RefundPayment(50.0);
    }
}
public interface IPaymentProcessor
{
    void ProcessPayment(double amount);
    void RefundPayment(double amount);
}
public class InternalPaymentProcessor : IPaymentProcessor
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing payment of {amount} via internal system.");
    }

    public void RefundPayment(double amount)
    {
        Console.WriteLine($"Refunding payment of {amount} via internal system.");
    }
}
public class ExternalPaymentSystemA
{
    public void MakePayment(double amount)
    {
        Console.WriteLine($"Making payment of {amount} via External Payment System A.");
    }

    public void MakeRefund(double amount)
    {
        Console.WriteLine($"Making refund of {amount} via External Payment System A.");
    }
}

public class ExternalPaymentSystemB
{
    public void SendPayment(double amount)
    {
        Console.WriteLine($"Sending payment of {amount} via External Payment System B.");
    }

    public void ProcessRefund(double amount)
    {
        Console.WriteLine($"Processing refund of {amount} via External Payment System B.");
    }
}
public class PaymentAdapterA : IPaymentProcessor
{
    private ExternalPaymentSystemA _externalSystemA;

    public PaymentAdapterA(ExternalPaymentSystemA externalSystemA)
    {
        _externalSystemA = externalSystemA;
    }

    public void ProcessPayment(double amount)
    {
        _externalSystemA.MakePayment(amount);
    }

    public void RefundPayment(double amount)
    {
        _externalSystemA.MakeRefund(amount);
    }
}

public class PaymentAdapterB : IPaymentProcessor
{
    private ExternalPaymentSystemB _externalSystemB;

    public PaymentAdapterB(ExternalPaymentSystemB externalSystemB)
    {
        _externalSystemB = externalSystemB;
    }

    public void ProcessPayment(double amount)
    {
        _externalSystemB.SendPayment(amount);
    }

    public void RefundPayment(double amount)
    {
        _externalSystemB.ProcessRefund(amount);
    }
}
/*my code*/
public class PaymentProcessorFactory
{
    public static IPaymentProcessor CreateProcessor(string currency)
    {
        switch (currency)
        {
            case "USD":
                return new InternalPaymentProcessor();
            case "EUR":
                return new PaymentAdapterA(new ExternalPaymentSystemA());
            case "KZT":
                return new PaymentAdapterB(new ExternalPaymentSystemB());
            default:
                throw new ArgumentException("currency not supported.");
        }
    }
}
