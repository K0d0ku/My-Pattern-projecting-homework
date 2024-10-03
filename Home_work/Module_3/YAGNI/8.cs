public class ReportGenerator
{
    public void GenerateReport(string format)
    {
        if (format == "PDF") 
        {
            // Генерация PDF отчета
        }
        else if (format == "Excel")
        {
            // Генерация Excel отчета
        }
        else if (format == "HTML")
        {
            // Генерация HTML отчета
        }
        else
        {
            Console.WriteLine("Unsupported formar");
        }
    }
}
