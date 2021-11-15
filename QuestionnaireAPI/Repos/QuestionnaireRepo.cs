using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Context;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Repos
{
    public class QuestionnaireRepo : GenRepo, IQuestionnaireRepo
    {
        private readonly QuestionnaireDbContext _context;
        public QuestionnaireRepo(QuestionnaireDbContext context) :base(context)
        {
            _context = context;
        }



        public async Task<Questionnaire> AddQuestionnaire(Questionnaire questionnaire)
        {

            await _context.AddAsync(questionnaire);
            await _context.SaveChangesAsync();

            return questionnaire;
        }

        public async Task DeleteQuestionnaire(int questionnaireId)
        {
            var questionnaire = await _context.Questionnaires.FirstOrDefaultAsync(x => x.Id == questionnaireId);
            if(questionnaire is null)
            {
                throw new System.Exception();
            }

             _context.Remove(questionnaire);
            await _context.SaveChangesAsync();

        }
    }
}