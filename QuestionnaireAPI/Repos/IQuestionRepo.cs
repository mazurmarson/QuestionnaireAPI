using System.Threading.Tasks;
using QuestionnaireAPI.Dtos;
using QuestionnaireAPI.Helpers;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Repos
{
    public interface IQuestionRepo : IGenRepo
    {
         Task<Question> AddQuestion(int questionnaireId,QuestionAddDto question, int userId);
         Task DeleteQuestion(int questionId, UserIdAndRole userIdAndRole);
    }
}