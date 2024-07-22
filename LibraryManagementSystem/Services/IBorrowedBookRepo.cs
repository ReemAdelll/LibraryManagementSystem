using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Shared;

namespace LibraryManagementSystem.Services
{
    public interface IBorrowedBookRepo : IBaseRepo<BorrowedBookDTO>
    {
        //specific operations for BorrowedBook
    }
}
