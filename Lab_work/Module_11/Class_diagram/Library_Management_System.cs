/*output:
Bakontea added C# in Depth to Programming.
Bakontea added Clean Code to Programming.
Bakontea added Design Patterns to Programming.
Bakontea added Cooking with Kuro to Cooking.
Bakontea added Creating a particle engine to Grapchics Programming.

category: Programming
Available Books:
- C# in Depth by Jon Skeet (ISBN: 123) - copies Available: 5/5
- Clean Code by Robert Martin (ISBN: 456) - copies Available: 2/2
- Design Patterns by GoF (ISBN: 789) - copies Available: 4/4
not Available Books:


category: Cooking
Available Books:
- Cooking with Kuro by Kuro Kodoku (ISBN: 101112) - copies Available: 2/2
not Available Books:


category: Grapchics Programming
Available Books:
- Creating a particle engine by Acerola (ISBN: 131415) - copies Available: 1/1
not Available Books:

Zhanibek borrowed C# in Depth.
Loan issued: Zhanibek borrowed C# in Depth on 11/20/2024 4:03:45 PM.
Adil borrowed C# in Depth.
Loan issued: Adil borrowed C# in Depth on 11/20/2024 4:03:45 PM.
Kuro borrowed Creating a particle engine.
Loan issued: Kuro borrowed Creating a particle engine on 11/20/2024 4:03:45 PM.

Creating a particle engine is not available for loan.

category: Programming
Available Books:
- C# in Depth by Jon Skeet (ISBN: 123) - copies Available: 3/5
- Clean Code by Robert Martin (ISBN: 456) - copies Available: 2/2
- Design Patterns by GoF (ISBN: 789) - copies Available: 4/4
not Available Books:


category: Cooking
Available Books:
- Cooking with Kuro by Kuro Kodoku (ISBN: 101112) - copies Available: 2/2
not Available Books:


category: Grapchics Programming
Available Books:
not Available Books:
- Creating a particle engine by Acerola (ISBN: 131415) - copies Available: 0/1

Zhanibek returned C# in Depth.
Loan completed: Zhanibek returned C# in Depth on 11/20/2024 4:03:45 PM.
Adil returned C# in Depth.
Loan completed: Adil returned C# in Depth on 11/20/2024 4:03:45 PM.
Kuro returned Creating a particle engine.
Loan completed: Kuro returned Creating a particle engine on 11/20/2024 4:03:45 PM.

category: Programming
Available Books:
- C# in Depth by Jon Skeet (ISBN: 123) - copies Available: 5/5
- Clean Code by Robert Martin (ISBN: 456) - copies Available: 2/2
- Design Patterns by GoF (ISBN: 789) - copies Available: 4/4
not Available Books:


category: Cooking
Available Books:
- Cooking with Kuro by Kuro Kodoku (ISBN: 101112) - copies Available: 2/2
not Available Books:


category: Grapchics Programming
Available Books:
- Creating a particle engine by Acerola (ISBN: 131415) - copies Available: 1/1
not Available Books:

*/

