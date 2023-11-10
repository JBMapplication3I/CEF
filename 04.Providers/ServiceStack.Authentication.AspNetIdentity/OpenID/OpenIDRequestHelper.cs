// <copyright file="OpenIDRequestHelper.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the OpenIDRequestHelper class</summary>
// ReSharper disable PossibleNullReferenceException
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
namespace ServiceStack.Auth
{
    using System;
    using System.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Clarity.Ecommerce;
    using Clarity.Ecommerce.Interfaces.Workflow;
    using Clarity.Ecommerce.JSConfigs;
    using IdentityModel;
    using IdentityModel.Client;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;

    /// <summary>The OpenIDRequest helper class.</summary>
    public static class OpenIDRequestHelper
    {
        /// <summary>Requests an id_token from the OIDC server.</summary>
        /// <param name="logger">  The error logger.</param>
        /// <param name="authCode">The authentication code.</param>
        /// <returns>A Task{OpenIDConnectCodeCallback}.</returns>
        public static async Task<OpenIDConnectCodeCallback?> RequestTokenAsync(ILogger logger, string authCode)
        {
            var url = CEFConfigDictionary.AuthProviderTokenUrl;
            var clientID = CEFConfigDictionary.AuthProviderClientId;
            var clientSecret = CEFConfigDictionary.AuthProviderClientSecret;
            var redirectURI = CEFConfigDictionary.AuthProviderRedirectUri;
            const string GrantType = "authorization_code";
            const string ResponseType = "code id_token token";
            var token = new OpenIDConnectCodeCallback();
            try
            {
                var tokenRequest = (HttpWebRequest)WebRequest.Create(url!);
#pragma warning disable SCS0004 // Certificate Validation has been disabled
                tokenRequest.ServerCertificateValidationCallback += (_, _, _, _) => true;
#pragma warning restore SCS0004 // Certificate Validation has been disabled
                tokenRequest.KeepAlive = true;
                ServicePointManager.Expect100Continue = true;
                ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls; // Disable 1.0
                ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11; // Disable 1.1
                // 1.2+ is the only thing that should be allowed
                tokenRequest.Method = "POST";
                tokenRequest.ServicePoint.Expect100Continue = false;
                tokenRequest.Accept = "application/json";
                tokenRequest.ContentType = "application/x-www-form-urlencoded";
                var body = $"grant_type={GrantType}"
                    + $"&response_type={ResponseType}"
                    + $"&code={authCode}"
                    + $"&redirect_uri={redirectURI}"
                    + $"&client_id={clientID}"
                    + $"&client_secret={clientSecret}";
                var postBytes = Encoding.UTF8.GetBytes(body);
                tokenRequest.ContentLength = postBytes.Length;
                using (var stream = tokenRequest.GetRequestStream())
                {
                    await stream.WriteAsync(postBytes, 0, postBytes.Length).ConfigureAwait(false);
                }
                using var response = (HttpWebResponse)await tokenRequest.GetResponseAsync().ConfigureAwait(false);
                using var dataStream = response.GetResponseStream() ?? throw new NullReferenceException();
                using var reader = new StreamReader(dataStream);
                var tokenResponse = await reader.ReadToEndAsync().ConfigureAwait(false);
                token = JsonConvert.DeserializeObject<OpenIDConnectCodeCallback>(tokenResponse);
            }
            catch (WebException ex)
            {
                if (ex.Status != WebExceptionStatus.ProtocolError)
                {
                    return token;
                }
                WebResponse? response = (HttpWebResponse?)ex.Response;
                using var dataStream = response?.GetResponseStream() ?? throw new NullReferenceException();
                using var reader = new StreamReader(dataStream);
                var tokenResponse = await reader.ReadToEndAsync().ConfigureAwait(false);
                token = JsonConvert.DeserializeObject<OpenIDConnectCodeCallback>(tokenResponse);
                await logger.LogErrorAsync(nameof(IAuthenticationWorkflow), token?.Error, ex, null).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await logger.LogErrorAsync(nameof(IAuthenticationWorkflow), ex.Message, ex, null).ConfigureAwait(false);
                if (token is not null)
                {
                    token.Error = ex.Message;
                }
            }
            return token;
        }

