using System;
public class program
{   /*i may excuse if this may sound rude or cause any misunderstanding, which i did not intended to, 
    but i have a geniuine question for you teacher,
    why was this home work was easier than practice and still worth more points to be graded ?*/
    public static void Main(string[] args)
    {
        PaymentContext context = new PaymentContext();
        /*i did the same thing i did in practice, make this inputtable with only int, 
         * cuz its faster instead of entering text*/
        Console.WriteLine("select payment method:");
        Console.WriteLine("1 - PayPal");
        Console.WriteLine("2 - Credit Card");
        Console.WriteLine("3 - Crypto");
        Console.Write("enter your choice (1-3): ");
        int choice;
        if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
        {
            Console.WriteLine("choice only from given options");
            return;
        }
        switch(choice)
        {
            case 1:
                context.SetPaymentStrategy(new PayPalPay());
                Console.WriteLine("paid with PayPal");
                break;
            case 2:
                context.SetPaymentStrategy(new CreditCardPay());
                Console.WriteLine("paid with Credit Card");
                break;
            case 3:
                context.SetPaymentStrategy(new CryptoPay());
                Console.WriteLine("paid with Crypto");
                break;
        }
        context.PaymentExec();
    }
}
public interface IPaymentStrategy
{
    public void Pay();
}
public class PayPalPay : IPaymentStrategy
{
    public void Pay() { }
}
public class CreditCardPay : IPaymentStrategy
{
    public void Pay() { }
}public class CryptoPay : IPaymentStrategy
{
    public void Pay() { }
}
public class PaymentContext
{
    private IPaymentStrategy paymentStrategy;
    public void SetPaymentStrategy(IPaymentStrategy strat)
    {
        this.paymentStrategy = strat;
    }
    public void PaymentExec()
    {
        if(paymentStrategy == null) 
        {
            throw new Exception("there is no Payment strat");
        }
        paymentStrategy.Pay();
    }
}
