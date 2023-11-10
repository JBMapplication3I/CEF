// <copyright file="Constants.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the constants class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    /// <summary>A constants.</summary>
    internal static class Constants
    {
        /// <summary>An errors.</summary>
        public static class Errors
        {
            /// <summary>The invalid client.</summary>
            public const string InvalidClient = "invalid_client";

            /// <summary>The invalid grant.</summary>
            public const string InvalidGrant = "invalid_grant";

            /// <summary>The invalid request.</summary>
            public const string InvalidRequest = "invalid_request";

            /// <summary>The unauthorized client.</summary>
            public const string UnauthorizedClient = "unauthorized_client";

            /// <summary>Type of the unsupported grant.</summary>
            public const string UnsupportedGrantType = "unsupported_grant_type";

            /// <summary>Type of the unsupported response.</summary>
            public const string UnsupportedResponseType = "unsupported_response_type";
        }

        /// <summary>An extra.</summary>
        public static class Extra
        {
            /// <summary>Identifier for the client.</summary>
            public const string ClientId = "client_id";

            /// <summary>URI of the redirect.</summary>
            public const string RedirectUri = "redirect_uri";
        }

        /// <summary>A grant types.</summary>
        public static class GrantTypes
        {
            /// <summary>The authorization code.</summary>
            public const string AuthorizationCode = "authorization_code";

            /// <summary>The client credentials.</summary>
            public const string ClientCredentials = "client_credentials";

            /// <summary>The password.</summary>
            public const string Password = "password";

            /// <summary>The refresh token.</summary>
            public const string RefreshToken = "refresh_token";
        }

        /// <summary>A parameters.</summary>
        public static class Parameters
        {
            /// <summary>The access token.</summary>
            public const string AccessToken = "access_token";

            /// <summary>Identifier for the client.</summary>
            public const string ClientId = "client_id";

            /// <summary>The client secret.</summary>
            public const string ClientSecret = "client_secret";

            /// <summary>The code.</summary>
            public const string Code = "code";

            /// <summary>The error.</summary>
            public const string Error = "error";

            /// <summary>Information describing the error.</summary>
            public const string ErrorDescription = "error_description";

            /// <summary>URI of the error.</summary>
            public const string ErrorUri = "error_uri";

            /// <summary>The expires in.</summary>
            public const string ExpiresIn = "expires_in";

            /// <summary>Type of the grant.</summary>
            public const string GrantType = "grant_type";

            /// <summary>The password.</summary>
            public const string Password = "password";

            /// <summary>URI of the redirect.</summary>
            public const string RedirectUri = "redirect_uri";

            /// <summary>The refresh token.</summary>
            public const string RefreshToken = "refresh_token";

            /// <summary>The response mode.</summary>
            public const string ResponseMode = "response_mode";

            /// <summary>Type of the response.</summary>
            public const string ResponseType = "response_type";

            /// <summary>The scope.</summary>
            public const string Scope = "scope";

            /// <summary>The state.</summary>
            public const string State = "state";

            /// <summary>Type of the token.</summary>
            public const string TokenType = "token_type";

            /// <summary>The username.</summary>
            public const string Username = "username";
        }

        /// <summary>A response modes.</summary>
        public static class ResponseModes
        {
            /// <summary>The form post.</summary>
            public const string FormPost = "form_post";
        }

        /// <summary>A response types.</summary>
        public static class ResponseTypes
        {
            /// <summary>The code.</summary>
            public const string Code = "code";

            /// <summary>The token.</summary>
            public const string Token = "token";
        }

        /// <summary>A token types.</summary>
        public static class TokenTypes
        {
            /// <summary>The bearer.</summary>
            public const string Bearer = "bearer";
        }
    }
}
