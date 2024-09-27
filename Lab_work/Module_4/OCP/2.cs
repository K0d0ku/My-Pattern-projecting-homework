/*Произведите корректную (правильную) по вашему мнению реализацию с применением принципа Open-Closed Principle, OCP:
Рассмотрим пример, в котором класс DiscountCalculator нарушает принцип OCP, поскольку каждый раз при добавлении нового типа скидки нужно изменять существующий код:
public enum CustomerType
{
    Regular,
    Silver,
    Gold
}

public class DiscountCalculator
{
    public double CalculateDiscount(CustomerType customerType, double amount)
    {
        if (customerType == CustomerType.Regular)
        {
            return amount;
        }
        else if (customerType == CustomerType.Silver)
        {
            return amount * 0.9; // 10% скидка
        }
        else if (customerType == CustomerType.Gold)
        {
            return amount * 0.8; // 20% скидка
        }
        else
        {
            throw new ArgumentException("Неизвестный тип клиента");
        }
    }
}

Если потребуется добавить новый тип клиента, например, Platinum, то потребуется модифицировать метод CalculateDiscount, что нарушает принцип OCP.
Необходимо используя полиморфизм, сделать класс DiscountCalculator открытым для расширения, но закрытым для модификации.
*/
