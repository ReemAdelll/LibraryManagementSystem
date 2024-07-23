using FluentValidation;

namespace LibraryManagementSystem.Shared.Validators
{
    public class MemberDTOValidator : AbstractValidator<MemberDTO>
    {
        //working
        public MemberDTOValidator()
        {
            RuleFor(m => m.FirstName).NotEmpty().WithMessage("Member First Name Is Required").MinimumLength(3);
            RuleFor(m => m.LastName).NotEmpty().WithMessage("Member Last Name Is Required").MinimumLength(3);
            RuleFor(m => m.Email).NotEmpty().WithMessage("Member Mail Is Required");
            RuleFor(m=>m.PhoneNumber).NotEmpty().WithMessage("Member Phone Number Is Required");
        }
    }
}
