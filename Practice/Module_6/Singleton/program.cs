/*this was hard, this specific one was hard to code (cuz i am stupid), 
and adding datetime filtering was very hard*/

using System;
using System.IO;
using System.Text.Json;
/*Singleton*/
public class Program
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
        Task task4 = Task.Run(() => LogMessaging(LogLevel.Warning));
        Task task5 = Task.Run(() => LogMessaging(LogLevel.Error));
        Task.WaitAll(task4, task5);

        LogReader logReader = new LogReader();
        Console.WriteLine("\nlog from specific time:");
        logReader.LogLoader(DateTime.Parse("2024-10-16 18:40:00"), DateTime.Parse("2024-10-16 18:42:05"));
        /*for the 2nd parse i couldnt figure out a way to get a data from the log itself cause after the file rotation everythin in the original 
         log file is transported to a new rotation, do get the specific date from log i have to make the code be able to read the entire direcotry and all of its files
        named Log i think*/
        Console.WriteLine("\nwarning lvl logs:");
        logReader.LogLoader(logLevel: LogLevel.Warning);

        /*logger.SetLevel(LogLevel.Warning);
        logger.Log("warning log msg", LogLevel.Warning);//1
        Logger.GetInstance().Log("info log msg", LogLevel.Info);//2*/
    }
    private static void LogMessaging(LogLevel level)
    {
        Logger logger = Logger.GetInstance();
        for (int i = 0; i < 6; i++)
        {
            logger.Log($"{level} msg {i + 1}", level);
        }
    }
}
public enum LogLevel
{
    Info, Warning, Error
}
public class Logger
{
    private Logger() { }
    private static Logger _logger;
    private static readonly object _lock = new object();
    public static LogLevel _level = LogLevel.Info;
    private string logPath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\6\txt\log.txt";
    private const long MaxSizeLog = 4096;/*had to change it to 4kb cuz 2kb kept getting full too fast with datetime filtering*/
    public static Logger GetInstance()
    {
        if (_logger == null)
        {
            lock (_lock)
            {
                if (_logger == null)
                    _logger = new Logger();
            }
        }
        return _logger;
    }
    public void SetLevel(LogLevel level)
    {
        lock (_lock)
        {
            _level = level;
            Console.WriteLine($"log lvl changed to: {_level}");
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
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {level} | {message}";
                File.AppendAllText(logPath, logEntry + Environment.NewLine);
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
            Console.WriteLine("config file have not been found.");
        }
    }
    public void LogFileRotation()
    {
        string newLogFileName = $"{Path.GetFileNameWithoutExtension(logPath)}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(logPath)}";
        string newLogFilePath = Path.Combine(Path.GetDirectoryName(logPath), newLogFileName);
        File.Move(logPath, newLogFilePath);
        Console.WriteLine($"new log file have been rotared: {newLogFilePath}");
    }
}
public class LogReader
{
    private string logPath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\6\txt\log.txt";
    public void LogLoader(DateTime? startTime = null, DateTime? endTime = null, LogLevel? logLevel = null)
    {
        if (File.Exists(logPath))
        {
            string[] logLines = File.ReadAllLines(logPath);
            foreach (var line in logLines)
            {
                string[] parts = line.Split('|');
                if (parts.Length >= 3)
                {
                    DateTime logTime = DateTime.Parse(parts[0].Trim());
                    LogLevel level = Enum.Parse<LogLevel>(parts[1].Trim());

                    if (startTime.HasValue && logTime < startTime.Value)
                        continue;
                    if (endTime.HasValue && logTime > endTime.Value)
                        continue;
                    if (logLevel.HasValue && level != logLevel.Value)
                        continue;

                    Console.WriteLine(line);
                }
            }
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
