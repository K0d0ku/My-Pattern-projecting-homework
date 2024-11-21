/*output:
Zhanibek Khan issued the book: C# in Depth
Adil Muratov issued the book: C# in Depth
Kuro Kodoku issued the book: Creating a Particle Engine
Jngu Null issued the book: Creating a Particle Engine
Loan completed for C# in Depth.
Zhanibek Khan returned the book: C# in Depth
Loan completed for C# in Depth.
Adil Muratov returned the book: C# in Depth
Loan completed for Creating a Particle Engine.
Kuro Kodoku returned the book: Creating a Particle Engine

--- Library Report ---
Books:
C# in Depth by Jon Skeet (ISBN: 123) - Genre: Programming - Copies Available: 5/5
Clean Code by Robert Martin (ISBN: 456) - Genre: Programming - Copies Available: 2/2
Design Patterns by GoF (ISBN: 789) - Genre: Software Engineering - Copies Available: 4/4
Cooking with Kuro by Kuro Kodoku (ISBN: 101112) - Genre: Cooking - Copies Available: 2/2
Creating a Particle Engine by Acerola (ISBN: 131415) - Genre: Game Development - Copies Available: 1/1
Loans:
  borrowed 'C# in Depth' on 11/21/2024 9:17:04 PM
  borrowed 'C# in Depth' on 11/21/2024 9:17:04 PM
  borrowed 'Creating a Particle Engine' on 11/21/2024 9:17:04 PM
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/*i just combine both tasks to one code cuz they were almost same*/
public class program
{
    public static void Main(string[] args)
    {
        var librarySystem = new LibrarySystem();

        librarySystem.AddBook(new Book("C# in Depth", "Jon Skeet", "Programming", "123", 5));
        librarySystem.AddBook(new Book("Clean Code", "Robert Martin", "Programming", "456", 2));
        librarySystem.AddBook(new Book("Design Patterns", "GoF", "Software Engineering", "789", 4));
        librarySystem.AddBook(new Book("Cooking with Kuro", "Kuro Kodoku", "Cooking", "101112", 2));
        librarySystem.AddBook(new Book("Creating a Particle Engine", "Acerola", "Game Development", "131415", 1));

        librarySystem.RegisterUser(new Reader { Id = 1, FirstName = "Zhanibek", LastName = "Khan", TicketNumber = "T001", Email = "no@gmail.com" });
        librarySystem.RegisterUser(new Reader { Id = 2, FirstName = "Adil", LastName = "Muratov", TicketNumber = "T002", Email = "nah@gmail.com" });
        librarySystem.RegisterUser(new Reader { Id = 3, FirstName = "Kuro", LastName = "Kodoku", TicketNumber = "T003", Email = "none@gmail.com" });
        librarySystem.RegisterUser(new Reader { Id = 4, FirstName = "Jngu", LastName = "Null", TicketNumber = "T004", Email = "null@gmail.com" });

        librarySystem.BorrowBook(1, "123");
        librarySystem.BorrowBook(2, "123");
        librarySystem.BorrowBook(3, "131415");
        librarySystem.BorrowBook(4, "131415");

        librarySystem.ReturnBook(1, "123");
        librarySystem.ReturnBook(2, "123");
        librarySystem.ReturnBook(3, "131415");

        librarySystem.GenerateReport();
    }
}
public interface ICatalog
{
    Book SearchBook(string searchTerm);
    List<Book> GetBooksByGenre(string genre);
    List<Book> GetBooksByAuthor(string author);
}
public interface IAccountingSystem
{
    void IssueBook(Book book, Reader reader);
    void ReturnBook(Book book, Reader reader);
    List<Loan> GetLoanHistory();
}
public class LibrarySystem
{
    private Catalog _catalog;
    private Librarian _librarian;
    private AccountingSystem _accountingSystem;
    private List<Book> _books;
    private List<User> _users;
    public LibrarySystem()
    {
        _books = new List<Book>();
        _users = new List<User>();
        _accountingSystem = new AccountingSystem();
        _catalog = new Catalog(_books);
        _librarian = new Librarian(_accountingSystem);
    }
    public void AddBook(Book book)
    {
        _books.Add(book);
        LogAction($"Book added: {book.Title}");
    }
    public void RegisterUser(User user)
    {
        _users.Add(user);
        LogAction($"User registered: {user.Name} ({user.Email})");
    }
    public void BorrowBook(int userId, string bookIsbn)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        var book = _books.FirstOrDefault(b => b.ISBN == bookIsbn);
        if (user is Reader reader && book != null)
        {
            _librarian.IssueBook(book, reader);
        }
    }
    public void ReturnBook(int userId, string bookIsbn)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        var book = _books.FirstOrDefault(b => b.ISBN == bookIsbn);
        if (user is Reader reader && book != null)
        {
            _librarian.ReturnBook(book, reader);
        }
    }
    public void GenerateReport()
    {
        Console.WriteLine("\n--- Library Report ---");
        Console.WriteLine("Books:");
        foreach (var book in _books)
        {
            Console.WriteLine(book.GetDetails());
        }
        Console.WriteLine("Loans:");
        foreach (var loan in _accountingSystem.GetLoanHistory())
        {
            Console.WriteLine($"{loan.Reader.Name} borrowed '{loan.Book.Title}' on {loan.LoanDate}");
        }
    }
    private void LogAction(string message)
    {
        string logPath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\11\Pr_11_Module_11\logs\library_log.txt";
        File.AppendAllText(logPath, $"{DateTime.Now}: {message}\n");
    }
}
public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public string ISBN { get; set; }
    public int TotalCopies { get; private set; }
    public int AvailableCopies { get; private set; }
    public Book(string title, string author, string genre, string isbn, int totalCopies)
    {
        Title = title;
        Author = author;
        Genre = genre;
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
        return $"{Title} by {Author} (ISBN: {ISBN}) - Genre: {Genre} - Copies Available: {AvailableCopies}/{TotalCopies}";
    }
}
public class User
{
    public int Id { get; set; }
    public string Name => $"{FirstName} {LastName}";
    public string Email { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
}
public class Reader : User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TicketNumber { get; set; }
}
public class Librarian
{
    private IAccountingSystem _accountingSystem;
    public Librarian(IAccountingSystem accountingSystem)
    {
        _accountingSystem = accountingSystem;
    }
    public void IssueBook(Book book, Reader reader)
    {
        _accountingSystem.IssueBook(book, reader);
        Console.WriteLine($"{reader.FirstName} {reader.LastName} issued the book: {book.Title}");
    }
    public void ReturnBook(Book book, Reader reader)
    {
        _accountingSystem.ReturnBook(book, reader);
        Console.WriteLine($"{reader.FirstName} {reader.LastName} returned the book: {book.Title}");
    }
}
public class Catalog : ICatalog
{
    private List<Book> _books;

