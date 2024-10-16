/*this was not easy and before you read this (or after), you may noticed that there is no export for pdf, i didnt do it because it requiered 
me to install iTextSharp a library or somethin, through out the visual stduio projects nuget manager, which i did not want to, and this was the only way that
i could find on inteernet*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
public class Program
{
    public static void Main(string[] args)
    {
        var style = new ReportStyle { BackgroundColor = "White", FontColor = "Black", FontSize = 12 };
        ReportDirector director = new ReportDirector();
        string exportPath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\6\reports\report";

        /*txt*/
        IReportBuilder textReportBuilder = new TextReportBuilder();
        director.ConstructReport(textReportBuilder, style, exportPath);
        Report textReport = textReportBuilder.GetReport();
        textReport.Export("txt");

        /*html*/
        IReportBuilder htmlReportBuilder = new HtmlReportBuilder();
        director.ConstructReport(htmlReportBuilder, style, exportPath);
        Report htmlReport = htmlReportBuilder.GetReport();
        htmlReport.Export("html");

        /*json*/
        IReportBuilder jsonReportBuilder = new JsonReportBuilder();
        director.ConstructReport(jsonReportBuilder, style, exportPath);
        Report jsonReport = jsonReportBuilder.GetReport();
        jsonReport.Export("json");
    }
}
public interface IReportBuilder
{
    void SetHeader(string header);
    void SetContent(string content);
    void SetFooter(string footer);
    void AddSection(string sectionName, string sectionContent);
    void SetStyle(ReportStyle style);
    Report GetReport();
}
public class TextReportBuilder : IReportBuilder
{
    private Report _report = new Report();
    public void SetHeader(string header) => _report.Header = header;
    public void SetContent(string content) => _report.Content = content;
    public void SetFooter(string footer) => _report.Footer = footer;
    public void AddSection(string sectionName, string sectionContent)
    {
        _report.Sections.Add($"{sectionName}: {sectionContent}");
    }
    public void SetStyle(ReportStyle style) => _report.Style = style;
    public Report GetReport() => _report;
}
public class HtmlReportBuilder : IReportBuilder
{
    private Report _report = new Report();
    public void SetHeader(string header) => _report.Header = $"<h1>{header}</h1>";
    public void SetContent(string content) => _report.Content = $"<p>{content}</p>";
    public void SetFooter(string footer) => _report.Footer = $"<footer>{footer}</footer>";
    public void AddSection(string sectionName, string sectionContent)
    {
        _report.Sections.Add($"<h2>{sectionName}</h2><p>{sectionContent}</p>");
    }
    public void SetStyle(ReportStyle style) => _report.Style = style;
    public Report GetReport() => _report;
}
public class PdfReportBuilder : IReportBuilder
{
    private Report _report = new Report();
    public void SetHeader(string header) { }
    public void SetContent(string content) { }
    public void SetFooter(string footer) { }
    public void AddSection(string sectionName, string sectionContent) { }
    public void SetStyle(ReportStyle style) { }
    public Report GetReport() => _report;
}
public class JsonReportBuilder : IReportBuilder
{
    private Report _report = new Report();
    public void SetHeader(string header) => _report.Header = header;
    public void SetContent(string content) => _report.Content = content;
    public void SetFooter(string footer) => _report.Footer = footer;
    public void AddSection(string sectionName, string sectionContent)
    {
        _report.Sections.Add($"{sectionName}: {sectionContent}");
    }
    public void SetStyle(ReportStyle style) => _report.Style = style;
    public Report GetReport() => _report;
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
    public List<string> Sections { get; set; } = new List<string>();
    public ReportStyle Style { get; set; }
    private string _exportPath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\6\reports";
    public string ExportPath
    {
        get => _exportPath;
        set => _exportPath = value;
    }
    public void Export(string format)
    {
        switch (format.ToLower())
        {
            case "txt":
                ExportToText();
                break;
            case "html":
                ExportToHtml();
                break;
            case "json":
                ExportToJson();
                break;
            default:
                throw new ArgumentException("format doesnt exist.");
        }
    }
    private void ExportToText()
    {
        File.WriteAllText(ExportPath + ".txt", ToString());
        Console.WriteLine($"Text report saved to {ExportPath}.txt");
    }
    private void ExportToHtml()
    {
        File.WriteAllText(ExportPath + ".html", ToHtml());
        Console.WriteLine($"HTML report saved to {ExportPath}.html");
    }
    private void ExportToJson()
    {
        string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(ExportPath + ".json", json);
        Console.WriteLine($"JSON report saved to {ExportPath}.json");
    }
    public override string ToString()
    {
        return $"{Header}\n{Content}\n{Footer}\n{string.Join("\n", Sections)}";
    }
    private string ToHtml()
    {
        return $"<html><body><h1>{Header}</h1><p>{Content}</p><footer>{Footer}</footer></body></html>";
    }
}
public class ReportDirector
{
    public void ConstructReport(IReportBuilder builder, ReportStyle style, string exportPath)
    {
        builder.SetStyle(style);
        builder.SetHeader("report header");
        builder.SetContent("main content for report.");
        builder.AddSection("section 1", "content for section 1");
        builder.AddSection("section 2", "content for section 2");
        builder.SetFooter("report footer");

        Report report = builder.GetReport();
        report.ExportPath = exportPath;
    }
}
