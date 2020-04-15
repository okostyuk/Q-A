using System.Collections.Generic;

namespace WebApp.Domain
{
    public interface IQuestionsService
    {
        public Question CreateQuestion(string authToken, Question question);
        public List<Question> GetQuestions(string authToken);
        public List<Question> GetUserQuestions(string authToken);
        public Question GetQuestion(string authToken, string questionId);
        public void Vote(string authToken, string questionId, string answerId);
    }
}