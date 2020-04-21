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

        public User SignUp(string email, string password)
        {
            var existUser = _userRepository.FindUserByEmail(email);
            if (existUser != null) throw new InvalidDataException("User with this email already exists");
            var storedUser = _userRepository.AddUser(email, Utils.Hash(password));
            return storedUser;
        }

        public User SignIn(string email, string password)
        {
            Console.WriteLine("SignIn {0}", email );
            var existUser = _userRepository.FindUserByEmail(email);
            Console.WriteLine("existUser: {0}", existUser);
            if (existUser == null) throw new InvalidDataException("User not found");
            if (existUser.Password.Equals(Utils.Hash(password)))
            {
                var authToken = Guid.NewGuid().ToString();
                _userRepository.AddToken(existUser.Id, authToken);
                existUser.AuthToken = authToken;
                return existUser;
            }

            throw new InvalidDataException("Invalid password");
        }

        public bool AuthTokenValid(string token)
        {
            return _userRepository.FindUserByToken(token) != null;
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

            foreach (var answer in from answer in question.Answers from vote in votes.Where(vote => vote.AnswerId.Equals(answer.Id)) select answer)
            {
                answer.VotesCount++;
            }
            
            return question;
        }

        public void Vote(string authToken, int questionId, int answerId)
        {
            var user = _userRepository.FindUserByToken(authToken);
            _questionsRepository.AddVote(questionId, answerId, user.Id);
        }
        
        
        public int AddAnswer(string authToken, Answer answer)
        {
            var question = _questionsRepository.FindQuestionById(answer.QuestionId);
            if (question.MaxCustomAnswers <= 0)
            {
                throw new InvalidDataException("MaxCustomAnswers limit reached");
            }

            var user = _userRepository.FindUserByToken(authToken);
            answer.UserId = user.Id;
            var answerId = _questionsRepository.AddAnswer(answer);
            _questionsRepository.DecreaseQuestionCustomAnswers(answer.QuestionId);
            return answerId;
        }

        public void DeleteQuestion(string authToken, int questionId)
        {
            _questionsRepository.DeleteQuestion(questionId);
        }
    }
}