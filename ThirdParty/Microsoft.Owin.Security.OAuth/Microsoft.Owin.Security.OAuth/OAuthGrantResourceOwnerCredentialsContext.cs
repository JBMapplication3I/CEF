// <copyright file="OAuthGrantResourceOwnerCredentialsContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication grant resource owner credentials context class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using System.Collections.Generic;

    /// <summary>Provides context information used in handling an OAuth resource owner grant.</summary>
    /// <seealso cref="BaseValidatingTicketContext{OAuthAuthorizationServerOptions}"/>
    public class OAuthGrantResourceOwnerCredentialsContext
        : BaseValidatingTicketContext<OAuthAuthorizationServerOptions>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="T:Microsoft.Owin.Security.OAuth.OAuthGrantResourceOwnerCredentialsContext" /> class.</summary>
        /// <param name="context"> .</param>
        /// <param name="options"> .</param>
        /// <param name="clientId">.</param>
        /// <param name="userName">.</param>
        /// <param name="password">.</param>
        /// <param name="scope">   .</param>
        public OAuthGrantResourceOwnerCredentialsContext(
            IOwinContext context,
            OAuthAuthorizationServerOptions options,
            string clientId,
            string userName,
            string password,
            IList<string> scope) : base(context, options, null)
        {
            ClientId = clientId;
            UserName = userName;
            Password = password;
            Scope = scope;
        }

        /// <summary>OAuth client id.</summary>
        /// <value>The identifier of the client.</value>
        public string ClientId
        {
            get;
        }

        /// <summary>Resource owner password.</summary>
        /// <value>The password.</value>
        public string Password
        {
            get;
        }

        /// <summary>List of scopes allowed by the resource owner.</summary>
        /// <value>The scope.</value>
        public IList<string> Scope
        {
            get;
        }

        /// <summary>Resource owner username.</summary>
        /// <value>The name of the user.</value>
        public string UserName
        {
            get;
        }
    }
}
