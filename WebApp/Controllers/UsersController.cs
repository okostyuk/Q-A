using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain;

/*
GET    users
POST   users/signUp
POST   users/auth
*/
namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        
        private readonly IApp _app;
        
        public UsersController(IApp app)
        {
            _app = app;
        }
        
        [HttpGet("users")]
        public List<User> GetUsers()
        {
            return _app.GetUsers();
        }


        [HttpGet("users/{userId}")]
        public User GetUser(string userId)
        {
            return _app.GetUser(userId);
        }
    }
}