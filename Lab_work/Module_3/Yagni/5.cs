/*Произведите корректную (правильную) по вашему мнению реализацию с применением принципа YAGNI:

Избыточное создание абстракций
public interface IShape
{
    double CalculateArea();
}

public class Circle : IShape
{
    private double _radius;

    public Circle(double radius)
    {
        _radius = radius;
    }

    public double CalculateArea()
    {
        return Math.PI * _radius * _radius;
    }
}

public class Square : IShape
{
    private double _side;

    public Square(double side)
    {
        _side = side;
    }

    public double CalculateArea()
    {
        return _side * _side;
    }
}*/
/*мой ответ*/
using System;
public class Circle
{
    private double _radius;

    public Circle(double radius)
    {
        _radius = radius;
    }

    public double CalculateArea()
    {
        return Math.PI * _radius * _radius;
    }
}
public class Square
{
    private double _side;

    public Square(double side)
    {
        _side = side;
    }

    public double CalculateArea()
    {
        return _side * _side;
    }
}
/*расчет площядей можно сделать без interface*/
