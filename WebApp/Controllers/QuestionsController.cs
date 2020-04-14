using System;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain;

/*
GET     /questions?userId=optional
GET     /questions/{questionId}
GET     /questions/my
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
        private readonly IQuestionsService _questionsService;
        
        public QuestionsController(IQuestionsService questionsService)
        {
            _questionsService = questionsService;
        }

        [HttpGet("questions")]
        public QuestionsResponse GetQuestions()
        {
            var response = new QuestionsResponse();
            try
            {
                response.Questions = _questionsService.GetQuestions(AuthToken());
            }
            catch (Exception ex)
            {
                response.Error = "Error: " + ex.Message;
            }

            return response;
        }
        
        [HttpGet("questions/my")]
        public QuestionsResponse GetMyQuestions()
        {
            var response = new QuestionsResponse();
            try
            {
                response.Questions = _questionsService.GetUserQuestions(AuthToken());
            }
            catch (Exception ex)
            {
                response.Error = "Error: " + ex.Message;
            }

            return response;
        }

        [HttpGet("questions/{id}")]
        public Question GetQuestion(string id)
        {
            return _questionsService.GetQuestion(AuthToken(), id);
        }
        
        [HttpPost("questions/add")]
        public QuestionResponse AddQuestion(Question question)
        {
            var response = new QuestionResponse();
            try
            {
                _questionsService.CreateQuestion(AuthToken(), question);
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
        public void Vote(string questionId, string answerId)
        {
            _questionsService.Vote(AuthToken(), questionId, answerId);
        }

        private string AuthToken()
        {
            return Request.Cookies[AuthController.AuthTokenCookieKey];
        }
    }
}