// <copyright file="CEFService.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cef service class</summary>
namespace Clarity.Ecommerce.MVC.Api.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Interop;
    using Microsoft.JSInterop;
    using Options;
    using ServiceStack;
    using ServiceStack.Blazor;
    using ServiceStack.Text;
    using Utilities;

    public class CEFService
    {
        public const string SessionCookieName = "ss-pid";
        public const string SessionHeaderName = "X-ss-pid";
        public const string SessionCookieOptName = "ss-opt";
        public const string SessionHeaderOptName = "X-ss-opt";
        public const string SessionOptPerm = "perm";

        /// <summary>Initializes a new instance of the <see cref="CEFService" /> class.</summary>
        public CEFService(APIOptions apiOptions)
        {
            if (Client is not null)
            {
                return;
            }
            try
            {
                Client = BlazorClient.Create(apiOptions.BaseAddress ?? "/");
                JsConfig.DateHandler = DateHandler.ISO8601;
                Client.UseCookies = false;
                Client.AddHeader(HttpHeaders.AcceptEncoding, "br, gzip, deflate");
                if (Client.HttpMessageHandler is null)
                {
                    var x = new EnableCorsMessageHandler();
                    x.InnerHandler ??= new HttpClientHandler();
                    Client.HttpMessageHandler = x;
                }
                if (Client.HttpMessageHandler is EnableCorsMessageHandler { InnerHandler: null } h)
                {
                    h.InnerHandler = new HttpClientHandler();
                }
                // "https://local.clarityclient.com/DesktopModules/ClarityEcommerce/API-BrandAdmin");
                //// Client = new JsonServiceClient(
                ////     "http://local.clarityclient.com/DesktopModules/ClarityEcommerce/API-Storefront")
                //// {
                ////     AlwaysSendBasicAuthHeader = true,
                ////     EmulateHttpViaPost = true,
                ////     Timeout = TimeSpan.FromMinutes(5),
                ////     StoreCookies = true,
                //// };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting up the ServiceStack Communication Client {ex.Message}");
                throw;
            }
        }

        /// <summary>Gets the client.</summary>
        /// <value>The client.</value>
        public JsonHttpClient Client { get; }

        /// <summary>Is authenticated.</summary>
        /// <param name="jsRuntime">The js runtime.</param>
        /// <param name="retry">    The retry.</param>
        /// <returns>A Task{bool}.</returns>
        public async Task<bool> IsAuthenticatedAsync(IJSRuntime jsRuntime, int retry = 0)
        {
            if (Client.Headers is not null
                && Client.Headers.AllKeys.Contains(HttpHeaders.XUserAuthId))
            {
                return int.TryParse(Client.Headers[HttpHeaders.XUserAuthId], out var userAuthID)
                    && Contract.CheckValidID(userAuthID);
            }
            if (retry > 1)
            {
                await WriteTempCookiesAsync(jsRuntime).ConfigureAwait(false);
                return false;
            }
            // We need to read the data and try again
            await ReadCookiesAsync(jsRuntime).ConfigureAwait(false);
            return await IsAuthenticatedAsync(jsRuntime, retry + 1).ConfigureAwait(false);
        }

        /// <summary>Reads cookies.</summary>
        /// <param name="jsRuntime">     The js runtime.</param>
        /// <param name="writeToClient"> True to write to client.</param>
        /// <returns>The cookies.</returns>
        // ReSharper disable once CognitiveComplexity, CyclomaticComplexity
        private async Task<IReadOnlyDictionary<string, string>> ReadCookiesAsync(
            IJSRuntime jsRuntime,
            bool writeToClient = true)
        {
            // Try to read the current cookies in to the client
            var collection = await jsRuntime.ReadBrowserCookiesAsync().ConfigureAwait(false);
            var retVal = new Dictionary<string, string>();
            var sessionWritten = false;
            foreach (Cookie cookie in collection)
            {
                if (!writeToClient)
                {
                    retVal[cookie.Name] = cookie.Value;
                    continue;
                }
                if (sessionWritten
                    || cookie.Name is not (SessionCookieName or HttpHeaders.XUserAuthId))
                {
                    continue;
                }
                sessionWritten = true;
                var userAuthId = collection.SingleOrDefault(x => x.Name == HttpHeaders.XUserAuthId);
                if (Client.Headers?.AllKeys.Contains(HttpHeaders.XUserAuthId) == true
                    && (userAuthId?.Value ?? "") == Client.Headers.Get(HttpHeaders.XUserAuthId))
                {
                    // Not Writing Header to client from ReadCookies (identical)
                }
                else
                {
                    Client.Headers?.Set(
                        HttpHeaders.XUserAuthId,
                        userAuthId?.Value ?? "");
                }
                var pid = collection.SingleOrDefault(x => x.Name == SessionCookieName);
                if (Client.Headers?.AllKeys.Contains(SessionHeaderName) == true
                    && (pid?.Value ?? "") == Client.Headers.Get(SessionHeaderName))
                {
                    // Not Writing Header to client from ReadCookies (identical)
                }
                else
                {
                    Client.Headers?.Set(
                        SessionHeaderName,
                        pid?.Value);
                }
                if (Client.Headers?.AllKeys.Contains(SessionHeaderOptName) == true
                    && SessionOptPerm == Client.Headers.Get(SessionHeaderOptName))
                {
                    // Not Writing Header to client from ReadCookies (identical)
                }
                else
                {
                    Client.Headers?.Set(
                        SessionHeaderOptName,
                        SessionOptPerm);
                }
            }
            return writeToClient ? Client.GetCookieValues() : retVal;
        }

        /// <summary>Writes the cookies.</summary>
        /// <param name="jsRuntime">     The js runtime.</param>
        /// <param name="sourceFilePath">Full pathname of the source file.</param>
        /// <param name="memberName">    Name of the member.</param>
        /// <returns>A Task.</returns>
        public async Task WriteCookiesAsync(
            IJSRuntime jsRuntime,
            [CallerFilePath] string sourceFilePath = "",
            [CallerMemberName] string? memberName = "")
        {
            var debugPrefix = sourceFilePath + "." + memberName + "->CEFService.WriteCookiesAsync: ";
            var browserCookies = await ReadCookiesAsync(jsRuntime, false).ConfigureAwait(false);
            var clientCookies = Client.GetCookieValues();
            foreach (var (key, value) in clientCookies)
            {
                if (browserCookies.ContainsKey(key) && browserCookies[key] == value)
                {
                    // Not Writing cookie to browser from WriteCookies (identical)
                    continue;
                }
                await jsRuntime.WriteCookieToBrowserAsync(
                        key,
                        value)
                    .ConfigureAwait(false);
            }
            if (Client.Headers?.AllKeys.Contains(SessionHeaderName) == true)
            {
                await jsRuntime.WriteCookieToBrowserAsync(
                        HttpHeaders.XUserAuthId,
                        Client.Headers.Get(HttpHeaders.XUserAuthId))
                    .ConfigureAwait(false);
                await jsRuntime.WriteCookieToBrowserAsync(
                        SessionCookieName,
                        Client.Headers.Get(SessionHeaderName))
                    .ConfigureAwait(false);
                await jsRuntime.WriteCookieToBrowserAsync(
                        SessionCookieOptName,
                        SessionOptPerm)
                    .ConfigureAwait(false);
            }
            else
            {
                // TODO
                Console.WriteLine(debugPrefix + "No session header name");
            }
        }

        /// <summary>Writes temporary cookies so it can have them as placeholders to use until a login occurs and not
        /// infinite loop the <see cref="ReadCookiesAsync"/> function.</summary>
        /// <param name="jsRuntime">The js runtime.</param>
        /// <returns>A Task.</returns>
        private async Task WriteTempCookiesAsync(IJSRuntime jsRuntime)
        {
            await jsRuntime.WriteCookieToBrowserAsync(HttpHeaders.XUserAuthId, string.Empty).ConfigureAwait(false);
            await jsRuntime.WriteCookieToBrowserAsync(SessionCookieName, string.Empty).ConfigureAwait(false);
            await jsRuntime.WriteCookieToBrowserAsync(SessionCookieOptName, SessionOptPerm).ConfigureAwait(false);
            Client.Headers?.Set(HttpHeaders.XUserAuthId, string.Empty);
            Client.Headers?.Set(SessionHeaderName, string.Empty);
            Client.Headers?.Set(SessionHeaderOptName, SessionOptPerm);
        }

        /// <summary>Makes a request to the ServiceStack host and returns the result.</summary>
        /// <typeparam name="TDto">   Type of the data transfer object.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="dto">      The data transfer object.</param>
        /// <param name="stopRetry">True to stop retry.</param>
        /// <returns>A TResult?</returns>
        public TResult? Request<TDto, TResult>(TDto dto, bool stopRetry = false)
            where TDto : IReturn<TResult>
        {
            var route = dto.GetType().GetCustomAttribute<RouteAttribute>(true);
            if (route is null)
            {
                return default;
            }
            var method = route.Verbs;
            try
            {
                return method switch
                {
                    HttpMethods.Post => Client.Post(dto),
                    HttpMethods.Delete => Client.Delete(dto),
                    HttpMethods.Patch => Client.Patch(dto),
                    HttpMethods.Put => Client.Put(dto),
                    HttpMethods.Get => Client.Get(dto),
                    _ => Client.Get(dto),
                };
            }
            catch (WebServiceException wex)
            {
                if (stopRetry) { throw; }
                if (!wex.IsAny400()
                    || !wex.Message.Contains(
                        "An error occurred while reading from the store provider's data reader. See the inner exception for details."))
                {
                    throw;
                }
                // Retry, was a deadlock error
                return Request<TDto, TResult>(dto, true);
            }
        }

        /// <summary>Makes a request to the ServiceStack host and returns the result.</summary>
        /// <typeparam name="TDto">   Type of the data transfer object.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="dto">      The data transfer object.</param>
        /// <param name="stopRetry">True to stop retry.</param>
        /// <returns>A Task{bool}</returns>
        public async Task<TResult?> RequestAsync<TDto, TResult>(TDto dto, bool stopRetry = false)
            where TDto : IReturn<TResult>
        {
            var route = dto.GetType().GetCustomAttribute<RouteAttribute>(true);
            if (route is null)
            {
                return default;
            }
            var method = route.Verbs;
            try
            {
                return method switch
                {
                    HttpMethods.Post => await Client.PostAsync(dto).ConfigureAwait(false),
                    HttpMethods.Delete => await Client.DeleteAsync(dto).ConfigureAwait(false),
                    HttpMethods.Patch => await Client.PatchAsync(dto).ConfigureAwait(false),
                    HttpMethods.Put => await Client.PutAsync(dto).ConfigureAwait(false),
                    HttpMethods.Get => await Client.GetAsync(dto).ConfigureAwait(false),
                    _ => await Client.GetAsync(dto).ConfigureAwait(false),
                };
            }
            catch (WebServiceException wex)
            {
                if (stopRetry) { throw; }
                if (!wex.IsAny400()
                    || !wex.Message.Contains(
                        "An error occurred while reading from the store provider's data reader. See the inner exception for details."))
                {
                    throw;
                }
                // Retry, was a deadlock error
                return await RequestAsync<TDto, TResult>(dto, true).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return default;
            }
        }

        /// <summary>Makes a request to the ServiceStack host which does not return a result.</summary>
        /// <typeparam name="TDto">Type of the data transfer object.</typeparam>
        /// <param name="dto">      The data transfer object.</param>
        /// <param name="stopRetry">True to stop retry.</param>
        /// <returns>A Task.</returns>
        public async Task RequestAsync<TDto>(TDto dto, bool stopRetry = false)
            where TDto : IReturnVoid
        {
            var route = dto.GetType().GetCustomAttribute<RouteAttribute>(true);
            if (route is null)
            {
                return;
            }
            var method = route.Verbs;
            try
            {
                switch (method)
                {
                    // ReSharper disable StyleCop.SA1107
                    case HttpMethods.Post:
                    {
                        await Client.PostAsync(dto).ConfigureAwait(false);
                        return;
                    }
                    case HttpMethods.Delete:
                    {
                        await Client.DeleteAsync(dto).ConfigureAwait(false);
                        return;
                    }
                    case HttpMethods.Patch:
                    {
                        await Client.PatchAsync(dto).ConfigureAwait(false);
                        return;
                    }
                    case HttpMethods.Put:
                    {
                        await Client.PutAsync(dto).ConfigureAwait(false);
                        return;
                    }
                    // ReSharper disable once RedundantCaseLabel
                    case HttpMethods.Get:
                    default:
                    {
                        await Client.GetAsync(dto).ConfigureAwait(false);
                        return;
                    }
                    // ReSharper restore StyleCop.SA1107
                }
            }
            catch (WebServiceException wex)
            {
                if (stopRetry)
                {
                    throw;
                }
                if (wex.StatusCode != 400
                    || !wex.Message.Contains(
                        "An error occurred while reading from the store provider's data reader. See the inner exception for details."))
                {
                    throw;
                }
                // Retry, was a deadlock error
                await RequestAsync(dto, true).ConfigureAwait(false);
            }
        }
    }
}
