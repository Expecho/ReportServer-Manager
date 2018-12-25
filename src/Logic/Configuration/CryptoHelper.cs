using System;
using System.Security;

namespace ReportingServerManager.Logic.Configuration
{
    class CryptoHelper
    {
        static readonly byte[] Entropy = System.Text.Encoding.Unicode.GetBytes("RSS_Salt");

        public static string EncryptString(SecureString input)
        {
            var encryptedData = System.Security.Cryptography.ProtectedData.Protect(
                System.Text.Encoding.Unicode.GetBytes(ToInsecureString(input)),
                Entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);
            
            return Convert.ToBase64String(encryptedData);
        }

        public static SecureString DecryptString(string encryptedData)
        {
            var decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                Convert.FromBase64String(encryptedData),
                Entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);

            return ToSecureString(System.Text.Encoding.Unicode.GetString(decryptedData));
        }

        public static SecureString ToSecureString(string input)
        {
            var secure = new SecureString();
           
            foreach (var c in input)
            {
                secure.AppendChar(c);
            }
            
            secure.MakeReadOnly();
            
            return secure;
        }

        public static string ToInsecureString(SecureString input)
        {
            string returnValue;
            
            var ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
            
            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }
    }
}
