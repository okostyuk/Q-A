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
        private readonly IMapper<Question, Question> _questionsMapper;
        
        public QuestionsController(IQuestionsService questionsService)
        {
            _questionsService = questionsService;
            _questionsMapper = new QuestionsMapper();
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
        public QuestionResponse GetQuestion(int id)
        {
            var response = new QuestionResponse();
            try
            {
                response.Question = _questionsService.GetQuestion(AuthToken(), id);
            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
            }

            return response;
        }
        
        [HttpPost("questions/add")]
        public QuestionResponse AddQuestion(Question question)
        {
            var response = new QuestionResponse();
            try
            {
                _questionsService.CreateQuestion(AuthToken(), _questionsMapper.Map(question));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                response.Error = ex.Message;
            }

            return response;
        }
        
        [HttpPost("questions/{questionId}/vote/{answerId}")]
        public void Vote(int questionId, int answerId)
        {
            _questionsService.Vote(AuthToken(), questionId, answerId);
        }

        [HttpDelete("questions/{questionId}")]
        public Response DeleteQuestion(int questionId)
        { 
            _questionsService.DeleteQuestion(AuthToken(), questionId);
            return new Response();
        }

        private string AuthToken()
        {
            return Request.Cookies[AuthController.AuthTokenCookieKey];
        }
    }
}