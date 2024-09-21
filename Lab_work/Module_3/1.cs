Произведите корректную (правильную) по вашему мнению реализацию с применением принципа DRY:

Использование методов для устранения дублирования кода
public class OrderService
{
    public void CreateOrder(string productName, int quantity, double price)
    {
        double totalPrice = quantity * price;
        Console.WriteLine($"Order for {productName} created. Total: {totalPrice}");
    }
    public void UpdateOrder(string productName, int quantity, double price)
    {
        double totalPrice = quantity * price;
        Console.WriteLine($"Order for {productName} updated. New total: {totalPrice}");
    }
}

Мой отыет:
using System;
class Program
{
    static void Main(string[] args)
    {
        OrderService orderService = new OrderService();

        orderService.CreateOrder("Car", 2, 1000);
        orderService.UpdateOrder("Car", 2, 1000, 3);
        
        orderService.CreateOrder("Bike", 5, 400);
        orderService.UpdateOrder("Bike", 5, 400, 2);
    }
} /*- для теста*/ 
public class OrderService
{   public double totalPrice = 0; /*- я сделал totalPrice публичным чтобы использовать ее в обейх методах по разному*/
    public void CreateOrder(string productName, int quantity, double price)
    {
        totalPrice = quantity * price; /*- тут totalPrice вывозит нам цену умножая количество на цену*/
        Console.WriteLine($"Order for {productName} created. Total: {totalPrice}");
    }
    public void UpdateOrder(string productName, int Original_quantity, double price, int new_items) /* тут я добавил new_item как количество добавленных обьектов для обновление результата*/
    {
        double updatedPrice = totalPrice * new_items; /* - а тут я использую totalPrice с CreateOrder для того чтобы обновить цену в UpdateOrder умножая totalprice с CreateOrder на new_items */
        Console.WriteLine($"Order for {productName} updated. New total: {updatedPrice}");
    } /* из того что я знаю в моем ответе я использую принцип DRY и убираю дублирование кода используя другую логику для UpdateOrder */
}
