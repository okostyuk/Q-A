using System.Security.Cryptography;
using System.Text;

namespace WebApp
{
    public class Utils
    {
        public static string Hash(string value)
        {
            var encoder = Encoding.ASCII;  // TODO replace ASCII by UTF-8?
            var hashedData = MD5.Create().ComputeHash(encoder.GetBytes(value)); 
            return encoder.GetString(hashedData);
        }
    }
}