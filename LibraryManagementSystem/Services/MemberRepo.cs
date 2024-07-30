using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagementSystem.Services
{
    public class MemberRepo : IMemberRepo
    {
        private readonly LibraryContext _context;
        public MemberRepo(LibraryContext context)
        {
            _context = context;
        }
        //working
        public async Task<MemberDTO> AddAsync(MemberDTO memberDTO)
        {
            var member = new Member
            {
                Id = memberDTO.Id,
                FirstName = memberDTO.FirstName,
                LastName = memberDTO.LastName,
                Email = memberDTO.Email,
                PhoneNumber = memberDTO.PhoneNumber
            };
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            memberDTO.Id = member.Id;
            return memberDTO;
        }
        //working
        public async Task<bool> DeleteAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null) return false;
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return true;
        }
        //working
        public IQueryable<MemberDTO> GetAll()
        {
            return _context.Members.Select(a => new MemberDTO
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber
            });
        }
     
        //working
        public async Task<MemberDTO> GetByIdAsync(int id)
        {
            var member =  await _context.Members.FindAsync(id);
            if (member == null) return null;
            return new MemberDTO
            {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber
            };
        }
        //working
        public async Task<MemberDTO> UpdateAsync(MemberDTO memberDTO)
        {
            var member = await _context.Members.FindAsync(memberDTO.Id);
            if (member == null) return null;
            member.Id = memberDTO.Id;
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
