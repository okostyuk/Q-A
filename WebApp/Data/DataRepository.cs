﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using WebApp.Domain;


namespace WebApp.Data
{
    public class DataRepository : IQuestionsRepository, IUserRepository
    {
        private const string DatabaseName = "q-a";
        private static volatile SqlConnection _connection;
       
        private readonly Dictionary<string, int> _token2UserMap = new Dictionary<string, int>();
        private readonly Dictionary<int, string> _user2TokenMap = new Dictionary<int, string>();
        private SqlConnection GetConnection()
        {
            if (_connection != null) return _connection;
            _connection = new SqlConnection(connString);
            _connection.Open();
            return _connection;
        }

        public DataRepository(String connectionString)
        {
            connString = connectionString;
        }

        private readonly string connString;

        public List<Question> FindPublicQuestions()
        {
            var questions = GetConnection().Query<Question>(
                    "SELECT * FROM dbo.questions WHERE publishDate is not NULL;"
                ).AsList();
            foreach (var question in questions)
            {
                question.Answers = FindAnswersByQuestionId(question.Id);
            }
            return questions;
        }

        public List<Question> FindQuestionsByUser(int userId)
        {
            var questions=  GetConnection().Query<Question>(
                "SELECT * FROM dbo.questions WHERE userId = @UserId;", new{UserId = userId}
            ).AsList();
            foreach (var question in questions)
            {
                question.Answers = FindAnswersByQuestionId(question.Id);
            }
            return questions;
        }

        public Question FindQuestionById(int id)
        {
            var question = GetConnection().Query<Question>(
                "SELECT * from dbo.questions WHERE id=@Id", new {Id = id}
            ).FirstOrDefault();

            if (question != null)
            {
                question.Answers = FindAnswersByQuestionId(question.Id);
            }
            return question;
        }

        public List<Answer> FindAnswersByQuestionId(int id)
        {
            return GetConnection().Query<Answer>(
                "SELECT * from dbo.answers WHERE questionId=@Id", new {Id = id}
            ).AsList();
        }

        public void DeleteQuestion(int questionId)
        {
            GetConnection().Execute(
                @"DELETE from dbo.votes where questionId=@QuestionId;
                      DELETE from dbo.answers where questionId=@QuestionId;
                      DELETE from dbo.questions where id=@QuestionId;",
                new {QuestionID = questionId});
        }

        public Question AddQuestion(Question question)
        {
            const string sql = @"INSERT INTO dbo.questions 
                   (title, maxCustomAnswers, maxVoteVariants, expireDate, userId, publishDate) 
            VALUES 
                   (@Title, @MaxCustomAnswers, @MaxVoteVariants, @ExpiresDate, @UserId, @PublishDate);";
            GetConnection().Execute(sql, question);

            var id = GetConnection().ExecuteScalar("SELECT IDENT_CURRENT('questions')");
            var questionId = int.Parse(id+""); 
            foreach (var answer in question.Answers)
            {
                answer.QuestionId = questionId;
                answer.UserId = question.UserId;
            }
            if (question.Answers != null)
            {
                AddAnswers(questionId, question.Answers);
            }

            question.Id = questionId;
            return question;
        }

        public void AddAnswers(int questionId, List<Answer> answers)
        {
            foreach (var answer in answers)
            {
                var sql = "INSERT INTO dbo.answers (questionId, userId, text) VALUES (@QuestionId, @UserId, @Text)";        
                GetConnection().Execute(sql, answer);
            }
        }

        public int AddAnswer(Answer answer)
        {
            var sql = "INSERT INTO dbo.answers (questionId, userId, text) VALUES (@QuestionId, @UserId, @Text)";        
            GetConnection().Execute(sql, answer);
            var id = GetConnection().ExecuteScalar<int>(@"SELECT CAST(IDENT_CURRENT('answers') as int)");
            return id;
        }

        public List<Vote> FindVotesByQuestionId(int questionId)
        {
            return GetConnection().Query<Vote>(
                "SELECT * from dbo.votes WHERE questionId=@QuestionId", 
                new {QuestionId = questionId}
            ).AsList();
        }

        public List<Vote> FindVotesByUserAndQuestion(int userId, int questionId)
        {
            return GetConnection().Query<Vote>(
                "SELECT * from dbo.votes WHERE questionId=@QuestionId AND userId=@UserId", 
                new {QuestionId = questionId, UserId = userId}
            ).AsList();
        }

        public void AddVote(int questionId, int answerId, int userId)
        {
            GetConnection().Execute(
                "INSERT into dbo.votes values (@QuestionId, @AnswerId, @UserId)", 
                new {QuestionId = questionId, AnswerId = answerId, UserId = userId });
        }

        private readonly object _customAnswersWriteLock = new object();
        public void DecreaseQuestionCustomAnswers(int questionId)
        {
            lock (_customAnswersWriteLock)
            {
                var question = FindQuestionById(questionId);
                var maxCustomAnswers = question.MaxCustomAnswers;
                if (maxCustomAnswers > 0)
                {
                    GetConnection().Execute(
                        "UPDATE dbo.questions SET maxCustomAnswers=@MaxCustomAnswers WHERE id=@QuestionId",
                        new {QuestionId = questionId, MaxCustomAnswers = maxCustomAnswers - 1}
                    );
                }
            }
        }


        // ***********************  USER DATA     *******************************************************
        
        
        
        
        public User AddUser(string email, string password)
        {
            const string sql = "INSERT INTO dbo.users (Email, Password) Values (@email, @password); SELECT CAST(SCOPE_IDENTITY() as int);";
            var userId = GetConnection().Query<int>(sql, new {Email = email, Password = password}).First();
            return GetConnection().Query<User>(
                "select * from dbo.users where id = @userId",
                new {UserId=userId}
            ).FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public User FindUserById(int id)
        {
            return GetConnection().Query<User>(
                    "SELECT * FROM dbo.users WHERE id = @Id;",new {Id = id})
                .FirstOrDefault();
        }

        public User FindUserByEmail(string email)
        {
            const string sql = "SELECT * FROM dbo.users WHERE email = @Email;";
            Console.WriteLine(sql);
            var resultSet = GetConnection().Query<User>(sql, new {Email = email});
            return resultSet.SingleOrDefault();
        }

        public void AddToken(int userId, string token)
        {
            var oldToken = _user2TokenMap.GetValueOrDefault(userId, null);
            if (oldToken != null)
            {
                _user2TokenMap.Remove(userId);
                _token2UserMap.Remove(oldToken);
            }

            _user2TokenMap.Add(userId, token);
            _token2UserMap.Add(token, userId);
        }

        public User FindUserByToken(string token)
        {
            var userId = _token2UserMap.GetValueOrDefault(token, int.MinValue);
            return userId == int.MinValue ? null : FindUserById(userId);
        }

        public void RemoveToken(string token)
        {
            var userId = _token2UserMap.GetValueOrDefault(token, int.MinValue);
            if (userId == int.MinValue) return;
            _token2UserMap.Remove(token);
            _user2TokenMap.Remove(userId);
        }

        private void CreateDatabase(SqlConnection connection)
        {
            Console.WriteLine("DB connected");
            var dbExists = GetConnection().Query<string>("SELECT DB_ID(N'" + DatabaseName + "')").AsList().First() != null;
            if (dbExists)
            {
                Console.WriteLine("DB {0} exist", DatabaseName);
            }
            connection.Execute("CREATE DATABASE " + DatabaseName);
        }
    }
}