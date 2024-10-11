using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
public class Program
{
    public static void Main(string[] args)
    {
        Document doc1 = new Document { Title = "doc1", Content = "content for the document 1", };
        doc1.Sections.Add(new Section { Title = "Section1", Text = "text for section 1" });
        doc1.Images.Add(new Image { URL = "https://noimage.com/noimage.png" });

        Table table = new Table { Title = "table 1" };
        table.Data.Add(new string[] { "a1", "b1" });
        table.Data.Add(new string[] { "a2", "b2" });
        doc1.Tables.Add(table);

        DocumentManager manager = new DocumentManager();
        var newSections = new List<Section>
        {
            new Section { Title = "cloned Section", Text = "new section for clone." }
        };
        var newImages = new List<Image>
        {
            new Image { URL = "https://newimage.com/newimage.png" }
        };
        var newTables = new List<Table>
        {
            new Table { Title = "New Table" }
        };
        newTables[0].Data.Add(new string[] { "c1", "d1" });
        newTables[0].Data.Add(new string[] { "c2", "d2" });

        Document cloneDocument = doc1.CloneWithChanges(newSections, newImages, newTables);

        Console.WriteLine("doc1 Document Title: " + doc1.Title);
        Console.WriteLine("doc1 Document Content: " + doc1.Content);
        Console.WriteLine("doc1 Section 1 Text: " + doc1.Sections[0].Text);
        Console.WriteLine("doc1 Image URL: " + doc1.Images[0].URL);
        Console.WriteLine("doc1 table Data: " + string.Join(", ", doc1.Tables[0].Data.SelectMany(row => row)));

        Console.WriteLine("\ncloned Document Title: " + cloneDocument.Title);
        Console.WriteLine("cloned Document Content: " + cloneDocument.Content);
        Console.WriteLine("cloned Section 1 Text: " + cloneDocument.Sections[0].Text);
        Console.WriteLine("cloned Image URL: " + cloneDocument.Images[1].URL);
        Console.WriteLine("New Section in Cloned Document: " + cloneDocument.Sections[1].Text);
        Console.WriteLine("cloned table Data: " + string.Join(", ", cloneDocument.Tables[1].Data.SelectMany(row => row)));

        string filePath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\lab\6\txt\document.json";
        manager.SaveDocument(doc1, filePath);
        Console.WriteLine("\ndocument saved to " + filePath);

        Document loadedDocument = manager.LoadDocument(filePath);
        Console.WriteLine("loaded Document Title: " + loadedDocument.Title);
        Console.WriteLine("loaded Document Content: " + loadedDocument.Content);
        Console.WriteLine("loaded Section 1 Text: " + loadedDocument.Sections[0].Text);
        Console.WriteLine("loaded Image URL: " + loadedDocument.Images[0].URL);
        Console.WriteLine("loaded table Data: " + string.Join(", ", loadedDocument.Tables[0].Data.SelectMany(row => row)));
    }
}
public interface IPrototype
{
    IPrototype Clone();
}
public class Document : IPrototype
{
    public string Title { get; set; }
    public string Content { get; set; }
    public List<Section> Sections { get; set; } = new List<Section>();
    public List<Image> Images { get; set; } = new List<Image>();
    public List<Table> Tables { get; set; } = new List<Table>();
    public IPrototype Clone()
    {
        Document clone = this.MemberwiseClone() as Document;
        clone.Sections = this.Sections.Select(section => section.Clone() as Section).ToList();
        clone.Images = this.Images.Select(image => image.Clone() as Image).ToList();
        clone.Tables = this.Tables.Select(table => table.Clone() as Table).ToList();
        return clone;
    }
    public Document CloneWithChanges(List<Section> newSections, List<Image> newImages, List<Table> newTables)
    {
        Document clonedDoc = (Document)this.Clone();
        clonedDoc.Sections.AddRange(newSections);
        clonedDoc.Images.AddRange(newImages);
        clonedDoc.Tables.AddRange(newTables);
        return clonedDoc;
    }
}
public class Section : IPrototype
{
    public string Title { get; set; }
    public string Text { get; set; }
    public IPrototype Clone()
    {
        return this.MemberwiseClone() as IPrototype;
    }
}
public class Image : IPrototype
{
    public string URL { get; set; }
    public IPrototype Clone()
    {
        return this.MemberwiseClone() as IPrototype;
    }
}
public class Table : IPrototype
{
    public string Title { get; set; }
    public List<string[]> Data { get; set; } = new List<string[]>();
    public IPrototype Clone()
    {
        Table clone = this.MemberwiseClone() as Table;
        clone.Data = this.Data.Select(row => (string[])row.Clone()).ToList();
        return clone;
    }
}
public class DocumentManager
{
    public IPrototype CreateDocument(IPrototype prototype)
    {
        return prototype.Clone();
    }
    public void SaveDocument(Document document, string filePath)
    {
        var json = JsonSerializer.Serialize(document);
        File.WriteAllText(filePath, json);
    }
    public Document LoadDocument(string filePath)
    {
        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<Document>(json);
    }
}
