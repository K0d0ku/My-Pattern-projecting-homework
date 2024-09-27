/*Использование общих конфигурационных настроек
public class DatabaseService
{
    public void Connect()
    {
        string connectionString = "Server=myServer;Database=myDb;User Id=myUser;Password=myPass;";
        // Логика подключения к базе данных
    }
}
public class LoggingService
{
    public void Log(string message)
    {
        string connectionString = "Server=myServer;Database=myDb;User Id=myUser;Password=myPass;";
        // Логика записи лога в базу данных
    }
}*/

/*мой ответ*/
using System;
public class Program
{
    public string connectionString = "Server=myServer;Database=myDb;User Id=myUser;Password=myPass;";
    public void Connect()
    {
        Console.WriteLine($"DatabaseService - {connectionString}");
    }
    public void Log(string message)
    {
        Console.WriteLine($"LoggingService - {connectionString}");
    }
    public static void Main()
    {
        Program program = new Program();
        program.Connect();
        program.Log("this text is here to provide a value for 'string message'" +
            " that is being used in 'void Log', without it the program wont run " +
            "because we are trying to run it where the 'void log' is using the 'message' parameter that has no value");
    }
}
/*
раз уж они используют одно и то же я просто сделал из connectionString общедоступную строку
и вызвал ее два раза 1 для DatabaseService, 2 для LoggingService*/
/*в строке "program.Log" в main я обясняю почему там стойт целый обзац:
этот текст здесь, чтобы предоставить значение для «string message», которое используется в 
«void Log», без него программа не будет работать, потому что мы пытаемся запустить ее там, 
где «void log» использует параметр «message», который не имеет значения
*/
