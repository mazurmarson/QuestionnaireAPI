using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionnaireAPI.Models;
using QuestionnaireAPI.Repos;

namespace QuestionnaireAPI.Controllers
{
    [Route("api/questionnaire/{questionnaireId}/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepo _repo;
        public QuestionController(IQuestionRepo repo)
        {
            _repo = repo;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddQuestion([FromRoute] int questionnaireId,[FromBody]Question question)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _repo.AddQuestion(questionnaireId,question, userId);

            return Ok();
        }
        [Authorize]
        [HttpDelete("{questionId}")]
        public async Task<ActionResult> DeleteQuestionnaire([FromRoute] int questionId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _repo.DeleteQuestion(questionId, userId);
            return Ok();
        }
    }
}