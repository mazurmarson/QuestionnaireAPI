using System.Collections.Generic;
using System.Threading.Tasks;
using QuestionnaireAPI.Dtos;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Repos
{
    public interface IQuestionnaireRepo : IGenRepo
    {
         Task<Questionnaire> AddQuestionnaire(Questionnaire questionnaire, int userId);
         Task DeleteQuestionnaire(int questionnaireId, int userId);
         Task<IEnumerable<QuestionnaireDisplayInListDto>> GetQuestionnaires();
         
         Task<QuestionnaireDetailsDto> GetQuestionnaire(int questionnaireId);

         Task<QuestionnaireResultsDto> GetQuestionnaireResults(int questionnaireId);
        

       // Task<List<Questionnaire>> GetQuestionnaire(int questionnaireId);
    }
}