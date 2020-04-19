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
            var savedQuestion = _questionsRepository.AddQuestion(question);
            return savedQuestion;
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

        public Question GetQuestion(string authToken, int questionId)
        {
            var question = _questionsRepository.FindQuestionById(questionId);
            question.Answers = _questionsRepository.FindAnswersByQuestionId(questionId);
            var user = _userRepository.FindUserByToken(authToken);
            var votes = _questionsRepository.FindVotesByQuestionId(question.Id);
            question.Votes = question.UserId.Equals(user.Id) 
                ? votes 
                : votes.FindAll(v => v.UserId.Equals(user.Id));

            return question;
        }

        public User GetUser(string authToken, int userId)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsers(string authToken)
        {
            throw new NotImplementedException();
        }

        public void Vote(string authToken, int questionId, int answerId)
        {
            var user = _userRepository.FindUserByToken(authToken);
            _questionsRepository.AddVote(questionId, answerId, user.Id);
        }

        public Question AddAnswer(string authToken, int questionId, string answerText)
        {
            var user = _userRepository.FindUserByToken(authToken);
            var question = _questionsRepository.FindQuestionById(questionId);

            if (!question.IsPublished() && !question.UserId.Equals(user.Id))
            {
                throw new InvalidDataException("You can`t add answer to other user`s question");
            }

            if (question.IsPublished() && question.MaxCustomAnswers == 0)
            {
                throw new InvalidDataException("Custom answers limit is reached");
            }
            
            var answer = new Answer {QuestionId = questionId, UserId = user.Id, Text = answerText};
            if (question.IsPublished())
            {
                _questionsRepository.DecreaseQuestionCustomAnswers(questionId);
            }
            _questionsRepository.AddAnswers(questionId, new List<Answer> {answer});

            question.Answers = _questionsRepository.FindAnswersByQuestionId(questionId);
            
            return question;
        }

        public void DeleteQuestion(string authToken, int questionId)
        {
            _questionsRepository.DeleteQuestion(questionId);
        }
    }
}