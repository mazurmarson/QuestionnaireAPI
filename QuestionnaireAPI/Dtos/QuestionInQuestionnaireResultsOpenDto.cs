using System.Collections.Generic;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Dtos
{
    public class QuestionInQuestionnaireResultsOpenDto
    {
        public int Id { get; set; }
        public QuestionType QuestionType { get; set; }
        public string QuestionContent { get; set; }

        public List<QuestionAnswerOpen> OpenAnswers {get; set;}

    }
}