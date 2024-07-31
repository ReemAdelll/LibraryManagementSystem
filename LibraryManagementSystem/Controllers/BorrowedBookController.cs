using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowedBookController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BorrowedBookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private BorrowedBook MapToBorrowedBook(BorrowedBookDTO borrowedBookDTO)
        {
            return new BorrowedBook
            {
                Id = borrowedBookDTO.Id,
                MemberId = borrowedBookDTO.MemberId
            ,
                BookId = borrowedBookDTO.BookId,
                BorrowDate = borrowedBookDTO.BorrowDate,
                ReturnDate = borrowedBookDTO.ReturnDate,

            };
        }
        private BorrowedBookDTO MapToBorrowedBookDTO(BorrowedBook borrowedBook)
        { return new BorrowedBookDTO {
            Id = borrowedBook.Id,
            MemberId = borrowedBook.MemberId
            ,
            BookId = borrowedBook.BookId,
            BorrowDate = borrowedBook.BorrowDate,
            ReturnDate = borrowedBook.ReturnDate,
        }; 
        }
        //working
        [HttpGet]
        public IQueryable<BorrowedBookDTO> GetAll()
        {
            return _unitOfWork.borrowedBooks.GetAll();
        }
        //get with filter
        [HttpGet("Get With Filter")]
        public async Task<IActionResult> GetAllWithFilter([FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null, [FromQuery] string? sortOrder = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var borrowedBooks = await _unitOfWork.borrowedBooks.GetAllWithFilter(startDate, endDate, sortOrder,page,pageSize);
            return Ok(borrowedBooks);
        }


        //working
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBorrowedBookById(int id)
        {
            var BorrowedBook = await _unitOfWork.borrowedBooks.GetByIdAsync(id);
            if (BorrowedBook == null) return NotFound();
            return Ok(BorrowedBook);
        }
        //working
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBorrowedBook(int id, [FromBody] BorrowedBookDTO BorrowedBookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBorrowedBook = await _unitOfWork.borrowedBooks.GetByIdAsync(id);
            if (existingBorrowedBook == null) return NotFound();

            BorrowedBookDto.Id = id;
            await _unitOfWork.borrowedBooks.UpdateAsync(BorrowedBookDto);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
        //working
        [HttpPost]
        public async Task<IActionResult> CreateBorrowedBook([FromBody] BorrowedBookDTO BorrowedBookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var borrowedBook = await _unitOfWork.borrowedBooks.AddAsync(BorrowedBookDto);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetBorrowedBookById), new { id = borrowedBook.Id }, borrowedBook);
        }
        //working
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrowedBook(int id)
        {
            var success = await _unitOfWork.borrowedBooks.DeleteAsync(id);
            if (!success) return NotFound();

            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
