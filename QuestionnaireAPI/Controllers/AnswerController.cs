using System.Collections.Generic;
using System.Threading.Tasks;
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

        [HttpPost("subanswer")]
        public async Task<ActionResult> AddSubAnswer([FromRoute] int questionId, [FromBody] Answer answer)
        {
            await _repo.AddSubAnswer(questionId, answer);

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
            await _repo.AddCloseAnswer(questionId,questionAnswerClosesList);

            return Ok();
        }


    }
}