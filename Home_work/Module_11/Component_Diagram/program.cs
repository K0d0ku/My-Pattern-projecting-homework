/*output:
Choose a hotel:
1. Beach Resort (Malibu)
2. Mountain Inn (Colorado)
3. City Center Hotel (New York)
3
found 1 hotel(s) in your price range.
selected Hotel: City Center Hotel in New York
booking confirmation to User 1: your booking with ID 1 is confirmed.
booking confirmed! Booking ID: 1
*/
using System;
using System.Collections.Generic;
using System.Linq;
/*same here i just rewrote the lab 11 task 2*/
public class program
{
    public static void Main(string[] args)
    {
        var userManagementService = new UserManagementService();
        var hotelService = new HotelService();
        var paymentService = new PaymentService();
        var notificationService = new NotificationService();

        var hotelBookingFacade1 = new HotelBookingFacade(hotelService, new BookingService(hotelService), paymentService, notificationService, userManagementService);
        var hotelBookingFacade2 = new HotelBookingFacade(hotelService, new BookingService(hotelService), paymentService, notificationService, userManagementService);
        var hotelBookingFacade3 = new HotelBookingFacade(hotelService, new BookingService(hotelService), paymentService, notificationService, userManagementService);

        var user = userManagementService.Register("Uali Jngu", "nah123456");

        Console.WriteLine("Choose a hotel:");
        Console.WriteLine("1. Beach Resort (Malibu)");
        Console.WriteLine("2. Mountain Inn (Colorado)");
        Console.WriteLine("3. City Center Hotel (New York)");

        int choice = int.Parse(Console.ReadLine());

        HotelBookingFacade selectedFacade = null;
        string location = "";
        string roomClass = "";
        double maxPrice = 0;

        switch (choice)
        {
            case 1:
                selectedFacade = hotelBookingFacade1;
                location = "Malibu"; 
                roomClass = "Luxury"; 
                maxPrice = 350;       
                break;
            case 2:/*wont output cuz price range too low*/
                selectedFacade = hotelBookingFacade2;
                location = "Colorado"; 
                roomClass = "Economy"; 
                maxPrice = 100;         
                break;
            case 3:
                selectedFacade = hotelBookingFacade3;
                location = "New York"; 
                roomClass = "Standart";  
                maxPrice = 450;         
                break;
            default:
                Console.WriteLine("non existing choice restart");
                return;
        }

        var hotels = selectedFacade.SearchHotel(location, roomClass, maxPrice);
        Console.WriteLine($"found {hotels.Count} hotel(s) in your price range.");

        if (hotels.Count > 0)
        {
            var hotel = hotels.First();
            Console.WriteLine($"selected Hotel: {hotel.Name} in {hotel.Location}");

            var booking = selectedFacade.MakeBooking(user.Id, hotel.Id, DateTime.Now, DateTime.Now.AddDays(3));
            Console.WriteLine($"booking confirmed! Booking ID: {booking.Id}");
        }
        else
        {
            Console.WriteLine("no hotels found within the given criteria.");
        }
    }
}
public interface IUserService
{
    User Register(string username, string password);
    User Login(string username, string password);
    User GetUserById(int userId);
}
public class UserManagementService : IUserService
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
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
public interface IHotelService
{
    List<Hotel> GetHotels();
    Hotel GetHotelById(int hotelId);
    List<Hotel> SearchHotels(string location, string roomClass, double maxPrice);
}
public class HotelService : IHotelService
{
    private readonly List<Hotel> _hotels = new List<Hotel>();
    private int _nextId = 1;
    public HotelService()
    {
        _hotels.Add(new Hotel { Id = _nextId++, Name = "Beach Resort", Location = "Malibu", RoomClass = "Luxury", PricePerNight = 300 });
        _hotels.Add(new Hotel { Id = _nextId++, Name = "Mountain Inn", Location = "Colorado", RoomClass = "Economy", PricePerNight = 200 });
        _hotels.Add(new Hotel { Id = _nextId++, Name = "City Center Hotel", Location = "New York", RoomClass = "Standart", PricePerNight = 450 });
    }
    public List<Hotel> GetHotels() => _hotels;
    public Hotel GetHotelById(int hotelId)
    {
        return _hotels.FirstOrDefault(h => h.Id == hotelId) ?? throw new Exception("Hotel not found.");
    }
    public List<Hotel> SearchHotels(string location, string roomClass, double maxPrice)
    {
        return _hotels.Where(h => h.Location.Contains(location, StringComparison.OrdinalIgnoreCase) &&
                                   h.RoomClass.Equals(roomClass, StringComparison.OrdinalIgnoreCase) &&
                                   h.PricePerNight <= maxPrice).ToList();
    }
}
public class Hotel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string RoomClass { get; set; }
    public double PricePerNight { get; set; }
}
public interface IBookingService
{
    Booking MakeBooking(int userId, int hotelId, DateTime checkInDate, DateTime checkOutDate);
    bool CheckRoomAvailability(int hotelId, DateTime checkInDate, DateTime checkOutDate);
}
public class BookingService : IBookingService
{
    private readonly IHotelService _hotelService;
    private readonly List<Booking> _bookings = new List<Booking>();
    private int _nextId = 1;
    public BookingService(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }
    public Booking MakeBooking(int userId, int hotelId, DateTime checkInDate, DateTime checkOutDate)
    {
        if (!CheckRoomAvailability(hotelId, checkInDate, checkOutDate))
        {
            throw new Exception("room not available for selected dates.");
        }
        var booking = new Booking
        {
            Id = _nextId++,
            UserId = userId,
            HotelId = hotelId,
            CheckInDate = checkInDate,
            CheckOutDate = checkOutDate,
            Status = "Confirmed"
        };
        _bookings.Add(booking);
        return booking;
    }
    public bool CheckRoomAvailability(int hotelId, DateTime checkInDate, DateTime checkOutDate)
    {
        return !_bookings.Any(b => b.HotelId == hotelId &&
                                   b.CheckInDate < checkOutDate &&
                                   b.CheckOutDate > checkInDate);
    }
}
public class Booking
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int HotelId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string Status { get; set; }
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
    void SendBookingConfirmation(int userId, int bookingId);
    void SendPreArrivalReminder(int userId, int bookingId);
}
public class NotificationService : INotificationService
{
    public void SendNotification(int userId, string message)
    {
        Console.WriteLine($"notification to User {userId}: {message}");
    }
    public void SendBookingConfirmation(int userId, int bookingId)
    {
        Console.WriteLine($"booking confirmation to User {userId}: your booking with ID {bookingId} is confirmed.");
    }
    public void SendPreArrivalReminder(int userId, int bookingId)
    {
        Console.WriteLine($"reminder to User {userId}: your booking with ID {bookingId} is coming up!");
    }
}
public class HotelBookingFacade
{
    private readonly IHotelService _hotelService;
    private readonly IBookingService _bookingService;
    private readonly IPaymentService _paymentService;
    private readonly INotificationService _notificationService;
    private readonly IUserService _userService;
    public HotelBookingFacade(IHotelService hotelService, IBookingService bookingService, IPaymentService paymentService, INotificationService notificationService, IUserService userService)
    {
        _hotelService = hotelService;
        _bookingService = bookingService;
        _paymentService = paymentService;
        _notificationService = notificationService;
        _userService = userService;
    }
    public List<Hotel> SearchHotel(string location, string roomClass, double maxPrice)
    {
        return _hotelService.SearchHotels(location, roomClass, maxPrice);
    }
    public Booking MakeBooking(int userId, int hotelId, DateTime checkInDate, DateTime checkOutDate)
    {
        var booking = _bookingService.MakeBooking(userId, hotelId, checkInDate, checkOutDate);
        _notificationService.SendBookingConfirmation(userId, booking.Id);
        return booking;
    }
    public bool CheckAvailability(int hotelId, DateTime checkInDate, DateTime checkOutDate)
    {
        return _bookingService.CheckRoomAvailability(hotelId, checkInDate, checkOutDate);
    }
}
