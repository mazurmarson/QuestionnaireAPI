using System.Threading.Tasks;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Repos
{
    public interface IUserRepo : IGenRepo
    {
         Task<User> Register(User user);
         Task<User> GetUserById(int id);
    }
}