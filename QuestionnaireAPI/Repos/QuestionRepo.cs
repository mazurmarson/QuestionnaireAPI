using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Context;
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
        public async Task<Question> AddQuestion(int questionnaireId, Question question) 
        {
            var questionnaire = _context.Questionnaires.FirstOrDefault(x => x.Id == questionnaireId);
            if(questionnaire is null)
            {
                throw new System.Exception();
            }
            question.QuestionnaireId = questionnaireId;
            await _context.AddAsync(question);
            await _context.SaveChangesAsync();

            return question;
        }

        public async Task DeleteQuestion(int questionId)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == questionId);
            if(question is null)
            {
                throw new System.Exception();
            }

             _context.Remove(question);
            await _context.SaveChangesAsync();
        }
    }
}