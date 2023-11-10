// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.PasswordVerificationResult
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    /// <summary>Return result for IPasswordHasher.</summary>
    public enum PasswordVerificationResult
    {
        /// <summary>Password verification failed</summary>
        Failed,

        /// <summary>Success</summary>
        Success,

        /// <summary>Success but should update and rehash the password</summary>
        SuccessRehashNeeded,
    }
}
