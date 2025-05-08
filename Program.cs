using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

class Book
{
    public Guid Id { get; }
    public string Title { get; }
    public string Author { get; }
    public int Year { get; }
    public bool IsAvailable { get; set; } = true;

    public Book(Guid id, string title, string author, int year)
    {
        Id=id;
        Title=title;
        Author=author;
        Year=year;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"[{Id}] {Title} by {Author} ({Year}) Available: {IsAvailable}");
    }
}

class User
{
    public string Name { get; }
    public Dictionary<Guid, Book> BorrowedBooks { get; } = new Dictionary<Guid, Book>();

    public User(string name)
    {
        Name = name;
    }
}

class Library
{
    private readonly Dictionary<Guid, Book> books = new Dictionary<Guid, Book>();
    private readonly Dictionary<string, User> users = new Dictionary<string, User>();

    public void AddBook(Book book)
    {
        if (!books.ContainsKey(book.Id))
        {
            books[book.Id] = book;
            Console.WriteLine($"Book {book.Title} added");
        }
    }

    public void RemoveBook(Guid id)
    {
        if (books.TryGetValue(id, out var book))
        {
            if (book.IsAvailable)
            {
                books.Remove(id);
                Console.WriteLine($"Book {book.Title} removed");
            }
            else
            {
                Console.WriteLine($"Book {book.Title} cannot be removed, book is not available");
            }
        }
    }

    public List<Book> FindBooks(string keyword)
    {
        return books.Values.Where(b => b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                       b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public void PrintBooksGroupedByAuthor()
    {
        var groups = books.Values.GroupBy(b => b.Author)
            .OrderBy(g => g.Key);

        foreach (var group in groups)
        {
            Console.WriteLine($"Author: {group.Key}");

            foreach (var book in group.OrderBy(b => b.Year))
            {
                book.PrintInfo();
            }
        }
    }

    public void RegisterUser(User user)
    {
        if (!users.ContainsKey(user.Name))
        {
            users[user.Name] = user;
            Console.WriteLine($"User {user.Name} registered");
        }
    }

    public void BorrowBook(string username, Guid bookId)
    {

        if (users.TryGetValue(username, out var user) && books.TryGetValue(bookId, out var book))
        {
            if (book.IsAvailable)
            {
                user.BorrowedBooks[bookId] = book;
                book.IsAvailable = false;
                Console.WriteLine($"{username} borrowed{book.Title}");
            }
            else
            {
                Console.WriteLine($"User {username} cannot be borrowed, book is not available");
            }
        }
    }

    public void ReturnBook(string userName, Guid bookId)
    {
        if (users.TryGetValue(userName, out var user) && user.BorrowedBooks.Remove(bookId))
        {
            if (books.TryGetValue(bookId, out var book))
            {
                book.IsAvailable = true;
                Console.WriteLine($"{userName} returned {book.Title}");
            }
        }
    }

    public void PrintUserBooks(string username)
    {
        if (users.TryGetValue(username, out var user))
        {
            Console.WriteLine($"Books borrowed by '{username}' ");

            foreach (var book in user.BorrowedBooks.Values)
            {
                book.PrintInfo();
            }
        }
    }
}

class Program
{
    static void Main()
    {
        var lib = new Library();
        
        // добавляем id книг 
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var id3 = Guid.NewGuid();
        
        // добавляем книги 
        lib.AddBook(new Book(id1, "1984", "George Orwell", 1949));
        lib.AddBook(new Book(id2, "Brave New World", "Aldous Huxley", 1932));
        lib.AddBook(new Book(id3, "Animal Farm", "George Orwell", 1945));
        
        // регистрируем пользователей 
        lib.RegisterUser(new User("Vlad"));
        lib.RegisterUser(new User("Markus"));
        
        // берем книги 
        lib.BorrowBook("Vlad", id1);
        lib.BorrowBook("Vlad", id3);
        
        // группируем по автору 
        Console.WriteLine("\nBooks grouped by author:");
        lib.PrintBooksGroupedByAuthor();
        
        // книги которые взял пользователь 
        Console.WriteLine("\nBooks borrowed by Vlad:");
        lib.PrintUserBooks("Vlad");
        
        //поиск книги по слову
        Console.WriteLine("\nBooks with keyword 'New':");
        var found = lib.FindBooks("New");
        foreach (var b in found) b.PrintInfo();
        
        // пробуем взять недоступную книгу 
        Console.WriteLine("\nTrying to remove borrowed book:");
        lib.RemoveBook(id1);
        
        // возвращаем и удаляем книгу 
        Console.WriteLine("\nReturning and removing book:");
        lib.ReturnBook("Vlad", id1);
        lib.RemoveBook(id1);
    }
}


















    









































