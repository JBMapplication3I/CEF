// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.SecurityToken
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    /// <summary>A security token. This class cannot be inherited.</summary>
    internal sealed class SecurityToken
    {
        /// <summary>The data.</summary>
        private readonly byte[] _data;

        /// <summary>Initializes a new instance of the <see cref="Microsoft.AspNet.Identity.SecurityToken" /> class.</summary>
        /// <param name="data">The data.</param>
        public SecurityToken(byte[] data)
        {
            _data = (byte[])data.Clone();
        }

        /// <summary>Gets data no clone.</summary>
        /// <returns>An array of byte.</returns>
        internal byte[] GetDataNoClone()
        {
            return _data;
        }
    }
}
