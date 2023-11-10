namespace Microsoft.Owin.Security.OAuth.Messages
{
    using System;

    /// <summary>Data object representing the information contained in form encoded body of a Token endpoint request.</summary>
    public class TokenEndpointRequest
    {
        /// <summary>Creates a new instance populated with values from the form encoded body parameters.</summary>
        /// <param name="parameters">Form encoded body parameters from a request.</param>
        public TokenEndpointRequest(IReadableStringCollection parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            Func<string, string> func = parameters.Get;
            Parameters = parameters;
            GrantType = func("grant_type");
            ClientId = func("client_id");
            if (string.Equals(GrantType, "authorization_code", StringComparison.Ordinal))
            {
                AuthorizationCodeGrant = new TokenEndpointRequestAuthorizationCode
                {
                    Code = func("code"),
                    RedirectUri = func("redirect_uri"),
                };
                return;
            }
            if (string.Equals(GrantType, "client_credentials", StringComparison.Ordinal))
            {
                ClientCredentialsGrant = new TokenEndpointRequestClientCredentials
                {
                    Scope = (func("scope") ?? string.Empty).Split(' '),
                };
                return;
            }
            if (string.Equals(GrantType, "refresh_token", StringComparison.Ordinal))
            {
                RefreshTokenGrant = new TokenEndpointRequestRefreshToken
                {
                    RefreshToken = func("refresh_token"),
                    Scope = (func("scope") ?? string.Empty).Split(' '),
                };
                return;
            }
            if (!string.Equals(GrantType, "password", StringComparison.Ordinal))
            {
                if (!string.IsNullOrEmpty(GrantType))
                {
                    CustomExtensionGrant = new TokenEndpointRequestCustomExtension { Parameters = parameters };
                }
                return;
            }
            ResourceOwnerPasswordCredentialsGrant = new TokenEndpointRequestResourceOwnerPasswordCredentials
            {
                UserName = func("username"),
                Password = func("password"),
                Scope = (func("scope") ?? string.Empty).Split(' '),
            };
        }

        /// <summary>Data object available when the "grant_type" is "authorization_code". See also
        /// http://tools.ietf.org/html/rfc6749#section-4.1.3.</summary>
        /// <value>The authorization code grant.</value>
        public TokenEndpointRequestAuthorizationCode AuthorizationCodeGrant { get; }

        /// <summary>Data object available when the "grant_type" is "client_credentials". See also
        /// http://tools.ietf.org/html/rfc6749#section-4.4.2.</summary>
        /// <value>The client credentials grant.</value>
        public TokenEndpointRequestClientCredentials ClientCredentialsGrant { get; }

        /// <summary>The "client_id" parameter of the Token endpoint request. This parameter is optional. It might not be
        /// present if the request is authenticated in a different way, for example, by using basic authentication
        /// credentials.</summary>
        /// <value>The identifier of the client.</value>
        public string ClientId { get; }

        /// <summary>Data object available when the "grant_type" is unrecognized. See also
        /// http://tools.ietf.org/html/rfc6749#section-4.5.</summary>
        /// <value>The custom extension grant.</value>
        public TokenEndpointRequestCustomExtension CustomExtensionGrant { get; }

        /// <summary>The "grant_type" parameter of the Token endpoint request. This parameter is required.</summary>
        /// <value>The type of the grant.</value>
        public string GrantType { get; }

        /// <summary>True when the "grant_type" is "authorization_code". See also
        /// http://tools.ietf.org/html/rfc6749#section-4.1.3.</summary>
        /// <value>True if this TokenEndpointRequest is authorization code grant type, false if not.</value>
        public bool IsAuthorizationCodeGrantType => AuthorizationCodeGrant != null;

        /// <summary>True when the "grant_type" is "client_credentials". See also
        /// http://tools.ietf.org/html/rfc6749#section-4.4.2.</summary>
        /// <value>True if this TokenEndpointRequest is client credentials grant type, false if not.</value>
        public bool IsClientCredentialsGrantType => ClientCredentialsGrant != null;

        /// <summary>True when the "grant_type" is unrecognized. See also http://tools.ietf.org/html/rfc6749#section-4.5.</summary>
        /// <value>True if this TokenEndpointRequest is custom extension grant type, false if not.</value>
        public bool IsCustomExtensionGrantType => CustomExtensionGrant != null;

        /// <summary>True when the "grant_type" is "refresh_token". See also http://tools.ietf.org/html/rfc6749#section-6.</summary>
        /// <value>True if this TokenEndpointRequest is refresh token grant type, false if not.</value>
        public bool IsRefreshTokenGrantType => RefreshTokenGrant != null;

        /// <summary>True when the "grant_type" is "password". See also http://tools.ietf.org/html/rfc6749#section-4.3.2.</summary>
        /// <value>True if this TokenEndpointRequest is resource owner password credentials grant type, false if not.</value>
        public bool IsResourceOwnerPasswordCredentialsGrantType => ResourceOwnerPasswordCredentialsGrant != null;

        /// <summary>The form encoded body parameters of the Token endpoint request.</summary>
        /// <value>The parameters.</value>
        public IReadableStringCollection Parameters { get; }

        /// <summary>Data object available when the "grant_type" is "refresh_token". See also
        /// http://tools.ietf.org/html/rfc6749#section-6.</summary>
        /// <value>The refresh token grant.</value>
        public TokenEndpointRequestRefreshToken RefreshTokenGrant { get; }

        /// <summary>Data object available when the "grant_type" is "password". See also
        /// http://tools.ietf.org/html/rfc6749#section-4.3.2.</summary>
        /// <value>The resource owner password credentials grant.</value>
        public TokenEndpointRequestResourceOwnerPasswordCredentials ResourceOwnerPasswordCredentialsGrant { get; }
    }
}
