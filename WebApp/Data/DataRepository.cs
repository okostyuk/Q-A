using System;
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
        private const string ConnString = @"Server=192.168.1.128\SQLEXPRESS;Database=q-a;User Id=sa;Password=server";

        private static volatile SqlConnection _connection;
       
        private readonly Dictionary<string, string> _token2UserMap = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _user2TokenMap = new Dictionary<string, string>();
        private static SqlConnection GetConnection()
        {
            if (_connection != null) return _connection;
            _connection = new SqlConnection(ConnString);
            _connection.Open();
            return _connection;
        }

        public List<Question> FindPublicQuestions()
        {
            return GetConnection().Query<Question>(
                    "SELECT * FROM [q-a].dbo.questions WHERE publishDate in not NULL;"
                ).AsList();
        }

        public List<Question> FindQuestionsByUser(string userId)
        {
            return GetConnection().Query<Question>(
                "SELECT * FROM [q-a].dbo.questions WHERE userId = @UserId;", new{UserId = userId}
            ).AsList();
        }

        public Question FindQuestionById(string id)
        {
            return GetConnection().Query<Question>(
                "SELECT * from [q-a].dbo.questions WHERE id=@Id", new {Id = id}
            ).FirstOrDefault();
        }

        public List<Answer> FindAnswersByQuestionId(string id)
        {
            return GetConnection().Query<Answer>(
                "SELECT * from [q-a].dbo.answers WHERE questionId=@Id", new {Id = id}
            ).AsList();
        }

        public Question AddQuestion(Question question)
        {
            const string sql = @"INSERT INTO [q-a].dbo.questions (title, maxCustomAnswers, expireDate, userId, publishDate)
            VALUES  (@Title, @MaxCustomAnswers, @ExpiresDate, @UserId, @PublishDate);";
            GetConnection().Execute(sql, question);
            return null;
        }

        public void AddAnswers(string questionId, List<Answer> answers)
        {
            foreach (var answer in answers)
            {
                var sql = "INSERT INTO [q-a].dbo.answers (questionId, userId, text) VALUES (@QuestionId, @UserId, @Text)";        
                GetConnection().Execute(sql, answer);
            }
        }

        public List<Vote> FindVotesByUserAndQuestion(string userId, string questionId)
        {
            return GetConnection().Query<Vote>(
                "SELECT * from [q-a].dbo.votes WHERE questionId=@QuestionId AND userId=@UserId", 
                new {QuestionId = questionId, UserId = userId}
            ).AsList();
        }

        public void AddVote(string questionId, string answerId, string userId)
        {
            GetConnection().Execute(
                "INSERT into [q-a].dbo.votes values (@QuestionId, @AnswerId, @UserId)", 
                new {QuestionId = questionId, AnswerId = answerId, UserId = userId });
        }

        private readonly object _customAnswersWriteLock = new object();
        public void DecreaseQuestionCustomAnswers(string questionId)
        {
            lock (_customAnswersWriteLock)
            {
                var question = FindQuestionById(questionId);
                var maxCustomAnswers = question.MaxCustomAnswers;
                if (maxCustomAnswers > 0)
                {
                    GetConnection().Execute(
                        "UPDATE [q-a].dbo.answers SET maxCustomAnswer=@MaxCustomAnswers WHERE id=@QuestionId",
                        new {QuestionId = questionId, MaxCustomAnswers = maxCustomAnswers - 1}
                    );
                }
            }
        }


        // ***********************  USER DATA     *******************************************************
        
        
        
        
        public User AddUser(string email, string password)
        {
            const string sql = "INSERT INTO [q-a].dbo.users (Email, Password) Values (@email, @password); SELECT CAST(SCOPE_IDENTITY() as int);";
            var userId = GetConnection().Query<int>(sql, new {Email = email, Password = password}).First();
            return GetConnection().Query<User>(
                "select * from [q-a].dbo.users where id = @userId",
                new {UserId=userId}
            ).FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public User FindUserById(string id)
        {
            return GetConnection().Query<User>(
                    "SELECT * FROM [q-a].dbo.users WHERE id = @Id;",new {Id = id})
                .FirstOrDefault();
        }

        public User FindUserByEmail(string email)
        {
            const string sql = "SELECT * FROM [q-a].dbo.users WHERE email = @Email;";
            Console.WriteLine(sql);
            var resultSet = GetConnection().Query<User>(sql, new {Email = email});
            return resultSet.SingleOrDefault();
        }

        public void AddToken(string userId, string token)
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
            var userId = _token2UserMap.GetValueOrDefault(token, null);
            return userId == null ? null : FindUserById(userId);
        }

        public void RemoveToken(string token)
        {
            var userId = _token2UserMap.GetValueOrDefault(token, null);
            if (userId == null) return;
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