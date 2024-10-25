/*output:
added section: section 1 - content for section 1 in PDF.
added section: section 2 - content for section 2 in PDF.
PDF report saved to C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\8\reports\PDF report\report.pdf
added section: section 1 - content for section 1 in Excel.
added section: section 2 - content for section 2 in Excel.
Excel report saved to C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\8\reports\Excel (csv) report.csv
added section: section 1 - <p>content for section 1 in HTML.</p>
added section: section 2 - <p>content for section 2 in HTML.</p>
HTML report saved to C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\8\reports\HTML report.html*/
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System;
public class program
{
    public static void Main(string[] args)
    {
        var style = new ReportStyle { BackgroundColor = "White", FontColor = "Black", FontSize = 12 };
        string exportPath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\8\reports";

        ReportGenerator pdfReportGenerator = new PdfReport();
        pdfReportGenerator.GenerateReport(style, exportPath + "\\PDF report");

        ReportGenerator excelReportGenerator = new ExcelReport();
        excelReportGenerator.GenerateReport(style, exportPath + "\\Excel (csv) report");

        ReportGenerator htmlReportGenerator = new HtmlReport();
        htmlReportGenerator.GenerateReport(style, exportPath + "\\HTML report");
    }
}
/*i just partially rewrote the code from practice 6 builder to practice 8 template*/
public abstract class ReportGenerator
{
    protected ReportStyle Style;
    protected string ExportPath;
    public void GenerateReport(ReportStyle style, string exportPath)
    {
        Style = style;
        ExportPath = exportPath;
        SetHeader();
        SetContent();
        AddSections();
        SetFooter();
        Export();
    }
    protected abstract void SetHeader();
    protected abstract void SetContent();
    protected abstract void AddSections();
    protected abstract void SetFooter();
    protected abstract void Export();
}
public class ReportStyle
{
    public string BackgroundColor { get; set; }
    public string FontColor { get; set; }
    public int FontSize { get; set; }
}
public class Report
{
    public string Header { get; set; }
    public string Content { get; set; }
    public string Footer { get; set; }
    private List<(string Title, string Content)> sections = new List<(string Title, string Content)>();
    public void AddSection(string title, string content)
    {
        sections.Add((title, content));
        Console.WriteLine($"added section: {title} - {content}");
    }
    public List<(string Title, string Content)> GetSections() => sections;
    public void DisplaySections()
    {
        foreach (var section in sections)
        {
            Console.WriteLine($"section Title: {section.Title}\nsection Content: {section.Content}\n");
        }
    }
    public override string ToString()
    {
        string report = $"{Header}\n{Content}\n{Footer}\n";
        foreach (var section in sections)
        {
            report += $"{section.Title}\n{section.Content}\n";
        }
        return report;
    }
}
public class PdfReport : ReportGenerator
{
    private Report _report = new Report();
    protected override void SetHeader()
    {
        _report.Header = "PDF report header";
    }
    protected override void SetContent()
    {
        _report.Content = "this is the content of the PDF report.";
    }
    protected override void AddSections()
    {
        _report.AddSection("section 1", "content for section 1 in PDF.");
        _report.AddSection("section 2", "content for section 2 in PDF.");
    }
    protected override void SetFooter()
    {
        _report.Footer = "PDF report footer";
    }
    protected override void Export()
    {   /*i had to rewrite this exact exporting method like 4 times or something
         every time when i tried to exec the code it would error me with systemio path error
        so i make it create a folder called PDF report then export into it*/
        string filePath = Path.Combine(ExportPath, "report.pdf");
        string directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        using (var document = new Document())
        {
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();
            document.Add(new Paragraph(_report.Header));
            document.Add(new Paragraph(_report.Content));
            foreach (var section in _report.GetSections())
            {
                document.Add(new Paragraph(section.Title));
                document.Add(new Paragraph(section.Content));
            }
            document.Add(new Paragraph(_report.Footer));
            document.Close();
        }
        Console.WriteLine($"PDF report saved to {filePath}");
    }
}
public class ExcelReport : ReportGenerator
{
    private Report _report = new Report();
    protected override void SetHeader()
    {
        _report.Header = "Excel report header";
    }
    protected override void SetContent()
    {
        _report.Content = "this is the content of the Excel report.";
    }
    protected override void AddSections()
    {
        _report.AddSection("section 1", "content for section 1 in Excel.");
        _report.AddSection("section 2", "content for section 2 in Excel.");
    }
    protected override void SetFooter()
    {
        _report.Footer = "Excel report footer";
    }
    protected override void Export()
    {   /*i aint gonna lie, i jsut rewrite this excel exporting method from stackoverflow, cuz i couldnt figure out a way myself*/
        string csvPath = Path.Combine(ExportPath + ".csv");
        string directoryPath = Path.GetDirectoryName(csvPath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        using (var writer = new StreamWriter(csvPath))
        {
            writer.WriteLine("header,content");
            writer.WriteLine($"\"{_report.Header}\",\"{_report.Content}\"");

            foreach (var section in _report.GetSections())
            {
                writer.WriteLine($"\"{section.Title}\",\"{section.Content}\"");
            }
            writer.WriteLine($"\"{_report.Footer}\",\"\"");
        }
        Console.WriteLine($"Excel report saved to {csvPath}");
    }
}
public class HtmlReport : ReportGenerator
{
    private Report _report = new Report();
    protected override void SetHeader()
    {
        _report.Header = "<h1>HTML report header</h1>";
    }
    protected override void SetContent()
    {
        _report.Content = "<p>this is the content of the HTML report.</p>";
    }
    protected override void AddSections()
    {
        _report.AddSection("section 1", "<p>content for section 1 in HTML.</p>");
        _report.AddSection("section 2", "<p>content for section 2 in HTML.</p>");
    }
    protected override void SetFooter()
    {
        _report.Footer = "<footer>HTML report footer</footer>";
    }
    protected override void Export()
    {
        string htmlPath = Path.Combine(ExportPath + ".html");
        string directoryPath = Path.GetDirectoryName(htmlPath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        using (var writer = new StreamWriter(htmlPath))
        {
            writer.WriteLine("<html>");
            writer.WriteLine("<head><title>HTML report</title></head>");
            writer.WriteLine("<body>");
            writer.WriteLine(_report.Header);
            writer.WriteLine(_report.Content);
            foreach (var section in _report.GetSections())
            {
                writer.WriteLine($"<h2>{section.Title}</h2>");
                writer.WriteLine($"<p>{section.Content}</p>");
            }
            writer.WriteLine(_report.Footer);
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }
        Console.WriteLine($"HTML report saved to {htmlPath}");
    }
}
