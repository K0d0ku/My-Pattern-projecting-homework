/*Произведите корректную (правильную) по вашему мнению реализацию с применением принципа Interface Segregation Principle, ISP:
Рассмотрим пример интерфейса IWorker, который объединяет слишком много методов:
public interface IWorker
{
    void Work();
    void Eat();
    void Sleep();
}

public class HumanWorker : IWorker
{
    public void Work()
    {
        // Логика работы
    }

    public void Eat()
    {
        // Логика питания
    }

    public void Sleep()
    {
        // Логика сна
    }
}

public class RobotWorker : IWorker
{
    public void Work()
    {
        // Логика работы
    }

    public void Eat()
    {
        // Робот не ест, но вынужден реализовать метод
        throw new NotImplementedException();
    }

    public void Sleep()
    {
        // Робот не спит, но вынужден реализовать метод
        throw new NotImplementedException();
    }
}
В этом примере класс RobotWorker вынужден реализовывать методы, которые ему не нужны (Eat и Sleep). Это нарушение принципа ISP.
Чтобы соблюсти принцип ISP, вам необходимо разделить интерфейс IWorker на несколько специализированных интерфейсов.
*/

/*мой ответ*/
using System;
public class Program
{
    public static void Main(string[] args)
    {
        IWorkable human = new HumanWorker();
        human.Work();
        IEatable humanEater = (IEatable)human;
        humanEater.Eat();
        ISleepable humanSleeper = (ISleepable)human;
        humanSleeper.Sleep();
        IWorkable robot = new RobotWorker();
        robot.Work();
    }
}
public interface IWorkable
{
    void Work();
}
public interface IEatable
{
    void Eat();
}
public interface ISleepable
{
    void Sleep();
}
public class HumanWorker : IWorkable, IEatable, ISleepable
{
    public void Work()
    {
        Console.WriteLine("the human is working.");
    }
    public void Eat()
    {
        Console.WriteLine("the human is eating.");
    }
    public void Sleep()
    {
        Console.WriteLine("the human is sleeping.");
    }
}
public class RobotWorker : IWorkable
{
    public void Work()
    {
        Console.WriteLine("the robot is working.");
    }
}
