﻿using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Services
{
	public class AuthorRepo : IAuthorRepo
	{
		private readonly LibraryContext _context;

		public AuthorRepo(LibraryContext context)
		{
			_context = context;
		}

		public IQueryable<Author> GetAll()
		{
			return _context.Authors
				.Select(a => new Author
				{
					Id = a.Id,
					Name = a.Name,
					Country = a.Country,
                    CreationTime = a.CreationTime,
                    LastUpdateTime = a.LastUpdateTime,
                });
		}
		public async Task<Author> GetByIdAsync(int id)
		{
			var author = await _context.Authors.FindAsync(id);
			if (author == null) return null;
			return new Author { Id = author.Id, Name = author.Name, CreationTime = author.CreationTime, LastUpdateTime= author.LastUpdateTime };
		}
      
        //special method for author, not from base
        //authors with their books (With filtering)
        public async Task<IEnumerable<Author>> GetAllAuthorsWithBooksAsync(string? authorName, string? bookName)
        {
            var query = _context.Authors.Include(a => a.Books).AsQueryable();

            if (!string.IsNullOrEmpty(authorName))
            {
                query = query.Where(a => a.Name.ToLower().Contains(authorName.ToLower()));
            }

            if (!string.IsNullOrEmpty(bookName))
            {
                query = query.Where(a => a.Books.Any(b => b.Title.ToLower().Contains(bookName.ToLower())));
            }

            return await query.ToListAsync();
        }

		public async Task<bool> DeleteAsync(int id)
		{
			var author = await _context.Authors.FindAsync(id);
			if (author == null) return false;
			_context.Authors.Remove(author);
			await _context.SaveChangesAsync();
			return true;
		}

        public async Task<Author> AddAsync(Author entity)
        {
            _context.Authors.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Author> UpdateAsync(Author entity)
        {
            var existingAuthor = await _context.Authors.FindAsync(entity.Id);
            if (existingAuthor == null) return null;

            existingAuthor.Name = entity.Name;
            existingAuthor.Country = entity.Country;

            _context.Authors.Update(existingAuthor);
            await _context.SaveChangesAsync();

            return existingAuthor;
        }
    }
}
