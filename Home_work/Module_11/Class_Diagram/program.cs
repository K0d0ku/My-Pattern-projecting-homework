/*output:
Bakontea added C# in Depth to Programming.
Bakontea added Clean Code to Programming.
Bakontea added Design Patterns to Programming.
Bakontea added Cooking with Kuro to Cooking.
Bakontea added Creating a particle engine to Graphics Programming.

Category: Programming
Available Books:
- C# in Depth by Jon Skeet (ISBN: 123) - copies Available: 5/5
- Clean Code by Robert Martin (ISBN: 456) - copies Available: 2/2
- Design Patterns by GoF (ISBN: 789) - copies Available: 4/4
Rented Books:


Category: Cooking
Available Books:
- Cooking with Kuro by Kuro Kodoku (ISBN: 101112) - copies Available: 2/2
Rented Books:


Category: Graphics Programming
Available Books:
- Creating a particle engine by Acerola (ISBN: 131415) - copies Available: 1/1
Rented Books:

Zhanibek borrowed C# in Depth.
Loan issued: Zhanibek borrowed C# in Depth on 11/21/2024 4:00:08 AM.
Zhanibek borrowed Clean Code.
Loan issued: Zhanibek borrowed Clean Code on 11/21/2024 4:00:08 AM.
Zhanibek borrowed Design Patterns.
Loan issued: Zhanibek borrowed Design Patterns on 11/21/2024 4:00:08 AM.
Adil borrowed Cooking with Kuro.
Loan issued: Adil borrowed Cooking with Kuro on 11/21/2024 4:00:08 AM.

Search Results for 'Acerola':
Creating a particle engine by Acerola (ISBN: 131415) - copies Available: 1/1

Zhanibek returned C# in Depth.
Loan completed: Zhanibek returned C# in Depth on 11/21/2024 4:00:08 AM.
Zhanibek returned Clean Code.
Loan completed: Zhanibek returned Clean Code on 11/21/2024 4:00:08 AM.
Zhanibek returned Design Patterns.
Loan completed: Zhanibek returned Design Patterns on 11/21/2024 4:00:08 AM.

Category: Programming
Available Books:
- C# in Depth by Jon Skeet (ISBN: 123) - copies Available: 5/5
- Clean Code by Robert Martin (ISBN: 456) - copies Available: 2/2
- Design Patterns by GoF (ISBN: 789) - copies Available: 4/4
Rented Books:


Category: Cooking
Available Books:
- Cooking with Kuro by Kuro Kodoku (ISBN: 101112) - copies Available: 1/2
Rented Books:


Category: Graphics Programming
Available Books:
- Creating a particle engine by Acerola (ISBN: 131415) - copies Available: 1/1
Rented Books:

*/

using System;
using System.Collections.Generic;
using System.Linq;
/*long story short i just rewrote what i created in lab 11 task 1, hence thats why there is no user entered search through terminal*/
public class Program
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
        var graphicsProgrammingCategory = new BookCategory("Graphics Programming");

        var librarian = new Librarian { Id = 1, Name = "Bakontea", Position = "Senior Librarian" };
        librarian.AddBook(book1, programmingCategory);
        librarian.AddBook(book2, programmingCategory);
        librarian.AddBook(book3, programmingCategory);
        librarian.AddBook(book4, cookingCategory);
        librarian.AddBook(book5, graphicsProgrammingCategory);

        Console.WriteLine(programmingCategory.GetDetails());
        Console.WriteLine(cookingCategory.GetDetails());
        Console.WriteLine(graphicsProgrammingCategory.GetDetails());

        var reader = new Reader { Id = 1, Name = "Zhanibek", Email = "no@gmail.com", MaxBooksAllowed = 3 };

        var loan1 = new Loan(book1, reader);
        loan1.IssueLoan(programmingCategory);
        var loan2 = new Loan(book2, reader);
        loan2.IssueLoan(programmingCategory);
        var loan3 = new Loan(book3, reader);
        loan3.IssueLoan(programmingCategory);

        var reader2 = new Reader { Id = 2, Name = "Adil", Email = "nah@gmail.com", MaxBooksAllowed = 2 };
        var loan4 = new Loan(book4, reader2);
        loan4.IssueLoan(cookingCategory);

        var searchResults = graphicsProgrammingCategory.SearchBooks("Acerola");
        Console.WriteLine("\nSearch Results for 'Acerola':");
        foreach (var book in searchResults)
        {
            Console.WriteLine(book.GetDetails());
            Console.WriteLine();
        }

        loan1.CompleteLoan(programmingCategory);
        loan2.CompleteLoan(programmingCategory);
        loan3.CompleteLoan(programmingCategory);

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
    public List<Book> SearchBooks(string searchTerm)
    {
        return availableBooks.Concat(notAvailableBooks)
                             .Where(book => book.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                            book.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                             .ToList();
    }
    public string GetDetails()
    {
        string details = $"\nCategory: {Name}\nAvailable Books:\n";
        foreach (var book in availableBooks)
        {
            details += $"- {book.GetDetails()}\n";
        }
        details += "Rented Books:\n";
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
    public int MaxBooksAllowed { get; set; }
    private int _booksRentedCount = 0;
    public void BorrowBook(Book book, BookCategory category)
    {
        if (_booksRentedCount >= MaxBooksAllowed)
        {
            Console.WriteLine($"{Name} cannot borrow more than {MaxBooksAllowed} books at a time.");
            return;
        }
        if (book.IsAvailable)
        {
            book.BorrowCopy();
            category.UpdateBookStatus(book);
            _booksRentedCount++;
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
        _booksRentedCount--;
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
