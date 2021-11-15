using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestionnaireAPI.Models;
using QuestionnaireAPI.Repos;

namespace QuestionnaireAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
         private readonly IUserRepo _repo;

         public UserController(IUserRepo repo)
         {
             _repo = repo;
         }

         [HttpPost("register")]
         public async Task<IActionResult> Register(User user)
         {
            await _repo.Register(user);
             return Ok();
         }
    }
}