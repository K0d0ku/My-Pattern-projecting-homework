/*i would also recomend you to check: FlourDough_Ingredients.cs in lab 10 composite*/

/*output:
Organization Structure:
- Department: Human Resources [Position: Head of HR]
--- Employee: Uali [Position: Manager, Salary: 5000]
- Department: Technology [Position: Head of Technology]
--- Employee: Kuro [Position: Developer, Salary: 3000]
--- Employee: Baha [Position: Designer, Salary: 3500]
--- Employee: Biba [Position: FS Dev, Salary: 2800]
--- Department: Software [Position: Head of Software]
----- Employee: Kuro [Position: Developer, Salary: 3000]
--- Department: Quality Assurance [Position: Head of QA]
----- Employee: Biba [Position: FS Dev, Salary: 2800]
Tech Department Budget: 15100
Tech Department Employee Count: 5
Tech Department Budget After Salary Change: 17100
Kuro
All Employees in Tech Department:
- Employee: Kuro [Position: Developer, Salary: 4000]
- Employee: Baha [Position: Designer, Salary: 3500]
- Employee: Biba [Position: FS Dev, Salary: 2800]
--- Employee: Kuro [Position: Developer, Salary: 4000]
--- Employee: Biba [Position: FS Dev, Salary: 2800]*/

using System;
/*Composite*/
public class program
{
    public static void Main(string[] args)
    {
        Employee emp1 = new Employee("Uali", "Manager", 5000);
        Employee emp2 = new Employee("Kuro", "Developer", 3000);
        Employee emp3 = new Employee("Baha", "Designer", 3500);
        Employee emp4 = new Employee("Biba", "FS Dev", 2800);

        Department hrDept = new Department("Human Resources", "Head of HR");
        Department techDept = new Department("Technology", "Head of Technology");

        Department softwareDept = new Department("Software", "Head of Software");
        Department qaDept = new Department("Quality Assurance", "Head of QA");

        hrDept.Add(emp1);
        techDept.Add(emp2);
        techDept.Add(emp3);
        techDept.Add(emp4);

        softwareDept.Add(emp2);
        qaDept.Add(emp4);

        techDept.Add(softwareDept);
        techDept.Add(qaDept);

        Console.WriteLine("Organization Structure:");
        hrDept.Display(1);
        techDept.Display(1);

        Console.WriteLine("Tech Department Budget: " + techDept.GetBudget());
        Console.WriteLine("Tech Department Employee Count: " + techDept.GetEmployeeCount());

        emp2.SetSalary(4000);
        Console.WriteLine("Tech Department Budget After Salary Change: " + techDept.GetBudget());

        var foundEmployee = techDept.FindEmployeeByName("Kuro");
        Console.WriteLine(foundEmployee?.Name ?? "Employee not found");

        Console.WriteLine("All Employees in Tech Department:");
        techDept.ListAllEmployees();
    }
}
public abstract class OrganizationComponent
{
    protected string _name;
    protected string _position;
    public string Name => _name;
    public string Position => _position;
    public OrganizationComponent(string name, string position)
    {
        _name = name;
        _position = position;
    }
    public abstract void Display(int depth);
    public abstract decimal GetBudget();
    public abstract int GetEmployeeCount();
}
public class Employee : OrganizationComponent
{
    private decimal _salary;
    public Employee(string name, string position, decimal salary)
        : base(name, position)
    {
        _salary = salary;
    }
    public override void Display(int depth)
    {
        Console.WriteLine(new string('-', depth) + " Employee: " + Name + " [Position: " + Position + ", Salary: " + _salary + "]");
    }
    public override decimal GetBudget()
    {
        return _salary;
    }
    public override int GetEmployeeCount()
    {
        return 1;
    }
    public void SetSalary(decimal newSalary)
    {
        _salary = newSalary;
    }
}
public class Department : OrganizationComponent
{
    private List<OrganizationComponent> _subComponents = new List<OrganizationComponent>();
    public Department(string name, string position)
        : base(name, position)
    { }
    public void Add(OrganizationComponent component)
    {
        _subComponents.Add(component);
    }
    public void Remove(OrganizationComponent component)
    {
        _subComponents.Remove(component);
    }
    public override void Display(int depth)
    {
        Console.WriteLine(new string('-', depth) + " Department: " + Name + " [Position: " + Position + "]");
        foreach (var component in _subComponents)
        {
            component.Display(depth + 2);
        }
    }
    public override decimal GetBudget()
    {
        decimal totalBudget = 0;
        foreach (var component in _subComponents)
        {
            totalBudget += component.GetBudget();
        }
        return totalBudget;
    }
    public override int GetEmployeeCount()
    {
        int totalEmployees = 0;
        foreach (var component in _subComponents)
        {
            totalEmployees += component.GetEmployeeCount();
        }
        return totalEmployees;
    }
    public OrganizationComponent FindEmployeeByName(string name)
    {
        foreach (var component in _subComponents)
        {
            if (component.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                return component;
            if (component is Department department)
            {
                var result = department.FindEmployeeByName(name);
                if (result != null)
                    return result;
            }
        }
        return null;
    }
    public void ListAllEmployees(int depth = 1)
    {
        foreach (var component in _subComponents)
        {
            if (component is Employee employee)
            {
                employee.Display(depth);
            }
            if (component is Department department)
            {
                department.ListAllEmployees(depth + 2);
            }
        }
    }
}
