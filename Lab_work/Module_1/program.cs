using System;
public class program
{
    public static void Main(string[] args)
    {
        Personal personal = new Personal();
        Worker worker1 = new Worker { Name = "Johnny Test" , ID = "2", Role = "Tester", HrsAmount = 40, PayForHRS = 80 }; /*3200*/
        Manager manager1 = new Manager { Name = "Ichigo Kurosaki", ID = "1", Role = "HR", SalaryFixed = 3200, Bonus = 200 }; /*3400*/
        Intern intern1 = new Intern { Name = "Kenpachi Zaraki", ID = "3", Role = "Intern", JobAmountDone = 80.0, PayPerJobDone = 30}; /*2400*/
        personal.AddEmployee(worker1);
        personal.AddEmployee(manager1);
        personal.AddEmployee(intern1);
        personal.CalculateSalaries();
    }
}
public class Employee
{
    public string Name { get; set; }
    public string ID { get; set; }
    public string Role { get; set; }
    public virtual int Salary()
    {
        return 0;
    }
}
public class Worker : Employee
{
    public int PayForHRS { get; set; }
    public int HrsAmount { get; set; }
    public override int Salary()
    {
        return PayForHRS * HrsAmount;
    }
}
public class Manager : Employee
{
    public int SalaryFixed { get; set; }
    public int Bonus { get; set; }
    public override int Salary()
    {
        return SalaryFixed + Bonus;
    }
}
public class Intern : Employee
{
    public int PayPerJobDone { get; set; }
    public double JobAmountDone { get; set; }
    public int TotalSalary { get; set; } = 100;
    public override int Salary()
    {
        double jobAmount = JobAmountDone * TotalSalary / 100;
        return (int)jobAmount * PayPerJobDone;
    }
}
public class Personal
{
    private List<Employee> employees = new List<Employee>();
    public void AddEmployee(Employee employee)
    {
        employees.Add(employee);
    }
    public void CalculateSalaries()
    {
        foreach (var employee in employees)
        {
            Console.WriteLine($"{employee.Name} ({employee.Role}): {employee.Salary()}");
        }
    }
}
