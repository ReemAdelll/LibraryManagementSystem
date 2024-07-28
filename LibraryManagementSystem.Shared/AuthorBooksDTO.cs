
namespace LibraryManagementSystem.Shared
{
    public class AuthorBooksDTO : AuthorDTO
    {
        public ICollection<BookDTO>? Books { get; set; }
    }
}
