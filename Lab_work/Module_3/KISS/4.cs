/*Произведите корректную (правильную) по вашему мнению реализацию с применением принципа KISS:
Избегание избыточного использования шаблонов проектирования
public class Singleton
{
    private static Singleton _instance;

    private Singleton() { }

    public static Singleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }
    }

    public void DoSomething()
    {
        Console.WriteLine("Doing something...");
    }
}

public class Client
{
    public void Execute()
    {
        Singleton.Instance.DoSomething();
    }
}*/

/*мой ответ*/
using System;
public class program
{
    public static void Main(string[] args)
    {
        Client client = new Client();
        client.Execute();
    }
}
public class NormalClassXD
{
    public NormalClassXD() { }
    public void DoSomething()
    {
        Console.WriteLine("Doing something...");
    }
}
public class Client
{
    public void Execute()
    {
        NormalClassXD normalClassInstance = new NormalClassXD();
        normalClassInstance.DoSomething();
    }
}
