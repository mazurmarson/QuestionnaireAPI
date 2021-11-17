using System;
using System.Collections.Generic;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Dtos
{
    public class QuestionnaireDetailsDto
    {
         public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public   List<QuestionInQuestionnaireDto> Questions {get; set;}
       // 
        //Pytania ==> odpowiedzi jeśli istnieją

    }
}