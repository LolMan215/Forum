using FluentValidation;
using ForumBL.DTOs;

namespace ForumBL.Validators
{
    public class CommentCreateNewValidator : AbstractValidator<CommentDTO>
    {
        public CommentCreateNewValidator()
        {
            RuleFor(x => x.PostId)
                .NotEmpty();

            RuleFor(x => x.Body)
                .NotEmpty();
        }
    }

    public class CommentEditValidator : AbstractValidator<CommentDTO>
    {
        public CommentEditValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Body)
                .NotEmpty();
        }
    }
}
