using System.Collections.Generic;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Dtos
{
    public class QuestionnaireAddDto
    {
        public string Name { get; set; }
        public List<QuestionAddDto> Questions { get; set; }
    }
}