using System;
/*i wrote the code using composite pattern, cuz i thought it was the most fitting to the given task*/
public class program
{
    public static void Main(string[] args)
    {
        var book1 = new Book("C# in Depth", "Jon Skeet", "123", 5);
        var book2 = new Book("Clean Code", "Robert Martin", "456", 2);
        var book3 = new Book("Design Patterns", "GoF", "789", 4);
        var book4 = new Book("Cooking with Kuro", "Kuro Kodoku", "101112", 2);
        var book5 = new Book("Creating a particle engine", "Acerola", "131415", 1);

        var programmingCategory = new BookCategory("Programming");
        var cookingCategory = new BookCategory("Cooking");
        var graphicsProgrammingCategory = new BookCategory("Grapchics Programming");

        var librarian = new Librarian { Id = 1, Name = "Bakontea", Position = "Senior Librarian" };
        librarian.AddBook(book1, programmingCategory);
        librarian.AddBook(book2, programmingCategory);
        librarian.AddBook(book3, programmingCategory);
        librarian.AddBook(book4, cookingCategory);
        librarian.AddBook(book5, graphicsProgrammingCategory);

        Console.WriteLine(programmingCategory.GetDetails());
        Console.WriteLine(cookingCategory.GetDetails());
        Console.WriteLine(graphicsProgrammingCategory.GetDetails());

        var reader = new Reader { Id = 1, Name = "Zhanibek", Email = "no@gmail.com" };
        var loan1 = new Loan(book1, reader);
        loan1.IssueLoan(programmingCategory);

        var reader2 = new Reader { Id = 2, Name = "Adil", Email = "nah@gmail.com" };
        var loan2 = new Loan(book1, reader2);
        loan2.IssueLoan(programmingCategory);

        var reader3 = new Reader { Id = 3, Name = "Kuro", Email = "none@gmail.com" };
        var loan3 = new Loan(book5, reader3);
        loan3.IssueLoan(graphicsProgrammingCategory);

        var reader4 = new Reader { Id = 4, Name = "Jngu", Email = "null@gmail.com"};
        var loan4 = new Loan(book5, reader4);
        loan4.IssueLoan(graphicsProgrammingCategory);

        Console.WriteLine(programmingCategory.GetDetails());
        Console.WriteLine(cookingCategory.GetDetails());
        Console.WriteLine(graphicsProgrammingCategory.GetDetails());

        loan1.CompleteLoan(programmingCategory);
        loan2.CompleteLoan(programmingCategory);
        loan3.CompleteLoan(graphicsProgrammingCategory);

        Console.WriteLine(programmingCategory.GetDetails());
        Console.WriteLine(cookingCategory.GetDetails());
        Console.WriteLine(graphicsProgrammingCategory.GetDetails());
    }
}
public interface ILibraryItem
{
    string GetDetails();
}
public class Book : ILibraryItem
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int TotalCopies { get; private set; }
    public int AvailableCopies { get; private set; }
    public Book(string title, string author, string isbn, int totalCopies)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        TotalCopies = totalCopies;
        AvailableCopies = totalCopies;
    }
    public bool IsAvailable => AvailableCopies > 0;
    public void BorrowCopy()
    {
        if (IsAvailable)
        {
            AvailableCopies--;
        }
        else
        {
            Console.WriteLine($"{Title} is not available.");
        }
    }
    public void ReturnCopy()
    {
        if (AvailableCopies < TotalCopies)
        {
            AvailableCopies++;
        }
    }
    public string GetDetails()
    {
        return $"{Title} by {Author} (ISBN: {ISBN}) - " +
               $"copies Available: {AvailableCopies}/{TotalCopies}";
    }
}
public class BookCategory : ILibraryItem
{
    public string Name { get; set; }
    private List<Book> availableBooks = new List<Book>();
    private List<Book> notAvailableBooks = new List<Book>();
    public BookCategory(string name)
    {
        Name = name;
    }
    public void AddBook(Book book)
    {
        if (book.IsAvailable)
        {
            availableBooks.Add(book);
        }
        else
        {
            notAvailableBooks.Add(book);
        }
    }
    public void RemoveBook(Book book)
    {
        availableBooks.Remove(book);
        notAvailableBooks.Remove(book);
    }
    public void UpdateBookStatus(Book book)
    {
        if (book.IsAvailable)
        {
            notAvailableBooks.Remove(book);
            if (!availableBooks.Contains(book))
            {
                availableBooks.Add(book);
            }
        }
        else
        {
            availableBooks.Remove(book);
            if (!notAvailableBooks.Contains(book))
            {
                notAvailableBooks.Add(book);
            }
        }
    }
    public string GetDetails()
    {
        string details = $"\ncategory: {Name}\nAvailable Books:\n";
        foreach (var book in availableBooks)
        {
            details += $"- {book.GetDetails()}\n";
        }
        details += "not Available Books:\n";
        foreach (var book in notAvailableBooks)
        {
            details += $"- {book.GetDetails()}\n";
        }
        return details;
    }
}
public class Reader
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public void BorrowBook(Book book, BookCategory category)
    {
        if (book.IsAvailable)
        {
            book.BorrowCopy();
            category.UpdateBookStatus(book);
            Console.WriteLine($"{Name} borrowed {book.Title}.");
        }
        else
        {
            Console.WriteLine($"{book.Title} is not available.");
        }
    }
    public void ReturnBook(Book book, BookCategory category)
    {
        book.ReturnCopy();
        category.UpdateBookStatus(book);
        Console.WriteLine($"{Name} returned {book.Title}.");
    }
}
public class Librarian
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public void AddBook(Book book, BookCategory category)
    {
        category.AddBook(book);
        Console.WriteLine($"{Name} added {book.Title} to {category.Name}.");
    }
    public void RemoveBook(Book book, BookCategory category)
    {
        category.RemoveBook(book);
        Console.WriteLine($"{Name} removed {book.Title} from {category.Name}.");
    }
}
public class Loan
{
    public Book Book { get; private set; }
    public Reader Reader { get; private set; }
    public DateTime LoanDate { get; private set; }
    public DateTime? ReturnDate { get; private set; }
    public Loan(Book book, Reader reader)
    {
        Book = book;
        Reader = reader;
        LoanDate = DateTime.Now;
        ReturnDate = null;
    }
    public void IssueLoan(BookCategory category)
    {
        if (Book.IsAvailable)
        {
            Reader.BorrowBook(Book, category);
            Console.WriteLine($"Loan issued: {Reader.Name} borrowed {Book.Title} on {LoanDate}.");
        }
        else
        {
            Console.WriteLine($"\n{Book.Title} is not available for loan.");
        }
    }
    public void CompleteLoan(BookCategory category)
    {
        if (ReturnDate == null)
        {
            Reader.ReturnBook(Book, category);
            ReturnDate = DateTime.Now;
            Console.WriteLine($"Loan completed: {Reader.Name} returned {Book.Title} on {ReturnDate}.");
        }
        else
        {
            Console.WriteLine($"Loan already completed for {Book.Title}.");
        }
    }
}
