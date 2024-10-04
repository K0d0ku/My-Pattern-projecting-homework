using System;
public interface IPrinter
{
    void Print(string content);
}
public interface IScanner
{
    void Scan(string content);
}
public interface IFax
{
    void Fax(string content);
}

public class AllInOnePrinter : IPrinter
{
    public void Print(string content)
    {
        Console.WriteLine("Printing: " + content);
    }

    public void Scan(string content)
    {
        Console.WriteLine("Scanning: " + content);
    }

    public void Fax(string content)
    {
        Console.WriteLine("Faxing: " + content);
    }
}

public class BasicPrinter : IPrinter
{
    public void Print(string content)
    {
        Console.WriteLine("Printing: " + content);
    }
}
