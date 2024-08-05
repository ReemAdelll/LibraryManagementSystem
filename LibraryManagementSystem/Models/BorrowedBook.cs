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

        public ICollection<BookDTO>? Books { get; set; }

        public static implicit operator BorrowedBookDTO(BorrowedBook borrowedBook)
         {
            return new BorrowedBookDTO
            { 
            Id = borrowedBook.Id,
            BookId = borrowedBook.BookId,
            MemberId = borrowedBook.MemberId,
            BorrowDate = borrowedBook.BorrowDate,
            ReturnDate = borrowedBook.ReturnDate,
            CreationTime = borrowedBook.CreationTime,
            LastUpdateTime = borrowedBook.LastUpdateTime,
            };
         }
        public static implicit operator BorrowedBookBooksDTO(BorrowedBook borrowedBook)
        {
            return new BorrowedBookBooksDTO
            {
                Id = borrowedBook.Id,
                BookId = borrowedBook.BookId,
                MemberId = borrowedBook.MemberId,
                BorrowDate = borrowedBook.BorrowDate,
                ReturnDate = borrowedBook.ReturnDate,
                CreationTime = borrowedBook.CreationTime,
                LastUpdateTime = borrowedBook.LastUpdateTime,
                Books = borrowedBook.Books?.Select(book => (BookDTO)book).ToList()
            };
        }
        public static implicit operator BorrowedBook(BorrowedBookCreateDTO borrowedBookCreateDTO)
        {
            return new BorrowedBook
            {
                Id = borrowedBookCreateDTO.Id,
                BookId = borrowedBookCreateDTO.BookId,
                MemberId = borrowedBookCreateDTO.MemberId,
                BorrowDate = borrowedBookCreateDTO.BorrowDate,
                ReturnDate = borrowedBookCreateDTO.ReturnDate,
            };
        }
        public static implicit operator BorrowedBookCreateDTO(BorrowedBook borrowedBook)
        {
            return new BorrowedBookCreateDTO {
                Id = borrowedBook.Id,
                BookId = borrowedBook.BookId,
                MemberId = borrowedBook.MemberId,
                BorrowDate = borrowedBook.BorrowDate,
                ReturnDate = borrowedBook.ReturnDate,
            };
        }
        
        public static implicit operator BorrowedBookEditDTO(BorrowedBook borrowedBook)
        {
            return new BorrowedBookEditDTO
            {
                Id = borrowedBook.Id,
                BookId = borrowedBook.BookId,
                MemberId = borrowedBook.MemberId,
                BorrowDate = borrowedBook.BorrowDate,
                ReturnDate = borrowedBook.ReturnDate,
            };
        }
        public static implicit operator BorrowedBook(BorrowedBookEditDTO borrowedBookEditDTO)
        {
            return new BorrowedBook
            {
                Id = borrowedBookEditDTO.Id,
                BookId = borrowedBookEditDTO.BookId,
                MemberId = borrowedBookEditDTO.MemberId,
                BorrowDate = borrowedBookEditDTO.BorrowDate,
                ReturnDate = borrowedBookEditDTO.ReturnDate,
            };
        }
    }
}
