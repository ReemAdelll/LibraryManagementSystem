

namespace LibraryManagementSystem.Shared
{
    public class BorrowedBookDTO
    {
        public int BorrowedBookId { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
