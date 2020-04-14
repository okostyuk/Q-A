using System.Collections.Generic;

namespace WebApp.Domain
{
    public interface IQuestionsRepository
    {
        public List<Question> FindPublicQuestions();
        public Question AddQuestion(Question question);
        public List<Question> FindQuestionsByUser(string userId);
        public Question FindQuestionById(string id);
        public void AddAnswers(string questionId, List<Answer> answers);
    }
}