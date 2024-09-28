/*Излишняя параметризация методов
public class MathOperations
{
    public int Add(int a, int b, bool shouldLog = false)
    {
        int result = a + b;
        if (shouldLog)
        {
            Console.WriteLine($"Result: {result}");
        }
        return result;
    }
}*/

/*мой ответ*/
using System;
public class program
{
    public static void Main(string[] args)
    {
        MathOperations mathOperations = new MathOperations();
        int result = mathOperations.Add(3, 6);
        Console.WriteLine($"{3}+{6}={result}");
    }
    public class MathOperations
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
