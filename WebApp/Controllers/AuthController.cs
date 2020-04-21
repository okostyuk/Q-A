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
        public const string UserIdCookieKey = "qa_user_id";
        public const string EmailCookieKey = "qa_user_email";

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("logout")]
        public Response<string> Logout(AuthRequest authRequest)
        {
            Response.Cookies.Append(AuthTokenCookieKey, "");
            Response.Cookies.Append(UserIdCookieKey, "");
            Response.Cookies.Append(EmailCookieKey, "");
            
            return new Response<string> {Result = "Session terminated successfully"};
        }

        [HttpPost("signUp")]
        public AuthResponse SignUp(AuthRequest authRequest)
        {
            try
            {
                _authService.SignUp(authRequest.Email, authRequest.Password);
                return Auth(authRequest);
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
                var user = _authService.SignIn(authRequest.Email, authRequest.Password);
                Response.Cookies.Append(AuthTokenCookieKey, user.AuthToken);
                Response.Cookies.Append(UserIdCookieKey, user.Id.ToString());
                Response.Cookies.Append(EmailCookieKey, user.Email);
                user.Password = "";
                return new AuthResponse() {Result = user};
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