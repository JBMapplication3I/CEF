// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IPasswordHasher
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    /// <summary>Abstraction for password hashing methods.</summary>
    public interface IPasswordHasher
    {
        /// <summary>Hash a password.</summary>
        /// <param name="password">.</param>
        /// <returns>A string.</returns>
        string HashPassword(string password);

        /// <summary>Verify that a password matches the hashed password.</summary>
        /// <param name="hashedPassword">  .</param>
        /// <param name="providedPassword">.</param>
        /// <returns>A PasswordVerificationResult.</returns>
        PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword);
    }
}
