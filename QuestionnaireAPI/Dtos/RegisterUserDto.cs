using System;

namespace QuestionnaireAPI.Dtos
{
    public class RegisterUserDto
    {
        public string Name { get; set; }  
        public string Mail { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
    }
}