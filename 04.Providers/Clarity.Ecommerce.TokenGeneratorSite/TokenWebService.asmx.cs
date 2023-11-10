// <copyright file="TokenWebService.asmx.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the token web service.asmx class</summary>
namespace Clarity.Ecommerce.TokenGenerator
{
    using System;
    using System.Linq;
    using System.Web.Services;

    /// <summary>A token web service.</summary>
    /// <seealso cref="WebService"/>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class TokenWebService : WebService
    {
        /// <summary>The token.</summary>
        private static string token;

        /// <summary>(An Action that handles HTTP GET requests) gets the get.</summary>
        /// <param name="accountKey">The account key to get.</param>
        /// <returns>A HttpResponseMessage.</returns>
        [WebMethod]
        public string Get(string accountKey)
        {
            token = CreateToken(accountKey);
            return token;
        }

        /// <summary>Validate the Token</summary>
        /// <param name="apiToken">The API token.</param>
        /// <returns>A DnnApiResponse.</returns>
        [WebMethod]
        public bool Validate(string apiToken)
        {
            return ValidateToken(apiToken);
        }

        /// <summary>Creates the token and returns the encrypted result.</summary>
        /// <param name="accountKey">The account key.</param>
        /// <returns>The new token.</returns>
        private static string CreateToken(string accountKey)
        {
            var time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            var key = Guid.NewGuid().ToByteArray();
            var accountKeyBytes = GetBytes("||" + accountKey);
            var encryptedToken = Convert.ToBase64String(time.Concat(key).Concat(accountKeyBytes).ToArray());
#pragma warning disable 618
            return Encryption.CMSApiEncoder.Encrypt(encryptedToken);
#pragma warning restore 618
        }

        private static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>Validates the token described by tokenEncrypted.</summary>
        /// <param name="encryptedToken">The encrypted token.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        private static bool ValidateToken(string encryptedToken)
        {
#pragma warning disable 618
            var decryptedToken = Encryption.CMSApiEncoder.Decrypt(encryptedToken);
#pragma warning restore 618
            var data = Convert.FromBase64String(decryptedToken);
            var created = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            return created >= DateTime.UtcNow.AddMinutes(-30);
        }
    }
}
