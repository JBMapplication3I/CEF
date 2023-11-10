// <copyright file="AspNetIdentityAuthProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ASP net identity authentication provider class</summary>
namespace ServiceStack.Auth
{
    using Configuration;
    using JetBrains.Annotations;

    /// <summary>An ASP net identity authentication provider.</summary>
    /// <seealso cref="CEFAuthProviderBase"/>
    [PublicAPI]
    public class AspNetIdentityAuthProvider : CEFAuthProviderBase
    {
        /// <summary>Initializes a new instance of the <see cref="AspNetIdentityAuthProvider"/> class.</summary>
        public AspNetIdentityAuthProvider()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="AspNetIdentityAuthProvider"/> class.</summary>
        /// <param name="appSettings">The application settings.</param>
        public AspNetIdentityAuthProvider(IAppSettings appSettings)
            : this(appSettings, StaticRealm, StaticName)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="AspNetIdentityAuthProvider"/> class.</summary>
        /// <param name="appSettings">  The application settings.</param>
        /// <param name="authRealm">    The authentication realm.</param>
        /// <param name="oAuthProvider">The authentication provider.</param>
        protected AspNetIdentityAuthProvider(IAppSettings appSettings, string authRealm, string oAuthProvider)
            : base(appSettings, authRealm, oAuthProvider)
        {
        }

        /// <summary>Gets the name of the static.</summary>
        /// <value>The name of the static.</value>
        public static string StaticName => "identity";

        /// <summary>Gets the static realm.</summary>
        /// <value>The static realm.</value>
        public static string StaticRealm => "/auth/identity";

        /// <inheritdoc/>
        public override string Name => StaticName;

        /// <inheritdoc/>
        public override string Realm => StaticRealm;
    }
}
