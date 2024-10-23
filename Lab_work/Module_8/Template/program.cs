/*output:
choose beverage type
1. tea
2. coffee
3. hot chocolate
3
Choose the ingredient:
1. lemon
2. sugar
3. milk
4. almond milk
5. marshmallow
6. cinamon
7. whipped cream
8. lime
7

making HotChoco with whipped cream

kipechenya vodi...
melting the chocolate
nalivanie v chashku...
adding whipped cream*/

using System;
public class program
{
    public static void Main(string[] args)
    {
        /*// Приготовление чая
        Beverage tea = new Tea();
        Console.WriteLine("prigotovlenye chaya:");
        tea.PrepareRecipe();

        Console.WriteLine();

        // Приготовление кофе
        Beverage coffee = new Coffee();
        Console.WriteLine("prigotovlenye cofe:");
        coffee.PrepareRecipe();

        *//*my code*//*
        Beverage hotChoco = new HotChoco();
        Console.WriteLine("Приготовление горячего шоколада:");
        hotChoco.PrepareRecipe();*/

        Console.WriteLine("choose beverage type");
        Console.WriteLine("1. tea");
        Console.WriteLine("2. coffee");
        Console.WriteLine("3. hot chocolate");
        int beverageChoice = Convert.ToInt32(Console.ReadLine());
        Beverage selectedBeverage;
        switch(beverageChoice)
        {
            case 1:
                selectedBeverage = new Tea();
                break;
            case 2:
                selectedBeverage = new Coffee();
                break;
            case 3:
                selectedBeverage = new HotChoco();
                break;
            default:
                Console.WriteLine("non exisitng choice, tea is gonna be used by default.\n");
                selectedBeverage = new Tea();
                break;
        }
        Console.WriteLine("Choose the ingredient:");
        List<string> availableIngredients = Ingredient.getingredients();
        for (int i = 0; i < availableIngredients.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {availableIngredients[i]}");
        }
        int ingredientChoice = Convert.ToInt32(Console.ReadLine());
        string selectIngredient;
        switch (ingredientChoice)
        {
            case 1:
                selectIngredient = "lemon";
                break;
            case 2:
                selectIngredient = "sugar";
                break;
            case 3:
                selectIngredient = "milk";
                break;
            case 4:
                selectIngredient = "almond milk";
                break;
            case 5:
                selectIngredient = "marshmallow";
                break;
            case 6:
                selectIngredient = "cinamon";
                break;
            case 7:
                selectIngredient = "whipped cream";
                break;
            case 8:
                selectIngredient = "lime";
                break;
            default:
                Console.WriteLine("non existing choice, milk is gonna be used by default.\n");
                selectIngredient = "milk";
                break;
        }
        Console.WriteLine($"\nmaking {selectedBeverage.GetType().Name} with {selectIngredient}\n");
        selectedBeverage.PrepareRecipe(selectIngredient);
    }
}
public abstract class Beverage
{
    // Шаблонный метод
    public void PrepareRecipe(string ingredient)
    {
        BoilWater();
        Brew();
        PourInCup();
        AddCondiments(ingredient);
    }

    // Общий шаг для всех напитков
    private void BoilWater()
    {
        Console.WriteLine("kipechenya vodi...");
    }

    // Общий шаг для всех напитков
    private void PourInCup()
    {
        Console.WriteLine("nalivanie v chashku...");
    }

    // Абстрактные методы для шагов, которые будут реализованы в подклассах
    protected abstract void Brew();
    protected abstract void AddCondiments(string ingredient);
}
public class Tea : Beverage
{
    protected override void Brew()
    {
        Console.WriteLine("zavarivanye chaya...");
    }

    protected override void AddCondiments(string ingredient)
    {
        Console.WriteLine($"dobavlenye {ingredient}...");
    }
}
public class Coffee : Beverage
{
    protected override void Brew()
    {
        Console.WriteLine("zavarivanye cofe...");
    }

    protected override void AddCondiments(string ingredient)
    {
        Console.WriteLine($"dobavlenye {ingredient}");
    }
}
/*my code*/
public class HotChoco : Beverage
{   
    protected override void Brew()
    {
        Console.WriteLine("melting the chocolate");
    }
    protected override void AddCondiments(string ingredient)
    {
        Console.WriteLine($"adding {ingredient}");
    }
}
public static class Ingredient
{   /*i created a separate method that holds the ingredients 
     * and can be accessable almost everywhere, that lets us make any kind of beverage
     with any ingredient that is available to us*/
    private static List<string> ingredients = new List<string>
    {
        "lemon",
        "sugar",
        "milk",
        "almond milk",
        "marshmallow",
        "cinamon",
        "whipped cream",
        "lime"
    };
    public static List<string> getingredients() 
    {
        return ingredients;
    }

}
