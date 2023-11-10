// <copyright file="OpenIDAuthProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the open identifier authentication provider class</summary>
namespace ServiceStack.Auth
{
    using Configuration;

    /// <summary>An OpenID authentication provider.</summary>
    /// <seealso cref="CEFAuthProviderBase"/>
    public class OpenIDAuthProvider : CEFAuthProviderBase
    {
        /// <summary>Initializes a new instance of the <see cref="OpenIDAuthProvider"/> class.</summary>
        public OpenIDAuthProvider()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="OpenIDAuthProvider"/> class.</summary>
        /// <param name="appSettings">The application settings.</param>
        public OpenIDAuthProvider(IAppSettings appSettings)
            : this(appSettings, StaticRealm, StaticName)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="OpenIDAuthProvider"/> class.</summary>
        /// <param name="appSettings">  The application settings.</param>
        /// <param name="authRealm">    The authentication realm.</param>
        /// <param name="oAuthProvider">The authentication provider.</param>
        protected OpenIDAuthProvider(
            IAppSettings appSettings,
            string authRealm,
            string oAuthProvider)
            : base(appSettings, authRealm, oAuthProvider)
        {
        }

        /// <summary>Gets the name of the static.</summary>
        /// <value>The name of the static.</value>
        public static string StaticName => "openid";

        /// <summary>Gets the static realm.</summary>
        /// <value>The static realm.</value>
        public static string StaticRealm => "/auth/openid";

        /// <inheritdoc/>
        public override string Name => StaticName;

        /// <inheritdoc/>
        public override string Realm => StaticRealm;
    }
}
