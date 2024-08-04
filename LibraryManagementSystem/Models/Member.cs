using LibraryManagementSystem.Shared;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Member : BaseEntity <int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        // Navigation Prop
        public ICollection<BorrowedBook> BorrowedBooks { get; set; }

        public static implicit operator MemberDTO(Member member)
        {
            return new MemberDTO {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber,
                CreationTime = member.CreationTime,
                LastUpdateTime = member.LastUpdateTime,
            };
        }
        public static implicit operator MemberCreateDTO(Member member)
        {
            return new MemberCreateDTO {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber,
            };
        }
        public static implicit operator Member(MemberCreateDTO memberCreateDTO)
        {
            return new Member {
                Id = memberCreateDTO.Id,
                FirstName = memberCreateDTO.FirstName,
                LastName = memberCreateDTO.LastName,
                Email = memberCreateDTO.Email,
                PhoneNumber = memberCreateDTO.PhoneNumber,
            };
        }
        public static implicit operator MemberEditDTO(Member member)
        {
            return new MemberEditDTO {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber,
            };
        }
        public static implicit operator Member(MemberEditDTO memberEditDTO)
        {
            return new Member {
                Id = memberEditDTO.Id,
                FirstName = memberEditDTO.FirstName,
                LastName = memberEditDTO.LastName,
                Email = memberEditDTO.Email,
                PhoneNumber = memberEditDTO.PhoneNumber,
            };
        }
    }
}
