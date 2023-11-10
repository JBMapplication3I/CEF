// <copyright file="BasicApiProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the basic API provider class</summary>
namespace Clarity.Ecommerce.Providers.Api.Basic
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Owin.Infrastructure;
    using Models;
    using Newtonsoft.Json;

    /// <inheritdoc/>
    public class BasicApiProvider : ApiProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => BasicApiProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<string?>> PostAsync(
            string url,
            string verb,
            Dictionary<string, string> customHeaders,
            Dictionary<string, string> parameters,
            int? timeout,
            string body,
            string contentType,
            string? contextProfileName)
        {
            using var client = new HttpClient();
            if (customHeaders?.Count > 0)
            {
                // Add all our custom headers to request
                foreach (var header in customHeaders)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            client.Timeout = timeout is null or 0
                ? TimeSpan.FromSeconds(3)
                : TimeSpan.FromSeconds(timeout.Value);
            if (parameters?.Count > 0)
            {
                url = WebUtilities.AddQueryString(url, parameters);
            }
            var guid = Guid.NewGuid().ToString()[..6];
            try
            {
                string responseString;
                switch (verb.ToLower())
                {
                    case "get":
                    {
                        await Logger.LogInformationAsync(
                                name: $"{nameof(BasicApiProvider)}.${nameof(PostAsync)}.SendRequest.GET.{guid}",
                                message: JsonConvert.SerializeObject(new
                                {
                                    url,
                                    verb,
                                    body,
                                    timeout,
                                    customHeaders,
                                    parameters,
                                    contentType,
                                }),
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        responseString = await client.GetStringAsync(url).ConfigureAwait(false);
                        await Logger.LogInformationAsync(
                                name: $"{nameof(BasicApiProvider)}.${nameof(PostAsync)}.Reply.GET.{guid}",
                                message: responseString,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        break;
                    }
                    case "post":
                    {
                        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
                        HttpContent? httpContent = null;
                        if (!string.IsNullOrWhiteSpace(body))
                        {
                            httpContent = new StringContent(body, Encoding.UTF8);
                            httpContent.Headers.ContentType = string.IsNullOrWhiteSpace(contentType)
                                ? new("application/json")
                                : new MediaTypeHeaderValue(contentType);
                            httpRequest.Content = httpContent;
                        }
                        await Logger.LogInformationAsync(
                                name: $"{nameof(BasicApiProvider)}.${nameof(PostAsync)}.SendRequest.POST.{guid}",
                                message: JsonConvert.SerializeObject(new
                                {
                                    url,
                                    verb,
                                    body,
                                    timeout,
                                    customHeaders,
                                    parameters,
                                    contentType,
                                }),
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        using var response = await client
                            .SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead)
                            .ConfigureAwait(false);
                        response.EnsureSuccessStatusCode();
                        responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        await Logger.LogInformationAsync(
                                name: $"{nameof(BasicApiProvider)}.${nameof(PostAsync)}.Reply.POST.{guid}",
                                message: responseString,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        httpContent?.Dispose();
                        break;
                    }
                    default:
                    {
                        await Logger.LogErrorAsync(
                                name: $"{nameof(BasicApiProvider)}.${nameof(PostAsync)}.MethodNotSupported",
                                message: "Verb type was: " + verb,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        return CEFAR.FailingCEFAR<string?>("Method not supported");
                    }
                }
                ////// JsonConvert can't deserialize primitives
                ////var responseObject = //typeof(string) == typeof(string)
                ////    /*?*/ responseString//as string
                ////    // : JsonConvert.DeserializeObject<string>(responseString)
                ////    ;
                var responseObject = responseString;
                return responseObject.WrapInPassingCEFAR();
            }
            catch (ArgumentNullException ex1)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(BasicApiProvider)}.${nameof(PostAsync)}.{ex1.GetType().Name}.{guid}",
                        message: ex1.Message,
                        ex: ex1,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR<string?>(ex1.Message);
            }
            catch (InvalidOperationException ex2)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(BasicApiProvider)}.${nameof(PostAsync)}.{ex2.GetType().Name}.{guid}",
                        message: ex2.Message,
                        ex: ex2,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR<string?>(ex2.Message);
            }
            catch (HttpRequestException ex3)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(BasicApiProvider)}.${nameof(PostAsync)}.{ex3.GetType().Name}.{guid}",
                        message: ex3.Message,
                        ex: ex3,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR<string?>(ex3.Message);
            }
            catch (TaskCanceledException ex4)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(BasicApiProvider)}.${nameof(PostAsync)}.{ex4.GetType().Name}.{guid}",
                        message: ex4.Message,
                        ex: ex4,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR<string?>(ex4.Message);
            }
            catch (Exception ex5)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(BasicApiProvider)}.${nameof(PostAsync)}.{ex5.GetType().Name}.{guid}",
                        message: ex5.Message,
                        ex: ex5,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR<string?>(ex5.Message);
            }
        }
    }
}
