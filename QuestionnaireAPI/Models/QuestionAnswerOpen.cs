namespace QuestionnaireAPI.Models
{
    public class QuestionAnswerOpen
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string AnswerContent { get; set; }
    }
}