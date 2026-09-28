namespace LibraryData;

public class IssuedBook
{
    public int IssueId { get; set; }
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime? ReturnDate { get; set; } // as return date can be null 
    


}