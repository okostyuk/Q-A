using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using WebApp.Domain;


namespace WebApp.Data
{
    public class DataRepository : IDataRepository 
    {
        private const string DatabaseName = "q-a";
        private const string ConnString = @"Server=.\SQLEXPRESS;Database=q-a;User Id=sa;Password=server";

        private readonly SqlConnection _connection;
       
        public DataRepository()
        {
            _connection = new SqlConnection(ConnString);
            _connection.Open();
            Console.WriteLine("DB connected");
            var dbExists = _connection.Query<string>("SELECT DB_ID(N'" + DatabaseName + "')").AsList().First() != null;
            if (dbExists)
            {
                Console.WriteLine("DB {0} exist", DatabaseName);
            }
        }

        private void CreateDatabase(SqlConnection connection)
        {
            connection.Execute("CREATE DATABASE " + DatabaseName);
        }

        public void AddUser(User user)
        {
            const string sql = "INSERT INTO [q-a].main.users (Email, Password) Values (@email, @password);";
            _connection.Execute(sql, user);
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public User FindUserById(string id)
        {
            return _connection.Query<User>(
                "SELECT * FROM [q-a].main.users WHERE id = @Id;",new {Id = id})
                .FirstOrDefault();
        }

        public User FindUserByEmail(string email)
        {
            const string sql = "SELECT * FROM [q-a].main.users WHERE email = @Email;";
            Console.WriteLine(sql);
            return _connection.Query<User>(sql, new {Email = email}).First();
        }

        public List<Question> FindPublicQuestions()
        {
            return _connection.Query<Question>(
                    "SELECT * FROM [q-a].main.questions WHERE published = 1;"
                ).AsList();
        }

        public List<Question> FindQuestionsByUser(string userId)
        {
            return _connection.Query<Question>(
                "SELECT * FROM [q-a].main.questions WHERE userId = @UserId;", new{UserId = userId}
            ).AsList();
        }

        public Question FindQuestionById(string id)
        {
            return _connection.Query<Question>(
                "SELECT * from [q-a].main.questions WHERE id=@Id", new {Id = id}
            ).First();
        }

        public Question addQuestion(Question question)
        {
            const string sql = @"INSERT INTO [q-a].main.questions
            (title, maxCustomAnswers, voteVariantsCount, expiresDate, userId, published, publishDate)
            VALUES 
            (@Title, @MaxCustomAnswers, @VoteVariantsCount, @ExpiresDate, @UserId, @Published, @PublishDate);
            SELECT CAST(SCOPE_IDENTITY() as int)";
            var questionId = _connection.ExecuteScalar<int>(sql, question);
            var result = _connection.Query<Question>(
                "SELECT * from [q-a].main.questions where id = @Id", new {Id = questionId}).First();
            return result;
        }

    }
}