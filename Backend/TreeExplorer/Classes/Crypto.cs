using System.Security.Cryptography;
using System.Text;
using TreeExplorer.Interfaces;

namespace TreeExplorer.Classes
{
    public class Crypto : ICrypto
    {
        public string EncryptSha256(string stringToEncrypt)
        {
            SHA256 sha256Hash = SHA256.Create();

            byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(stringToEncrypt));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

    }
}
