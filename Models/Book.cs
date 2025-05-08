namespace library_4_blyat.Models;

public class Book
{
    public Guid Id { get; } = new Guid();
    
    public string Title { get; }
    
    public string Author { get; }
    
    public int Year { get; }
    
    public bool IsAvailable { get; set; } = true;

    public Book( string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"[{Id}] {Title} by {Author} ({Year}) Available: {IsAvailable}");
    }
}