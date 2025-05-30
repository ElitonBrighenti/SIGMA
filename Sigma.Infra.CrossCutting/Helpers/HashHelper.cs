using System.Security.Cryptography;
using System.Text;

namespace Sigma.Infra.CrossCutting.Helpers
{
    public static class HashHelper
    {
        public static string GerarHashMD5(string input)
        {
            using var md5 = MD5.Create();
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes).ToLower();
        }
    }
}
