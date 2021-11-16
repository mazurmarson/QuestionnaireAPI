using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Context;
using QuestionnaireAPI.Exceptions;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Repos
{
    public class QuestionRepo : GenRepo, IQuestionRepo
    {
        private readonly QuestionnaireDbContext _context;

        public QuestionRepo(QuestionnaireDbContext context):base(context)
        {
            _context = context;
        }
        public async Task<Question> AddQuestion(int questionnaireId, Question question, int userId) 
        {
            var questionnaire = _context.Questionnaires.FirstOrDefault(x => x.Id == questionnaireId);
            if(questionnaire is null)
            {
                throw new NotFoundException("Not found questionnaire");
            }
            if(questionnaire.UserId != userId)
            {
                throw new UnauthorizedException("Unauthorized, is not your questionnaire");
            }
            question.QuestionnaireId = questionnaireId;
            await _context.AddAsync(question);
            await _context.SaveChangesAsync();

            return question;
        }

        public async Task DeleteQuestion(int questionId, int userId)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == questionId);
            if(question is null)
            {
                throw new NotFoundException("Not found question");
            }
            var questionnaireOwnerId = await _context.Questionnaires.Where( x=> x.Id == question.QuestionnaireId ).Select(x => x.UserId).FirstOrDefaultAsync();
            if(questionnaireOwnerId != userId)
            {
                throw new UnauthorizedException("Unauthorized, is not your question");
            }

             _context.Remove(question);
            await _context.SaveChangesAsync();
        }
    }
}