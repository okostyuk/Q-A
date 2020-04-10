using System.Collections.Generic;
using WebApp.Domain;

namespace WebApp.Domain
{
    public interface IApp
    {
        public User SignUp(string email, string password);
        public User SignIn(string email, string password);
        public Question CreateQuestion(Question question);
        public List<Question> GetQuestions(string questionId, string userId);
        public Question GetQuestion(string id);
        User GetUser(string userId);
        List<User> GetUsers();
        public void Vote(string questionId, string answerId, string userId);
    }
}