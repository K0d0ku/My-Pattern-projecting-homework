/*in my defence, the singleton part of the lab work has almost and even less , but exactly the same task that we did in practice singleton*/

using System;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json;
/*Singleton*/
public class program
{
    public static void Main(string[] args)
    {
        Logger logger = Logger.GetInstance();
        logger.ConfigLoader();

        /*Console.WriteLine($"current log level is : {Logger._level}");
        logger.Log("warning msg", LogLevel.Warning);
        logger.Log("info msg", LogLevel.Info);*/

        Task task1 = Task.Run(() => LogMessaging(LogLevel.Info));
        Task task2 = Task.Run(() => LogMessaging(LogLevel.Warning));
        Task task3 = Task.Run(() => LogMessaging(LogLevel.Error));
        Task.WaitAll(task1, task2, task3);

        logger.SetLevel(LogLevel.Warning);
        Task task4 = Task.Run(() => LogMessaging(LogLevel.Warning));/*wont*/
        Task task5 = Task.Run(() => LogMessaging(LogLevel.Error));/*will*/
        Task.WaitAll(task4, task5);

        LogReader logReader = new LogReader();
        logReader.LogLoader();

        /*logger.SetLevel(LogLevel.Warning);
        logger.Log("warning log msg", LogLevel.Warning);//1
        Logger.GetInstance().Log("info log msg", LogLevel.Info);//2*/
    }
    private static void LogMessaging(LogLevel level)
    {
        Logger logger = Logger.GetInstance();
        logger.SetLevel(level);
        for (int i = 0; i < 6; i++)
        {
            Logger.GetInstance().Log($"{level} msg {i + 1}", level);
        }
    }
 }
public enum LogLevel
{
    Info, Warning, Error
}
public class Logger
{
    private Logger() 
    {
        //
    }
    private static Logger _logger;
    public static LogLevel _level = LogLevel.Info;
    
    private static readonly object _lock = new object();

    private string logPath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\6\txt\log.txt";
    private const long MaxSizeLog = 2048;
    public static Logger GetInstance()
    {
        if (_logger == null)
            _logger = new Logger();
        return _logger;
    }
    public void SetLevel (LogLevel level)
    {
        lock (_lock)
        {
            _level = level;
            Console.WriteLine($"level changed to: {_level}");
        }
    } 
    public void Log(string message, LogLevel level)
    {
        if (_level <= level)
        {
            lock (_lock)
            {
                if (File.Exists(logPath) && new FileInfo(logPath).Length >= MaxSizeLog)
                {
                    LogFileRotation();
                }
                /*File.WriteAllText(@"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\6\txt\log.txt", "New logs \n");*/
                /*the line that is commented in here only writes new changes with every new execution of code, i was using that untill i got to "Дополнительные задачи:"*/
                File.AppendAllText(logPath, level + " | " + message + Environment.NewLine);
            }
        }
        /*if (_level == level)
        {
            File.AppendAllText(@"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\6\txt\log.txt",
                level + " | " + message + Environment.NewLine);
        }*/
    }
    public void ConfigLoader()
    {
        string configPath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\6\PR_6_Module_6\Config.json";
        if (File.Exists(configPath))
        {
            string json = File.ReadAllText(configPath);
            var config = JsonSerializer.Deserialize<LogConfig>(json);
            if (config != null)
            {
                _level = Enum.Parse<LogLevel>(config.LogLevel);
            }
        }
        else
        {
            Console.WriteLine("config file have not been found");
        }
    }
    public void LogFileRotation()
    {
        string newLogFileName = $"{Path.GetFileNameWithoutExtension(logPath)}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(logPath)}";
        string newLogFilePath = Path.Combine(Path.GetDirectoryName(logPath), newLogFileName);
        File.Move(logPath, newLogFilePath);
        Console.WriteLine($"new log file have been crated: {newLogFilePath}");
    }
}
public class LogReader
{
    public void LogLoader()
    {
        string logPath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\6\txt\log.txt";
        if (File.Exists(logPath))
        {
            string txt = File.ReadAllText(logPath);
            Console.WriteLine(txt);
        }
        else
        {
            Console.WriteLine("log file does not exist");
        }
    }
}
public class LogConfig
{
    public string LogLevel { get; set; }
}
