using System.Linq;
using FluentValidation;
using QuestionnaireAPI.Context;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Validators
{
    public class AddOpenAnswersValidator : AbstractValidator<QuestionAnswerOpen>
    {
        public AddOpenAnswersValidator(QuestionnaireDbContext dbContext)
        {

        }
    }
}