/*i would also recomend you to check: Online_delivery_service.cs in lab 10 facade*/

/*output:
settling down in the hotel
the room: 404 is available
made a reservation to room: 404
ordering: ribeye steak
setting the cleaning time for the room: 404, at 17:50
starting to clean the room: 404, at 17:50

organizing an event
ordering the conference room: 267
ordering equipment: projector
ordering equipment: microphone
ordering equipment: surround sound audio system

reserving table in restaurant
made a reservation to a table: 4
ordering: shrimp & sausage gumbo
ordering: triple cheese & bacon dauphinoise

cancelling the reservation
cancelling the reservation for room: 404
you dont have any reservations*/
using System;
/*facade*/
public class program
{
    public static void Main(string[] args)
    {
        RoomBookingSystem roomBookingSystem = new RoomBookingSystem();
        RestaurantSystem restaurantSystem = new RestaurantSystem();
        EventManagementSystem eventManagementSystem = new EventManagementSystem();
        CleaningService cleaningService = new CleaningService(roomBookingSystem);

        HotelFacade hotelFacade = new HotelFacade(roomBookingSystem, restaurantSystem, eventManagementSystem, cleaningService);
        hotelFacade.SettleDown();
        Console.WriteLine();
        hotelFacade.OrganizingEvent();
        Console.WriteLine();
        hotelFacade.BookinRestaurantTable();
        Console.WriteLine();
        hotelFacade.CancelReservation();
        Console.WriteLine();
    }
}
public class RoomBookingSystem
{
    public void MakeRoomReservation(int roomNum)
    {
        Console.WriteLine($"made a reservation to room: {roomNum}");
    }
    public void IsRoomAvailable(bool available, int roomNum)
    {
        if(available == true)
        {
            Console.WriteLine($"the room: {roomNum} is available");
        }
        else
        {
            Console.WriteLine($"the room: {roomNum} is not available");
        }
    }
    public void CancelRoomReservation(bool hasReservation, int roomNum)
    {
        if (hasReservation == true)
        {
            Console.WriteLine($"cancelling the reservation for room: {roomNum}");
        }
        else
        {
            Console.WriteLine("you dont have any reservations");
        }
    }
}
public class RestaurantSystem
{
    public void MakeTableReservation(int tableNum)
    {
        Console.WriteLine($"made a reservation to a table: {tableNum}");
    }
    public void OrderFood(string foodName)
    {
        Console.WriteLine($"ordering: {foodName}");
    }
}
public class EventManagementSystem
{
    public void OrderConferenceRoom(int conferenceRoomNum) 
    {
        Console.WriteLine($"ordering the conference room: {conferenceRoomNum}");
    }
    public void OrderEquipment(string equipmentName)
    {
        Console.WriteLine($"ordering equipment: {equipmentName}");
    }
}
public class CleaningService
{
    private readonly RoomBookingSystem _roomBookingSystem;
    public CleaningService(RoomBookingSystem roomBookingSystem)
    {
        _roomBookingSystem = roomBookingSystem;
    }
    public void SetCleaningTime(string cleaningTime, int roomNum) 
        => Console.WriteLine($"setting the cleaning time for the room: {roomNum}, at {cleaningTime}");
    public void CleaningRoom(bool roomIsNotClean, string cleaningTime, int roomNum)
    {
        if(roomIsNotClean == true)
        {
            Console.WriteLine($"starting to clean the room: {roomNum}, at {cleaningTime}");
        }
        else
        {
            Console.WriteLine($"the room: {roomNum} already have been cleaned at {cleaningTime}");
        }
    }
}
public class HotelFacade
{
    private readonly RoomBookingSystem _roomBookingSystem;
    private readonly RestaurantSystem _restaurantSystem;
    private readonly EventManagementSystem _eventManagementSystem;
    private readonly CleaningService _cleaningService;
    public HotelFacade(RoomBookingSystem roomBookingSystem, RestaurantSystem restaurantSystem, EventManagementSystem eventManagementSystem, CleaningService cleaningService)
    {
        _roomBookingSystem = roomBookingSystem;
        _restaurantSystem = restaurantSystem;
        _eventManagementSystem = eventManagementSystem;
        _cleaningService = cleaningService;
    }
    public void SettleDown()
    {
        Console.WriteLine("settling down in the hotel");
        _roomBookingSystem.IsRoomAvailable(true, 404);
        _roomBookingSystem.MakeRoomReservation(404);
        _restaurantSystem.OrderFood("ribeye steak");
        _cleaningService.SetCleaningTime("17:50", 404);
        _cleaningService.CleaningRoom(true, "17:50", 404);
    }
    public void OrganizingEvent()
    {
        Console.WriteLine("organizing an event");
        _eventManagementSystem.OrderConferenceRoom(267);
        _eventManagementSystem.OrderEquipment("projector");
        _eventManagementSystem.OrderEquipment("microphone");
        _eventManagementSystem.OrderEquipment("surround sound audio system");
    }
    public void BookinRestaurantTable() 
    {
        Console.WriteLine("reserving table in restaurant");
        _restaurantSystem.MakeTableReservation(4);
        _restaurantSystem.OrderFood("shrimp & sausage gumbo");
        _restaurantSystem.OrderFood("triple cheese & bacon dauphinoise");
    }
    public void CancelReservation()
    {
        Console.WriteLine("cancelling the reservation");
        _roomBookingSystem.CancelRoomReservation(true, 404);
        _roomBookingSystem.CancelRoomReservation(false, 404);
    }
}
