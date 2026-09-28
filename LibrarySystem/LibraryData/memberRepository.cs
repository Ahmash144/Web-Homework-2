using Microsoft.Data.SqlClient;

namespace LibraryData;

public class MemberRepository
{
    private string connectionString = "Server=localhost;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True;";

    public void AddMember(Member member)
    {
        using SqlConnection connection = new SqlConnection(connectionString);

        string query = "INSERT INTO Members (Name) VALUES (@name)";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@name", member.Name);

        connection.Open();
        command.ExecuteNonQuery();
    }

    public List<Member> GetAllMembers()
    {
        List<Member> members = new List<Member>();

        using SqlConnection connection = new SqlConnection(connectionString);

        string query = "SELECT MemberId, Name FROM Members";

        using SqlCommand command = new SqlCommand(query, connection);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Member member = new Member
            {
                MemberId = reader.GetInt32(0),
                Name = reader.GetString(1)
            };
            members.Add(member);
            
        }
        return members;
    }
}