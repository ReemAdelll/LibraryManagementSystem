﻿using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Shared;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

        public AuthorController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		private Author MapToAuthor(AuthorDTO authorDto)
		{
			return new Author
			{
                Id = authorDto.Id,
				Name = authorDto.Name,
				Country = authorDto.Country,
				Bio = authorDto.Bio,
                //CreationTime = DateTime.Now,
                //LastUpdateTime = DateTime.Now
            };
		}
		private AuthorDTO MapToAuthorDTO(Author author)
		{
			return new AuthorDTO
			{
				Id = author.Id,
				Name = author.Name,
				Country = author.Country,
				Bio = author.Bio
			};
		}

        //working
        //[HttpGet]
        //public async Task<IActionResult> GetAllAuthors()
        //{
        //	var authors = await _unitOfWork.Authors.GetAllAsync();
        //	return Ok(authors);
        //}

        //working
        //old get without filter
        //[HttpGet]
        //public IQueryable<AuthorDTO> GetAll()
        //{
        //   return _unitOfWork.Authors.GetAll();
        //}

        //new get with filter added
		//Working
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? name,[FromQuery] string? sortOrder,[FromQuery] int page = 1,[FromQuery] int pageSize = 10)
        {
            var authorsQuery = _unitOfWork.Authors.GetAll();

            if (!string.IsNullOrEmpty(name))
            {
                authorsQuery = authorsQuery.Where(a => a.Name.ToLower().Contains(name.ToLower()));
            }
            if (sortOrder == "desc")
            {
                authorsQuery = authorsQuery.OrderByDescending(a => a.Country);
            }
            else
            {
                authorsQuery = authorsQuery.OrderBy(a => a.Country);
            }
            var authors = await authorsQuery.ToListAsync();

            var totalCount = await authorsQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var auth = await authorsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Authors = authors
            };
            return Ok(response);
        }



        //working
        [HttpGet("{id}")]
		public async Task<IActionResult> GetAuthorById(int id)
		{
			var author = await _unitOfWork.Authors.GetByIdAsync(id);
			if (author == null) return NotFound();
			return Ok(author);
		}
		//working
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDTO AuthorDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var existingAuthor = await _unitOfWork.Authors.GetByIdAsync(id);
			if (existingAuthor == null) return NotFound();

			AuthorDto.Id = id;
			await _unitOfWork.Authors.UpdateAsync(AuthorDto);
			await _unitOfWork.CompleteAsync();

			return NoContent();
		}
		//working
		[HttpPost]
		public async Task<IActionResult> CreateAuthor([FromBody] AuthorDTO AuthorDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var author = await _unitOfWork.Authors.AddAsync(AuthorDto);
			await _unitOfWork.CompleteAsync();

			return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
		}
		//working
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAuthor(int id)
		{
			var success = await _unitOfWork.Authors.DeleteAsync(id);
			if (!success) return NotFound();

			await _unitOfWork.CompleteAsync();
			return NoContent();
		}

        //working
        [HttpGet("authors")]
        public async Task<IActionResult> GetAllAuthorsWithBooks([FromQuery] string? authorName, [FromQuery] string? bookName)
        {
            var authors = await _unitOfWork.Authors.GetAllAuthorsWithBooksAsync(authorName, bookName);
            return Ok(authors);
        }
    }
}
