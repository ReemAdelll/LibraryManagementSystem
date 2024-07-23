using FluentValidation;


namespace LibraryManagementSystem.Shared.Validators
{
    public class AuthorDTOValidator : AbstractValidator<AuthorDTO>
    {
        //working
        public AuthorDTOValidator()
        {
            RuleFor(a=>a.Name).NotEmpty().WithMessage("Author name is required.");
            RuleFor(a=>a.Country).NotEmpty().WithMessage("Author Country is required.");
            RuleFor(a => a.Bio).NotEmpty().WithMessage("Author Bio is required.");
        }
    }
}
