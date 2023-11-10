// <copyright file="BasicApiService.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the basic API service class</summary>
#pragma warning disable CA1822
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Api.Basic
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>Creates a request on behalf of a client.</summary>
    [PublicAPI, Authenticate,
     Route("/Providers/Api/Basic/Post", "POST", Summary = "Creates a request on behalf of a client.")]
    public class ApiPostRequestModel : IReturn<CEFActionResponse<string>>
    {
        /// <summary>Gets or sets the url.</summary>
        /// <value>The URL.</value>
        [ApiMember(Name = nameof(Url), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string Url { get; set; } = null!;

        /// <summary>Gets or sets the url.</summary>
        /// <value>The verb.</value>
        [ApiMember(Name = nameof(Verb), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string Verb { get; set; } = null!;

        /// <summary>Gets or sets the custom headers.</summary>
        /// <value>The custom headers.</value>
        [ApiMember(Name = nameof(CustomHeaders), DataType = "Dictionary<string, string>", ParameterType = "body", IsRequired = false)]
        public Dictionary<string, string> CustomHeaders { get; set; } = null!;

        /// <summary>Gets or sets the parameters.</summary>
        /// <value>The parameters.</value>
        [ApiMember(Name = nameof(Parameters), DataType = "Dictionary<string, string>", ParameterType = "body", IsRequired = false)]
        public Dictionary<string, string> Parameters { get; set; } = null!;

        /// <summary>Gets or sets the timeout.</summary>
        /// <value>The timeout.</value>
        [ApiMember(Name = nameof(Timeout), DataType = "int?", ParameterType = "body", IsRequired = false)]
        public int Timeout { get; set; }

        /// <summary>Gets or sets the body.</summary>
        /// <value>The body.</value>
        [ApiMember(Name = nameof(Body), DataType = "string", ParameterType = "body", IsRequired = false)]
        public string Body { get; set; } = null!;

        /// <summary>Gets or sets the content-type.</summary>
        /// <value>The type of the content.</value>
        [ApiMember(Name = nameof(ContentType), DataType = "string", ParameterType = "body", IsRequired = false)]
        public string ContentType { get; set; } = null!;
    }

    /// <summary>An excel import file as sales quote service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    [PublicAPI]
    public class BasicApiService : ClarityEcommerceServiceBase
    {
        /// <summary>React to this endpoint when accessed with a POST request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="CEFActionResponse"/>.</returns>
        public async Task<object> Post(ApiPostRequestModel request)
        {
            return await new BasicApiProvider().PostAsync(
                    url: request.Url,
                    verb: request.Verb,
                    customHeaders: request.CustomHeaders,
                    parameters: request.Parameters,
                    timeout: request.Timeout,
                    body: request.Body,
                    contentType: request.ContentType,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }
    }

    /// <inheritdoc/>
    [PublicAPI]
    public class BasicApiFeature : IPlugin
    {
        /// <summary>Registers this object.</summary>
        /// <param name="appHost">The application host.</param>
        public void Register(IAppHost appHost)
        {
            // This function intentionally left blank
        }
    }
}
