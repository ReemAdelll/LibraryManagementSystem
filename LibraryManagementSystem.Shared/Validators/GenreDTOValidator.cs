using FluentValidation;


namespace LibraryManagementSystem.Shared.Validators
{
    public class GenreDTOValidator : AbstractValidator<GenreDTO>
    {
        //working
        public GenreDTOValidator()
        {
            RuleFor(g => g.GenreName).NotEmpty().WithMessage("Genre Name Is Required").MaximumLength(50);
        }
    }
}
