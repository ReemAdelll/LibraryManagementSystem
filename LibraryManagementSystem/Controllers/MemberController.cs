using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //new get with filter
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? firstName, [FromQuery] string? lastName, [FromQuery] string? sortOrder, [FromQuery] int page = 1, [FromQuery] int pagesize=10)
        {
            var membrquery = _unitOfWork.Members.GetAll();
            if(!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                membrquery = membrquery.Where(a=>a.FirstName.ToLower().Contains(firstName.ToLower()));
            }
            if (!string.IsNullOrEmpty(sortOrder))
            {
                membrquery = sortOrder.ToLower() == "desc"
                    ? membrquery.OrderByDescending(a => a.FirstName)
                    : membrquery.OrderBy(a => a.FirstName);
            }
            var members = await membrquery.ToListAsync();
    
            var totalcount = await membrquery.CountAsync();
            var totalpages = (int)Math.Ceiling((double)totalcount / pagesize);
            var memb = await membrquery
                .Skip((page - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();

            var response = new
            {
                TotalCount = totalcount,
                TotalPages = totalpages,
                CurrentPage = page,
                PageSize = pagesize,
                Members = members
            };
            return Ok(response);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member == null) return NotFound();
            return Ok(member);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, [FromBody] MemberEditDTO memberEditDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingMember = await _unitOfWork.Members.GetByIdAsync(id);
            if (existingMember == null) return NotFound();

            //implicit conversion
            Member member = memberEditDTO;

            member.Id = id;
            await _unitOfWork.Members.UpdateAsync(member);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember([FromBody] MemberCreateDTO memberCreateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // implicit conversion
            Member member = memberCreateDTO;

            var memberAdded = await _unitOfWork.Members.AddAsync(member);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetMemberById), new { id = memberAdded.Id }, (MemberDTO)memberAdded);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var success = await _unitOfWork.Members.DeleteAsync(id);
            if (!success) return NotFound();

            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
