using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionnaireAPI.Models;
using QuestionnaireAPI.Repos;

namespace QuestionnaireAPI.Controllers
{
    [Route("api/questionnaire/{questionnaireId}/question/{questionId}/[controller]")]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerRepo _repo;
        public AnswerController(IAnswerRepo repo)
        {
            _repo = repo;
        }
        [Authorize]
        [HttpPost("subanswer")]
        public async Task<ActionResult> AddSubAnswer([FromRoute] int questionId, [FromBody] List<SubAnswer> subAnswers)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _repo.AddSubAnswer(questionId, subAnswers, userId);

            return Ok();
        }

        [HttpPost] 
        public async Task<ActionResult> AddOpenAnswer([FromRoute] int questionId, [FromBody] QuestionAnswerOpen questionAnswerOpen)
        {
            await _repo.AddOpenAnswer(questionId, questionAnswerOpen);

            return Ok();
        }

        [HttpPost("close")]
        public async Task<ActionResult> AddCloseAnswer([FromRoute] int questionId, [FromBody] List<QuestionAnswerClose> questionAnswerClosesList)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _repo.AddCloseAnswer(questionId,questionAnswerClosesList);

            return Ok();
        }
        [Authorize]
        [HttpDelete("{subAnswerId}")]
        public async Task<ActionResult> DeleteSubAnswer([FromRoute] int subAnswerId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
             await _repo.DeleteSubAnswer(subAnswerId, userId);

            return Ok();
        }
        


    }
}