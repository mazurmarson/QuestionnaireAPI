using System;
using System.Collections.Generic;

namespace QuestionnaireAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }  
        public string Mail { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PasswordHash { get; set; }
        public List<Questionnaire> Questionnaires {get; set;} 
    }
}