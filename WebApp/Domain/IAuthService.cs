namespace WebApp.Domain
{
    public interface IAuthService
    {
        public string SignUp(string email, string password);
        public string SignIn(string email, string password);
    }
}