using System.Threading.Tasks;
using QuestionnaireAPI.Context;

namespace QuestionnaireAPI.Repos
{
    public class GenRepo : IGenRepo
    {
        private readonly QuestionnaireDbContext _context;

        public GenRepo(QuestionnaireDbContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T enitty) where T : class
        {
            _context.Remove(enitty);
        }

        public void Edit<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveAll()
        {
            
            return await _context.SaveChangesAsync() > 0;
        }
    }
}