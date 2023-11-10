// <copyright file="OAuthRequestTokenContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication request token context class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using Provider;

    /// <summary>Specifies the HTTP request header for the bearer authentication scheme.</summary>
    /// <seealso cref="BaseContext"/>
    public class OAuthRequestTokenContext : BaseContext
    {
        /// <summary>Initializes a new <see cref="T:Microsoft.Owin.Security.OAuth.OAuthRequestTokenContext" /></summary>
        /// <param name="context">OWIN environment.</param>
        /// <param name="token">  The authorization header value.</param>
        public OAuthRequestTokenContext(IOwinContext context, string token) : base(context)
        {
            Token = token;
        }

        /// <summary>The authorization header value.</summary>
        /// <value>The token.</value>
        public string Token
        {
            get;
            set;
        }
    }
}
