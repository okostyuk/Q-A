namespace WebApp.Domain
{
    public interface IUserRepository
    {
        public User AddUser(string email, string password);
        public void UpdateUser(User user);
        public User FindUserById(int id);
        public User FindUserByEmail(string email);
        public void AddToken(int userId, string token);
        public User FindUserByToken(string token);
        public void RemoveToken(string token);
    }
}