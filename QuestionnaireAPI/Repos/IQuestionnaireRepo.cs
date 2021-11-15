using System.Threading.Tasks;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Repos
{
    public interface IQuestionnaireRepo : IGenRepo
    {
         Task<Questionnaire> AddQuestionnaire(Questionnaire questionnaire);
         Task DeleteQuestionnaire(int questionnaireId);
         
    }
}