namespace WebApp.Controllers
{
    public class Response <T>
    {
        public string Error { get; set; }
        public T Result { get; set; }
    }
}