using LibraryData;
class Program
{
    static void Main()
    {
        BookRepository bookRepository = new BookRepository();
        MemberRepository memberRepository = new MemberRepository();
        LibraryRepository libraryRepository = new LibraryRepository();
        BackupHelper backupHelper = new BackupHelper();

        while (true)
        {
            Console.WriteLine("Choose the option u wanna perform");
            Console.WriteLine("1. Add book");
            Console.WriteLine("2. View all books");
            Console.WriteLine("3. Add member");
            Console.WriteLine("4. View all members");
            Console.WriteLine("5. Issue book to a member");
            Console.WriteLine("6. Return book");
            Console.WriteLine("7. Export books to JSON");
            Console.WriteLine("8. Import books from JSON");
            Console.WriteLine("9. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter book title: ");
                    string title = Console.ReadLine();

                    Console.Write("Enter author: ");
                    string author = Console.ReadLine();

                    Console.Write("Enter stock: ");
                    int stock = int.Parse(Console.ReadLine());

                    Book book = new Book
                    {
                        Title = title,
                        Author = author,
                        Stock = stock
                    };

                    bookRepository.AddBook(book);

                    Console.WriteLine("Book added successfully.");
                    break;


                case "2":
                    List<Book> books = bookRepository.GetAllBooks();
                    foreach (Book b in books)
                    {
                        Console.WriteLine($"ID: {b.BookId}, Title: {b.Title}, Author: {b.Author}, Stock: {b.Stock}");
                    }
                    break;


                case "3":
                    Console.Write("Enter member name: ");
                    string name = Console.ReadLine();

                    Member member = new Member{Name = name};
                    memberRepository.AddMember(member);

                    Console.WriteLine("Member added successfully.");
                    break;


                case "4":
                    List<Member> members = memberRepository.GetAllMembers();

                    foreach (Member m in members)
                    {
                        Console.WriteLine($"ID: {m.MemberId}, Name: {m.Name}");
                    }
                    break;


                case "5":
                    Console.Write("Enter book id: ");
                    int bookId = int.Parse(Console.ReadLine());

                    Console.Write("Enter member id: ");
                    int memberId = int.Parse(Console.ReadLine());

                    bool issued = libraryRepository.IssueBook(bookId, memberId);

                    if (issued)
                        Console.WriteLine("Book issued successfully.");
                    else
                        Console.WriteLine("Book could not be issued.");
                    break;


                case "6":
                    Console.Write("Enter issue id: ");
                    int issueId = int.Parse(Console.ReadLine());
                    bool returned = libraryRepository.ReturnBook(issueId);

                    if (returned)
                        Console.WriteLine("Book returned successfully.");
                    else
                        Console.WriteLine("Book could not be returned.");
                    break;


                case "7":
                    List<Book> booksToExport = bookRepository.GetAllBooks();
                    backupHelper.ExportBooks(booksToExport, "books.json");

                    Console.WriteLine("Books exported to books.json");
                    break;


                case "8":
                    List<Book> importedBooks = backupHelper.ImportBooks("books.json");

                    foreach (Book b in importedBooks)
                    {
                        Console.WriteLine($"Title: {b.Title}, Stock: {b.Stock}");
                    }
                    break;


                case "9":
                    Console.WriteLine("exiting..!");
                    return;


                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
}
