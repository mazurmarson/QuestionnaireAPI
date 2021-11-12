using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Context
{
    public class QuestionnaireDbContext : DbContext
    {
        private string _connectionString = "Server=DESKTOP-A0EEVH8\\SQLEXPRESS;Database=QuestionnaireAPIDB;Trusted_Connection=True;MultipleActiveResultSets=true";
        public DbSet<Answer> Answers {get;set;}
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswerOpen> OpenQuestionsAnswers { get; set; }
        public DbSet<QuestionAnswerClose> CloseQuestionsAnswers { get; set; }
        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<SubAnswer> SubAnswers { get; set; }
        public DbSet<User> Users {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}