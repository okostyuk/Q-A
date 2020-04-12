using WebApp.Domain;

namespace WebApp.Controllers
{
    public class QuestionResponse
    {
        public string Status { get; set; }
        public string Error { get; set; }
        public Question Question { get; set; }
    }
}