/*output:
choose the item from menu:
1: Burger - 4.50$
2: Cheeseburger - 5.50$
3: HotDog - 3.70$
4: Pizza - 9.20$
5: Chicken nuggets - 1.60$/Piece
6: Done
2
enter the quantity for Cheeseburger: 2
the: Cheeseburger has been chosen from the menu
the price for: Cheeseburger is 5.5
the quantity of choosen item is: 2
paying for the amount of: $11
choose the item from menu:
1: Burger - 4.50$
2: Cheeseburger - 5.50$
3: HotDog - 3.70$
4: Pizza - 9.20$
5: Chicken nuggets - 1.60$/Piece
6: Done
5
enter the quantity for Chicken nuggets: 30
the: Chicken nuggets has been chosen from the menu
the price for: Chicken nuggets is 1.6
the quantity of choosen item is: 30
paying for the amount of: $48
choose the item from menu:
1: Burger - 4.50$
2: Cheeseburger - 5.50$
3: HotDog - 3.70$
4: Pizza - 9.20$
5: Chicken nuggets - 1.60$/Piece
6: Done
4
enter the quantity for Pizza: 1
the: Pizza has been chosen from the menu
the price for: Pizza is 9.2
the quantity of choosen item is: 1
paying for the amount of: $9.2
choose the item from menu:
1: Burger - 4.50$
2: Cheeseburger - 5.50$
3: HotDog - 3.70$
4: Pizza - 9.20$
5: Chicken nuggets - 1.60$/Piece
6: Done
6
enter delivery adress:
Satbayev University

order summary:
Cheeseburger x2 - $11
Chicken nuggets x30 - $48
Pizza x1 - $9.2

final price: $68.2
delivering to: Satbayev University*/

using System;
using System.Net.NetworkInformation;
/*Facade - My, online Delivery service*/
/*i made this to show that i understood the facade pattern, 
  its a simple online delivery service that uses facade pattern and follows srp, ocp and isp principles*/

/*i am pointing out on HUMAN MADE ERRORS here, the main point here is to prove that i learned the 
* facade pattern, and thats why i did not wanted to spend so much time for a task that was not given*/

/*there are several issues here, as you can see in realization we ony expand the while statement,
  but upon updating it and adding new items to menu we have to constantly keep changing the loop breaker 
  case number, and after changing that we also have to change quantity price calculators condition number
  cuz its based on the loopbreakers case numbers, and when it loops we can choose the items we've already 
  chosen again, also if user decides not to make an order we still have to write the adress*/
public class Client
{
    public static void Main(string[] args)
    {
        Menu menu = new Menu();
        Price price = new Price(menu);
        Quantity quantity = new Quantity();
        Payment payment = new Payment(menu, price, quantity);
        Delivery delivery = new Delivery();
        OrderFacade orderFacade = new OrderFacade(menu, price, quantity, payment, delivery);

        bool multipleOrders = true;
        double totalPrice = 0.0;
        List<string> orderSummary = new List<string>();
        while (multipleOrders)
        {   /*the ocp happens here, because i wrote the facade the way that it can handle dynamic changes
             i can easily expand the menu here instead of rewriting the whole code*/
            Console.WriteLine("choose the item from menu:");
            Console.WriteLine("1: Burger - 4.50$");/*base*/
            Console.WriteLine("2: Cheeseburger - 5.50$");/*base*/
            Console.WriteLine("3: HotDog - 3.70$");/*newly added*/
            Console.WriteLine("4: Pizza - 9.20$");/*newly added*/
            Console.WriteLine("5: Chicken nuggets - 1.60$/Piece");/*newly added*/
            Console.WriteLine("6: Done");/*has to constantly change the case number because of expansion*/
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                string itemName = "";
                double itemPrice = 0.0;
                switch (choice)
                {
                    case 1:/*base*/
                        itemName = "Burger";
                        itemPrice = 4.50;
                        break;
                    case 2:/*base*/
                        itemName = "Cheeseburger";
                        itemPrice = 5.50;
                        break;
                    case 3:/*expanded*/
                        itemName = "HotDog";
                        itemPrice = 3.70;
                        break;
                    case 4:/*expanded*/
                        itemName = "Pizza";
                        itemPrice = 9.20;
                        break;
                    case 5:/*expanded*/
                        itemName = "Chicken nuggets";
                        itemPrice = 1.60;
                        break;
                    case 6:/*has to constantly change the case number because of expansion*/
                        multipleOrders = false;
                        break;
                    default:
                        Console.WriteLine("non existing choice restart");
                        continue;
                }
                if (choice != 6)/*we also have to constantly change the price and quantity calculating
                                 systems condition because we changed the loopbreakers case number*/
                {
                    Console.Write($"enter the quantity for {itemName}: ");
                    int quantityCount = int.Parse(Console.ReadLine());
                    orderFacade.OrderItem(itemName, itemPrice, quantityCount);
                    orderSummary.Add($"{itemName} x{quantityCount} - ${itemPrice * quantityCount}");
                    totalPrice += itemPrice * quantityCount;
                }
            }/*after the loops break we can choose the chosen items again*/
            else
            {
                Console.WriteLine("invalid input enter numbers only");
            }
        }
        Console.WriteLine("enter delivery adress:");
        string address = Console.ReadLine();
        Console.WriteLine("\norder summary:");
        foreach (var item in orderSummary)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine($"\nfinal price: ${totalPrice}");
        orderFacade.DeliveryOrder(address);
        /*also even if the user chooses to not to oreder we still have to enter the adress*/
    } 
}
public class Menu
{
    public void SelectItem(string item) => Console.WriteLine($"the: {item} has been chosen from the menu");
}
public class Price
{
    private readonly Menu _menu;
    public Price (Menu menu)
    {
        _menu = menu;
    }
    public void ItemPrice(string item ,double price) => Console.WriteLine($"the price for: {item} is {price}");
}
public class Quantity
{
    public void ItemQuantity(int quantity) => Console.WriteLine($"the quantity of choosen item is: {quantity}");
}
public class Payment
{
    private readonly Menu _menu;
    private readonly Price _price;
    private readonly Quantity _quantity;
    public Payment(Menu menu, Price price, Quantity quantity)
    {
        _menu = menu;
        _price = price;
        _quantity = quantity;
    }
    public void ProcessPayment(double unitPrice, int quantity)
    {
        double totalAmount = unitPrice * quantity;
        Console.WriteLine($"paying for the amount of: ${totalAmount}");
    }
}
public class Delivery
{
    public void DeliveryAdress(string adress) => Console.WriteLine($"delivering to: {adress}");
}
public class OrderFacade
{
    private readonly Menu _menu;
    private readonly Price _price;
    private readonly Quantity _quantity;
    private readonly Payment _payment;
    private readonly Delivery _delivery;
    public OrderFacade(Menu menu, Price price, Quantity quantity, Payment payment, Delivery delivery)
    {
        _menu = menu;
        _price = price;
        _quantity = quantity;
        _payment = payment;
        _delivery = delivery;
    }
    public void OrderItem(string itemName, double price, int quantity)
    {
        _menu.SelectItem(itemName);
        _price.ItemPrice(itemName, price);
        _quantity.ItemQuantity(quantity);
        _payment.ProcessPayment(price, quantity);
    }
    public void DeliveryOrder(string address)
    {
        _delivery.DeliveryAdress(address);
    }
}
