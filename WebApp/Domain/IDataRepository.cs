
using System.Collections.Generic;

namespace WebApp.Domain
{
    public interface IDataRepository
    {
        public User AddUser(string email, string password);
        public void UpdateUser(User user);
        public User FindUserById(string id);
        public User FindUserByEmail(string email);
        public List<Question> FindPublicQuestions();
        public Question AddQuestion(Question question);
        public List<Question> FindQuestionsByUser(string userId);
        public Question FindQuestionById(string id);
        public void AddAnswers(List<Answer> answers);

        public void AddToken(string userId, string token);

        public User FindUserByToken(string token);
        public void RemoveToken(string token);
    }
}