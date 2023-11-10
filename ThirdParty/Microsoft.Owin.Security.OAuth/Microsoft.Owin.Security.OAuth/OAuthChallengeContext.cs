// <copyright file="OAuthChallengeContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication challenge context class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using Provider;

    /// <summary>Specifies the HTTP response header for the bearer authentication scheme.</summary>
    /// <seealso cref="BaseContext"/>
    public class OAuthChallengeContext : BaseContext
    {
        /// <summary>Initializes a new <see cref="T:Microsoft.Owin.Security.OAuth.OAuthRequestTokenContext" /></summary>
        /// <param name="context">  OWIN environment.</param>
        /// <param name="challenge">The www-authenticate header value.</param>
        public OAuthChallengeContext(IOwinContext context, string challenge) : base(context)
        {
            Challenge = challenge;
        }

        /// <summary>The www-authenticate header value.</summary>
        /// <value>The challenge.</value>
        public string Challenge
        {
            get;
            protected set;
        }
    }
}
