using LibraryManagementSystem.Shared;
using NPOI.SS.Formula.Functions;

namespace LibraryManagementSystem.Models
{
    public class BorrowedBook : BaseEntity<int>
    {
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        // Navigation Prop
        public Book Book { get; set; }
        public Member Member { get; set; }
    }
}
