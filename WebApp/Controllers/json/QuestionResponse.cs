using WebApp.Domain;

namespace WebApp.Controllers
{
    public class QuestionResponse : Response
    {
        public Question Question { get; set; }
    }
}