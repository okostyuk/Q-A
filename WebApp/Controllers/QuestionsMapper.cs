using WebApp.Domain;

namespace WebApp.Controllers
{
    public class QuestionsMapper : IMapper<Question, Question>
    {
        public Question Map(Question src)
        {
            src.Title = src.ClientTitle;
            src.Answers = src.ClientAnswers;
            return src;
        }
    }
}