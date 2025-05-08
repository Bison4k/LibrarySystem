using library_4_blyat.Models;

namespace library_4_blyat;

public class Program
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