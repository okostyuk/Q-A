namespace WebApp.Domain
{
    public interface IAuthService
    {
        public User SignUp(string email, string password);
        public User SignIn(string email, string password);
        bool AuthTokenValid(string token);
    }
}