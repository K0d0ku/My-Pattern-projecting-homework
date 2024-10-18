using System;
class Program
{   /*answers for question are below*/
    static void Main(string[] args)
    {
        DeliveryContext deliveryContext = new DeliveryContext();

        Console.WriteLine("vyberity typ dostavki: 1 - standart, 2 - express, 3 - international, 4 - za noch");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                deliveryContext.SetShippingStrategy(new StandardShippingStrategy());
                break;
            case "2":
                deliveryContext.SetShippingStrategy(new ExpressShippingStrategy());
                break;
            case "3":
                deliveryContext.SetShippingStrategy(new InternationalShippingStrategy());
                break; 
            case "4":
                deliveryContext.SetShippingStrategy(new OverNightDelivery()); /*my code*/
                break;
            default:
                Console.WriteLine("nevernyu vybor.");
                return;
        }

        Console.WriteLine("vvedite ves posilku (kg):");
        decimal weight = Convert.ToDecimal(Console.ReadLine());

        Console.WriteLine("vvedite rasstoyanye posilky (km):");
        decimal distance = Convert.ToDecimal(Console.ReadLine());

        decimal cost = deliveryContext.CalculateCost(weight, distance);
        Console.WriteLine($"stoymost dostavky: {cost:C}");
    }
}
public interface IShippingStrategy
{
    decimal CalculateShippingCost(decimal weight, decimal distance);
}
public class StandardShippingStrategy : IShippingStrategy
{
    public decimal CalculateShippingCost(decimal weight, decimal distance)
    {
        return weight * 0.5m + distance * 0.1m;
    }
}
public class ExpressShippingStrategy : IShippingStrategy
{
    public decimal CalculateShippingCost(decimal weight, decimal distance)
    {
        return (weight * 0.75m + distance * 0.2m) + 10; // Дополнительная плата за скорость
    }
}
public class InternationalShippingStrategy : IShippingStrategy
{
    public decimal CalculateShippingCost(decimal weight, decimal distance)
    {
        return weight * 1.0m + distance * 0.5m + 15; // Дополнительные сборы за международную доставку
    }
}
public class OverNightDelivery : IShippingStrategy /*My code*/
{
    public decimal CalculateShippingCost(decimal weight, decimal distance)
    {
        return weight * 2.0m + distance * 1.0m + 20;
    }
}
public class DeliveryContext
{
    private IShippingStrategy _shippingStrategy;

    // Метод для установки стратегии доставки
    public void SetShippingStrategy(IShippingStrategy strategy)
    {
        _shippingStrategy = strategy;
    }

    // Метод для расчета стоимости доставки
    public decimal CalculateCost(decimal weight, decimal distance)
    {
        if (_shippingStrategy == null)
        {
            throw new InvalidOperationException("strategya dostavky ne ustanovlena.");
        }
        return _shippingStrategy.CalculateShippingCost(weight, distance);
    }
}
/*answers for questions:
Вопросы для самопроверки:
1.	Какие преимущества дает использование паттерна "Стратегия" в этом проекте?
его гибкость, можно добавлять новые методы стратегий не меняя основную часть кода
2.	Как можно изменить поведение программы без модификации существующего кода?
из того что я знаю то можно при реальизаций кода или при созданий нового класса который 
имплементируется с основоного кода интерфейса или абстракций, давая ему новые свойства и методы 
3.	Почему важно, чтобы каждый метод расчета был независимым и реализован в отдельном классе?
для гибкости и расширяемости, чтобы легче можно было обновить одну стратегий не трогая существющий код,
и в добавок оно сохраняет принцип SRP
*/
