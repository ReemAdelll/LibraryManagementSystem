using LibraryManagementSystem.Shared;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Member : BaseEntity <int>
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        // Navigation Prop
        public ICollection<BorrowedBook> BorrowedBooks { get; set; }
    }
}
