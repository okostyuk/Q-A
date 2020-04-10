using Microsoft.AspNetCore.Mvc;
using WebApp.Domain;

namespace WebApp.Controllers
{
    
    /*
    POST   /auth/signUp
    POST   /auth
    */
    
    [ApiController]
    [Route("api/auth/")]
    public class AuthController
    {
        private readonly IApp _app;

        public AuthController(IApp app)
        {
            _app = app;
        }

        [HttpPost("signUp")]
        public User SignUp(AuthRequest authRequest)
        {
            return _app.SignUp(authRequest.Email, authRequest.Password);
        }
        
        [HttpPost("")]
        public User Auth(AuthRequest authRequest)
        {
            return _app.SignIn(authRequest.Email, authRequest.Password);
        }
        
    }
}