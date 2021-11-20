using FluentValidation;
using QuestionnaireAPI.Dtos;

namespace QuestionnaireAPI.Validators
{
    public class QuestionAnswerContentAddDtoValidator : AbstractValidator<QuestionAnswerContentAddDto>
    {
        public QuestionAnswerContentAddDtoValidator()
        {
            RuleFor(x => x.AnswerContent).NotNull().NotEmpty().MinimumLength(1).MaximumLength(250);
        }
    }
}