using System.Security.Cryptography;

namespace Lexacom.Autofac.Generics
{
    public class HashService
    {
        public byte[] Hash(byte[] data)
        {
            using var sha = SHA256.Create();  
            return sha.ComputeHash(data);
        }
    }
}