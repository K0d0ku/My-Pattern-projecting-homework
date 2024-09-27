/*Избегание ненужного использования LINQ
public void PrintPositiveNumbers(int[] numbers)
{
    var positiveNumbers = numbers.Where(n => n > 0).OrderBy(n => n).ToList();

    foreach (var number in positiveNumbers)
    {
        Console.WriteLine(number);
    }
}*/

/*мой ответ*/
using System;
using System.Collections.Generic;
public class Program
{
    public static void Main()
    {
        int[] numbers = { -2, 3, 1, -5, 0, 4, 2 };/*на этот раз я испольщовал готовые числа вместо вводных*/
        PrintPositiveNumbers(numbers);/*вызвано для вывода*/
    }
    public static void PrintPositiveNumbers(int[] numbers)/*создал целый отдельный метод чтобы не использовать LINQ*/
    {
        List<int> positiveNumbers = new List<int>();/*для добавления позитивных чисел*/
        foreach (var number in numbers)/*для цикла введенных чисел*/
        {
            if (number > 0)/*для того чтобы только числя больше 0 могли*/
            {
                positiveNumbers.Add(number);/*могли будут добавлены в список*/
            }
        }
        positiveNumbers.Sort(); /*сортирует в возрасаюшем порядке*/
        foreach (var number in positiveNumbers)/*для цикла отсортированных чисел*/
        {
            Console.WriteLine(number);/*вывод*/
        }
    }
}
