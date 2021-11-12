using System;
using System.Collections.Generic;

namespace QuestionnaireAPI.Models
{
    public class Questionnaire
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public List<Question> Questions { get; set; }
    }
}