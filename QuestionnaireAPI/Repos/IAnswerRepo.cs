using System.Collections.Generic;
using System.Threading.Tasks;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Repos
{
    public interface IAnswerRepo
    {
         Task<Answer> AddSubAnswer(int questionId,Answer answer);
         Task<QuestionAnswerOpen> AddOpenAnswer(int questionId,QuestionAnswerOpen questionAnswerOpen);
         Task<List<QuestionAnswerClose>> AddCloseAnswer(int questionId, List<QuestionAnswerClose> questionAnswerCloseList);
    }
}