using System.Collections.Generic;
using FluentValidation;
using QuestionnaireAPI.Context;
using QuestionnaireAPI.Dtos;

namespace QuestionnaireAPI.Validators
{
    public class SubAnswerAddValidatior : AbstractValidator<List<SubAnswerAddDto>>
    {
        public SubAnswerAddValidatior(QuestionnaireDbContext dbContext)
        {
           RuleForEach(x => x).ChildRules(x => x.RuleFor(x => x.Content).MinimumLength(1).MaximumLength(100));
        }

    }
}