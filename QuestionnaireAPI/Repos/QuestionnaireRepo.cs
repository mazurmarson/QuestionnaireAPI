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
    }
}