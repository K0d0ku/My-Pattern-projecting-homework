using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
public class Program
{
    public static void Main(string[] args)
    {
        var style = new ReportStyle { BackgroundColor = "Black", FontColor = "White", FontSize = 12, FontFamily = "Comic Sans MS" };
        ReportDirector director = new ReportDirector();
        string exportPath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\lecture\6\reports\report";

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

        /*xml*/
        IReportBuilder xmlReportBuilder = new XmlReportBuilder();
        director.ConstructReport(xmlReportBuilder, style, exportPath);
        Report xmlReport = xmlReportBuilder.GetReport();
        xmlReport.Export("xml");

        /*txt update*/
        textReport.UpdateHeader("updated Text report header");
        textReport.UpdateContent("updated main content for Text report.");
        textReport.UpdateFooter("updated text report footer.");
        textReport.UpdateSection(0, "updated section 1", "updated content for section 1");
        textReport.Export("txt");

        /*html update*/
        htmlReport.UpdateHeader("updated HTML report header");
        htmlReport.UpdateContent("updated main content for HTML report.");
        htmlReport.UpdateFooter("updated html report footer.");
        htmlReport.UpdateSection(0, "updated section 1", "updated content for section 1");
        htmlReport.Export("html");

        /*xml update*/
        xmlReport.UpdateHeader("updated XML report header");
        xmlReport.UpdateContent("updated main content for XML report.");
        xmlReport.UpdateFooter("updated XML report footer.");
        xmlReport.UpdateSection(0, "updated section 1", "updated content for section 1");
        xmlReport.Export("xml");
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
public class XmlReportBuilder : IReportBuilder
{
    private Report _report = new Report();
    public void SetHeader(string header) => _report.Header = header;
    public void SetContent(string content) => _report.Content = content;
    public void SetFooter(string footer) => _report.Footer = footer;
    public void AddSection(string sectionName, string sectionContent)
    {
        _report.Sections.Add($"<section><name>{sectionName}</name><content>{sectionContent}</content></section>");
    }
    public void SetStyle(ReportStyle style) => _report.Style = style;
    public Report GetReport() => _report;
}
public class ReportStyle
{
    public string BackgroundColor { get; set; }
    public string FontColor { get; set; }
    public int FontSize { get; set; }
    public string FontFamily { get; set; }
}
public class Report
{
    public string Header { get; set; }
    public string Content { get; set; }
    public string Footer { get; set; }
    public List<string> Sections { get; set; } = new List<string>();
    public ReportStyle Style { get; set; }
    private string _exportPath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\lecture\6\reports";
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
            case "xml":
                ExportToXml();
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
    private void ExportToXml()
    {
        File.WriteAllText(ExportPath + ".xml", ToXml());
        Console.WriteLine($"XML report saved to {ExportPath}.xml");
    }
    public override string ToString()
    {
        return $"{Header}\n{Content}\n{Footer}\n{string.Join("\n", Sections)}";
    }
    private string ToHtml()
    {
        return $"<html><head><style>body {{ font-family: {Style.FontFamily}; }}</style></head><body><h1>{Header}</h1><p>{Content}</p><footer>{Footer}</footer></body></html>";
    }
    private string ToXml()
    {
        return $"<report><header>{Header}</header><content>{Content}</content><footer>{Footer}</footer><sections>{string.Join("", Sections)}</sections></report>";
    }
    public void UpdateHeader(string newHeader) => Header = newHeader;
    public void UpdateContent(string newContent) => Content = newContent;
    public void UpdateFooter(string newFooter) => Footer = newFooter;
    public void UpdateSection(int index, string newSectionName, string newSectionContent)
    {
        if (index >= 0 && index < Sections.Count)
        {
            Sections[index] = $"{newSectionName}: {newSectionContent}";
        }
        else
        {
            throw new IndexOutOfRangeException("invalid section index.");
        }
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
