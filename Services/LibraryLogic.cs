using library_4_blyat.Models;

namespace library_4_blyat.Services;

public class LibraryLogic
{
    private readonly Library _library = new();
    private readonly Dictionary<string, Guid> _userBooks = new(); // => (userName,  guid => book id)

    public IReadOnlyList<Book> LibraryBooks => _library.Books; 
    
    public void AddBook(Book book)
    {
        if (_library.Books.TryAdd(book.Id, book))
        {
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
        return books.Values.Where(b => b.Title.Contains(keyword) || b.Author.Contains(keyword)).ToList();
    }

    public void PrintBooksGroupedByAuthor()
    {
        var orderedGroups = books.Values
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
        if (users.TryAdd(user.Name, user))
        {
            Console.WriteLine($"User {user.Name} registered");
            return;
        }

        Console.WriteLine($"{user.Name} уже в библиотеке, анлаки(");
    }

    public void BorrowBook(string username, Guid bookId)
    {
        if (users.TryGetValue(username, out var user) && books.TryGetValue(bookId, out var book))
        {
            if (book.IsAvailable)
            {
                user.BorrowedBooks[bookId] = book;
                book.IsAvailable = false;
                Console.WriteLine($"{username} borrowed: {book.Title}");
            }
            else
            {
                Console.WriteLine($"User {username} cannot be borrowed, book is not available");
            }
        }
    }

    public void ReturnBook(string userName, Guid bookId)
    {
        if (users.TryGetValue(userName, out var user) 
            && books.TryGetValue(bookId, out var book) 
            && user.BorrowedBooks.Remove(bookId))
        {
            book.IsAvailable = true;
            Console.WriteLine($"{userName} returned {book.Title}");
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