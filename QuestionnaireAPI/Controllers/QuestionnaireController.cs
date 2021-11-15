using System.Threading.Tasks;
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
        [HttpPost]
        public async Task<ActionResult> AddQuestionnaire(Questionnaire questionnaire)
        {
           var user = await _userRepo.GetUserById(questionnaire.UserId);
           questionnaire.User = user;
           questionnaire.CreateDate = System.DateTime.Now;
            await _repo.AddQuestionnaire(questionnaire);

            return Ok(user);
        }

    }
}