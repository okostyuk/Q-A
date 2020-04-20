using System.Collections.Generic;
using WebApp.Domain;

namespace WebApp.Controllers
{
    public class QuestionsResponse : Response<List<Question>>
    {
        public List<Question> Questions { get; set; }
    }
}