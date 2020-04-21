namespace WebApp.Controllers
{
    public class Response <T>
    {
        public string AuthError { get; set; }
        public string Error { get; set; }
        public T Result { get; set; }
    }
}