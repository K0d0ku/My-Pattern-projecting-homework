using System;
public enum DocType
{
    Report, Resume, Letter, Invoice
}
public class program
{
    public static void Main(string[] args)
    {
        /*GetDocument(DocType.Report).Open();
        GetDocument(DocType.Resume).Open();
        GetDocument(DocType.Letter).Open();
        GetDocument(DocType.Invoice).Open();*/

        Console.WriteLine("Enter a Document Type to open: ");
        string UserChoice = Console.ReadLine();
        if (Enum.TryParse(UserChoice, true, out DocType docType))
        {
            GetDocument(docType).Open();
        }
        else
        {
            Console.WriteLine("this type of documenr does not exist");
        }
    }

    public static IDocument GetDocument(DocType docType)
    {
        DocumentCreator creator = null;
        IDocument document = null;
        switch (docType) 
        {
            case DocType.Report:
                creator = new ReportCreator();
                document = creator.CreateDocument();
                break;
            case DocType.Resume:
                creator = new ResumeCreator();
                document = creator.CreateDocument();
                break;
            case DocType.Letter:
                creator = new LetterCreator();
                document = creator.CreateDocument();
                break;
            case DocType.Invoice:
                creator = new InvoiceCreator();
                document = creator.CreateDocument();
                break;
            default:
                throw new Exception("WRONG TYPE OF DOCUMENT");
        }
        return document;
    }
}
public interface IDocument
{
    void Open();
}
public class Report : IDocument
{
    public void Open()
    {
        Console.WriteLine("Report open!");
    }
}
public class Resume : IDocument
{
    public void Open()
    {
        Console.WriteLine("Resume open!");
    }
}
public class Letter : IDocument
{
    public void Open()
    {
        Console.WriteLine("Letter open!");
    }
}
public class Invoice : IDocument
{
    public void Open()
    {
        Console.WriteLine("Invoice open!");
    }
}
public abstract class DocumentCreator
{
    public abstract IDocument CreateDocument();
}

public class ReportCreator : DocumentCreator
{
    public override IDocument CreateDocument()
    {
        return new Report();
    }
}
public class ResumeCreator : DocumentCreator
{
    public override IDocument CreateDocument()
    {
        return new Resume();
    }
}
public class LetterCreator : DocumentCreator
{
    public override IDocument CreateDocument()
    {
        return new Letter();
    }
}
public class InvoiceCreator : DocumentCreator
{
    public override IDocument CreateDocument()
    {
        return new Invoice();
    }
}