    public Catalog(List<Book> books)
    {
        _books = books;
    }
    public Book SearchBook(string searchTerm)
    {
        return _books.FirstOrDefault(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm));
    }
    public List<Book> GetBooksByGenre(string genre)
    {
        return _books.Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();
    }
    public List<Book> GetBooksByAuthor(string author)
    {
        return _books.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}
public class AccountingSystem : IAccountingSystem
{
    private List<Loan> _loans = new List<Loan>();
    public void IssueBook(Book book, Reader reader)
    {
        if (book.IsAvailable)
        {
            var loan = new Loan(book, reader);
            _loans.Add(loan);
            loan.IssueLoan();
        }
    }
    public void ReturnBook(Book book, Reader reader)
    {
        var loan = _loans.FirstOrDefault(l => l.Book.ISBN == book.ISBN && l.Reader.TicketNumber == reader.TicketNumber);
        if (loan != null)
        {
            loan.CompleteLoan();
        }
        else
        {
            Console.WriteLine("No active loan found for this book and reader.");
        }
    }
    public List<Loan> GetLoanHistory()
    {
        return _loans;
    }
}
public class Loan
{
    public Book Book { get; private set; }
    public Reader Reader { get; private set; }
    public DateTime LoanDate { get; private set; }

    public Loan(Book book, Reader reader)
    {
        Book = book;
        Reader = reader;
    }

    public void IssueLoan()
    {
        LoanDate = DateTime.Now;
        Book.BorrowCopy();
    }

    public void CompleteLoan()
    {
        Book.ReturnCopy();
        Console.WriteLine($"Loan completed for {Book.Title}.");
    }
}
