using System.Collections.Generic;

namespace WebApp.Domain
{
    public interface IQuestionsRepository
    {
        public List<Question> FindPublicQuestions();
        public Question AddQuestion(Question question);
        public List<Question> FindQuestionsByUser(string userId);
        public Question FindQuestionById(string id);
        public List<Answer> FindAnswersByQuestionId(string questionId);
        public void AddAnswers(string questionId, List<Answer> answers);
        public List<Vote> FindVotesByUserAndQuestion(string userId, string questionId);
        void AddVote(string questionId, string answerId, string userId);
        void DecreaseQuestionCustomAnswers(string questionId);
    }
}