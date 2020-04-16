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

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signUp")]
        public AuthResponse SignUp(AuthRequest authRequest)
        {
            try
            {
                Console.WriteLine(HttpContext.User.Identity.ToString());
                var authToken = _authService.SignUp(authRequest.Email, authRequest.Password);
                Response.Cookies.Append(AuthTokenCookieKey, authToken);
                return new AuthResponse();
            }
            catch (InvalidDataException ex)
            {
                return new AuthResponse { Error = ex.Message};
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new AuthResponse { Error = "Internal error, try back shortly"};
            }
        }
        
        [HttpPost("")]
        public AuthResponse Auth(AuthRequest authRequest)
        {
            try
            {
                var authToken = _authService.SignIn(authRequest.Email, authRequest.Password);
                Response.Cookies.Append(AuthTokenCookieKey, authToken);
                return new AuthResponse();
            }
            catch (InvalidDataException ex)
            {
                return new AuthResponse { Error = ex.Message };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new AuthResponse { Error = "Internal error, try back shortly"};
            }
        }
    }
}