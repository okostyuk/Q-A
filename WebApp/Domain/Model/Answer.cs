namespace WebApp.Domain
{
    public class Answer
    {
        public string Id { get; set; }
        public string QuestionId { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
    }
}