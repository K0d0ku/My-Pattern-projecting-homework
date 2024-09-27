/*Произведите корректную (правильную) по вашему мнению реализацию с применением принципа DRY:
Использование параметризованных методов
public void LogError(string message)
{
    Console.WriteLine($"ERROR: {message}");
}
public void LogWarning(string message)
{
    Console.WriteLine($"WARNING: {message}");
}
public void LogInfo(string message)
{
    Console.WriteLine($"INFO: {message}");
}*/

/*мой ответ*/
using System;
public class Program
{
    public static void Main()
    {
        Output log1 = new Output();
        log1.Error = "file have not been found";
        log1.Warning = "lack of any more memory";
        log1.Info = "the project has been updated succesfully";

        Output log2 = new Output();
        log2.Error = "network error";
        log2.Warning = "the CPU is overheating";
        log2.Info = "all systems are running smoothly";

        Console.WriteLine(log1.ToString());
        Console.WriteLine(log2.ToString());
    }
} /*для теста*/
public class Output
{
    public string Error { get; set; }
    public string Warning { get; set; }
    public string Info { get; set; }
    public override string ToString()
    {
        return $"Error: {Error}, \nWarning: {Warning}, \nInfo: {Info}\n";
    }
}
/*в коде на заданий используется 3 разные методы с одной и той же функцией вывода,
для моего кода я создал класс Output в котором есть 3 строки которые можно переписать,
в Main я создаю 2 разных Log для разных выводов*/