        /// <summary>Requests user info from the OIDC server.</summary>
        /// <param name="logger"> The error logger.</param>
        /// <param name="idToken">The id_token.</param>
        /// <returns>An OpenIDUser.</returns>
        public static OpenIDUser? RequestUserInfo(ILogger logger, string idToken)
        {
            var url = CEFConfigDictionary.AuthProviderUserInfoUrl;
            var user = new OpenIDUser();
            try
            {
                var tokenRequest = (HttpWebRequest)WebRequest.Create(url!);
#pragma warning disable SCS0004 // Certificate Validation has been disabled
                tokenRequest.ServerCertificateValidationCallback += (_, _, _, _) => true;
#pragma warning restore SCS0004 // Certificate Validation has been disabled
                tokenRequest.KeepAlive = true;
                ServicePointManager.Expect100Continue = true;
                ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls; // Disable 1.0
                ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11; // Disable 1.1
                // 1.2+ is the only thing that should be allowed
                tokenRequest.Headers["Authorization"] = "Bearer " + idToken;
                tokenRequest.Method = "POST";
                tokenRequest.ServicePoint.Expect100Continue = false;
                tokenRequest.Accept = "application/json";
                tokenRequest.ContentType = "application/x-www-form-urlencoded";
                tokenRequest.ContentLength = 0;
                using var response = (HttpWebResponse)tokenRequest.GetResponse();
                using var dataStream = response.GetResponseStream() ?? throw new NullReferenceException();
                using var reader = new StreamReader(dataStream);
                var tokenResponse = reader.ReadToEnd();
                user = JsonConvert.DeserializeObject<OpenIDUser>(tokenResponse);
            }
            catch (WebException ex)
            {
                if (ex.Status != WebExceptionStatus.ProtocolError)
                {
                    return user;
                }
                WebResponse? response = (HttpWebResponse?)ex.Response;
                using var dataStream = response?.GetResponseStream() ?? throw new NullReferenceException();
                using var reader = new StreamReader(dataStream);
                var tokenResponse = reader.ReadToEnd();
                user = JsonConvert.DeserializeObject<OpenIDUser>(tokenResponse);
                logger.LogError(nameof(IAuthenticationWorkflow), user?.Error ?? string.Empty, ex, null);
            }
            catch (Exception ex)
            {
                logger.LogError(nameof(IAuthenticationWorkflow), ex.Message, ex, null);
                if (user is not null)
                {
                    user.Error = ex.Message;
                }
            }
            return user;
        }

        /// <summary>Validates the JWT.</summary>
        /// <param name="jwt">     The JWT.</param>
        /// <param name="issuer">  (Optional) The issuer.</param>
        /// <param name="clientId">(Optional) Identifier for the client.</param>
        /// <returns>An result that yields a ClaimsPrincipal.</returns>
        public static async Task<ClaimsPrincipal?> ValidateJwtAsync(string jwt, string? issuer = null, string? clientId = null)
        {
            issuer ??= CEFConfigDictionary.AuthProviderIssuer;
            clientId ??= CEFConfigDictionary.AuthProviderClientId;
#pragma warning disable CS0618 // Type or member is obsolete
            var disco = await DiscoveryClient.GetAsync(issuer).ConfigureAwait(false);
#pragma warning restore CS0618 // Type or member is obsolete
            var parameters = new TokenValidationParameters
            {
                ValidIssuer = disco.Issuer,
                ValidAudience = clientId,
                IssuerSigningKeys = disco.KeySet.Keys
                    .Select(key => new
                    {
                        key,
                        @params = new RSAParameters
                        {
                            Exponent = Base64Url.Decode(key.E),
                            Modulus = Base64Url.Decode(key.N),
                        },
                    })
                    .Select(x => new Microsoft.IdentityModel.Tokens.RsaSecurityKey(x.@params) { KeyId = x.key.Kid })
                    .Cast<Microsoft.IdentityModel.Tokens.SecurityKey>()
                    .ToList()!,
                NameClaimType = JwtClaimTypes.Name,
                RoleClaimType = JwtClaimTypes.Role,
                RequireSignedTokens = true,
            };
            var handler = new JwtSecurityTokenHandler();
            handler.InboundClaimTypeMap.Clear();
            return handler.ValidateToken(jwt, parameters, out _);
        }
    }
}
