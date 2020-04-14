#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebApp.Domain
{
    public class App : IAuthService, IQuestionsService
    {
        private readonly IUserRepository _userRepository;
        private readonly IQuestionsRepository _questionsRepository;
        
        public App(IUserRepository userRepository,  IQuestionsRepository questionsRepository)
        {
            _userRepository = userRepository;
            _questionsRepository = questionsRepository;
        }

        public string SignUp(string email, string password)
        {
            var existUser = _userRepository.FindUserByEmail(email);
            if (existUser != null) throw new InvalidDataException("User with this email already exists");
            var storedUser = _userRepository.AddUser(email, Utils.Hash(password));
            var authToken = Guid.NewGuid().ToString();
            _userRepository.AddToken(storedUser.Id, authToken);
            return authToken;
        }

        public string SignIn(string email, string password)
        {
            Console.WriteLine("SignIn {0}", email );
            var existUser = _userRepository.FindUserByEmail(email);
            Console.WriteLine("existUser: {0}", existUser);
            if (existUser == null) throw new InvalidDataException("User not found");
            if (existUser.Password.Equals(Utils.Hash(password)))
            {
                var authToken = Guid.NewGuid().ToString();
                _userRepository.AddToken(existUser.Id, authToken);
                return authToken;
            }

            throw new InvalidDataException("Invalid password");
        }

        public Question CreateQuestion(string authToken, Question question)
        {
            var user = _userRepository.FindUserByToken(authToken);
            question.UserId = user.Id;
            return _questionsRepository.AddQuestion(question);
        }

        public List<Question> GetUserQuestions(string authToken)
        {
            var user = _userRepository.FindUserByToken(authToken);
            return _questionsRepository.FindQuestionsByUser(user.Id);
        }
        public List<Question> GetQuestions(string authToken)
        {
            var questions = _questionsRepository.FindPublicQuestions();
            var user = _userRepository.FindUserByToken(authToken);
            foreach (var question in questions.Where(question => question.UserId.Equals(user.Id)))
            {
                question.Editable = true;
            }
            return questions;
        }

        public Question GetQuestion(string authToken, string id)
        {
            return _questionsRepository.FindQuestionById(id);
        }

        public User GetUser(string authToken, string userId)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsers(string authToken)
        {
            throw new NotImplementedException();
        }

        public void Vote(string authToken, string questionId, string answerId)
        {
            throw new NotImplementedException();
        }
    }
}