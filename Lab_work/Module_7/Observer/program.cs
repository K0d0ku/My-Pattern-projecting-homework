using System;
class Program
{   /*answers for question are below*/
    static void Main(string[] args)
    {
        WeatherStation weatherStation = new WeatherStation();

        // Создаем несколько наблюдателей
        WeatherDisplay mobileApp = new WeatherDisplay("Mobilnoe prilozhenye");
        WeatherDisplay digitalBillboard = new WeatherDisplay("electronnoe tablo");
        EmailNotification emailNotification = new EmailNotification("random@gmail.com"); /*email*/

        // Регистрируем наблюдателей в системе
        weatherStation.RegisterObserver(mobileApp);
        weatherStation.RegisterObserver(digitalBillboard);
        weatherStation.RegisterObserver(emailNotification); /*email*/

        // Изменяем температуру на станции, что приводит к уведомлению наблюдателей
        weatherStation.SetTemperature(25.0f);
        weatherStation.SetTemperature(30.0f);

        // Убираем один из дисплеев и снова меняем температуру
        weatherStation.RemoveObserver(digitalBillboard);
        weatherStation.SetTemperature(28.0f);

        weatherStation.RemoveObserver(digitalBillboard); /*for error*/
        weatherStation.SetTemperature(60.0f);
    }
}
public interface IObserver
{
    void Update(float temperature);
}
public interface ISubject
{
    void RegisterObserver(IObserver observer);  // Регистрация наблюдателя
    void RemoveObserver(IObserver observer);    // Удаление наблюдателя
    void NotifyObservers();                     // Уведомление всех наблюдателей
}
public class WeatherStation : ISubject
{
    private List<IObserver> observers;
    private float temperature;
    public WeatherStation()
    {
        observers = new List<IObserver>();
    }
    public void RegisterObserver(IObserver observer)
    {
        /*observers.Add(observer);*/
        if (!observers.Contains(observer)) /*my code for existin observer handling*/
        {
            observers.Add(observer);
        }
        else
        {
            Console.WriteLine("observer uzhe sushestvuyet");
        }
    }
    public void RemoveObserver(IObserver observer)
    {
        /*observers.Remove(observer);*/
        if (observers.Contains(observer)) /*same here but for non exstin observer*/
        {
            observers.Remove(observer);
        }
        else
        {
            Console.WriteLine("observer ne sushestvuyet.");
        }
    }
    public void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.Update(temperature);
        }
    }
    public void SetTemperature(float newTemperature)
    {
        if (newTemperature < -50 || newTemperature > 50)
        {
            Console.WriteLine("oshibka ne pravilnaya temperatura");
        }
        Console.WriteLine($"izmenenie temperatury: {newTemperature}°C");
        temperature = newTemperature;
        NotifyObservers();
    }
}
public class WeatherDisplay : IObserver
{
    private string _name;
    public WeatherDisplay(string name)
    {
        _name = name;
    }
    public void Update(float temperature)
    {
        Console.WriteLine($"{_name} pokazyvaet novuy temperaturu: {temperature}°C");
    }
}
public class EmailNotification : IObserver /*my code*/
{
    private string _email;
    public EmailNotification(string email)
    {
        _email = email;
    }
    public void Update(float temperature)
    {
        Console.WriteLine($"otprovlayu email na {_email}: novaya temperatura ravna: {temperature}°C");
    }
}
/*answers for question:
 Вопросы для самопроверки:
1.	Какие преимущества дает использование паттерна "Наблюдатель" в данной системе?
расширяемость и динамическое управление наблюдателями
2.	Как можно изменить список наблюдателей, не изменяя код субъекта?
список наблюдателей можно динамический изменить во время реализаций без изменений кода субьекта
3.	Какие изменения можно внести в реализацию, чтобы сделать систему асинхронной?
чтобы сделать систему асинхронной, не меняя существенно ее структуру, можно ввести async и await
*/
