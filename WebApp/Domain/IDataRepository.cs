﻿
using System.Collections.Generic;

namespace WebApp.Domain
{
    public interface IDataRepository
    {
        public User AddUser(User user);
        public void UpdateUser(User user);
        public User FindUserById(string id);
        public User FindUserByEmail(string email);
        List<Question> FindPublicQuestions();
        Question AddQuestion(Question question);
        List<Question> FindQuestionsByUser(string userId);
        Question FindQuestionById(string id);
    }
}