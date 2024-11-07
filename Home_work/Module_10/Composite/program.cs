/*output:
Added File : File1.txt
Added File : File2.txt
Added File : SubFile1.txt
Added Directory : SubDirectory
- Directory: Root
--- File: File1.txt [Size: 150 bytes]
--- File: File2.txt [Size: 200 bytes]
--- Directory: SubDirectory
----- File: SubFile1.txt [Size: 50 bytes]
size of the root Directory is: 400 bytes*/

using System;
/*Composite*/
public class program
{
    public static void Main(string[] args)
    {
        File file1 = new File("File1.txt", 150);
        File file2 = new File("File2.txt", 200);

        Directory root = new Directory("Root");
        Directory subDir = new Directory("SubDirectory");
        File subFile1 = new File("SubFile1.txt", 50);

        root.Add(file1);
        root.Add(file2);
        subDir.Add(subFile1);
        root.Add(subDir);
        root.Display(1);

        Console.WriteLine("size of the root Directory is: " + root.GetSize() + " bytes");
    }
}
public abstract class FileSystemComponent
{
    protected string _name;
    public string Name => _name;
    public FileSystemComponent(string name)
    {
        _name = name;
    }
    public abstract void Display(int depth);
    public abstract long GetSize();
}
public class File : FileSystemComponent
{
    private long _size;
    public File(string name, long size) : base(name)
    {
        _size = size;
    }
    public override void Display(int depth)
    {
        Console.WriteLine(new string('-', depth) + " File: " + Name + " [Size: " + _size + " bytes]");  // Accessing Name property
    }
    public override long GetSize()
    {
        return _size;
    }
}
public class Directory : FileSystemComponent
{
    private List<FileSystemComponent> _children = new List<FileSystemComponent>();
    public Directory(string name) : base(name)
    {
    }
    public void Add(FileSystemComponent component)
    {
        if (!_children.Contains(component))
        {
            _children.Add(component);
            Console.WriteLine($"Added {component.GetType().Name} : {component.Name}");
        }
        else
        {
            Console.WriteLine($"{component.Name} is already added.");
        }
    }
    public void Remove(FileSystemComponent component)
    {
        if (_children.Contains(component))
        {
            _children.Remove(component);
            Console.WriteLine($"Removed {component.GetType().Name} : {component.Name}");
        }
        else
        {
            Console.WriteLine($"{component.Name} not found to remove.");
        }
    }
    public override void Display(int depth)
    {
        Console.WriteLine(new string('-', depth) + " Directory: " + Name);
        foreach (var component in _children)
        {
            component.Display(depth + 2);
        }
    }
    public override long GetSize()
    {
        long totalSize = 0;
        foreach (var component in _children)
        {
            totalSize += component.GetSize();
        }
        return totalSize;
    }
}
