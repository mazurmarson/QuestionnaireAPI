using System.Collections.Generic;

namespace QuestionnaireAPI.Models
{
    public class SubAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string Content { get; set; }
        public List<QuestionAnswerClose> QuestionAnswerCloseList {get; set;}
    }
}