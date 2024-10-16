/*output:
original Order:
products:
laptop (duantity: 1, price: $999.99)
mouse (duantity: 2, price: $25.50)
delivery price: $15.00
discount: 10%
payment type: credit card

cloned Order:
products:
laptop (duantity: 1, price: $999.99)
mouse (duantity: 2, price: $25.50)
delivery price: $15.00
discount: 10%
payment type: credit card*/

using System;
public class program
{
    public static void Main(string[] args)
    {
        var product1 = new Product { Name = "laptop", Price = 999.99, Quantity = 1 };
        var product2 = new Product { Name = "mouse", Price = 25.50, Quantity = 2 };

        var order = new Order
        {
            Products = new List<Product> { product1, product2 },
            DeliveryPrice = 15.0,
            Discount = 10,
            PaymentType = "credit card"
        };
        var clonedOrder = (Order)order.Clone();

        /*original*/
        Console.WriteLine("original Order:");
        Console.WriteLine(order);

        /*clone*/
        Console.WriteLine("\ncloned Order:");
        Console.WriteLine(clonedOrder);
    }
}
public class Order : ICloneable
{
    public List<Product> Products { get; set; }
    public double DeliveryPrice { get; set; }
    public double Discount { get; set; }
    public string PaymentType { get; set; }
    public object Clone()
    {
        return new Order()
        {
            Products = this.Products.ConvertAll(products => (Product)products.Clone()),
            DeliveryPrice = this.DeliveryPrice,
            Discount = this.Discount,
            PaymentType = this.PaymentType

        };
    }
    public override string ToString()
    {
        var productDetails = string.Join("\n", Products);
        return $"products:\n{productDetails}\n" +
               $"delivery price: {DeliveryPrice:C}\n" +
               $"discount: {Discount}%\n" +
               $"payment type: {PaymentType}";
    }
}
public class Product : ICloneable
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public object Clone()
    {
        return new Product { Name = this.Name, Price = this.Price, Quantity = this.Quantity };
    }
    public override string ToString()
    {
        return $"{Name} (duantity: {Quantity}, price: {Price:C})";
    }
}
