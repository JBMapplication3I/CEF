// <copyright file="OAuthValidateClientAuthenticationContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication validate client authentication context class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using System;
    using System.Text;

    /// <summary>Contains information about the client credentials.</summary>
    /// <seealso cref="BaseValidatingClientContext"/>
    public class OAuthValidateClientAuthenticationContext : BaseValidatingClientContext
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="T:Microsoft.Owin.Security.OAuth.OAuthValidateClientAuthenticationContext" /> class.</summary>
        /// <param name="context">   .</param>
        /// <param name="options">   .</param>
        /// <param name="parameters">.</param>
        public OAuthValidateClientAuthenticationContext(
            IOwinContext context,
            OAuthAuthorizationServerOptions options,
            IReadableStringCollection parameters) : base(context, options, null)
        {
            Parameters = parameters;
        }

        /// <summary>Gets the set of form parameters from the request.</summary>
        /// <value>The parameters.</value>
        public IReadableStringCollection Parameters
        {
            get;
        }

        /// <summary>Extracts HTTP basic authentication credentials from the HTTP authenticate header.</summary>
        /// <param name="clientId">    .</param>
        /// <param name="clientSecret">.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool TryGetBasicCredentials(out string clientId, out string clientSecret)
        {
            bool flag;
            var str = Request.Headers.Get("Authorization");
            if (!string.IsNullOrWhiteSpace(str) && str.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    var numArray = Convert.FromBase64String(str["Basic ".Length..].Trim());
                    var str1 = Encoding.UTF8.GetString(numArray);
                    var num = str1.IndexOf(':');
                    if (num < 0)
                    {
                        clientId = null;
                        clientSecret = null;
                        return false;
                    }
                    clientId = str1.Substring(0, num);
                    clientSecret = str1[(num + 1)..];
                    ClientId = clientId;
                    flag = true;
                }
                catch (FormatException)
                {
                    clientId = null;
                    clientSecret = null;
                    return false;
                }
                catch (ArgumentException)
                {
                    clientId = null;
                    clientSecret = null;
                    return false;
                }
                return flag;
            }
            clientId = null;
            clientSecret = null;
            return false;
        }

        /// <summary>Extracts forms authentication credentials from the HTTP request body.</summary>
        /// <param name="clientId">    .</param>
        /// <param name="clientSecret">.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool TryGetFormCredentials(out string clientId, out string clientSecret)
        {
            clientId = Parameters.Get("client_id");
            if (string.IsNullOrEmpty(clientId))
            {
                clientId = null;
                clientSecret = null;
                return false;
            }
            clientSecret = Parameters.Get("client_secret");
            ClientId = clientId;
            return true;
        }

        /// <summary>Sets the client id and marks the context as validated by the application.</summary>
        /// <param name="clientId">.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool Validated(string clientId)
        {
            ClientId = clientId;
            return Validated();
        }
    }
}
