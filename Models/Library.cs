namespace library_4_blyat.Models;

public class Library
{
    public List<Book> Books { get; set; } = new();

    public List<User> Users { get; set; } = new();
}