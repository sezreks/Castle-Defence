using System; 
using System.Security.Cryptography;
using System.Text; 

namespace Components
{
    public static class EndlessEncryption
    {
        private static readonly string key = "A60A5770F 5E7AB200BA9CF 94E4E8B0";


        private const string saltIndiatior = "EndlessDot";
        private const int startSaltLength = 32;
        private const int endSaltLength = 32;

        public static string Encrypt(string toEncrypt)
        {
            string startSalt = RandomString(startSaltLength);
            string salt = GetSalt(endSaltLength);
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key); 
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(startSalt + saltIndiatior + "{" + salt.Length + "}" + toEncrypt + salt);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7; 
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypt(string toDecrypt)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            int mod4 = toDecrypt.Length % 4;
            if (mod4 > 0) 
                toDecrypt += new string('=', 4 - mod4); 

            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            // better lang support
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            string decoded = UTF8Encoding.UTF8.GetString(resultArray);

            decoded = decoded.Remove(0, startSaltLength);

            if (decoded.StartsWith(saltIndiatior))
            {
                string saltSize = decoded.Substring(0, 1 + decoded.IndexOf("}"));
                int saltLength = int.Parse(saltSize.Replace(saltIndiatior, "").Replace("{", "").Replace("}", ""));
                decoded = decoded.Remove(0, saltSize.Length);
                if (saltLength > 0)
                {
                    decoded = decoded.Remove(decoded.Length - saltLength, saltLength);
                }
                return decoded;
            }
            else
            {
                return decoded;
            }

        }


        private static string RandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*<>+-=_";
            var stringChars = new char[length];
            var random = new System.Random();

            for (int i = 0; i < stringChars.Length; i++) 
                stringChars[i] = chars[random.Next(chars.Length)]; 

            return new String(stringChars);
        }
        private static string GetSalt(int max)
        {
            byte[] salt = new byte[max];
            RNGCryptoServiceProvider.Create().GetNonZeroBytes(salt);
            return UTF8Encoding.UTF8.GetString(salt);
        }
    }
}
