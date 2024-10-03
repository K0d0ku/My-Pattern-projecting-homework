using System;
public enum TransportType{
    Car, Motorcycle, Plane, Bicycle
}
public class program
{
    public static void Main(string[] args) 
    {
        Console.WriteLine("Enter a Transport Type to open: ");
        string UserChoice = Console.ReadLine();
        if (Enum.TryParse(UserChoice, true, out TransportType transportType))
        {
            Transport transport = GetTransport(transportType);
            Console.WriteLine("Enter the Transport model: ");
            transport.Model = Console.ReadLine();
            Console.WriteLine("Enter the transport speed: ");
            if (int.TryParse(Console.ReadLine(), out int speed))
            {
                transport.speed = speed;
            }
            else
            {
                Console.WriteLine("Wrong speed entered");
                transport.speed = 0;
            }
            transport.Move();
            transport.FuelUp();
        }
        else
        {
            Console.WriteLine("this type of factory does not exist");
        }
    }
    public static Transport GetTransport(TransportType transportType)
    {
        TransportFactory creator = null;
        Transport transport = null; 
        switch (transportType)
        {
            case TransportType.Car:
                creator = new CarFactory();
                break;
            case TransportType.Motorcycle:
                creator = new MotorcycleFactory();
                break;
            case TransportType.Plane:
                creator = new PlaneFactory();
                break;
            case TransportType.Bicycle:
                creator = new BicycleFactory();
                break;
            default:
                throw new Exception("Wrong factory");
        }
        return creator.CreateTransport();
    }
}
public interface Transport
{
    void Move();
    void FuelUp();
    string Model { get; set; }
    int speed { get; set; } /*i could also use double but i didnt want to*/
}
public class Car : Transport
{
    public void Move()
    {
        Console.WriteLine("The car is moving");
    }
    public void FuelUp()
    {
        Console.WriteLine("The car is fueling up");
    }
    public string Model { get; set; }
    public int speed { get; set; }
}
public class Motorcycle : Transport
{
    public void Move()
    {
        Console.WriteLine("The motorcycle is moving");
    }
    public void FuelUp()
    {
        Console.WriteLine("The motorcycle is fueling up");
    }
    public string Model { get; set; }
    public int speed { get; set; }
}
public class Plane : Transport
{
    public void Move()
    {
        Console.WriteLine("The plane is moving");
    }
    public void FuelUp()
    {
        Console.WriteLine("The plane is fueling up");
    }
    public string Model { get; set; }
    public int speed { get; set; }
}
public class Bicycle : Transport
{
    public void Move()
    {
        Console.WriteLine("The bike is moving");
    }
    public void FuelUp()
    {
        Console.WriteLine("The bike does not need to fuel up");
    }
    public string Model { get; set; }
    public int speed { get; set; }
}
public abstract class TransportFactory
{
    public abstract Transport CreateTransport();
}
public class CarFactory : TransportFactory
{
    public override Transport CreateTransport()
    {
        return new Car();
    }
}
public class MotorcycleFactory : TransportFactory
{
    public override Transport CreateTransport()
    {
        return new Motorcycle();
    }
}
public class PlaneFactory : TransportFactory
{
    public override Transport CreateTransport()
    {
        return new Plane();
    }
}
public class BicycleFactory : TransportFactory
{
    public override Transport CreateTransport()
    {
        return new Bicycle();
    }
}
