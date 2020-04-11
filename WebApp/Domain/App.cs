#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Transactions;

namespace WebApp.Domain
{
    public class App : IApp
    {
        private readonly IDataRepository _dataRepository;
        
        public App(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public User SignUp(string email, string password)
        {
            var existUser = _dataRepository.FindUserByEmail(email);
            if (existUser != null) throw new InvalidDataException("User with this email already exists");
            var storedUser = _dataRepository.AddUser(
                new User{Email = email, Password = Utils.Hash(password)});
            return new User {Email = email, Password = Utils.Hash(password)};
        }

        public User SignIn(string email, string password)
        {
            Console.WriteLine("SignIn {0}", email );
            var existUser = _dataRepository.FindUserByEmail(email);
            Console.WriteLine("existUser: {0}", existUser);
            if (existUser == null) throw new InvalidDataException("User not found");
            if (existUser.Password.Equals(Utils.Hash(password)))
            {
                return existUser;
            }

            throw new InvalidDataException("Invalid password");
        }

        public Question CreateQuestion(Question question)
        {
            return _dataRepository.AddQuestion(question);
        }

        public List<Question> GetQuestions(string? questionId, string? userId)
        {
            return userId != null 
                ? _dataRepository.FindQuestionsByUser(userId) 
                : _dataRepository.FindPublicQuestions();
        }

        public Question GetQuestion(string id)
        {
            return _dataRepository.FindQuestionById(id);
        }

        public User GetUser(string userId)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public void Vote(string questionId, string answerId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}