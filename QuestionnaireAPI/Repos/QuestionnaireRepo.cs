using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Context;
using QuestionnaireAPI.Exceptions;
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



        public async Task<Questionnaire> AddQuestionnaire(Questionnaire questionnaire, int userId)
        {
          var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if(user is null)
            {
                throw new NotFoundException("This user does not exist");
            }
           questionnaire.User = user;
           questionnaire.CreateDate = System.DateTime.Now;
            await _context.AddAsync(questionnaire);
            await _context.SaveChangesAsync();

            return questionnaire;
        }

        public async Task DeleteQuestionnaire(int questionnaireId, int userId)
        {
            var questionnaire = await _context.Questionnaires.FirstOrDefaultAsync(x => x.Id == questionnaireId);
            if(questionnaire is null)
            {
                throw new NotFoundException("This questionnaire does not exist");
            }
            if(questionnaire.UserId != userId)
            {
                throw new UnauthorizedException("This resource is not your");
            }

             _context.Remove(questionnaire);
            await _context.SaveChangesAsync();

        }
    }
}