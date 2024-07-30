using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Shared;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

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
        //to get with filter
        public async Task<IEnumerable<BorrowedBookBooksDTO>> GetAllWithFilter(DateTime? startDate = null, DateTime? endDate = null, string? sortOrder = null)
        {
            var query = _context.BorrowedBooks.Include(bb => bb.Book).AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(bb => bb.BorrowDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(bb => bb.BorrowDate <= endDate.Value);
            }

            if (!string.IsNullOrEmpty(sortOrder))
            {
                query = sortOrder.ToLower() == "desc"? query.OrderByDescending(bb => bb.BorrowDate) : query.OrderBy(bb => bb.BorrowDate);
            }

            var borrowedBooks = await query
                .Select(bb => new BorrowedBookBooksDTO
                {
                    Id = bb.Id,
                    BorrowDate = bb.BorrowDate,
                    ReturnDate = bb.ReturnDate,
                    Books = new List<BookDTO> 
                    {
                new BookDTO
                {
                    //Id = bb.Book.Id,
                    Title = bb.Book.Title,
                    PublishedYear = bb.Book.PublishedYear
                }
                    }
                }).ToListAsync();

            return borrowedBooks;
        }


        public async Task<BorrowedBookDTO> GetByIdAsync(int id)
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
