using System.Threading.Tasks;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Repos
{
    public interface IQuestionRepo : IGenRepo
    {
         Task<Question> AddQuestion(int questionnaireId,Question question, int userId);
         Task DeleteQuestion(int questionId, int userId);
    }
}