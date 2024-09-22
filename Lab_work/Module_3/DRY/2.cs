Использование общих базовых классов
public class Car
{
    public void Start()
    {
        Console.WriteLine("Car is starting");
    }
    public void Stop()
    {
        Console.WriteLine("Car is stopping");
    }
}

public class Truck
{
    public void Start()
    {
        Console.WriteLine("Truck is starting");
    }
    public void Stop()
    {
        Console.WriteLine("Truck is stopping");
    }
}
