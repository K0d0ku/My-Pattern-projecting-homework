/*output:
current Order: Coffee
current Cost: 50

choose an ingredient to add:
1. Milk (+10.0)
2. Sugar (+5.0)
3. Whipped Cream (+12.0)
4. Done - Finish order
1
Added Milk.

current Order: Coffee, Milk
current Cost: 60

choose an ingredient to add:
1. Milk (+10.0)
2. Sugar (+5.0)
3. Whipped Cream (+12.0)
4. Done - Finish order
2
Added Sugar.

current Order: Coffee, Milk, Sugar
current Cost: 65

choose an ingredient to add:
1. Milk (+10.0)
2. Sugar (+5.0)
3. Whipped Cream (+12.0)
4. Done - Finish order
3
Added Whipped Cream.

current Order: Coffee, Milk, Sugar, Whipped Cream
current Cost: 77

choose an ingredient to add:
1. Milk (+10.0)
2. Sugar (+5.0)
3. Whipped Cream (+12.0)
4. Done - Finish order
4

final Order: Coffee, Milk, Sugar, Whipped Cream
total Cost: 77*/

using System;
/*Decorator*/
public class program
{
    public static void Main(string[] args)
    {
        IBeverage beverage = new Coffee();
        bool addingIngredients = true;

        while (addingIngredients)
        {
            Console.WriteLine($"\ncurrent Order: {beverage.GetDescription()}");
            Console.WriteLine($"current Cost: {beverage.GetCost()}");

            Console.WriteLine("\nchoose an ingredient to add:");
            Console.WriteLine("1. Milk (+10.0)");
            Console.WriteLine("2. Sugar (+5.0)");
            Console.WriteLine("3. Whipped Cream (+12.0)");
            Console.WriteLine("4. Done - Finish order");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    beverage = new MilkDecorator(beverage);
                    Console.WriteLine("Added Milk.");
                    break;
                case "2":
                    beverage = new SugarDecorator(beverage);
                    Console.WriteLine("Added Sugar.");
                    break;
                case "3":
                    beverage = new WhippedCreamDecorator(beverage);
                    Console.WriteLine("Added Whipped Cream.");
                    break;
                case "4":
                    addingIngredients = false;
                    break;
                default:
                    Console.WriteLine("non existing choice, restart.");
                    break;
            }
        }

        Console.WriteLine($"\nfinal Order: {beverage.GetDescription()}");
        Console.WriteLine($"total Cost: {beverage.GetCost()}");
    }
}
public interface IBeverage
{
    double GetCost();
    string GetDescription();
}
public class Coffee : IBeverage
{
    public double GetCost()
    {
        return 50.0;
    }
    public string GetDescription()
    {
        return "Coffee";
    }
}
public class Espresso : IBeverage
{
    public double GetCost()
    {
        return 60.0;
    }
    public string GetDescription()
    {
        return "Espresso";
    }
}
public class Tea : IBeverage
{
    public double GetCost()
    {
        return 40.0;
    }
    public string GetDescription()
    {
        return "Tea";
    }
}
public abstract class BeverageDecorator : IBeverage
{
    protected IBeverage _beverage;
    public BeverageDecorator(IBeverage beverage)
    {
        _beverage = beverage;
    }
    public virtual double GetCost()
    {
        return _beverage.GetCost();
    }
    public virtual string GetDescription()
    {
        return _beverage.GetDescription();
    }
}
public class MilkDecorator : BeverageDecorator
{
    public MilkDecorator(IBeverage beverage) : base(beverage) { }
    public override double GetCost()
    {
        return base.GetCost() + 10.0;
    }
    public override string GetDescription()
    {
        return base.GetDescription() + ", Milk";
    }
}
public class SugarDecorator : BeverageDecorator
{
    public SugarDecorator(IBeverage beverage) : base(beverage) { }
    public override double GetCost()
    {
        return base.GetCost() + 5.0;
    }
    public override string GetDescription()
    {
        return base.GetDescription() + ", Sugar";
    }
}
public class WhippedCreamDecorator : BeverageDecorator
{
    public WhippedCreamDecorator(IBeverage beverage) : base(beverage) { }
    public override double GetCost()
    {
        return base.GetCost() + 12.0;
    }
    public override string GetDescription()
    {
        return base.GetDescription() + ", Whipped Cream";
    }
}
