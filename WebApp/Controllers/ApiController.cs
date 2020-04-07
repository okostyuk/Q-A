using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain;

/*
POST    /signUp
POST    /signIn

GET     /questions?userId=optional
POST    /questions/add
PUT     /questions/{id}/edit

GET     /questions/{id}
DELETE  /questions/{id}

GET    /answers/{questionId}
POST   /answers/{questionId}/add
PUT    /answers/{questionId}/edit

GET    /votes/{questionId}
POST   /votes/{questionId}/{answerId}
*/
namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        
        private readonly IApp _app;
        
        public ApiController(IApp app)
        {
            _app = app;
        }

        [HttpPost("signUp")]
        public string SignUp(User user)
        {
            _app.SignUp(user);
            return _app.SignIn(user);
        }
        
        [HttpPost("signIn")]
        public string SignIn(User user)
        {
            return _app.SignIn(user);
        }

        [HttpGet("questions/{userId}")]
        public List<Question> GetQuestions(string userId)
        {
            return _app.GetQuestions(userId);
        }

        [HttpPost("questions/add")]
        public Question AddQuestion(Question question)
        {
            return _app.CreateQuestion(question);
        }

        [HttpGet("questions/{id}")]
        public Question GetQuestion(string id)
        {
            return _app.GetQuestion(id);
        }
    }
}