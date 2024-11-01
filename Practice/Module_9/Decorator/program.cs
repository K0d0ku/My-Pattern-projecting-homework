/*output:
Sales Report:
filtering by dates from 10/23/2024 to 11/2/2024:
Date, Amount
10/30/2024, 200
10/28/2024, 150
 (exported to CSV at C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\9\reports\report_sales.csv) (exported to PDF at C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\9\reports\report_sales.pdf)

User Report:
sorting by UserID:
UserID, Name, RegistrationDate
1, Baha, 10/18/2024
2, Kuro, 10/3/2024
3, Uali, 10/23/2024
 (exported to CSV at C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\9\reports\report_users.csv) (exported to PDF at C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\9\reports\report_users.pdf)*/

/*i just rewrote the exporting method from practice 8 template, and for datetime sorting i just used datetime now and - 10 days from this date, 
so i dont have to manually write the datetime data for it to sort
*/
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using static UserReport;
/*Decorator*/
public class program
{
    public static void Main(string[] args)
    {
        string exportPath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\9\reports\report";

        List<Sale> salesData = new List<Sale>
        {
            new Sale { Date = DateTime.Now.AddDays(-12), Amount = 100 },
            new Sale { Date = DateTime.Now.AddDays(-3), Amount = 200 },
            new Sale { Date = DateTime.Now.AddDays(-5), Amount = 150 },
            new Sale { Date = DateTime.Now.AddDays(-10), Amount = 300 }
        };
        IReport salesReport = new SalesReport(salesData);
        salesReport = new DateFilterDecorator(salesReport, DateTime.Now.AddDays(-10), DateTime.Now);
        salesReport = new CsvExportDecorator(salesReport, exportPath + "_sales.csv");
        salesReport = new PdfExportDecorator(salesReport, exportPath + "_sales.pdf");
        Console.WriteLine("Sales Report:");
        Console.WriteLine(salesReport.Generate());

        List<User> userData = new List<User>
        {
            new User { UserID = 2, Name = "Kuro", RegistrationDate = DateTime.Now.AddDays(-30) },
            new User { UserID = 3, Name = "Uali", RegistrationDate = DateTime.Now.AddDays(-10) },
            new User { UserID = 1, Name = "Baha", RegistrationDate = DateTime.Now.AddDays(-15) }
        };
        IReport userReport = new UserReport(userData);
        userReport = new SortingDecorator(userReport, "UserID");
        userReport = new CsvExportDecorator(userReport, exportPath + "_users.csv");
        userReport = new PdfExportDecorator(userReport, exportPath + "_users.pdf");
        Console.WriteLine("\nUser Report:");
        Console.WriteLine(userReport.Generate());
    }
    /*if you have concerns why i sort then export, and not sort the exported data, id say that is because
     i did exactly by the steps of the task you provided me with, in steps we sort first then export*/
}
public interface IReport
{
    string Generate();
}
public class Sale
{   /*added this to actually export and sort the data*/
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
}
public class User
{   /*same reason as sale*/
    public int UserID { get; set; }
    public string Name { get; set; }
    public DateTime RegistrationDate { get; set; }
}
public class SalesReport : IReport
{
    private List<Sale> _salesData;
    public SalesReport(List<Sale> salesData)
    {
        _salesData = salesData;
    }
    public string Generate()
    {
        string reportData = "Date, Amount\n";
        foreach (var sale in _salesData)
        {
            reportData += $"{sale.Date.ToShortDateString()}, {sale.Amount}\n";
        }
        return reportData;
    }
    public List<Sale> GetFilteredSales(DateTime startDate, DateTime endDate)
    {
        return _salesData.FindAll(sale => sale.Date >= startDate && sale.Date <= endDate);
    }
}
public class UserReport : IReport
{
    private List<User> _userData;
    public UserReport(List<User> userData)
    {
        _userData = userData;
    }
    public string Generate()
    {
        string reportData = "UserID, Name, RegistrationDate\n";
        foreach (var user in _userData)
        {
            reportData += $"{user.UserID}, {user.Name}, {user.RegistrationDate.ToShortDateString()}\n";
        }
        return reportData;
    }
    public List<User> GetSortedUsers(string sortBy)
    {
        switch (sortBy)
        {
            case "UserID":
                _userData.Sort((u1, u2) => u1.UserID.CompareTo(u2.UserID));
                break;
            case "Name":
                _userData.Sort((u1, u2) => string.Compare(u1.Name, u2.Name));
                break;
            case "RegistrationDate":
                _userData.Sort((u1, u2) => u1.RegistrationDate.CompareTo(u2.RegistrationDate));
                break;
        }
        return _userData;
    }
}
public abstract class ReportDecorator : IReport
{
    protected IReport _report;

    public ReportDecorator(IReport report)
    {
        _report = report;
    }
    public virtual string Generate()
    {
        return _report.Generate();
    }
}
public class DateFilterDecorator : ReportDecorator
{
    private DateTime _startDate;
    private DateTime _endDate;
    public DateFilterDecorator(IReport report, DateTime startDate, DateTime endDate) : base(report)
    {
        _startDate = startDate;
        _endDate = endDate;
    }
     public override string Generate()
    {
        if (_report is SalesReport salesReport)
        {
            var filteredSales = salesReport.GetFilteredSales(_startDate, _endDate);
            string reportData = "Date, Amount\n";
            foreach (var sale in filteredSales)
            {
                reportData += $"{sale.Date.ToShortDateString()}, {sale.Amount}\n";
            }
            return $"filtering by dates from {_startDate.ToShortDateString()} to {_endDate.ToShortDateString()}:\n{reportData}";
        }
        return base.Generate();
    }
}
public class SortingDecorator : ReportDecorator
{
    private string _sortBy;
    public SortingDecorator(IReport report, string sortBy) : base(report)
    {
        _sortBy = sortBy;
    }
    public override string Generate()
    {
        if (_report is UserReport userReport)
        {
            userReport.GetSortedUsers(_sortBy);
        }
        string data = base.Generate();
        return $"sorting by {_sortBy}:\n{data}";
    }
}
public class CsvExportDecorator : ReportDecorator
{
    private string _filePath;
    public CsvExportDecorator(IReport report, string filePath) : base(report)
    {
        _filePath = filePath;
    }
    public override string Generate()
    {
        string data = base.Generate();
        File.WriteAllText(_filePath, data);
        return $"{data} (exported to CSV at {_filePath})";
    }
}
public class PdfExportDecorator : ReportDecorator
{
    private string _filePath;
    public PdfExportDecorator(IReport report, string filePath) : base(report)
    {
        _filePath = filePath;
    }
    public override string Generate()
    {
        string data = base.Generate();
        Document document = new Document();
        PdfWriter.GetInstance(document, new FileStream(_filePath, FileMode.Create));
        document.Open();
        document.Add(new Paragraph(data));
        document.Close();
        return $"{data} (exported to PDF at {_filePath})";
    }
}
