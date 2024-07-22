using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Shared;

namespace LibraryManagementSystem.Services
{
    public class MemberRepo : IMemberRepo
    {
        private readonly LibraryContext _context;
        public MemberRepo(LibraryContext context)
        {
            _context = context;
        }
        public async Task<MemberDTO> AddAsync(MemberDTO memberDTO)
        {
            var member = new Member
            {
                MemberId = memberDTO.MemberId,
                FirstName = memberDTO.FirstName,
                LastName = memberDTO.LastName,
                Email = memberDTO.Email,
                PhoneNumber = memberDTO.PhoneNumber
            };
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            memberDTO.MemberId = member.MemberId;
            return memberDTO;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null) return false;
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<MemberDTO> GetAll()
        {
            return _context.Members.Select(a => new MemberDTO { MemberId = a.MemberId,
                FirstName = a.FirstName, LastName = a.LastName,
                Email= a.Email,PhoneNumber= a.PhoneNumber});
        }

        public async Task<MemberDTO> GetByIdAsync(int id)
        {
            var member =  await _context.Members.FindAsync(id);
            if (member == null) return null;
            return new MemberDTO
            {
                MemberId = member.MemberId,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber
            };
        }

        public async Task<MemberDTO> UpdateAsync(MemberDTO memberDTO)
        {
            var member = await _context.Members.FindAsync(memberDTO.MemberId);
            if (member == null) return null;
            member.MemberId = memberDTO.MemberId;
            member.FirstName = memberDTO.FirstName;
            member.LastName = memberDTO.LastName;
            member.Email = memberDTO.Email;
            member.PhoneNumber = memberDTO.PhoneNumber;
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
            return memberDTO;
        }
    }
}
