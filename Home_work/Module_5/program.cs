using System;
using System.Diagnostics;

public enum TransportType
{
    Car, Motorcycle, Truck, Bus, EScooter
}
public class program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter a Transport Type to open: ");
        string UserChoice = Console.ReadLine();
        if (Enum.TryParse(UserChoice, true, out TransportType transportType))
        {
            IVechicle transport = GetTransport(transportType);
            if (transportType == TransportType.Car)
            {
                var car = (Car)transport;
                car.Drive();
                car.Refuel();
                Console.WriteLine($"The car has {car.AmountOfDoors} doors.");
                Console.WriteLine("Enter the Car model: ");
                car.Model = Console.ReadLine();
                Console.WriteLine($"The model of the car is '{car.Model}'.");
            }
            else if (transportType == TransportType.Motorcycle)
            {
                var motorcycle = (Motorcycle)transport;
                motorcycle.Drive();
                motorcycle.Refuel();
                Console.WriteLine($"Box availability: {motorcycle.BoxAvailability}");
                Console.WriteLine("Enter the motorcycle category: ");
                motorcycle.Category = Console.ReadLine();
                Console.WriteLine($"The category of the motorcycle is '{motorcycle.Category}'.");
            }
            else if (transportType == TransportType.Truck)
            {
                var truck = (Truck)transport;
                truck.Drive();
                truck.Refuel();
                Console.WriteLine($"The truck can carry {truck.CarryWeight} kg.");
                Console.WriteLine("Enter the fuel capacity: ");
                string userinput = Console.ReadLine();
                if (double.TryParse(userinput, out double Fuelcapacity))
                {
                    truck.FuelCapacity = Fuelcapacity;
                    Console.WriteLine($"The fuel capacity of the truck is '{truck.FuelCapacity}l'.");
                }
                else
                {
                    Console.WriteLine("Wrong input for fuel capacity");
                }
            }
            else if (transportType == TransportType.Bus)
            {
                var bus = (Bus)transport;
                bus.Drive();
                bus.Refuel();
                Console.WriteLine($"The bus has a limit of {bus.MaxAmountOfPassangers} passengers.");
                Console.WriteLine("Enter the bus type: ");
                bus.Type = Console.ReadLine();
                Console.WriteLine($"The type of the bus is '{bus.Type}'.");
            }
            else if (transportType == TransportType.EScooter)
            {
                var eScooter = (EScooter)transport;
                eScooter.Drive();
                eScooter.Refuel();
                Console.WriteLine($"The maximum allowed speed is {eScooter.MaxAllowedSpeed}.");
                Console.WriteLine("Enter the Escooter brand: ");
                eScooter.Brand = Console.ReadLine();
                Console.WriteLine($"The brand of escooter is '{eScooter.Brand}'.");
            }
        }
        else
        {
            Console.WriteLine("this type of factory does not exist");
        }
    }
    public static IVechicle GetTransport(TransportType transportType)
    {
        VechicletFactory creator = null;
        IVechicle transport = null;
        switch (transportType)
        {
            case TransportType.Car:
                creator = new CarFactory();
                break;
            case TransportType.Motorcycle:
                creator = new MotorcycleFactory();
                break;
            case TransportType.Truck:
                creator = new TruckFactory();
                break;
            case TransportType.Bus:
                creator = new BusFactory();
                break;
            case TransportType.EScooter:
                creator = new EScooterFactory();
                break;
            default:
                throw new Exception("Wrong factory");
        }
        return creator.CreateTransport();
    }
}
public interface IVechicle
{
    void Drive();
    void Refuel();
}
public class Car : IVechicle
{
    public void Drive()
    {
        Console.WriteLine("The car is Driving");
    }
    public void Refuel()
    {
        Console.WriteLine("The car is fueling up");
    }
    public int AmountOfDoors { get; set; } = 4;
    public string Model { get; set; }
}
public class Motorcycle : IVechicle
{
    public void Drive()
    {
        Console.WriteLine("The motorcycle is Driving");
    }
    public void Refuel()
    {
        Console.WriteLine("The motorcycle is fueling up");
    }
    public bool BoxAvailability { get; set; } = false;
    public string Category { get; set; }
}
public class Truck : IVechicle
{
    public void Drive()
    {
        Console.WriteLine("The truck is Driving");
    }
    public void Refuel()
    {
        Console.WriteLine("The truck is fueling up");
    }
    public double CarryWeight { get; set; } = 4000.00;
    public double FuelCapacity { get; set; }
}
public class Bus : IVechicle
{
    public void Drive()
    {
        Console.WriteLine("The bus is Driving");
    }
    public void Refuel()
    {
        Console.WriteLine("The bus is fueling up");
    }
    public int MaxAmountOfPassangers { get; set; } = 70;
    public string Type { get; set; }
}
public class EScooter : IVechicle
{
    public void Drive()
    {
        Console.WriteLine("The electric scooter is Driving");
    }
    public void Refuel()
    {
        Console.WriteLine("The electric scoorer does not need to fuel up");
    }
    public double MaxAllowedSpeed { get; set; } = 25.50;
    public string Brand { get; set; }
}
public abstract class VechicletFactory
{
    public abstract IVechicle CreateTransport();
}
public class CarFactory : VechicletFactory
{
    public override IVechicle CreateTransport()
    {
        return new Car();
    }
}
public class MotorcycleFactory : VechicletFactory
{
    public override IVechicle CreateTransport()
    {
        return new Motorcycle();
    }
}
public class TruckFactory : VechicletFactory
{
    public override IVechicle CreateTransport()
    {
        return new Truck();
    }
}
public class BusFactory : VechicletFactory
{
    public override IVechicle CreateTransport()
    {
        return new Bus();
    }
}
public class EScooterFactory : VechicletFactory
{
    public override IVechicle CreateTransport()
    {
        return new EScooter();
    }
}
