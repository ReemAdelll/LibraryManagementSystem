using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Shared.Validators
{
    public class BookDTOValidator : AbstractValidator<BookDTO>
    {
        //working
        public BookDTOValidator()
        {
            RuleFor(b => b.Title).NotEmpty().WithMessage("Title Is Required").MaximumLength(20);
            RuleFor(b => b.AuthorId).NotEmpty().WithMessage("Author Id Is Required");
            RuleFor(b => b.PublishedYear).NotEmpty();
        }
    }
}
