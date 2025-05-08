namespace library_4_blyat.Models;

public class User
{
    public string Name { get; }
    
    public Dictionary<Guid, Book> BorrowedBooks { get; } = new Dictionary<Guid, Book>();

    public User(string name)
    {
        Name = name;
    }
}