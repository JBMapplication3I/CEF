// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.PasswordHasher
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    /// <summary>Implements password hashing methods.</summary>
    /// <seealso cref="IPasswordHasher"/>
    /// <seealso cref="IPasswordHasher"/>
    public class PasswordHasher : IPasswordHasher
    {
        /// <summary>Hash a password.</summary>
        /// <param name="password">.</param>
        /// <returns>A string.</returns>
        public virtual string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        /// <summary>Verify that a password matches the hashedPassword.</summary>
        /// <param name="hashedPassword">  .</param>
        /// <param name="providedPassword">.</param>
        /// <returns>A PasswordVerificationResult.</returns>
        public virtual PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            return Crypto.VerifyHashedPassword(hashedPassword, providedPassword)
                ? PasswordVerificationResult.Success
                : PasswordVerificationResult.Failed;
        }
    }
}
