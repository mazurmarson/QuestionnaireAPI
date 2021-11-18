using System;
using System.Collections.Generic;

namespace QuestionnaireAPI.Dtos
{
    public class QuestionnaireResultsDto
    {
        public int Id {get; set;}
        public string Name  {get; set;}
        public DateTime CreateDate {get; set;}
        public List<QuestionInQuestionnaireResultsCloseDto> CloseQuestionsResults {get; set;}
        public List<QuestionInQuestionnaireResultsOpenDto> OpenQuestionResults {get; set;}
    }
}