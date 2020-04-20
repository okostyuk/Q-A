using WebApp.Domain;

namespace WebApp.Controllers
{
    public class QuestionResponse : Response<Question>
    {
        public Question Question { get; set; }
    }
}