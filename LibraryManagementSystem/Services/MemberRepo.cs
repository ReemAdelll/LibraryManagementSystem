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
        
        public async Task<Member> AddAsync(Member entity)
        {
            _context.Members.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null) return false;
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return true;
        }
        public IQueryable<Member> GetAll()
        {
            return _context.Members.Select(a => new Member
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                CreationTime = a.CreationTime,
                LastUpdateTime = a.LastUpdateTime,
            });
        }
     
        public async Task<Member> GetByIdAsync(int id)
        {
            var member =  await _context.Members.FindAsync(id);
            if (member == null) return null;
            return new Member
            {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber,
                CreationTime = member.CreationTime,
                LastUpdateTime = member.LastUpdateTime,
            };
        }
        public async Task<Member> UpdateAsync(Member entity)
        {
            var existingMember = await _context.Members.FindAsync(entity.Id);
            if (existingMember == null) return null;
            existingMember.Id = entity.Id;
            existingMember.FirstName = entity.FirstName;
            existingMember.Email = entity.Email;
            existingMember.PhoneNumber = entity.PhoneNumber;
            _context.Members.Update(existingMember);
            await _context.SaveChangesAsync();
            return existingMember;
        }
    }
}
