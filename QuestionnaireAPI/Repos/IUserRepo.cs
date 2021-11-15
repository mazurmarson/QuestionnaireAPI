using System.Threading.Tasks;
using QuestionnaireAPI.Dtos;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Repos
{
    public interface IUserRepo : IGenRepo
    {
         Task Register(RegisterUserDto registerUserDto);
         Task<User> GetUserById(int id);
         Task<string> GenerateJwt(LoginDto dto);
    }
}