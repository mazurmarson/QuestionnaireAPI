using System.Linq;
using FluentValidation;
using QuestionnaireAPI.Context;
using QuestionnaireAPI.Dtos;

namespace QuestionnaireAPI.Validators
{
    public class QuestionnaireAddDtoValidator : AbstractValidator<QuestionnaireAddDto>
    {
        public QuestionnaireAddDtoValidator(QuestionnaireDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(4).MaximumLength(100);
            
        }
    }
}