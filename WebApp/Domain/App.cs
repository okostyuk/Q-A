#nullable enable
using System;
using System.Collections.Generic;
using System.IO;

namespace WebApp.Domain
{
    public class App : IApp
    {
        private readonly IDataRepository _dataRepository;
        
        public App(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public string SignUp(string email, string password)
        {
            var existUser = _dataRepository.FindUserByEmail(email);
            if (existUser != null) throw new InvalidDataException("User with this email already exists");
            var storedUser = _dataRepository.AddUser(email, Utils.Hash(password));
            var authToken = Guid.NewGuid().ToString();
            _dataRepository.AddToken(storedUser.Id, authToken);
            return authToken;
        }

        public string SignIn(string email, string password)
        {
            Console.WriteLine("SignIn {0}", email );
            var existUser = _dataRepository.FindUserByEmail(email);
            Console.WriteLine("existUser: {0}", existUser);
            if (existUser == null) throw new InvalidDataException("User not found");
            if (existUser.Password.Equals(Utils.Hash(password)))
            {
                var authToken = Guid.NewGuid().ToString();
                _dataRepository.AddToken(existUser.Id, authToken);
                return authToken;
            }

            throw new InvalidDataException("Invalid password");
        }

        public Question CreateQuestion(string authToken, Question question)
        {
            var user = _dataRepository.FindUserByToken(authToken);
            question.UserId = user.Id;
            return _dataRepository.AddQuestion(question);
        }

        public List<Question> GetUserQuestions(string authToken)
        {
            var user = _dataRepository.FindUserByToken(authToken);
            return _dataRepository.FindQuestionsByUser(user.Id);
        }
        public List<Question> GetQuestions(string authToken)
        {
            return _dataRepository.FindPublicQuestions();
        }

        public Question GetQuestion(string authToken, string id)
        {
            return _dataRepository.FindQuestionById(id);
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