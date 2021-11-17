using System.Collections.Generic;

namespace QuestionnaireAPI.Models
{
    public class Question
    {
        public int Id { get; set; }
        public QuestionType QuestionType { get; set; }
        public string QuestionContent { get; set; }
        public int QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; }
        public List<SubAnswer> SubAnswers {get; set;}
        public List<QuestionAnswerOpen> OpenQuestionAnswerList {get; set;}

    }
}