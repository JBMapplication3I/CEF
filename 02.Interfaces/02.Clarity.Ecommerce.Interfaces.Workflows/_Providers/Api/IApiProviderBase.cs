// <copyright file="IApiProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IApiProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;

    /// <inheritdoc/>
    public interface IApiProviderBase : IProviderBase
    {
        /// <summary>Sends an http post request.</summary>
        /// <param name="url">               URL of the document.</param>
        /// <param name="verb">              The verb.</param>
        /// <param name="customHeaders">     The custom headers.</param>
        /// <param name="parameters">        Options for controlling the operation.</param>
        /// <param name="timeout">           The timeout.</param>
        /// <param name="body">              The body.</param>
        /// <param name="contentType">       Type of the content.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse{string}}.</returns>
        Task<CEFActionResponse<string?>> PostAsync(
            string url,
            string verb,
            Dictionary<string, string> customHeaders,
            Dictionary<string, string> parameters,
            int? timeout,
            string body,
            string contentType,
            string? contextProfileName);
    }
}
