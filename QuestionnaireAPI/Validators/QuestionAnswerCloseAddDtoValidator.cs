using System.Collections.Generic;
using FluentValidation;
using QuestionnaireAPI.Context;
using QuestionnaireAPI.Dtos;

namespace QuestionnaireAPI.Validators
{
    public class QuestionAnswerCloseAddDtoValidator : AbstractValidator<List<QuestionAnswerCloseAddDto>>
    {
        public QuestionAnswerCloseAddDtoValidator(QuestionnaireDbContext dbContext)
        {

        }
    }
}