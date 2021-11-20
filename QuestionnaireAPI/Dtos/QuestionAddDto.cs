using System.Collections.Generic;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Dtos
{
    public class QuestionAddDto
    {
        public int Id { get; set; }
        public QuestionType QuestionType { get; set; }
        public string QuestionContent { get; set; }

        public List<SubAnswerAddDto> SubAnswers {get; set;}
      //  public List<QuestionAnswerOpen> OpenQuestionAnswerList {get; set;}
    }
}