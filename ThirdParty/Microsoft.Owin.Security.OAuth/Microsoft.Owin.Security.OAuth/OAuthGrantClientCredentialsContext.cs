// <copyright file="OAuthGrantClientCredentialsContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication grant client credentials context class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using System.Collections.Generic;

    /// <summary>Provides context information used in handling an OAuth client credentials grant.</summary>
    /// <seealso cref="BaseValidatingTicketContext{OAuthAuthorizationServerOptions}"/>
    public class OAuthGrantClientCredentialsContext : BaseValidatingTicketContext<OAuthAuthorizationServerOptions>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="T:Microsoft.Owin.Security.OAuth.OAuthGrantClientCredentialsContext" />
        /// class.</summary>
        /// <param name="context"> .</param>
        /// <param name="options"> .</param>
        /// <param name="clientId">.</param>
        /// <param name="scope">   .</param>
        public OAuthGrantClientCredentialsContext(
            IOwinContext context,
            OAuthAuthorizationServerOptions options,
            string clientId,
            IList<string> scope) : base(context, options, null)
        {
            ClientId = clientId;
            Scope = scope;
        }

        /// <summary>OAuth client id.</summary>
        /// <value>The identifier of the client.</value>
        public string ClientId
        {
            get;
        }

        /// <summary>List of scopes allowed by the resource owner.</summary>
        /// <value>The scope.</value>
        public IList<string> Scope
        {
            get;
        }
    }
}
