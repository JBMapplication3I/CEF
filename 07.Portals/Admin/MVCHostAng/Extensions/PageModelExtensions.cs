// <copyright file="PageModelExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the page model extensions class</summary>
namespace Clarity.Ecommerce.UI.MVCHost.Extensions
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    /// <summary>A page model extensions.</summary>
    public static class PageModelExtensions
    {
        /// <summary>A PageModel extension method that sets up for sso asynchronous.</summary>
        /// <typeparam name="TCategoryName">Type of the category name.</typeparam>
        /// <param name="pageModel">        The pageModel to act on.</param>
        /// <param name="apiUri">           URI of the API.</param>
        /// <param name="cookieDomain">     The cookie domain.</param>
        /// <param name="requireAllRoles">  The require all roles.</param>
        /// <param name="logger">           The logger.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task{bool}.</returns>
        public static async Task<bool> SetupForSSOAsync<TCategoryName>(
            this PageModel pageModel,
            string apiUri,
            string cookieDomain,
            string[] requireAllRoles,
            ILogger<TCategoryName> logger,
            CancellationToken cancellationToken)
        {
            if (true /* if OIDC was turned on */)
            {
                return true;
            }
            // ReSharper disable HeuristicUnreachableCode
#pragma warning disable 162
            cancellationToken.ThrowIfCancellationRequested();
            logger.LogInformation($"{nameof(SetupForSSOAsync)} entered");
            var contextUser = pageModel.User;
            var upn = contextUser.FindFirst("upn");
            if (upn == null)
            {
                logger.LogInformation($"{nameof(SetupForSSOAsync)} upn null, returning false");
                return false;
            }
            var email = pageModel.User.FindFirst("upn").Value;
            logger.LogInformation($"{nameof(SetupForSSOAsync)} Email from UPN {email}");
            var index = email.IndexOf("@", StringComparison.Ordinal);
            logger.LogInformation($"{nameof(SetupForSSOAsync)} index {index}");
            var globalAccountNumber = email[..index];
            logger.LogInformation($"{nameof(SetupForSSOAsync)} GAN {globalAccountNumber}");
            var requestCookies = pageModel.HttpContext.Request.Cookies;
            var cookies = new CookieContainer();
            foreach (var cookie in requestCookies)
            {
                cookies.Add(new Cookie
                {
                    Domain = cookieDomain,
                    Name = cookie.Key,
                    Value = cookie.Value,
                });
            }
            logger.LogInformation($"{nameof(SetupForSSOAsync)} cookie container created");
            using var handler = new HttpClientHandler { CookieContainer = cookies };
            logger.LogInformation($"{nameof(SetupForSSOAsync)} handler created");
            using var client = new HttpClient(handler) { Timeout = new(0, 5, 0) };
            logger.LogInformation($"{nameof(SetupForSSOAsync)} client created");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new("application/json"));
            logger.LogInformation($"{nameof(SetupForSSOAsync)} calling for user from CEF");
            var userID = await pageModel.GetCurrentUserIDAsync(
                    apiUri,
                    client,
                    globalAccountNumber,
                    cookies,
                    cookieDomain,
                    logger,
                    cancellationToken)
                .ConfigureAwait(false);
            logger.LogInformation($"{nameof(SetupForSSOAsync)} got reply from CEF");
            if (requireAllRoles.Length == 0)
            {
                logger.LogInformation($"{nameof(SetupForSSOAsync)} page doesn't require roles, returning true");
                return true;
            }
            logger.LogInformation($"{nameof(SetupForSSOAsync)} page requires roles, checking roles");
            if (userID == null)
            {
                logger.LogInformation($"{nameof(SetupForSSOAsync)} user is null, can't verify roles");
                return false;
            }
            logger.LogInformation($"{nameof(SetupForSSOAsync)} requesting roles from CEF");
            var requestUri = new Uri($"{apiUri}/Authentication/Roles/User/{userID}");
            logger.LogInformation($"{nameof(SetupForSSOAsync)} using request URI {requestUri}");
            using var response = await client.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
            logger.LogInformation($"{nameof(SetupForSSOAsync)} response awaited");
            var responseString = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            logger.LogInformation($"{nameof(SetupForSSOAsync)} response string\r\n{responseString}");
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(responseString);
            }
            logger.LogInformation($"{nameof(SetupForSSOAsync)} {nameof(response.EnsureSuccessStatusCode)} didn't throw");
            var roles = JsonConvert.DeserializeObject<dynamic[]>(responseString);
            logger.LogInformation($"{nameof(SetupForSSOAsync)} roles parsed, read {roles.Length} roles");
            foreach (var requiredRoleName in requireAllRoles)
            {
                logger.LogInformation($"{nameof(SetupForSSOAsync)} required role {requiredRoleName}");
                if (roles.All(x => x.Name != requiredRoleName))
                {
                    logger.LogInformation($"{nameof(SetupForSSOAsync)} user doesn't have it");
                    return false;
                }
                logger.LogInformation($"{nameof(SetupForSSOAsync)} user has it");
            }
            logger.LogInformation($"{nameof(SetupForSSOAsync)} user has all required roles, returning true");
            return true;
