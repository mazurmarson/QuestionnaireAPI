using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionnaireAPI.Dtos;
using QuestionnaireAPI.Helpers;
using QuestionnaireAPI.Models;
using QuestionnaireAPI.Repos;

namespace QuestionnaireAPI.Controllers
{
    [Route("api/questionnaire/{questionnaireId}/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepo _repo;
        public QuestionController(IQuestionRepo repo)
        {
            _repo = repo;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddQuestion([FromRoute] int questionnaireId,[FromBody]QuestionAddDto questionAddDto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _repo.AddQuestion(questionnaireId,questionAddDto, userId);

            return Ok();
        }
        [Authorize]
        [HttpDelete("{questionId}")]
        public async Task<ActionResult> DeleteQuestionnaire([FromRoute] int questionId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            string userRole = (User.FindFirst(ClaimTypes.Role).Value);
            UserType userRoleEnum = (UserType)Enum.Parse(typeof(UserType),userRole, true);
            var userIdAndRole = new UserIdAndRole{
                UserId = userId,
                UserType = userRoleEnum
            };
            await _repo.DeleteQuestion(questionId, userIdAndRole);
            return Ok();
        }
    }
}