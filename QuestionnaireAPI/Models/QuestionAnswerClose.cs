using System.Collections.Generic;

namespace QuestionnaireAPI.Models
{
    public class QuestionAnswerClose
    {
        public int Id { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        public int? SubAnswerId {get; set;}
        public virtual SubAnswer SubAnswer { get; set; }
    }
}