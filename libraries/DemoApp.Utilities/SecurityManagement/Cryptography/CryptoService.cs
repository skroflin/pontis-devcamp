using System.Security.Cryptography;
using System.Text;

namespace DemoApp.WPF.UserControls.DataEncryption.Cryptography
{
    public static class CryptoService
    {
        // Source: https://tekeye.uk/visual_studio/encrypt-decrypt-c-sharp-string
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private const string initVector = "pemeqil9uzpgzl33";

        // This constant is used to determine the keysize of the encryption algorithm
        private const int keysize = 256;

        // PassPhrase
        private const string passPhrase = "YouShallPass!";

        public static string EncryptString(string plainText)
        {
            var initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var password = new PasswordDeriveBytes(passPhrase, null);
            var keyBytes = password.GetBytes(keysize / 8);

            var symmetricKey = Aes.Create();

            var encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();

                    cipherTextBytes = memoryStream.ToArray();
                }
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string DecryptString(string cipherText)
        {
            var initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            var cipherTextBytes = Convert.FromBase64String(cipherText);
            var password = new PasswordDeriveBytes(passPhrase, null);
            var keyBytes = password.GetBytes(keysize / 8);

            var symmetricKey = Aes.Create();

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            byte[] plainTextBytes;
            int decryptedByteCount;

            using (var memoryStream = new MemoryStream(cipherTextBytes))
            {
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    plainTextBytes = new byte[cipherTextBytes.Length];
                    decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                }
            }

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}
