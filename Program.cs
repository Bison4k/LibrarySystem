using library_4_blyat.Models;
using library_4_blyat.Services; 


namespace library_4_blyat;

public class Program
{
    static void Main()
    {
        var lib = new LibraryLogic();
        
        var book1 = new Book("1984", "George Orwell", 1949);
        var book2 = new Book("Brave New World", "Aldous Huxley", 1932);
        var book3 = new Book("Animal Farm", "George Orwell", 1945);
        
        var id1 = book1.Id;
        var id2 = book2.Id;
        var id3 = book3.Id;
        
        lib.AddBook(book1);
        lib.AddBook(book2);
        lib.AddBook(book3);
        
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