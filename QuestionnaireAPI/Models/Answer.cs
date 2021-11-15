using System.Collections.Generic;

namespace QuestionnaireAPI.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public List<SubAnswer> Subanswers { get; set; }
     //   public List<QuestionAnswerClose> UserAnswers {get; set;}
    }
}