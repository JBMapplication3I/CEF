// <copyright file="Service.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the SignalRCore Service class</summary>
namespace Clarity.Ecommerce.SignalRCore
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>A Service.</summary>
    public class Service : IService
    {
        private readonly HttpClient httpClient;

        /// <summary>Initializes a new instance of the <see cref="Service"/> class.</summary>
        /// <param name="httpClientFactory">The Http Client factory.</param>
        public Service(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient(nameof(Service));
            JsonConvert.DefaultSettings = () => new()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                ////NullValueHandling = NullValueHandling.Ignore
            };
        }

        /// <summary>Sends a POST request to the specified endpoint.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="endpoint">     The endpoint.</param>
        /// <param name="urlParameters">The search model.</param>
        /// <returns>The object specified in the generic tag.</returns>
        public async Task<TModel?> GetAsync<TModel>(string endpoint, Dictionary<string, object> urlParameters)
        {
            var requestUri = $"{httpClient.BaseAddress}/{endpoint}{urlParameters.AsQueryString()}";
            using var response = await httpClient.GetAsync(requestUri).ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            try
            {
                return JsonConvert.DeserializeObject<TModel>(responseString);
            }
            catch (Exception e)
            {
                var error = $"Message: {e.Message}. InnerException: {e.InnerException?.Message}. ResponseString: {responseString}";
                throw new(error);
            }
        }

        /// <summary>Sends a POST request to the specified endpoint.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="model">   The model.</param>
        /// <returns>The object specified in the generic tag.</returns>
        public async Task<TModel?> PostAsync<TModel>(string endpoint, TModel model)
        {
            var requestUri = $"{httpClient.BaseAddress}/{endpoint}";
            var search = JsonConvert.SerializeObject(model);
            using var content = new StringContent(search, Encoding.UTF8, "application/json");
            using var response = await httpClient.PostAsync(requestUri, content).ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            try
            {
                return JsonConvert.DeserializeObject<TModel>(responseString);
            }
            catch (Exception e)
            {
                var error = $"Message: {e.Message}. InnerException: {e.InnerException?.Message}. ResponseString: {responseString}";
                throw new(error);
            }
        }

        /////// <summary>Sends a GET request to the specified endpoint.</summary>
        /////// <param name="endpoint">The endpoint.</param>
        /////// <returns>The object specified in the generic tag.</returns>
        ////public async Task<TModel> GetAsync<TModel>(string endpoint)
        ////{
        ////    var requestUri = $"{httpClient.BaseAddress}/{endpoint}";
        ////    using var response = await httpClient.GetAsync(requestUri).ConfigureAwait(false);
        ////    var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        ////    try
        ////    {
        ////        return JsonConvert.DeserializeObject<TModel>(responseString);
        ////    }
        ////    catch (Exception e)
        ////    {
        ////        var error = $"Message: {e.Message}. InnerException: {e.InnerException?.Message}. ResponseString: {responseString}";
        ////        throw new Exception(error);
        ////    }
        ////}

        /////// <summary>Sends a POST request to the specified endpoint.</summary>
        /////// <param name="search">The search model.</param>
        /////// <param name="endpoint">The endpoint.</param>
        /////// <returns>The object specified in the generic tag.</returns>
        ////public async Task<List<TModel>> PostAsync<TModel>(string endpoint, BaseSearchModel search)
        ////{
        ////    var requestUri = $"{httpClient.BaseAddress}/{endpoint}";
        ////    var model = JsonConvert.SerializeObject(search);
        ////    using var content = new StringContent(model, Encoding.UTF8, "application/json");
        ////    using var response = await httpClient.PostAsync(requestUri, content).ConfigureAwait(false);
        ////    var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        ////    try
        ////    {
        ////        var pagedResults = JsonConvert.DeserializeObject<PagedResultsBase<TModel>>(responseString);
        ////        return pagedResults.Results;
        ////    }
        ////    catch (Exception e)
        ////    {
        ////        var error = $"Message: {e.Message}. InnerException: {e.InnerException?.Message}. ResponseString: {responseString}";
        ////        throw new Exception(error);
        ////    }
        ////}
    }
}
