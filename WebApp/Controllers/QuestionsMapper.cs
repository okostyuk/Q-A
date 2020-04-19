using System;
using System.Globalization;
using WebApp.Domain;

namespace WebApp.Controllers
{
    public class QuestionsMapper : IMapper<Question, Question>
    {
        public Question Map(Question src)
        {
            src.Title = src.ClientTitle;
            src.Answers = src.ClientAnswers;

            if (src.ClientExpiresDate != null)
            {
                src.ExpiresDate = DateTime.ParseExact(
                    src.ClientExpiresDate, 
                    "yyyy-MM-dd", 
                    CultureInfo.InvariantCulture);
            }

            if (src.ClientPublish)
            {
                src.PublishDate = DateTime.Now;
            }

            return src;
        }
    }
}