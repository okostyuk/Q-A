using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain;

/*
GET     /questions
    ?userId=optional
    &questionId=optional
POST    /questions/add
PUT     /questions/{id}/edit

DELETE  /questions/
    ?questionId=optional

POST    /questions/{id}/vote/{answerId}
*/
namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/questions")]
    public class QuestionsController : ControllerBase
    {
        
        private readonly IApp _appModel;
        
        public QuestionsController(IApp appModel)
        {
            _appModel = appModel;
        }

        [HttpGet("questions")]
        public List<Question> GetQuestions(string questionId, string userId)
        {
            return _appModel.GetQuestions(questionId, userId);
        }

        [HttpGet("questions/{id}")]
        public Question GetQuestion(string id)
        {
            return _appModel.GetQuestion(id);
        }
        
        [HttpPost("questions/add")]
        public Question AddQuestion(Question question)
        {
            return _appModel.CreateQuestion(question);
        }
        
        [HttpPost("questions/{questionId}/vote/{answerId}")]
        public void Vote(string questionId, string answerId, string userId)
        {
            _appModel.Vote(questionId, answerId, userId);
        }

    }
}