/*output:
+ bread
  + sourdough dough
    + basic dough
      - flour
      - water
    - yeast
+ pasta
  + pasta dough
    + basic dough
      - flour
      - water
    - salt
+ cookie
  + sugar dough
    + basic dough
      - flour
      - water
    - sugar
*/
using System;
/*Composite - my, Flour Recipe*/
/*same here i wrote a composite pattern code hierchachy of flour being the 
* base of the hierarchy but is being used to create different products like bread pasta and cookie*/
public class FlourRecipes
{
    public static void Main(string[] args)
    {
        Ingredient flour = new Ingredient("flour");
        Ingredient water = new Ingredient("water");
        Ingredient salt = new Ingredient("salt");
        Ingredient sugar = new Ingredient("sugar");
        Ingredient yeast = new Ingredient("yeast");

        Product dough = new Product("basic dough");
        dough.Add(flour);
        dough.Add(water);

        Product sourdoughDough = new Product("sourdough dough");
        sourdoughDough.Add(dough);
        sourdoughDough.Add(yeast);

        Product pastaDough = new Product("pasta dough");
        pastaDough.Add(dough);
        pastaDough.Add(salt);

        Product sugarDough = new Product("sugar dough");
        sugarDough.Add(dough);
        sugarDough.Add(sugar);

        Product bread = new Product("bread");
        bread.Add(sourdoughDough);

        Product pasta = new Product("pasta");
        pasta.Add(pastaDough);

        Product cookie = new Product("cookie");
        cookie.Add(sugarDough);

        bread.Display("");
        pasta.Display("");
        cookie.Display("");
    }
}
public interface IRecipeComponent
{
    void Display(string indent);
}
public class Ingredient : IRecipeComponent
{
    public string Name { get; private set; }
    public Ingredient(string name)
    {
        Name = name;
    }
    public void Display(string indent)
    {
        Console.WriteLine($"{indent}- {Name}");
    }
}
public class Product : IRecipeComponent
{
    public string Name { get; private set; }
    private List<IRecipeComponent> _components = new List<IRecipeComponent>();
    public Product(string name)
    {
        Name = name;
    }
    public void Add(IRecipeComponent component)
    {
        _components.Add(component);
    }
    public void Display(string indent)
    {
        Console.WriteLine($"{indent}+ {Name}");
        foreach (var component in _components)
        {
            component.Display(indent + "  ");
        }
    }
}
