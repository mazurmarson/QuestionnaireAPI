using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionnaireAPI.Models;
using QuestionnaireAPI.Repos;

namespace QuestionnaireAPI.Controllers
{

        [Route("api/[controller]")]
    [ApiController]
    public class QuestionnaireController : ControllerBase
    {
        private readonly IQuestionnaireRepo _repo;
        private readonly IUserRepo _userRepo;
        public QuestionnaireController(IQuestionnaireRepo repo, IUserRepo userRepo)
        {
            _repo = repo;
            _userRepo = userRepo;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddQuestionnaire(Questionnaire questionnaire)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await _repo.AddQuestionnaire(questionnaire, userId);

            return Ok(questionnaire);
        }
        [Authorize]
        [HttpDelete("{questionnaireId}")]
        public async Task<ActionResult> DeleteQuestionnaire([FromRoute] int questionnaireId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _repo.DeleteQuestionnaire(questionnaireId, userId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetQuestionnaires()
        {
            var questionnaries = await _repo.GetQuestionnaires();

            return Ok(questionnaries);
        }

        [HttpGet("{questionnaireId}")]
        public async Task<ActionResult> GetQuestionnaire([FromRoute] int questionnaireId)
        {
            var questionnaire = await _repo.GetQuestionnaire(questionnaireId);
            return Ok(questionnaire);
        }

        [HttpGet("{questionnaireId}/results")]
        public async Task<ActionResult> GetQuestionnaireResults([FromRoute] int questionnaireId)
        {
            var results = await _repo.GetQuestionnaireResults(questionnaireId);
            return Ok(results);
        }
    }
}