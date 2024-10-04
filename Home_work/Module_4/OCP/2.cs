using System;
public class Employee
{
    public string Name { get; set; }
    public double BaseSalary { get; set; }
    public string EmployeeType { get; set; } // "Permanent", "Contract", "Intern"
}
public interface IEmployeeTypeSalary
{
    public double CalculateSalary(double BaseSalary);
    /*i could make this an abstract so ebery time this function is called it does not break DRY and
    on an abstract class i can store the method with the value too that is can be overridden ebery 
    time its called, i just noted that*/
}
public class EmployeePermanent : IEmployeeTypeSalary
{
    public double CalculateSalary(double BaseSalary)
    {
        return BaseSalary * 1.2;
    }
}
public class EmployeeContract : IEmployeeTypeSalary
{
    public double CalculateSalary(double BaseSalary) 
    {
        return BaseSalary * 1.1;
    }
}
public class EmployeeIntern : IEmployeeTypeSalary
{
    public double CalculateSalary (double BaseSalary) 
    {
        return BaseSalary * 0.8;
    }
}
public class EmployeeFreelancer : IEmployeeTypeSalary
{
    public double CalculateSalary(double BaseSalary)
    {
        return BaseSalary * 1.0;
    }
}
