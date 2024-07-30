using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Shared;
using NPOI.SS.Formula.Functions;

namespace LibraryManagementSystem.Services
{
    public interface IBorrowedBookRepo : IBaseRepo<BorrowedBookDTO>
    {
        //specific operations for BorrowedBook
        Task<IEnumerable<BorrowedBookBooksDTO>> GetAllWithFilter(DateTime? startDate = null, DateTime? endDate = null);
    }
}
