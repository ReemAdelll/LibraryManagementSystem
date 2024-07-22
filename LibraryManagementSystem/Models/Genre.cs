using LibraryManagementSystem.Shared;

namespace LibraryManagementSystem.Models
{
    public class Genre : BaseEntity<int>
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
    }

}
