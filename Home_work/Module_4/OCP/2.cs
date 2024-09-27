/*Произведите корректную (правильную) по вашему мнению реализацию с применением принципа Open-Closed Principle, OCP:
Расчет зарплаты сотрудников
В этом примере класс EmployeeSalaryCalculator нарушает принцип OCP, так как для добавления новой логики расчета зарплаты приходится изменять код метода CalculateSalary.
public class Employee
{
    public string Name { get; set; }
    public double BaseSalary { get; set; }
    public string EmployeeType { get; set; } // "Permanent", "Contract", "Intern"
}

public class EmployeeSalaryCalculator
{
    public double CalculateSalary(Employee employee)
    {
        if (employee.EmployeeType == "Permanent")
        {
            return employee.BaseSalary * 1.2; // Permanent employee gets 20% bonus
        }
        else if (employee.EmployeeType == "Contract")
        {
            return employee.BaseSalary * 1.1; // Contract employee gets 10% bonus
        }
        else if (employee.EmployeeType == "Intern")
        {
            return employee.BaseSalary * 0.8; // Intern gets 80% of the base salary
        }
        else
        {
            throw new NotSupportedException("Employee type not supported");
        }
    }
}
Проблемы:
•	Если нужно добавить новый тип сотрудника, например, "Freelancer", придется изменить метод CalculateSalary. Это нарушает принцип OCP, так как мы изменяем уже существующий код, что может привести к ошибкам.
*/
