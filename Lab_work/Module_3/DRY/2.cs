/*Произведите корректную (правильную) по вашему мнению реализацию с применением принципа DRY:
Использование общих базовых классов:
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
}*/

/*Мой ответ:*/
using System;
public class Program
{
    public static void Main()
    {
        Car myCar = new Car();
        myCar.start();
        myCar.stop();

        Truck myTruck = new Truck();
        myTruck.start();
        myTruck.stop();
    }
} /* - для теста */
public class vechicle
{
    public virtual void start()
    {
        Console.WriteLine("vechicle is starting");
    }
    public virtual void stop()
    {
        Console.WriteLine("vechicle is stopping");
    }
} /* я создал обший класс Vechicle и добавил в него 2 виртуальных метода start и stop 
чтобы перезаписать их в классах Car и Truck так как в начальном коде эти методы дублировались */
public class Car : vechicle
{
    public override void start()
    {
        Console.WriteLine("car is starting");
    }
    public override void stop()
    {
        Console.WriteLine("car is stopping");
    }
}
public class Truck : vechicle
{
    public override void start()
    {
        Console.WriteLine("truck is starting");
    }
    public override void stop()
    {
        Console.WriteLine("truck is stopping");
    }
}
/*
результаты теста:
car is starting
car is stopping
truck is starting
truck is stopping
*/
