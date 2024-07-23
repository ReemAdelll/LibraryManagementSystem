using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Shared.Validators
{
    public class BorrowedBookDTOValidator : AbstractValidator<BorrowedBookDTO>
    {
        //working
        public BorrowedBookDTOValidator() 
        {
         RuleFor(b=>b.MemberId).NotEmpty().WithMessage("Member id Is Required");
            RuleFor(b=>b.BookId).NotEmpty().WithMessage("Book id Is Required");
            RuleFor(b=>b.BorrowDate).NotEmpty().WithMessage("Borrow Date id Is Required");
            RuleFor(b=>b.ReturnDate).NotEmpty().WithMessage("Return Date id Is Required");
        }
    }
}
