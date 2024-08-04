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
        
        [HttpGet]
        public IQueryable<BorrowedBook> GetAll()
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBorrowedBookById(int id)
        {
            var BorrowedBook = await _unitOfWork.borrowedBooks.GetByIdAsync(id);
            if (BorrowedBook == null) return NotFound();
            return Ok(BorrowedBook);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBorrowedBook(int id, [FromBody] BorrowedBookEditDTO BorrowedBookEditDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBorrowedBook = await _unitOfWork.borrowedBooks.GetByIdAsync(id);
            if (existingBorrowedBook == null) return NotFound();

            //implicit conversion
            BorrowedBook borrowedBook = BorrowedBookEditDTO;

            borrowedBook.Id = id;
            await _unitOfWork.borrowedBooks.UpdateAsync(borrowedBook);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateBorrowedBook([FromBody] BorrowedBookCreateDTO borrowedBookCreateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // implicit conversion
            BorrowedBook borrowedBook = borrowedBookCreateDTO;

            var borrowedBOOK = await _unitOfWork.borrowedBooks.AddAsync(borrowedBook);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetBorrowedBookById), new { id = borrowedBOOK.Id }, (BorrowedBook)borrowedBOOK);
        }
        
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
