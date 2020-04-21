using System.Collections.Generic;

namespace WebApp.Domain
{
    public interface IQuestionsService
    {
        public Question CreateQuestion(string authToken, Question question);
        public List<Question> GetQuestions(string authToken);
        public List<Question> GetUserQuestions(string authToken);
        public Question GetQuestion(string authToken, int questionId);
        public void Vote(string authToken, int questionId, int answerId);
        void DeleteQuestion(string authToken, int questionId);
        int AddAnswer(string authToken, Answer answer);
    }
}