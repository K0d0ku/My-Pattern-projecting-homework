/*there is a human error in the code, i forgot that we did the service type previous week, so my blind dumba## did it the 2nd time, 
and noticed that after i finished writing the code, and in realization i make everything inputted in integers cause i did not wanted to write all of the text 
choices by text cuz im lazy*/
using System;
public class program
{
    public static void Main(string[] args)
    {
        TravelBookingContext context = new TravelBookingContext();
        Console.WriteLine("choose travellin method: ");
        Console.WriteLine("1 - plane");
        Console.WriteLine("2 - train");
        Console.WriteLine("3 - bus");
        int transportChoice = int.Parse(Console.ReadLine());

        switch (transportChoice)
        {
            case 1:
                context.ChangeCalculation(new PaymentTypePLane());
                break;
            case 2:
                context.ChangeCalculation(new PaymentTypeTrain());
                break;
            case 3:
                context.ChangeCalculation(new PaymentTypeBus());
                break;
            default:
                Console.WriteLine("non existing choice.");
                return;
        }

        Console.WriteLine("enter the number of passengers: ");
        int passengers = int.Parse(Console.ReadLine());

        Console.WriteLine("choose class: ");
        Console.WriteLine("1 - first class");
        Console.WriteLine("2 - economy class");
        int typeClass = int.Parse(Console.ReadLine());

        Console.WriteLine("do you have any luggage? ");
        Console.WriteLine("1 - yes");
        Console.WriteLine("2 - no");
        bool hasBaggage = int.Parse(Console.ReadLine()) == 1;

        Console.WriteLine("choose discount type: ");
        Console.WriteLine("1 - no discount");
        Console.WriteLine("2 - children discount");
        Console.WriteLine("3 - elder discount");
        int discountChoice = int.Parse(Console.ReadLine());

        string discount = discountChoice switch
        {
            2 => "children",
            3 => "elder",
            _ => "none"
        };

        Console.WriteLine("choose service class: ");
        Console.WriteLine("1 - economy");
        Console.WriteLine("2 - business");
        int serviceChoice = int.Parse(Console.ReadLine());

        string serviceClass = serviceChoice switch
        {
            2 => "business",
            _ => "econom"
        };

        try
        {
            double result = context.CalculateTravelCost(passengers, typeClass, hasBaggage, discount, serviceClass);
            Console.WriteLine($"total cost is: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"error claculating cost: {ex.Message}");
        }
    }
}
public interface ICostCalculationStrategy
{
    double PaymentCost(int passenger, int TypeClass, bool bag, string discount, string service);
}
public class PaymentTypePLane : ICostCalculationStrategy
{
    public double PaymentCost(int passenger, int TypeClass, bool bag, string discount, string service)
    {
        double cost = 0;
        double taxes = 0.15;
        if (TypeClass == 1)
        {
            cost = passenger * 1000.6754;
        }
        else if (TypeClass == 2 )
        {
            cost = passenger * 900.6754;
        }
        else
        {
            cost = passenger * 900.6754;
        }
        if (bag)
        {
            cost = cost + cost * 0.5;
        }

        if (service == "econom")
        {
            cost *= 1.0;
        }
        else if (service == "business")
        {
            cost *= 1.5;
        }

        if (discount == "children")
        {
            cost *= 0.8;
        }
        else if (discount == "elder")
        {
            cost *= 0.7;
        }
        else
        {
            cost *= 1;
        }
        cost += cost * taxes;
        return cost;
    }
}
public class PaymentTypeTrain : ICostCalculationStrategy
{
    public double PaymentCost(int passenger, int TypeClass, bool bag, string discount, string service)
    {
        double cost = 0;
        double taxes = 0.1;
        if (TypeClass == 1)
        {
            cost = passenger * 300.6754;
        }
        else if (TypeClass == 2)
        {
            cost = passenger * 200.6754;
        }
        else
        {
            cost = passenger * 150.6754;
        }
        if (bag)
        {
            cost = cost + 0;
        }

        if (service == "econom")
        {
            cost *= 1.0;
        }
        else if (service == "business")
        {
            cost *= 1.5;
        }

        if (discount == "children")
        {
            cost *= 0.8;
        }
        else if (discount == "elder")
        {
            cost *= 0.7;
        }
        else
        {
            cost *= 1;
        }
        cost += cost * taxes;
        return cost;
    }
}
public class PaymentTypeBus : ICostCalculationStrategy
{
    public double PaymentCost(int passenger, int TypeClass, bool bag, string discount, string service)
    {
        double cost = 0;
        double taxes = 0.05;
        if (TypeClass == 1)
        {
            cost = passenger * 100.6754;
        }
        else if (TypeClass == 2)
        {
            cost = passenger * 70.6754;
        }
        else
        {
            cost = passenger * 40.6754;
        }

        if (discount == "children")
        {
            cost *= 0.8;
        }
        else if (discount == "elder")
        {
            cost *= 0.7;
        }
        else
        {
            cost *= 1;
        }
        cost += cost * taxes;
        return cost;
    }
}
public class TravelBookingContext
{
    private ICostCalculationStrategy calculation;
    public void ChangeCalculation(ICostCalculationStrategy calculation)
    {
        this.calculation = calculation;
    }
    public double CalculateTravelCost (int passenger, int TypeClass, bool bag, string discount, string service)
    {
        if (calculation == null)
        {
            throw new Exception("no");
        }
            return calculation.PaymentCost(passenger, TypeClass, bag, discount, service);
    }
}
