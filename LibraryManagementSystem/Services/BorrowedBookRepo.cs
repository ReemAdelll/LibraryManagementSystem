using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Shared;

namespace LibraryManagementSystem.Services
{
    public class BorrowedBookRepo : IBorrowedBookRepo
    {
        private readonly LibraryContext _context;
        public BorrowedBookRepo(LibraryContext context)
        {
            _context = context;
        }
        public async Task<BorrowedBookDTO> AddAsync(BorrowedBookDTO borrowedBookDTO)
        {
            var borrowedBook = new BorrowedBook
            {
                BookId = borrowedBookDTO.BookId,
                MemberId = borrowedBookDTO.MemberId
           ,
                Id = borrowedBookDTO.Id,
                BorrowDate = borrowedBookDTO.BorrowDate,
                ReturnDate = borrowedBookDTO.ReturnDate
            };
            _context.BorrowedBooks.Add(borrowedBook);
            await _context.SaveChangesAsync();
            borrowedBookDTO.Id = borrowedBook.Id;
            return borrowedBookDTO;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var borrowedBook = await _context.BorrowedBooks.FindAsync(id);
            if (borrowedBook == null) return false;
            _context.BorrowedBooks.Remove(borrowedBook);
            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<BorrowedBookDTO> GetAll()
        {
            return _context.BorrowedBooks.Select(a=> new BorrowedBookDTO { BookId = a.BookId,MemberId = a.MemberId
            , Id = a.Id, BorrowDate= a.BorrowDate, ReturnDate= a.ReturnDate});

        }

        public  async Task<BorrowedBookDTO> GetByIdAsync(int id)
        {
            var BorrowedBook = await _context.BorrowedBooks.FindAsync(id);
            if (BorrowedBook == null) return null;
            return new BorrowedBookDTO {
                BookId = BorrowedBook.BookId,
                MemberId = BorrowedBook.MemberId
            ,Id = BorrowedBook.Id,
                BorrowDate = BorrowedBook.BorrowDate,
                ReturnDate = BorrowedBook.ReturnDate
            };
        }

        public async Task<BorrowedBookDTO> UpdateAsync(BorrowedBookDTO borrowedBookDTO)
        {
            var BorrowedBook = await _context.BorrowedBooks.FindAsync(borrowedBookDTO.Id);
            if (BorrowedBook == null) return null;
            BorrowedBook.BookId = borrowedBookDTO.BookId;
            BorrowedBook.MemberId = borrowedBookDTO.MemberId;
            BorrowedBook.Id = borrowedBookDTO.Id;
            BorrowedBook.BorrowDate = borrowedBookDTO.BorrowDate;
            BorrowedBook.ReturnDate = borrowedBookDTO.ReturnDate;
            _context.BorrowedBooks.Update(BorrowedBook);
            await _context.SaveChangesAsync();
            return borrowedBookDTO;
        }
    }
}
