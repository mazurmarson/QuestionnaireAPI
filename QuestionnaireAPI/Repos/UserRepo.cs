using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Context;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Repos
{
    public class UserRepo : GenRepo, IUserRepo
    {
        private readonly QuestionnaireDbContext _context;

        public UserRepo(QuestionnaireDbContext context):base(context)
        {
            _context = context;
        }


        public async Task<User> Register(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }


    }
}