using System.IO;
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
        public AuthResponse SignUp(AuthRequest authRequest)
        {
            try
            {
                var user = _app.SignUp(authRequest.Email, authRequest.Password);
                return new AuthResponse() {User = user, Status = "OK"};
            }
            catch (InvalidDataException ex)
            {
                return new AuthResponse() { Status = "Error", Error = ex.Message};
            }
            catch
            {
                return new AuthResponse() { Status = "Error", Error = "Internal error, try back shortly"};
            }
        }
        
        [HttpPost("")]
        public AuthResponse Auth(AuthRequest authRequest)
        {
            try
            {
                var user = _app.SignIn(authRequest.Email, authRequest.Password);
                return new AuthResponse() {User = user, Status = "OK"};
            }
            catch (InvalidDataException ex)
            {
                return new AuthResponse() { Status = "Error", Error = ex.Message};
            }
            catch
            {
                return new AuthResponse() { Status = "Error", Error = "Internal error, try back shortly"};
            }
        }
        
    }
}