using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Shared;

namespace LibraryManagementSystem.Services
{
    public interface IMemberRepo : IBaseRepo<Member>
    {
        //specific operations for Member
        //public IQueryable<MemberDTO> GetAllWithFilter(string? firstName, string? lastName);
    }
}
