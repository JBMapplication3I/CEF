// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IIdentityMessageService
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System.Threading.Tasks;

    /// <summary>Expose a way to send messages (i.e. email/sms)</summary>
    public interface IIdentityMessageService
    {
        /// <summary>This method should send the message.</summary>
        /// <param name="message">.</param>
        /// <returns>A Task.</returns>
        Task SendAsync(IdentityMessage message);
    }
}
