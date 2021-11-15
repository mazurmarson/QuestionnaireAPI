using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<ActionResult> AddQuestion([FromRoute] int questionnaireId,[FromBody]Question question)
        {
            await _repo.AddQuestion(questionnaireId,question);

            return Ok();
        }
    }
}