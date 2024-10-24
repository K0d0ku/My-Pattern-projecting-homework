/*output
making tea
boiling the water
brewing the tea
pouring in to the cup
adding the lemon

making coffee
boiling the water
brewing the coffee
would you like to add sugar and milk? (y/n)
y
adding sugar & milk
pouring in to the cup

making hot chocolate
boiling the water
melting the chocolate
pouring in to the cup
adding marshmallow*/

using System;
public class program
{
    public static void Main(string[] args)
    {
        Beverage tea = new Tea();
        tea.PrepareRecipe();

        Beverage coffee = new Coffee();
        coffee.PrepareRecipe();

        Beverage hotChoco = new HotChoco();
        hotChoco.PrepareRecipe();
    }
}
public abstract class Beverage
{
    public virtual void PrepareRecipe()
    {
        Name();
        BoilWater();
        Brew();
        PourInCup();
        AddCondiments();
    }
    public virtual void BoilWater()
    {
        Console.WriteLine("boiling the water");
    }
    public virtual void PourInCup()
    {
        Console.WriteLine("pouring in to the cup");
    }
    protected abstract void Brew();
    protected abstract void AddCondiments();
    protected abstract void Name();
}

public class Tea : Beverage
{
    public override void PrepareRecipe()
    {
        Name();
        BoilWater();
        Brew();
        PourInCup();
        AddCondiments();
    }
    protected override void Name()
    {
        Console.WriteLine("\nmaking tea");
    }
    protected override void Brew()
    {
        Console.WriteLine("brewing the tea");
    }
    protected override void AddCondiments()
    {
        Console.WriteLine($"adding the lemon");
    }
}
public class Coffee : Beverage
{
    public override void PrepareRecipe()
    {
        Name();
        BoilWater();
        Brew();
        if (CustomerWantsCondiments())
        {
            AddCondiments();
        }
        PourInCup();
    }
    protected override void Name()
    {
        Console.WriteLine("\nmaking coffee");
    }
    protected override void Brew()
    {
        Console.WriteLine("brewing the coffee");
    }
    protected override void AddCondiments()
    {
        Console.WriteLine("adding sugar & milk");
    }
    private bool CustomerWantsCondiments()
    {
        string answer = "";
        while (true)
        {
            Console.WriteLine("would you like to add sugar and milk? (y/n)");
            answer = Console.ReadLine();
            if (answer.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (answer.Equals("n", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else
            {
                Console.WriteLine("non existing choice. please answer 'y' or 'n'.");
            }
        }
    }
}
public class HotChoco : Beverage
{
    protected override void Name()
    {
        Console.WriteLine("\nmaking hot chocolate");
    }
    protected override void Brew()
    {
        Console.WriteLine("melting the chocolate");
    }
    protected override void AddCondiments()
    {
        Console.WriteLine($"adding marshmallow");
    }
}