#pragma warning restore 162
            // ReSharper restore HeuristicUnreachableCode
        }

        private static async Task<int?> GetCurrentUserIDAsync<TCategoryName>(
            this PageModel pageModel,
            string apiUri,
            HttpClient client,
            string globalAccountNumber,
            CookieContainer cookies,
            string cookieDomain,
            ILogger<TCategoryName> logger,
            CancellationToken token,
            bool retry = true)
        {
            token.ThrowIfCancellationRequested();
            logger.LogInformation($"{nameof(GetCurrentUserIDAsync)} entered{(retry ? " as retry" : string.Empty)}");
            int? userID = null;
            var requestUri = new Uri($"{apiUri}/Contacts/CurrentUser");
            logger.LogInformation($"{nameof(GetCurrentUserIDAsync)} using request uri {requestUri}");
            using (var response = await client.GetAsync(requestUri, token).ConfigureAwait(false))
            {
                logger.LogInformation($"{nameof(GetCurrentUserIDAsync)} awaited response");
                if (response.IsSuccessStatusCode)
                {
                    logger.LogInformation($"{nameof(GetCurrentUserIDAsync)} response successful, applying cookies");
                    pageModel.SetCookies(cookies, requestUri, cookieDomain, logger);
                    logger.LogInformation($"{nameof(GetCurrentUserIDAsync)} Cookies set, deserializing value");
                    var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    logger.LogInformation($"{nameof(GetCurrentUserIDAsync)}\r\n{responseString}");
                    if (responseString.Contains("\"LogID\":\""))
                    {
                        logger.LogInformation($"{nameof(GetCurrentUserIDAsync)} user call got an error, sending to retry");
                    }
                    else
                    {
                        try
                        {
                            userID = (int)JsonConvert.DeserializeObject<dynamic>(responseString)!.ID;
                        }
                        catch (Exception ex)
                        {
                            logger.LogInformation($"{nameof(GetCurrentUserIDAsync)} user couldn't convert ID to an int: {ex.Message}");
                        }
                        logger.LogInformation($"{nameof(GetCurrentUserIDAsync)} user deserialized");
                        logger.LogInformation($"{nameof(GetCurrentUserIDAsync)} user ID: '{userID}'");
                        logger.LogInformation($"{nameof(GetCurrentUserIDAsync)} returning user");
                        return userID;
                    }
                }
                if (!retry)
                {
                    logger.LogWarning($"{nameof(GetCurrentUserIDAsync)} response not successful but not allowed to retry");
                    logger.LogWarning($"{nameof(GetCurrentUserIDAsync)} response {response.StatusCode}");
                    var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    logger.LogWarning(responseString);
                    return null;
                }
            }
            logger.LogWarning($"{nameof(GetCurrentUserIDAsync)} response not successful but allowed to retry");
            await pageModel.LoginAsync(
                    client,
                    cookies,
                    apiUri,
                    globalAccountNumber,
                    cookieDomain,
                    logger,
                    token)
                .ConfigureAwait(false);
            logger.LogInformation($"{nameof(GetCurrentUserIDAsync)} login call awaited");
            userID = await pageModel.GetCurrentUserIDAsync(
                    apiUri,
                    client,
                    globalAccountNumber,
                    cookies,
                    cookieDomain,
                    logger,
                    token,
                    false)
                .ConfigureAwait(false);
            logger.LogInformation($"{nameof(GetCurrentUserIDAsync)} get user retry call awaited");
            logger.LogInformation($"{nameof(GetCurrentUserIDAsync)} returning user");
            return userID;
        }

        private static async Task LoginAsync<TCategoryName>(
            this PageModel pageModel,
            HttpClient client,
            CookieContainer cookies,
            string apiUri,
            string globalAccountNumber,
            string cookieDomain,
            ILogger<TCategoryName> logger,
            CancellationToken token)
        {
            ////var logWriter = new System.IO.StreamWriter(@"C:\Data\Projects\DEV\ADM\WEB\log.txt");
            logger.LogInformation($"{nameof(LoginAsync)} entered");
            ////logWriter.Write($"{nameof(LoginAsync)} entered");
            var requestUri = new Uri($"{apiUri}/auth/cobalt");
            logger.LogInformation($"{nameof(LoginAsync)} using request uri {requestUri}");
            ////logWriter.Write($"{nameof(LoginAsync)} using request uri {requestUri}");
            var request = JsonConvert.SerializeObject(new { UserName = globalAccountNumber });
            logger.LogInformation($"{nameof(LoginAsync)} using request body {request}");
            ////logWriter.Write($"{nameof(LoginAsync)} using request body {request}");
            using var content = new StringContent(request, Encoding.UTF8, "application/json");
            logger.LogInformation($"{nameof(LoginAsync)} string content created");
            ////logWriter.Write($"{nameof(LoginAsync)} string content created");
            using var response = await client.PostAsync(requestUri, content, token).ConfigureAwait(false);
            logger.LogInformation($"{nameof(LoginAsync)} response awaited");
            ////logWriter.Write($"{nameof(LoginAsync)} response awaited");
            var responseString = await response.Content.ReadAsStringAsync(token).ConfigureAwait(false);
            logger.LogInformation($"{nameof(LoginAsync)}\r\n{responseString}");
            ////logWriter.Write($"{nameof(LoginAsync)}\r\n{responseString}");
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(responseString);
            }
            logger.LogInformation($"{nameof(LoginAsync)} {nameof(response.EnsureSuccessStatusCode)} didn't throw, setting cookies");
            ////logWriter.Write($"{nameof(LoginAsync)} {nameof(response.EnsureSuccessStatusCode)} didn't throw, setting cookies");
            var authResponse = JsonConvert.DeserializeObject<ServiceStackAuthResponse>(responseString);
            pageModel.SetCookies(cookies, requestUri, cookieDomain, logger, authResponse);
            logger.LogInformation($"{nameof(LoginAsync)} cookies set");
            ////logWriter.Write($"{nameof(LoginAsync)} cookies set");
        }

        private static void SetCookies<TCategoryName>(
            this PageModel pageModel,
            CookieContainer cookies,
            Uri requestUri,
            string cookieDomain,
            ILogger<TCategoryName> logger,
            ServiceStackAuthResponse? authResponse = null)
        {
            logger.LogInformation($"{nameof(SetCookies)} entered");
            foreach (var cookie in cookies.GetCookies(requestUri).Cast<Cookie>())
            {
                if (cookie == null)
                {
                    throw new NullReferenceException("Casted Cookie was null");
                }
                if (string.IsNullOrWhiteSpace(cookie.Name))
                {
                    throw new NullReferenceException($"Cookie Name was null or whitespace: '{cookie.Name}'");
                }
                if (string.IsNullOrWhiteSpace(cookie.Value))
                {
                    throw new NullReferenceException($"Cookie Value was null or whitespace: '{cookie.Value}'");
                }
                logger.LogInformation($"{nameof(SetCookies)} setting cookie '{cookie.Name}' value '{cookie.Value}' in domain '{cookieDomain}'");
                pageModel.Response.Cookies.Append(
                    cookie.Name.Replace(" ", "%20"),
                    cookie.Value,
                    new()
                    {
                        Domain = cookieDomain,
                    });
            }
            if (authResponse != null)
            {
                logger.LogInformation($"{nameof(SetCookies)} setting cookie 'ss-pid' value '{authResponse.SessionId}' in domain '{cookieDomain}'");
                if (string.IsNullOrWhiteSpace(authResponse.SessionId))
                {
                    throw new NullReferenceException($"Auth Response Session Id was null or whitespace: '{authResponse.SessionId}'");
                }
                cookies.Add(new Cookie("ss-pid", authResponse.SessionId, "/", cookieDomain));
                pageModel.Response.Cookies.Append(
                    "ss-pid",
                    authResponse.SessionId,
                    new()
                    {
                        Domain = cookieDomain,
                    });
            }
            logger.LogInformation($"{nameof(SetCookies)} exiting");
        }

        /// <summary>A service stack authentication response.</summary>
        internal class ServiceStackAuthResponse
        {
            /// <summary>Gets or sets the identifier of the user.</summary>
            /// <value>The identifier of the user.</value>
            // ReSharper disable once UnusedMember.Local
            public string? UserId { get; set; } // CEF Record ID

            /// <summary>Gets or sets the identifier of the session.</summary>
            /// <value>The identifier of the session.</value>
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public string? SessionId { get; set; } // ss-pid

            /// <summary>Gets or sets the name of the user.</summary>
            /// <value>The name of the user.</value>
            // ReSharper disable once UnusedMember.Local
            public string? UserName { get; set; } // GAN
        }
    }
}
