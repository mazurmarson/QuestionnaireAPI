using System.Threading.Tasks;

namespace QuestionnaireAPI.Repos
{
    public interface IGenRepo
    {
        void Add<T>(T entity) where T: class;
         void Delete<T>(T enitty) where T: class;
         void Edit<T>(T entity) where T: class;
         Task<bool> SaveAll();
    }
}