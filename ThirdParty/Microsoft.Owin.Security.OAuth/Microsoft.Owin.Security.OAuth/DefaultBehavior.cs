// <copyright file="DefaultBehavior.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the default behavior class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using System;
    using System.Threading.Tasks;

    /// <summary>A default behavior.</summary>
    internal static class DefaultBehavior
    {
        /// <summary>The grant authorization code.</summary>
        internal static readonly Func<OAuthGrantAuthorizationCodeContext, Task> GrantAuthorizationCode;

        /// <summary>The grant refresh token.</summary>
        internal static readonly Func<OAuthGrantRefreshTokenContext, Task> GrantRefreshToken;

        /// <summary>The validate authorize request.</summary>
        internal static readonly Func<OAuthValidateAuthorizeRequestContext, Task> ValidateAuthorizeRequest;

        /// <summary>The validate token request.</summary>
        internal static readonly Func<OAuthValidateTokenRequestContext, Task> ValidateTokenRequest;

        /// <summary>Initializes static members of the Microsoft.Owin.Security.OAuth.DefaultBehavior class.</summary>
        static DefaultBehavior()
        {
            ValidateAuthorizeRequest = context =>
            {
                context.Validated();
                return Task.FromResult<object>(null);
            };
            ValidateTokenRequest = context =>
            {
                context.Validated();
                return Task.FromResult<object>(null);
            };
            GrantAuthorizationCode = context =>
            {
                if (context.Ticket != null
                    && context.Ticket.Identity != null
                    && context.Ticket.Identity.IsAuthenticated)
                {
                    context.Validated();
                }
                return Task.FromResult<object>(null);
            };
            GrantRefreshToken = context =>
            {
                if (context.Ticket != null
                    && context.Ticket.Identity != null
                    && context.Ticket.Identity.IsAuthenticated)
                {
                    context.Validated();
                }
                return Task.FromResult<object>(null);
            };
        }
    }
}
