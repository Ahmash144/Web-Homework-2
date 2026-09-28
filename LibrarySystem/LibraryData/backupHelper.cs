using System.Text.Json;

namespace LibraryData;

public class BackupHelper
{
    public void ExportBooks(List<Book> books, string filePath)
    {
        string json = JsonSerializer.Serialize(books);
        File.WriteAllText(filePath, json);
    }

    public List<Book> ImportBooks(string filePath){
        string json = File.ReadAllText(filePath);

        List<Book> books =
            JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();

        return books;
    }
    
}