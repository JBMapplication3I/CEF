// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IIdentityMessageServiceExtensions
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;

    /// <summary>An identity message service extensions.</summary>
    public static class IIdentityMessageServiceExtensions
    {
        /// <summary>Sync method to send the IdentityMessage.</summary>
        /// <param name="service">.</param>
        /// <param name="message">.</param>
        public static void Send(this IIdentityMessageService service, IdentityMessage message)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            AsyncHelper.RunSync(() => service.SendAsync(message));
        }
    }
}
