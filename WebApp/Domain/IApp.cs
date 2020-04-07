using System.Collections.Generic;
using WebApp.Domain;

namespace WebApp.Domain
{
    public interface IApp
    {
        public void SignUp(User user);
        public string SignIn(User user);
        public Question CreateQuestion(Question question);
        public List<Question> GetQuestions(string userId);
        public Question GetQuestion(string id);
    }
}