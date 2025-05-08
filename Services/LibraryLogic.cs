using library_4_blyat.Models;
using System.Linq;
namespace library_4_blyat.Services;

public class LibraryLogic
{
    private readonly Library _library = new();
    private readonly Dictionary<string, List<Guid>> _userBooks = new(); // что бы пользователь мог брать несколько книг 

    public IReadOnlyList<Book> LibraryBooks => _library.Books;
    
    public void AddBook(Book book)
    {
        if (_library.Books.All(b=>b.Id != book.Id))
        {
            _library.Books.Add(book);
            Console.WriteLine($"Book {book.Title} added");
        }
        else
        {
            Console.WriteLine($"Book {book.Title} already exists");
        }
    }

    public void RemoveBook(Guid id)
    {
        var book = _library.Books.FirstOrDefault(b => b.Id == id);
        if (book != null)
        {
            if (book.IsAvailable)
            {
                _library.Books.Remove(book);
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
        return _library.Books
            .Where(b => b.Title.Contains(keyword) || b.Author.Contains(keyword))
            .ToList();
    }

    public void PrintBooksGroupedByAuthor()
    {
        var orderedGroups = _library.Books
            .GroupBy(b => b.Author)
            .OrderBy(g => g.Key);

        foreach (var group in orderedGroups)
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
        if (_library.Users.All(u=>u.Name != user.Name))
        {
            _library.Users.Add(user);
            Console.WriteLine($"User {user.Name} registered");
        }
        else
        {
            Console.WriteLine($"User {user.Name} already exists");
        }
    }

    public void BorrowBook(string username, Guid bookId)
    {
        var user = _library.Users.FirstOrDefault(u => u.Name == username);
        var book = _library.Books.FirstOrDefault(b => b.Id == bookId);
        
        if (user !=null && book != null)
        {
            if (!book.IsAvailable)
            {
                Console.WriteLine($"Book {book.Title} already exists");
                return;
            }

            if (!_userBooks.ContainsKey(username))
            {
                _userBooks[username] = new List<Guid>();
            }

            if (_userBooks[username].Contains(book.Id))
            {
                Console.WriteLine($"{username} already borrowed {book.Title} ");
                return;
            }
            
            user.BorrowedBooks[bookId] = book; 
            book.IsAvailable = false;
            _userBooks[username].Add(book.Id);
            
            Console.WriteLine($"{username} borrowed: {book.Title}");
        }
    }

    public void ReturnBook(string userName, Guid bookId)
    {
        var user = _library.Users.FirstOrDefault(u => u.Name == userName);
        var book = _library.Books.FirstOrDefault(b => b.Id == bookId);

        if (user != null && book != null && user.BorrowedBooks.Remove(book.Id))
        {
            book.IsAvailable = true;

            if (!_userBooks.ContainsKey(userName))
            {
                _userBooks[userName].Remove(bookId);
            }
            Console.WriteLine($"{userName} return: {book.Title}");
        }
    }

    public void PrintUserBooks(string username)
    {
        var user = _library.Users.FirstOrDefault(u => u.Name == username);
        if (user != null)
        {
            Console.WriteLine($"Books borrowed by '{username}' ");

            foreach (var book in user.BorrowedBooks.Values)
            {
                book.PrintInfo();
            }
        }
    }
}