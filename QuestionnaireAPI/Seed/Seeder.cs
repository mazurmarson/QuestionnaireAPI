using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using QuestionnaireAPI.Context;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Seed
{
    public class Seeder
    {
        private readonly QuestionnaireDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwrodHasher;
        public Seeder(QuestionnaireDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwrodHasher = passwordHasher;
        }

        public void SeedUsers()
        {
            var userData = File.ReadAllText("Seed/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            foreach(var user in users)
            {
                var hashedPassword = _passwrodHasher.HashPassword(user, user.PasswordHash);
                user.PasswordHash = hashedPassword;

                _dbContext.Users.Add(user);
            }
            _dbContext.SaveChanges();
        }

        public void SeedQuestionnaries()
        {
            var questionnariesData = File.ReadAllText("Seed/QuestionnaireSeedData.json");
            var questionnaires = JsonConvert.DeserializeObject<List<Questionnaire>>(questionnariesData);

            foreach(var questionnaire in questionnaires)
            {
                _dbContext.Questionnaires.Add(questionnaire);
            }
            _dbContext.SaveChanges();
        }

        public void SeedCloseQuestionsAnswers()
        {
            var closeAnswersData = File.ReadAllText("Seed/CloseQuestionAnswerSeedData.json");
            var closeAnswers = JsonConvert.DeserializeObject<List<QuestionAnswerClose>>(closeAnswersData);

            foreach(var closeAnswer in closeAnswers)
            {
                _dbContext.CloseQuestionsAnswers.Add(closeAnswer);
            }
            _dbContext.SaveChanges();
        }

        public void SeedOpenQuestionsAnswers()
        {
            var openQuestionsData = File.ReadAllText("Seed/OpenQuestionAnswerSeedData.json");
            var openQuestions = JsonConvert.DeserializeObject<List<QuestionAnswerOpen>>(openQuestionsData);
            foreach(var openAnswer in openQuestions)
            {
                _dbContext.OpenQuestionsAnswers.Add(openAnswer);
            }
            _dbContext.SaveChanges();
        }


    }
}