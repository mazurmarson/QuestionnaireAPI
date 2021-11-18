using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Dtos
{
    public class SubAnswerInQuestionnaireResultDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int AmountOfAnswers {get; set;}
    }
}