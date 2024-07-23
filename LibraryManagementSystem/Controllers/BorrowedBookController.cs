using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Shared;
using Microsoft.AspNetCore.Mvc;

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
                BorrowedBookId = borrowedBookDTO.BorrowedBookId,
                MemberId = borrowedBookDTO.MemberId
            ,
                BookId = borrowedBookDTO.BookId,
                BorrowDate = borrowedBookDTO.BorrowDate,
                ReturnDate = borrowedBookDTO.ReturnDate,

            };
        }
        private BorrowedBookDTO MapToBorrowedBookDTO(BorrowedBook borrowedBook)
        { return new BorrowedBookDTO {
            BorrowedBookId = borrowedBook.BorrowedBookId,
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

            BorrowedBookDto.BorrowedBookId = id;
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

            return CreatedAtAction(nameof(GetBorrowedBookById), new { id = borrowedBook.BorrowedBookId }, borrowedBook);
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
