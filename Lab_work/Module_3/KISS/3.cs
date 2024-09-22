Произведите корректную (правильную) по вашему мнению реализацию с применением принципа KISS:
Избегание чрезмерного использования абстракций
public interface IOperation
{
    void Execute();
}

public class AdditionOperation : IOperation
{
    private int _a;
    private int _b;

    public AdditionOperation(int a, int b)
    {
        _a = a;
        _b = b;
    }

    public void Execute()
    {
        Console.WriteLine(_a + _b);
    }
}

public class Calculator
{
    public void PerformOperation(IOperation operation)
    {
        operation.Execute();
    }
}

Мой ответ:
using System;
public class Program
{
    public static void Main()
    {
        var calculator = new Calculator();
        calculator.add(9, 3);
    }
} /* - для теста */
public class Calculator
{
    public void add(int a, int b)
    {
        Console.WriteLine(a+b);
    }
}
/* 
уж какой то OVERKILL для сложение двух чисел, сначало создали интерфеис Ioperation с методом Execute, потом создали класс AdditionOperation
который реализует IOperation, где создали конструктор AdditionOperation для того чтобы приватные _a и _b можно было сложить в методе Execute в том же классе,
после этого создали класс Calculator чтобы вызвать сложенное на Execute с класса AdditionOperation
/*
/*
результат теста:
12
*/
