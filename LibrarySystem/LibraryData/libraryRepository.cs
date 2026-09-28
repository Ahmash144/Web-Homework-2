using Microsoft.Data.SqlClient;

namespace LibraryData;

public class LibraryRepository
{
    private string connectionString = "Server=localhost;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True;";

    public bool IssueBook(int bookId, int memberId)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            string checkQuery = "SELECT Stock FROM Books WHERE BookId = @bookId";

            using SqlCommand checkCommand = new SqlCommand(checkQuery, connection, transaction);
            checkCommand.Parameters.AddWithValue("@bookId", bookId);

            object result = checkCommand.ExecuteScalar();

            if (result == null)
            {
                transaction.Rollback();
                return false;
            }

            int stock = Convert.ToInt32(result);
            if (stock <= 0)
            {
                transaction.Rollback();
                return false;
            }

            string updateQuery = "UPDATE Books SET Stock = Stock - 1 WHERE BookId = @bookId";

            using SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction);

            updateCommand.Parameters.AddWithValue("@bookId", bookId);
            updateCommand.ExecuteNonQuery();

            string insertQuery ="INSERT INTO IssuedBooks (BookId, MemberId, IssueDate) " + "VALUES (@bookId, @memberId, @issueDate)";

            using SqlCommand insertCommand = new SqlCommand(insertQuery, connection, transaction);
            insertCommand.Parameters.AddWithValue("@bookId", bookId);
            insertCommand.Parameters.AddWithValue("@memberId", memberId);
            insertCommand.Parameters.AddWithValue("@issueDate", DateTime.Now);

            insertCommand.ExecuteNonQuery();
            transaction.Commit();

            return true;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }

    }

    public bool ReturnBook(int issueId){

        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            string findQuery = "SELECT BookId FROM IssuedBooks " + "WHERE IssueId = @issueId AND ReturnDate IS NULL";

            using SqlCommand findCommand = new SqlCommand(findQuery, connection, transaction);

            findCommand.Parameters.AddWithValue("@issueId", issueId);
            object result = findCommand.ExecuteScalar();

            if (result == null)
            {
                transaction.Rollback();
                return false;
            }

            int bookId = Convert.ToInt32(result);

            string updateIssueQuery = "UPDATE IssuedBooks " +"SET ReturnDate = @returnDate " +"WHERE IssueId = @issueId";

            using SqlCommand updateIssueCommand = new SqlCommand(updateIssueQuery, connection, transaction);
            updateIssueCommand.Parameters.AddWithValue("@returnDate", DateTime.Now);
            updateIssueCommand.Parameters.AddWithValue("@issueId", issueId);

            updateIssueCommand.ExecuteNonQuery();

            string updateBookQuery = "UPDATE Books SET Stock = Stock + 1 WHERE BookId = @bookId";

            using SqlCommand updateBookCommand = new SqlCommand(updateBookQuery, connection, transaction);
            updateBookCommand.Parameters.AddWithValue("@bookId", bookId);
            updateBookCommand.ExecuteNonQuery();

            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }

    }

}