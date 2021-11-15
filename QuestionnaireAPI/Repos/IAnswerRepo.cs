using System.Collections.Generic;
using System.Threading.Tasks;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Repos
{
    public interface IAnswerRepo
    {
         Task<List<SubAnswer>> AddSubAnswer(int questionId,List<SubAnswer> subAnswers);
         Task<QuestionAnswerOpen> AddOpenAnswer(int questionId,QuestionAnswerOpen questionAnswerOpen);
         Task<List<QuestionAnswerClose>> AddCloseAnswer(int questionId, List<QuestionAnswerClose> questionAnswerCloseList);
         Task DeleteSubAnswer(int subAnswerId);
    }
}