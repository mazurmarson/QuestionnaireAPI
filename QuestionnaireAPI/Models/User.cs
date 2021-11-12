using System;

namespace QuestionnaireAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }  
        public string Mail { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
    }
}