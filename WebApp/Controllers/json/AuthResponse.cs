using WebApp.Domain;

namespace WebApp.Controllers
{
    public class AuthResponse : Response
    {
        public User User { get; set; }
    }
}