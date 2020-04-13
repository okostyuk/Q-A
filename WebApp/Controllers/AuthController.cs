using System;
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
    public class AuthController : ControllerBase
    {
        public const string AuthTokenCookieKey = "qa_auth_token";

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
                Console.WriteLine(HttpContext.User.Identity.ToString());
                var authToken = _app.SignUp(authRequest.Email, authRequest.Password);
                Response.Cookies.Append(AuthTokenCookieKey, authToken);
                return new AuthResponse {Status = "OK"};
            }
            catch (InvalidDataException ex)
            {
                return new AuthResponse { Status = "Error", Error = ex.Message};
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new AuthResponse { Status = "Error", Error = "Internal error, try back shortly"};
            }
        }
        
        [HttpPost("")]
        public AuthResponse Auth(AuthRequest authRequest)
        {
            try
            {
                var authToken = _app.SignIn(authRequest.Email, authRequest.Password);
                Response.Cookies.Append(AuthTokenCookieKey, authToken);
                return new AuthResponse() { Status = "OK" };
            }
            catch (InvalidDataException ex)
            {
                return new AuthResponse() { Status = "Error", Error = ex.Message };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new AuthResponse() { Status = "Error", Error = "Internal error, try back shortly"};
            }
        }
    }
}