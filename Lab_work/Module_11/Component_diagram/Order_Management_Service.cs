/*output:
Notification to User 1: Your order has been paid successfully.
Order Status: Paid
*/
using System;
/*component diagram, i rewrote the example code you provided me with facade pattern 
 * cuz i thought its the most fit for by the example, also in some parts of the code like in the 
 implementations of the interfaces i used lambda expression to direclty work with the already existing 
objects , instead of creating a new instances of them*/
public class program
{
    static void Main(string[] args)
    {
        var userService = new UserService();
        var productService = new ProductService();
        var paymentService = new PaymentService();
        var notificationService = new NotificationService();
        var orderService = new OrderService(productService, paymentService, notificationService);

        var orderManagementFacade = new OrderManagementFacade(productService, paymentService, notificationService, orderService);

        var user = userService.Register("Uali Jngu", "nah123456");

        var product1 = productService.AddProduct(new Product { Name = "Monitor", Price = 2999.99 });
        var product2 = productService.AddProduct(new Product { Name = "Pc", Price = 4999.99 });

        var order = orderManagementFacade.PlaceOrder(user.Id, new List<int> { product1.Id, product2.Id });

        var orderStatus = orderManagementFacade.CheckOrderStatus(order.Id);

        Console.WriteLine($"Order Status: {orderStatus.Status}");
    }
}
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
}
public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<Product> Products { get; set; }
    public string Status { get; set; }
}
public interface IUserService
{
    User Register(string username, string password);
    User Login(string username, string password);
    User GetUserById(int userId);
}
public class UserService : IUserService
{
    private readonly List<User> _users = new List<User>();
    private int _nextId = 1;
    public User Register(string username, string password)
    {
        var user = new User { Id = _nextId++, Username = username, Password = password };
        _users.Add(user);
        return user;
    }
    public User Login(string username, string password)
    {
        return _users.FirstOrDefault(u => u.Username == username && u.Password == password)
               ?? throw new Exception("Invalid credentials.");
    }
    public User GetUserById(int userId)
    {
        return _users.FirstOrDefault(u => u.Id == userId)
               ?? throw new Exception("User not found.");
    }
}
public interface IProductService
{
    List<Product> GetProducts();
    Product AddProduct(Product product);
    Product GetProductById(int productId);
}
public class ProductService : IProductService
{
    private readonly List<Product> _products = new List<Product>();
    private int _nextId = 1;
    public List<Product> GetProducts() => _products;
    public Product AddProduct(Product product)
    {
        product.Id = _nextId++;
        _products.Add(product);
        return product;
    }
    public Product GetProductById(int productId)
    {
        return _products.FirstOrDefault(p => p.Id == productId) ?? throw new Exception("Product not found.");
    }
}
public interface IOrderService
{
    Order CreateOrder(int userId, List<int> productIds);
    Order GetOrderStatus(int orderId);
}
public class OrderService : IOrderService
{
    private readonly IProductService _productService;
    private readonly IPaymentService _paymentService;
    private readonly INotificationService _notificationService;
    private readonly List<Order> _orders = new List<Order>();
    private int _nextId = 1;
    public OrderService(IProductService productService, IPaymentService paymentService, INotificationService notificationService)
    {
        _productService = productService;
        _paymentService = paymentService;
        _notificationService = notificationService;
    }
    public Order CreateOrder(int userId, List<int> productIds)
    {
        var products = _productService.GetProducts().Where(p => productIds.Contains(p.Id)).ToList();
        if (!products.Any()) throw new Exception("Products not found.");
        var order = new Order
        {
            Id = _nextId++,
            UserId = userId,
            Products = products,
            Status = "Created"
        };
        double totalAmount = products.Sum(p => p.Price);

        if (_paymentService.ProcessPayment(order.Id, totalAmount))
        {
            order.Status = "Paid";
            _notificationService.SendNotification(userId, "Your order has been paid successfully.");
        }
        else
        {
            order.Status = "Payment Failed";
            _notificationService.SendNotification(userId, "Payment failed, please try again.");
        }
        _orders.Add(order);
        return order;
    }
    public Order GetOrderStatus(int orderId)
    {
        return _orders.FirstOrDefault(o => o.Id == orderId) ?? throw new Exception("Order not found.");
    }
}
public interface IPaymentService
{
    bool ProcessPayment(int orderId, double amount);
}
public class PaymentService : IPaymentService
{
    public bool ProcessPayment(int orderId, double amount)
    {
        return true;
    }
}
public interface INotificationService
{
    void SendNotification(int userId, string message);
}
public class NotificationService : INotificationService
{
    public void SendNotification(int userId, string message)
    {
        Console.WriteLine($"Notification to User {userId}: {message}");
    }
}
public class OrderManagementFacade
{
    private readonly IProductService _productService;
    private readonly IPaymentService _paymentService;
    private readonly INotificationService _notificationService;
    private readonly IOrderService _orderService;
    public OrderManagementFacade(IProductService productService, IPaymentService paymentService, INotificationService notificationService, IOrderService orderService)
    {
        _productService = productService;
        _paymentService = paymentService;
        _notificationService = notificationService;
        _orderService = orderService;
    }
    public Order PlaceOrder(int userId, List<int> productIds)
    {
        return _orderService.CreateOrder(userId, productIds);
    }
    public Order CheckOrderStatus(int orderId)
    {
        return _orderService.GetOrderStatus(orderId);
    }
}
