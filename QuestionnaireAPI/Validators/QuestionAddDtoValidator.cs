using FluentValidation;
using QuestionnaireAPI.Context;
using QuestionnaireAPI.Dtos;

namespace QuestionnaireAPI.Validators
{
    public class QuestionAddDtoValidator : AbstractValidator<QuestionAddDto>
    {
        public QuestionAddDtoValidator(QuestionnaireDbContext dbContext)
        {
            RuleFor(x => x.QuestionContent).NotEmpty().MaximumLength(100).MinimumLength(5);
            RuleFor(x => x.QuestionType).Custom((value, context)=> {
                if(!(value.ToString() == "Open" || value.ToString() == "Single" || value.ToString() == "Multiple"))
                {
                    context.AddFailure("QuestionType", "Wrong question type");
                }
            });
            RuleForEach(x => x.SubAnswers).ChildRules(subanswers => {
                subanswers.RuleFor(x => x.Content).MaximumLength(100).MinimumLength(1).NotNull();
            });
        }


    }
}