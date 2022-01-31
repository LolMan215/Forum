using FluentValidation;
using ForumBL.DTOs;

namespace ForumBL.Validators
{
    public class ForumCreateNewValidator : AbstractValidator<ForumDTO>
    {
        public ForumCreateNewValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }

    public class ForumEditValidator : AbstractValidator<ForumDTO>
    {
        public ForumEditValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
