// <copyright file="OAuthGrantCustomExtensionContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication grant custom extension context class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    /// <summary>Provides context information used when handling OAuth extension grant types.</summary>
    /// <seealso cref="BaseValidatingTicketContext{OAuthAuthorizationServerOptions}"/>
    public class OAuthGrantCustomExtensionContext : BaseValidatingTicketContext<OAuthAuthorizationServerOptions>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="T:Microsoft.Owin.Security.OAuth.OAuthGrantCustomExtensionContext" />
        /// class.</summary>
        /// <param name="context">   .</param>
        /// <param name="options">   .</param>
        /// <param name="clientId">  .</param>
        /// <param name="grantType"> .</param>
        /// <param name="parameters">.</param>
        public OAuthGrantCustomExtensionContext(
            IOwinContext context,
            OAuthAuthorizationServerOptions options,
            string clientId,
            string grantType,
            IReadableStringCollection parameters) : base(context, options, null)
        {
            ClientId = clientId;
            GrantType = grantType;
            Parameters = parameters;
        }

        /// <summary>Gets the OAuth client id.</summary>
        /// <value>The identifier of the client.</value>
        public string ClientId
        {
            get;
        }

        /// <summary>Gets the name of the OAuth extension grant type.</summary>
        /// <value>The type of the grant.</value>
        public string GrantType
        {
            get;
        }

        /// <summary>Gets a list of additional parameters from the token request.</summary>
        /// <value>The parameters.</value>
        public IReadableStringCollection Parameters
        {
            get;
        }
    }
}
