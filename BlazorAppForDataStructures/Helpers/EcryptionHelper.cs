using System.Security.Cryptography;
using System.Text;


namespace BlazorAppForDataStructures.Helpers
{
    public static class EncryptionHelper
    {
        public static string Encrypt(string plainText, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            using var aes = Aes.Create();
            aes.Key = keyBytes;
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var writer = new StreamWriter(cs))
            {
                writer.Write(plainText);
            }

            var iv = Convert.ToBase64String(aes.IV);
            var encryptedText = Convert.ToBase64String(ms.ToArray());
            return $"{iv}:{encryptedText}";
        }

        public static string Decrypt(string encryptedText, string key)
        {
            var parts = encryptedText.Split(':');
            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid encrypted text format");
            }

            var iv = Convert.FromBase64String(parts[0]);
            var cipherText = Convert.FromBase64String(parts[1]);

            var keyBytes = Encoding.UTF8.GetBytes(key);
            using var aes = Aes.Create();
            aes.Key = keyBytes;
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(cipherText);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(cs);
            return reader.ReadToEnd();
        }
    }
}