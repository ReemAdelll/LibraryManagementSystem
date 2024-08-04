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
        public async Task<Member> AddAsync(Member entity)
        {
            //var member = new Member
            //{
            //    Id = memberDTO.Id,
            //    FirstName = memberDTO.FirstName,
            //    LastName = memberDTO.LastName,
            //    Email = memberDTO.Email,
            //    PhoneNumber = memberDTO.PhoneNumber
            //};
            //_context.Members.Add(member);
            //await _context.SaveChangesAsync();
            //memberDTO.Id = member.Id;
            //return memberDTO;
            _context.Members.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
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
        public IQueryable<Member> GetAll()
        {
            return _context.Members.Select(a => new Member
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber
            });
        }
     
        //working
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
                PhoneNumber = member.PhoneNumber
            };
        }
        //working
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
