using LibraryManagementSystem.Shared;

namespace LibraryManagementSystem.Models
{
    public class Genre : BaseEntity<int>
    {
        public string GenreName { get; set; }
    }

}
