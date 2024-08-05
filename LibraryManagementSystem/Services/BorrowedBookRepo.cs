using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Shared;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<BorrowedBook> AddAsync(BorrowedBook entity)
        {
            _context.BorrowedBooks.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var borrowedBook = await _context.BorrowedBooks.FindAsync(id);
            if (borrowedBook == null) return false;
            _context.BorrowedBooks.Remove(borrowedBook);
            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<BorrowedBook> GetAll()
        {
            return _context.BorrowedBooks.Select(a=> new BorrowedBook { BookId = a.BookId,MemberId = a.MemberId
            , Id = a.Id, BorrowDate= a.BorrowDate, ReturnDate= a.ReturnDate, CreationTime = a.CreationTime, LastUpdateTime = a.LastUpdateTime});

        }

        //get with filter
        public async Task<IEnumerable<BorrowedBook>> GetAllWithFilter(DateTime? startDate = null, DateTime? endDate = null, string? sortOrder = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
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
                query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(bb => bb.BorrowDate) : query.OrderBy(bb => bb.BorrowDate);
            }

            //var borrowedBooks = await query
            //    .Select(bb => new BorrowedBookBooksDTO
            //    {
            //        Id = bb.Id,
            //        BorrowDate = bb.BorrowDate,
            //        ReturnDate = bb.ReturnDate,
            //        Books = new List<BookDTO>
            //        {
            //    new BookDTO
            //    {
            //        //Id = bb.Book.Id,
            //        Title = bb.Book.Title,
            //        PublishedYear = bb.Book.PublishedYear
            //    }
            //        }
            //    })
            //    .Skip((page - 1) * pageSize)
            //    .Take(pageSize)
            //    .ToListAsync();

            //return borrowedBooks;
            return await query.ToListAsync();
        }

        public async Task<BorrowedBook> GetByIdAsync(int id)
        {
            var BorrowedBook = await _context.BorrowedBooks.FindAsync(id);
            if (BorrowedBook == null) return null;
            return new BorrowedBook {
                BookId = BorrowedBook.BookId,
                MemberId = BorrowedBook.MemberId,
                Id = BorrowedBook.Id,
                BorrowDate = BorrowedBook.BorrowDate,
                ReturnDate = BorrowedBook.ReturnDate,
                CreationTime = BorrowedBook.CreationTime,
                LastUpdateTime = BorrowedBook.LastUpdateTime,
            };
        }

        public async Task<BorrowedBook> UpdateAsync(BorrowedBook  entity)
        {
            var existingBorrowedBook = await _context.BorrowedBooks.FindAsync(entity.Id);
            if (existingBorrowedBook == null) return null;
            existingBorrowedBook.BookId = entity.BookId;
            existingBorrowedBook.MemberId = entity.MemberId;
            existingBorrowedBook.Id = entity.Id;
            existingBorrowedBook.BorrowDate = entity.BorrowDate;
            existingBorrowedBook.ReturnDate = entity.ReturnDate;
            _context.BorrowedBooks.Update(existingBorrowedBook);
            await _context.SaveChangesAsync();
            return existingBorrowedBook;

        }
    }
}
