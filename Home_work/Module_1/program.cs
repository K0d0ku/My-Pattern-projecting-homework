using System;
using System.Collections.Generic;

public class Librarian
{
    public static void Main()
    {
        Library library = new Library();

        Book book1 = new Book { book_name = "Christine", author = "Steven King", ISBN = "1234567890", copies_amount = 1 };
        Book book2 = new Book { book_name = "The call of Cthulhu", author = "Howard Lovecraft", ISBN = "0987654321", copies_amount = 1 };

        library.Add_book(book1);
        library.Add_book(book2);

        Reader reader1 = new Reader { name = "Kuro", ID = 1 };
        Reader reader2 = new Reader { name = "Kodoku", ID = 2 };

        library.Register_reader(reader1);
        library.Register_reader(reader2);

        library.Lend_book(reader1, book1);
        library.Lend_book(reader2, book2);

        /* попытка взять книгу у которой закончились копий */ 
        library.Lend_book(reader2, book2);

        library.Return_book(reader1, book1);
        library.Return_book(reader2, book2);

        /* попытка вернуть книгу которую не взяли */
        library.Return_book(reader2, book2);
    }
}

public class Book
{
    public string book_name { get; set; }
    public string author { get; set; }
    public string ISBN { get; set; }
    public int copies_amount { get; set; }
}
public class Reader
{
    public string name { get; set; }
    public int ID { get; set; }
    public List<Book> Borrowed_books { get; set; }

    public Reader()
    {
        Borrowed_books = new List<Book>();
    }
}

public class Library
{
    public List<Book> books { get; set; }
    public Library()
    {
        books = new List<Book>();
        Readers = new List<Reader>();
    }
    public void Add_book(Book book)
    {
        books.Add(book);
        Console.WriteLine($"'{book.book_name}', '{book.author}', '{book.ISBN}', '{book.copies_amount}' have been added to the list");
    }
    public void Remove_book(Book book) 
    {
        if (books.Contains(book))
        {
            Console.WriteLine($"'{book.book_name}', '{book.author}', '{book.ISBN}', '{book.copies_amount}' has been removed from the list");
        }
        else
        {
            Console.WriteLine($"'{book.book_name}', '{book.author}', '{book.ISBN}', '{book.copies_amount}' has not been found in the list");
        }
    }

    public List<Reader> Readers { get; set; }
    public void Register_reader(Reader reader)
    {
        Readers.Add(reader);
        Console.WriteLine($"reader '{reader.name}' have been registered");
    }
    public void Lend_book(Reader reader, Book book)
    {
        if (book.copies_amount > 0)
        {
            reader.Borrowed_books.Add(book);
            book.copies_amount--;
            Console.WriteLine($"'{book.book_name}', '{book.ISBN}' has been lent to '{reader.name}', '{reader.ID}'");
        }
        else
        {
            Console.WriteLine($"Sorry, '{book.book_name}' is currently not available for lending.");
        }
    }
    public void Return_book(Reader reader ,Book book)
    {
        if (reader.Borrowed_books.Contains(book))
        {
            reader.Borrowed_books.Remove(book);
            book.copies_amount++;
            Console.WriteLine($"'{book.book_name}', '{book.ISBN}' has been returned by '{reader.name}', '{reader.ID}'");
        }
        else
        {
            Console.WriteLine($"reader '{reader.name}', '{reader.ID}' did not take this book '{book.book_name}', '{book.ISBN}'");
        }
    }
}
