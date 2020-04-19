namespace WebApp.Domain
{
    public class Vote
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int UserId { get; set; }
    }
}