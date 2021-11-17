using System.Collections.Generic;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Dtos
{
    public class QuestionInQuestionnaireDto
    {
         public int Id { get; set; }
        public QuestionType QuestionType { get; set; }
        public string QuestionContent { get; set; }

        public List<SubAnswerInQuestionnaireDto> SubAnswers {get; set;}

    }
}