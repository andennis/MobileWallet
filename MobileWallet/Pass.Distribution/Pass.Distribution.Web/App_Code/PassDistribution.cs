using Common.Extensions;
using Common.Utils;

namespace Pass.Distribution.Web
{
    public static class PassDistribution
    {
        private const string SecurityVector = "nh2!0hg#vbPu&QSd";

        public static PassTokenInfo DecryptPassToken(string passToken, string password)
        {
            string decrypted = DecryptString(passToken, password);
            return decrypted.JsonToObject<PassTokenInfo>();
        }
        public static string EncryptPassToken(PassTokenInfo tokenInfo, string password)
        {
            string passToken = tokenInfo.ObjectToJson();
            return EncryptString(passToken, password);
        }


        private static string EncryptString(string textToEncrypt, string password)
        {
            return Crypto.EncryptString(textToEncrypt, password, SecurityVector);
        }
        private static string DecryptString(string textToDecrypt, string password)
        {
            return Crypto.DecryptString(textToDecrypt, password, SecurityVector);
        }

    }
}