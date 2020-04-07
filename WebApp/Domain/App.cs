using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WebApp.Domain
{
    public class App : IApp
    {
        private readonly IDataRepository _dataRepository;
        
        public App(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public void SignUp(User user)
        {
            var existUser = _dataRepository.FindUserByEmail(user.Email);
            if (existUser != null) throw new InvalidDataException("User with this email already exists");
            user.Password = Hash(user.Password);
            _dataRepository.AddUser(user);
        }

        public string SignIn(User user)
        {
            Console.WriteLine("SignIn {0}", user.Email );
            var existUser = _dataRepository.FindUserByEmail(user.Email);
            Console.WriteLine("existUser: {0}", existUser);
            if (existUser == null) throw new InvalidDataException("User not found");
            if (existUser.Password.Equals(Hash(user.Password)))
            {
                return existUser.Id;
            }

            throw new InvalidDataException("Invalid password");
        }

        public Question CreateQuestion(Question question)
        {
            return _dataRepository.addQuestion(question);
        }

        public List<Question> GetQuestions(string userId)
        {
            if (userId == null)
            {
                return _dataRepository.FindQuestionsByUser(userId);
            }
            else
            {
                return _dataRepository.FindPublicQuestions();
            }
        }

        public Question GetQuestion(string id)
        {
            return _dataRepository.FindQuestionById(id);
        }

        private static string Hash(string value)
        {
            var encoder = Encoding.ASCII;  // TODO replace ASCII by UTF-8?
            var hashedData = MD5.Create().ComputeHash(encoder.GetBytes(value)); 
            return encoder.GetString(hashedData);
        }
    }
}