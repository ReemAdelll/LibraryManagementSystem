using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Shared;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;

namespace LibraryManagementSystem.Services
{
    public interface IBorrowedBookRepo : IBaseRepo<BorrowedBook>
    {
        //specific operations for BorrowedBook
        Task<IEnumerable<BorrowedBook>> GetAllWithFilter(DateTime? startDate = null, DateTime? endDate = null, string? sortOrder = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10);
    }
}
