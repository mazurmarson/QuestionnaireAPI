using System.Collections.Generic;
using System.Threading.Tasks;
using QuestionnaireAPI.Dtos;
using QuestionnaireAPI.Helpers;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Repos
{
    public interface IAnswerRepo
    {
         Task<List<SubAnswer>> AddSubAnswer(int questionId,List<SubAnswerAddDto> subAnswers, int userId);
         Task<QuestionAnswerOpen> AddOpenAnswer(int questionId,QuestionAnswerContentAddDto questionAnswerContentAddDto);
         Task<List<QuestionAnswerClose>> AddCloseAnswer(int questionId, List<QuestionAnswerCloseAddDto> questionAnswerCloseList);
         Task DeleteSubAnswer(int subAnswerId, UserIdAndRole userIdAndRole);
    }
}