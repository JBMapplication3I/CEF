// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.Security.OAuth.Messages.AuthorizeEndpointRequest
// Assembly: Microsoft.Owin.Security.OAuth, Version=4.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 56B828B8-2BAD-4A4C-8B1F-A15ED0031673
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin.security.oauth\4.1.0\lib\net45\Microsoft.Owin.Security.OAuth.dll

namespace Microsoft.Owin.Security.OAuth.Messages
{
    using System;
    using System.Collections.Generic;

    /// <summary>Data object representing the information contained in the query string of an Authorize endpoint
    /// request.</summary>
    public class AuthorizeEndpointRequest
    {
        /// <summary>Creates a new instance populated with values from the query string parameters.</summary>
        /// <param name="parameters">Query string parameters from a request.</param>
        public AuthorizeEndpointRequest(IReadableStringCollection parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            Scope = new List<string>();
            foreach (var parameter in parameters)
            {
                AddParameter(parameter.Key, parameters.Get(parameter.Key));
            }
        }

        /// <summary>The "client_id" query string parameter of the Authorize request.</summary>
        /// <value>The identifier of the client.</value>
        public string ClientId { get; set; }

        /// <summary>True if the "response_type" query string parameter is "code". See also,
        /// http://tools.ietf.org/html/rfc6749#section-4.1.1.</summary>
        /// <value>True if this AuthorizeEndpointRequest is authorization code grant type, false if not.</value>
        public bool IsAuthorizationCodeGrantType => ContainsGrantType("code");

        /// <summary>Gets a value indicating whether this AuthorizeEndpointRequest is form post response mode.</summary>
        /// <value>True if this AuthorizeEndpointRequest is form post response mode, false if not.</value>
        public bool IsFormPostResponseMode => string.Equals(ResponseMode, "form_post", StringComparison.Ordinal);

        /// <summary>True if the "response_type" query string parameter is "token". See also,
        /// http://tools.ietf.org/html/rfc6749#section-4.2.1.</summary>
        /// <value>True if this AuthorizeEndpointRequest is implicit grant type, false if not.</value>
        public bool IsImplicitGrantType => ContainsGrantType("token");

        /// <summary>The "redirect_uri" query string parameter of the Authorize request. May be absent if the server
        /// should use the redirect uri known to be registered to the client id.</summary>
        /// <value>The redirect URI.</value>
        public string RedirectUri { get; set; }

        /// <summary>The "response_mode" query string parameter of the Authorize request. Known values are "query",
        /// "fragment" and "form_post" See also, http://openid.net/specs/oauth-v2-form-post-response-mode-1_0.html.</summary>
        /// <value>The response mode.</value>
        public string ResponseMode { get; set; }

        /// <summary>The "response_type" query string parameter of the Authorize request. Known values are "code" and
        /// "token".</summary>
        /// <value>The type of the response.</value>
        public string ResponseType { get; set; }

        /// <summary>The "scope" query string parameter of the Authorize request. May be absent if the server should use
        /// default scopes.</summary>
        /// <value>The scope.</value>
        public IList<string> Scope { get; private set; }

        /// <summary>The "scope" query string parameter of the Authorize request. May be absent if the client does not
        /// require state to be included when returning to the RedirectUri.</summary>
        /// <value>The state.</value>
        public string State { get; set; }

        /// <summary>True if the "response_type" query string contains the passed responseType. See also,
        /// http://openid.net/specs/oauth-v2-multiple-response-types-1_0.html.</summary>
        /// <param name="responseType">The responseType that is expected within the "response_type" query string.</param>
        /// <returns>True if the "response_type" query string contains the passed responseType.</returns>
        public bool ContainsGrantType(string responseType)
        {
            var responseType1 = ResponseType;
            var chArray = new char[1] { ' ' };
            foreach (var a in responseType1.Split(chArray))
            {
                if (string.Equals(a, responseType, StringComparison.Ordinal))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Adds a parameter to 'value'.</summary>
        /// <param name="name"> The name.</param>
        /// <param name="value">The value.</param>
        private void AddParameter(string name, string value)
        {
            if (string.Equals(name, "response_type", StringComparison.Ordinal))
            {
                ResponseType = value;
                return;
            }
            if (string.Equals(name, "client_id", StringComparison.Ordinal))
            {
                ClientId = value;
                return;
            }
            if (string.Equals(name, "redirect_uri", StringComparison.Ordinal))
            {
                RedirectUri = value;
                return;
            }
            if (string.Equals(name, "scope", StringComparison.Ordinal))
            {
                Scope = value.Split(' ');
                return;
            }
            if (string.Equals(name, "state", StringComparison.Ordinal))
            {
                State = value;
                return;
            }
            if (string.Equals(name, "response_mode", StringComparison.Ordinal))
            {
                ResponseMode = value;
            }
        }
    }
}
