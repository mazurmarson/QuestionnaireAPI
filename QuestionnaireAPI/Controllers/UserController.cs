using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestionnaireAPI.Dtos;
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
         public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
         {
            await _repo.Register(registerUserDto);
            return StatusCode(201);
         }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            string token = await _repo.GenerateJwt(dto);
            return Ok(token);
        }
    }
}