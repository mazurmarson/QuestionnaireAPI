using System.Collections.Generic;

namespace QuestionnaireAPI.Models
{
    public class SubAnswer
    {
        public int Id { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        public string Content { get; set; }
        public List<QuestionAnswerClose> QuestionAnswerCloseList {get; set;}
    }
}