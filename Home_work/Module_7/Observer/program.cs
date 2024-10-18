using System;
public class program
{
    public static void Main(string[] args)
    {
        CurrencyExchange currencyExchange = new CurrencyExchange();
        var reportObserver = new ReportingObserver("KZT", 1.7, 0.2);
        var notifObserver = new NotificationOnserver("USD", 2.6);
        currencyExchange.Attach(reportObserver);
        Console.WriteLine(reportObserver);
        currencyExchange.Attach(notifObserver);
        Console.WriteLine(notifObserver);
        Console.WriteLine("updating echange rates...");
        currencyExchange.UpdateExchangeRate("USD", 2.3);
        currencyExchange.UpdateExchangeRate("KZT", 2.0);
        Console.WriteLine("removing notification observer...");
        currencyExchange.Detach(notifObserver);
        currencyExchange.UpdateExchangeRate("USD", 2.7);
        currencyExchange.UpdateExchangeRate("KZT", 1.4);
        Console.WriteLine("reattachin notification observer...");
        currencyExchange.Attach(notifObserver);
        currencyExchange.UpdateExchangeRate("USD", 2.9);
        currencyExchange.UpdateExchangeRate("KZT", 0.8);
    }
}
public interface IObserver
{
    public void Update(string currency, double rate);
}
public interface ISubject
{
    public void Attach(IObserver observer);
    public void Detach(IObserver observer);
    public void Notify(string currency, double rate);
}
public class CurrencyExchange : ISubject
{
    private Dictionary<string, double> _exchangerate = new Dictionary<string, double>();
    private List<IObserver> _observers = new List<IObserver>();
    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }
    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }
    public void Notify(string currency, double rate)
    {
        foreach (var observer in _observers) 
        {
            observer.Update(currency, rate);
        }
    }
    public void UpdateExchangeRate(string currency, double rate)
    {
        _exchangerate[currency] = rate;
        Notify(currency, rate);
    }
}
public class CurrencyObserver : IObserver
{
    private string _name;
    public CurrencyObserver(string name)
    {
        _name = name;
    }
    public void Update(string currency, double rate)
    {
        Console.WriteLine($"{_name} got an update: {currency} rate is {rate}");
    }
}
public class ReportingObserver : IObserver
{
    private string _currency;
    private double _highPoint;
    private double _lowPoint;
    public ReportingObserver(string currency, double highPoint, double lowPoint)
    {
        _currency = currency;
        _highPoint = highPoint;
        _lowPoint = lowPoint;
    }
    public void Update(string currency, double rate)
    {
        if (currency == _currency)
        {
            if (rate >= _highPoint)
            {
                Console.WriteLine($"{_currency} exchange rate is at a high point: {rate}");
            }
            else if (rate <= _lowPoint)
            {
                Console.WriteLine($"{_currency} exchange rate is at a low point: {rate}");
            }
        }
    }
    public override string ToString()
    {
        return $"reporting observer: currency={_currency}, highPoint={_highPoint}, lowPoint={_lowPoint}";
    }
}
public class NotificationOnserver : IObserver
{
    private string _currency;
    private double _changeLim;
    public NotificationOnserver(string currency, double changeLim)
    {
        _currency = currency;
        _changeLim = changeLim;
    }
    public void Update(string currency, double rate)
    {
        if (currency == _currency)
        {
            if (rate >= _changeLim)
            {
                Console.WriteLine($"notification: {currency} exchange rate has increased to {rate}");
            }
            else
            {
                Console.WriteLine($"notification: {currency} exchange rate has decreased to {rate}");
            }
        }
    }
    public override string ToString()
    {
        return $"notification observer: currency={_currency}, significantly changed={_changeLim}";
    }
}
