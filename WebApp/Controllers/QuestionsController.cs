using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain;

/*
GET     /questions?userId=optional
GET     /questions/{questionId}
POST    /questions/add
PUT     /questions/{questionId}/edit

DELETE  /questions/{questionId}
POST    /questions/{questionId}/vote/{answerId}
*/
namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/")]
    public class QuestionsController : ControllerBase
    {
        
        private readonly IApp _appModel;
        
        public QuestionsController(IApp appModel)
        {
            _appModel = appModel;
        }

        [HttpGet("questions")]
        public List<Question> GetQuestions()
        {
            return _appModel.GetQuestions(AuthToken());
        }

        [HttpGet("questions/{id}")]
        public Question GetQuestion(string id)
        {
            return _appModel.GetQuestion(AuthToken(), id);
        }
        
        [HttpPost("questions/add")]
        public QuestionResponse AddQuestion(Question question)
        {
            var response = new QuestionResponse();
            try
            {
                var storeQuestion = _appModel.CreateQuestion(AuthToken(), question);
                response.Status = "OK";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                response.Status = "ERROR";
                response.Error = ex.Message;
            }

            return response;
        }
        
        [HttpPost("questions/{questionId}/vote/{answerId}")]
        public void Vote(string questionId, string answerId, string userId)
        {
            _appModel.Vote(questionId, answerId, userId);
        }

        private string AuthToken()
        {
            return Request.Cookies[AuthController.AuthTokenCookieKey];
        }
    }
}