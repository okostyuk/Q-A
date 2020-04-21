using System.Collections.Generic;

namespace WebApp.Domain
{
    public interface IQuestionsRepository
    {
        public List<Question> FindPublicQuestions();
        public Question AddQuestion(Question question);
        public List<Question> FindQuestionsByUser(int userId);
        public Question FindQuestionById(int id);
        public List<Answer> FindAnswersByQuestionId(int questionId);
        public void AddAnswers(int questionId, List<Answer> answers);
        public int AddAnswer(Answer answer);
        public List<Vote> FindVotesByUserAndQuestion(int userId, int questionId);
        void AddVote(int questionId, int answerId, int userId);
        void DecreaseQuestionCustomAnswers(int questionId);
        List<Vote> FindVotesByQuestionId(int questionId);
        void DeleteQuestion(int questionId);
    }
}