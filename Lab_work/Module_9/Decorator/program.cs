/*output:
current Order: Coffee
current Cost: 50

choose an ingredient to add:
1. Milk (+10.0)
2. Sugar (+5.0)
3. Chocolate (+15.0)
4. Vanilla (+20.0)
5. Cinnamon (+25.0)
6. Almond Milk (+30.0)
7. Done - Finish order
6
added Almond Milk.

current Order: Coffee, Almond Milk
current Cost: 80

choose an ingredient to add:
1. Milk (+10.0)
2. Sugar (+5.0)
3. Chocolate (+15.0)
4. Vanilla (+20.0)
5. Cinnamon (+25.0)
6. Almond Milk (+30.0)
7. Done - Finish order
4
added Vanilla.

current Order: Coffee, Almond Milk, Vanilla
current Cost: 100

choose an ingredient to add:
1. Milk (+10.0)
2. Sugar (+5.0)
3. Chocolate (+15.0)
4. Vanilla (+20.0)
5. Cinnamon (+25.0)
6. Almond Milk (+30.0)
7. Done - Finish order
3
added Chocolate.

current Order: Coffee, Almond Milk, Vanilla, Chocolate
current Cost: 115

choose an ingredient to add:
1. Milk (+10.0)
2. Sugar (+5.0)
3. Chocolate (+15.0)
4. Vanilla (+20.0)
5. Cinnamon (+25.0)
6. Almond Milk (+30.0)
7. Done - Finish order
7

final Order: Coffee, Almond Milk, Vanilla, Chocolate
total Cost: 115*/

using System;
/*Decorator*/
public class program
{
    public static void Main(string[] arg)
    {
        /*// Создаем базовый напиток — кофе
        IBeverage beverage = new Coffee();
        Console.WriteLine($"{beverage.GetDescription()} : {beverage.GetCost()}");

        // Добавляем молоко
        beverage = new MilkDecorator(beverage);
        Console.WriteLine($"{beverage.GetDescription()} : {beverage.GetCost()}");

        // Добавляем сахар
        beverage = new SugarDecorator(beverage);
        Console.WriteLine($"{beverage.GetDescription()} : {beverage.GetCost()}");

        // Добавляем шоколад
        beverage = new ChocolateDecorator(beverage);
        Console.WriteLine($"{beverage.GetDescription()} : {beverage.GetCost()}");
        
        beverage = new AlmondMilkDecorator(beverage);
        Console.WriteLine($"{beverage.GetDescription()} : {beverage.GetCost()}");

        beverage = new CinnamonDecorator(beverage);
        Console.WriteLine($"{beverage.GetDescription()} : {beverage.GetCost()}");
    
        beverage = new VanillaDecorator(beverage);
        Console.WriteLine($"{beverage.GetDescription()} : {beverage.GetCost()}");*/

        /*my code*/
        IBeverage beverage = new Coffee();
        bool addingIngredients = true;
        while (addingIngredients)
        {
            Console.WriteLine("\ncurrent Order: " + beverage.GetDescription());
            Console.WriteLine("current Cost: " + beverage.GetCost());

            Console.WriteLine("\nchoose an ingredient to add:");
            Console.WriteLine("1. Milk (+10.0)");
            Console.WriteLine("2. Sugar (+5.0)");
            Console.WriteLine("3. Chocolate (+15.0)");
            Console.WriteLine("4. Vanilla (+20.0)");
            Console.WriteLine("5. Cinnamon (+25.0)");
            Console.WriteLine("6. Almond Milk (+30.0)");
            Console.WriteLine("7. Done - Finish order");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    beverage = new MilkDecorator(beverage);
                    Console.WriteLine("added Milk.");
                    break;
                case "2":
                    beverage = new SugarDecorator(beverage);
                    Console.WriteLine("added Sugar.");
                    break;
                case "3":
                    beverage = new ChocolateDecorator(beverage);
                    Console.WriteLine("added Chocolate.");
                    break;
                case "4":
                    beverage = new VanillaDecorator(beverage);
                    Console.WriteLine("added Vanilla.");
                    break;
                case "5":
                    beverage = new CinamonDecorator(beverage);
                    Console.WriteLine("added Cinnamon.");
                    break;
                case "6":
                    beverage = new AlmondMilkDecorator(beverage);
                    Console.WriteLine("added Almond Milk.");
                    break;
                case "7":
                    addingIngredients = false;
                    break;
                default:
                    Console.WriteLine("non existing choice, restart");
                    break;
            }
        }
        Console.WriteLine($"\nfinal Order: {beverage.GetDescription()}");
        Console.WriteLine($"total Cost: {beverage.GetCost()}");
    }
}
public interface IBeverage
{
    double GetCost();  // Получить стоимость напитка
    string GetDescription();  // Получить описание напитка
}
public class Coffee : IBeverage
{
    public double GetCost()
    {
        return 50.0;  // Стоимость кофе
    }

    public string GetDescription()
    {
        return "Coffee";
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
        return _beverage.GetCost();  // Стоимость основного напитка
    }

    public virtual string GetDescription()
    {
        return _beverage.GetDescription();  // Описание основного напитка
    }
}
public class MilkDecorator : BeverageDecorator
{
    public MilkDecorator(IBeverage beverage) : base(beverage) { }

    public override double GetCost()
    {
        return base.GetCost() + 10.0;  // Стоимость с добавлением молока
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
        return base.GetCost() + 5.0;  // Стоимость с добавлением сахара
    }

    public override string GetDescription()
    {
        return base.GetDescription() + ", Sugar";
    }
}
public class ChocolateDecorator : BeverageDecorator
{
    public ChocolateDecorator(IBeverage beverage) : base(beverage) { }

    public override double GetCost()
    {
        return base.GetCost() + 15.0;  // Стоимость с добавлением шоколада
    }

    public override string GetDescription()
    {
        return base.GetDescription() + ", Chocolate";
    }
}
/*my code*/
public class VanillaDecorator : BeverageDecorator
{
    public VanillaDecorator(IBeverage beverage) : base(beverage) { }
    public override double GetCost()
    {
        return base.GetCost() + 20.0;
    }
    public override string GetDescription()
    {
        return base.GetDescription() + ", Vanilla";
    }
}
public class CinamonDecorator : BeverageDecorator
{
    public CinamonDecorator(IBeverage beverage) : base(beverage) { }
    public override double GetCost()
    {
        return base.GetCost() + 25.0;
    }
    public override string GetDescription()
    {
        return base.GetDescription() + ", Cinamon";
    }
}
public class AlmondMilkDecorator : BeverageDecorator
{
    public AlmondMilkDecorator(IBeverage beverage) : base(beverage) { }
    public override double GetCost()
    {
        return base.GetCost() + 30.0;
    }
    public override string GetDescription()
    {
        return base.GetDescription() + ", Almond Milk";
    }
}
