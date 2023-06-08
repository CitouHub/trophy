using System.Security.Cryptography;
using System.Text;

namespace Trophy.Domain
{
    public class TrophyHolderDTO
    {
        public string Name { get; set; } = string.Empty;
        public string AvatarColor { 
            get {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(Name));
                    return $"#{new Guid(hash).ToString().Substring(0, 6)}";
                }
            } 
        }
    }
}
