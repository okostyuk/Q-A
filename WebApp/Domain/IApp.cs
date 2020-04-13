using System.Collections.Generic;
using WebApp.Domain;

namespace WebApp.Domain
{
    public interface IApp
    {
        public string SignUp(string email, string password);
        public string SignIn(string email, string password);
        public Question CreateQuestion(string authToken, Question question);
        public List<Question> GetQuestions(string authToken);
        public List<Question> GetUserQuestions(string authToken);
        public Question GetQuestion(string authToken, string id);
        User GetUser(string authToken, string userId);
        List<User> GetUsers(string authToken);
        public void Vote(string authToken, string questionId, string answerId);
    }
}