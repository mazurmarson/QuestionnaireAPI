using System;

namespace QuestionnaireAPI.Dtos
{
    public class QuestionnaireDisplayInListDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }

    }
}