#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace JPMC.MSDK.Filer
{
    public class AESProcessor : IDisposable
    {
        private string currentPassword;
        private Rfc2898DeriveBytes key;
        private int iterationCount = 50;
        private RijndaelManaged AES = new RijndaelManaged();

        public byte[] Encrypt( byte[] bytesToBeEncrypted, string password, byte[] salt )
        {
            byte[] encryptedBytes = null;

            using ( var ms = new MemoryStream() )
            {
                PrepareKey( password, salt );

                using ( var cs = new CryptoStream( ms, AES.CreateEncryptor(), CryptoStreamMode.Write ) )
                {
                    cs.Write( bytesToBeEncrypted, 0, bytesToBeEncrypted.Length );
                    cs.Close();
                }
                encryptedBytes = ms.ToArray();
            }

            return encryptedBytes;
        }

        public byte[] Decrypt( byte[] bytesToBeDecrypted, string password, byte[] salt )
        {
            byte[] decryptedBytes = null;

            using ( var ms = new MemoryStream() )
            {
                PrepareKey( password, salt );

                using ( var cs = new CryptoStream( ms, AES.CreateDecryptor(), CryptoStreamMode.Write ) )
                {
                    cs.Write( bytesToBeDecrypted, 0, bytesToBeDecrypted.Length );
                    cs.Close();
                }
                decryptedBytes = ms.ToArray();
            }

            return decryptedBytes;
        }

        private void PrepareKey( string password, byte[] salt )
        {
            if ( password == currentPassword )
            {
                return;
            }

            // Hash the password with SHA256
            var passwordBytes = Encoding.UTF8.GetBytes( password );
            passwordBytes = SHA256.Create().ComputeHash( passwordBytes );

            AES.KeySize = 256;
            AES.BlockSize = 128;
            key = new Rfc2898DeriveBytes( passwordBytes, salt, iterationCount );
            currentPassword = password;
            AES.Key = key.GetBytes( AES.KeySize / 8 );
            AES.IV = key.GetBytes( AES.BlockSize / 8 );

            AES.Mode = CipherMode.CBC;
        }

        public void Dispose()
        {
            AES.Dispose();
        }
    }
}
