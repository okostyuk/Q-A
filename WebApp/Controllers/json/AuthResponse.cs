using WebApp.Domain;

namespace WebApp.Controllers
{
    public class AuthResponse
    {
        public string Status { get; set; }
        public string Error { get; set; }
        public User User { get; set; }
    }
}