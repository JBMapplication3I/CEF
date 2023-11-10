// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IdentityMessage
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    /// <summary>Represents a message.</summary>
    public class IdentityMessage
    {
        /// <summary>Message contents.</summary>
        /// <value>The body.</value>
        public virtual string Body { get; set; }

        /// <summary>Destination, i.e. To email, or SMS phone number.</summary>
        /// <value>The destination.</value>
        public virtual string Destination { get; set; }

        /// <summary>Subject.</summary>
        /// <value>The subject.</value>
        public virtual string Subject { get; set; }
    }
}
