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

        private Member MapToMember(MemberDTO memberDto) 
        {
        return new Member { Id = memberDto.Id, FirstName= memberDto.FirstName,
            LastName= memberDto.LastName, Email=memberDto.Email, PhoneNumber= memberDto.PhoneNumber };
        }
        private MemberDTO MapToMemberDTO( MemberDTO memberDto)
        {
            return new MemberDTO
            {
                Id = memberDto.Id,
                FirstName = memberDto.FirstName,
                LastName = memberDto.LastName,
                Email = memberDto.Email,
                PhoneNumber = memberDto.PhoneNumber
            };
        }

        //old get without filter
        //[HttpGet]
        //public IQueryable<MemberDTO> GetAll()
        //{
        //    return _unitOfWork.Members.GetAll();
        //}


        //new get with filter
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? firstName, [FromQuery] string? lastName, [FromQuery] string? sortOrder)
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
            return Ok(members);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member == null) return NotFound();
            return Ok(member);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, [FromBody] MemberDTO memberDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingMember = await _unitOfWork.Members.GetByIdAsync(id);
            if (existingMember == null) return NotFound();

            memberDto.Id = id;
            await _unitOfWork.Members.UpdateAsync(memberDto);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember([FromBody] MemberDTO memberDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var member = await _unitOfWork.Members.AddAsync(memberDto);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetMemberById), new { id = member.Id }, member);
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
