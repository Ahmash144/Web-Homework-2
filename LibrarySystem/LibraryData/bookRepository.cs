using Microsoft.Data.SqlClient;

namespace LibraryData;

public class BookRepository
{
    private string connectionString = "Server=localhost;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True;";


    public void AddBook(Book book)
    {
        using SqlConnection connection = new SqlConnection(connectionString);

        string query = "INSERT INTO Books (Title, Author, Stock) VALUES (@title, @author, @stock)";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@title", book.Title);
        command.Parameters.AddWithValue("@author", book.Author);
        command.Parameters.AddWithValue("@stock", book.Stock);

        connection.Open();
        command.ExecuteNonQuery();
    }

    public List<Book> GetAllBooks()
    {
        List<Book> books = new List<Book>();

        using SqlConnection connection = new SqlConnection(connectionString);

        string query = "SELECT BookId, Title, Author, Stock FROM Books";

        using SqlCommand command = new SqlCommand(query, connection);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Book book = new Book
            {
                BookId = reader.GetInt32(0),
                Title = reader.GetString(1),
                Author = reader.GetString(2),
                Stock = reader.GetInt32(3)
            };
            books.Add(book);

        }
        return books;
    }

    public void UpdateStock(int bookId, int stock)
    {
        using SqlConnection connection = new SqlConnection(connectionString);

        string query = "UPDATE Books SET Stock = @stock WHERE BookId = @bookId";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@stock", stock);
        command.Parameters.AddWithValue("@bookId", bookId);

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void DeleteBook(int bookId)
    {
        using SqlConnection connection = new SqlConnection(connectionString);

        string query = "DELETE FROM Books WHERE BookId = @bookId";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@bookId", bookId);

        connection.Open();
        command.ExecuteNonQuery();
    }
}