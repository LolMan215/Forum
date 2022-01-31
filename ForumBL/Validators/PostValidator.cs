using FluentValidation;
using ForumBL.DTOs;

namespace ForumBL.Validators
{
    public class PostCreateNewValidator : AbstractValidator<PostDTO>
    {
        public PostCreateNewValidator()
        {
            RuleFor(x => x.ForumId)
                .NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty();

            RuleFor(x => x.Body)
                .NotEmpty();
        }
    }

    public class PostEditValidator : AbstractValidator<PostDTO>
    {
        public PostEditValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty();

            RuleFor(x => x.Body)
                .NotEmpty();
        }
    }
}
