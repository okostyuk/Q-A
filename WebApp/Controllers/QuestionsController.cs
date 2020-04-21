using System;
using System.Security.Authentication;
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
        private readonly IAuthService _authService;
        private readonly IMapper<Question, Question> _questionsMapper;
        
        public QuestionsController(IQuestionsService questionsService, IAuthService authService)
        {
            _questionsService = questionsService;
            _authService = authService;
            _questionsMapper = new QuestionsMapper();
        }

        [HttpGet("questions")]
        public QuestionsResponse GetQuestions()
        {
            var response = new QuestionsResponse();
            try
            {
                response.Result = _questionsService.GetQuestions(AuthToken());
            }
            catch (AuthenticationException ex)
            {
                response.AuthError = "AuthError: " + ex.Message;
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
                response.Result = _questionsService.GetUserQuestions(AuthToken());
            }
            catch (AuthenticationException ex)
            {
                response.AuthError = "AuthError: " + ex.Message;
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
                response.Result = _questionsService.GetQuestion(AuthToken(), id);
            }
            catch (AuthenticationException ex)
            {
                response.AuthError = "AuthError: " + ex.Message;
            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
            }

            return response;
        }
        
        [HttpPost("questions/add")]
        public Response<Question> AddQuestion(Question question)
        {
            var response = new Response<Question>();
            try
            {
                response.Result = _questionsService.CreateQuestion(AuthToken(), _questionsMapper.Map(question));
            }
            catch (AuthenticationException ex)
            {
                response.AuthError = "AuthError: " + ex.Message;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                response.Error = ex.Message;
            }

            return response;
        }
        
        [HttpPost("questions/{questionId}/vote/{answerId}")]
        public Response<Question> Vote(int questionId, int answerId)
        {
            _questionsService.Vote(AuthToken(), questionId, answerId);
            return new Response<Question>
            {
                Result = _questionsService.GetQuestion(AuthToken(), questionId)
            };
        }
        
        [HttpPost("questions/voteCustom")]
        public Response<Question> VoteCustom(Answer answer)
        {
            var authToken = AuthToken();
            var answerId = _questionsService.AddAnswer(authToken, answer);
            _questionsService.Vote(authToken, answer.QuestionId, answerId);
            return new Response<Question>
            {
                Result = _questionsService.GetQuestion(authToken, answer.QuestionId)
            };
        }

        [HttpDelete("questions/{questionId}")]
        public Response<string> DeleteQuestion(int questionId)
        { 
            _questionsService.DeleteQuestion(AuthToken(), questionId);
            return new Response<string>() {Result = "Deleted"};
        }

        private string AuthToken()
        {
            var authToken = Request.Cookies[AuthController.AuthTokenCookieKey];
            if (!string.IsNullOrEmpty(authToken) && _authService.AuthTokenValid(authToken)) 
                return authToken;
            
            Response.Cookies.Append(AuthController.AuthTokenCookieKey, "");
            Response.Cookies.Append(AuthController.UserIdCookieKey, "");
            Response.Cookies.Append(AuthController.EmailCookieKey, "");
            throw new AuthenticationException("invalid token");

        }
    }
}