// <copyright file="BasicApiProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the basic API provider tests class</summary>
namespace Clarity.Ecommerce.Providers.Api.Testing
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Basic;
    using Xunit;

    [Trait("Category", "Providers.Api.Basic")]
    public class BasicApiProviderTests
    {
        private const string ExpectedPost =
            "{\"args\":{\"hand\":\"wave\"},\"data\":\"This is expected to be sent back as part of response body.\",\"files\":{},\"form\":{},\"headers\":{\"x-forwarded-proto\":\"https\",\"host\":\"postman-echo.com\",\"content-length\":\"58\",\"accept\":\"application/json\",\"content-type\":\"text/plain\",\"x-forwarded-port\":\"443\"},\"json\":null,\"url\":\"https://postman-echo.com/post?hand=wave\"}";

        private const string ExpectedGet = "{\"args\":{\"hand\":\"wave\"},\"headers\":{\"x-forwarded-proto\":\"https\",\"host\":\"postman-echo.com\",\"accept\":\"application/json\",\"x-forwarded-port\":\"443\"},\"url\":\"https://postman-echo.com/get?hand=wave\"}";

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_Get_CompletesWithoutError()
        {
            var provider = new BasicApiProvider();
            var parameters = new Dictionary<string, string> { { "hand", "wave" } };
            var customHeaders = new Dictionary<string, string> { { "Accept", "application/json" } };
            var response = await provider.PostAsync(
                    url: "https://postman-echo.com/get",
                    verb: "GET",
                    customHeaders: customHeaders,
                    parameters: parameters,
                    timeout: 3,
                    body: null,
                    contentType: null,
                    contextProfileName: null)
                .ConfigureAwait(false);
            Assert.True(response.ActionSucceeded);
            Assert.Equal(ExpectedGet, response.Result);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_Post_CompletesWithoutError()
        {
            var provider = new BasicApiProvider();
            var parameters = new Dictionary<string, string> { { "hand", "wave" } };
            var customHeaders = new Dictionary<string, string> { { "Accept", "application/json" } };
            var response = await provider.PostAsync(
                    url: "https://postman-echo.com/post",
                    verb: "POST",
                    customHeaders: customHeaders,
                    parameters: parameters,
                    timeout: 3,
                    body: "This is expected to be sent back as part of response body.",
                    contentType: "text/plain",
                    contextProfileName: null)
                .ConfigureAwait(false);
            Assert.True(response.ActionSucceeded);
            Assert.Equal(ExpectedPost, response.Result);
        }

        [Fact]
        public async Task Verify_AnUnsupportedVerb_Returns_AFailingCEFAR()
        {
            var provider = new BasicApiProvider();
            var parameters = new Dictionary<string, string> { { "hand", "wave" } };
            var customHeaders = new Dictionary<string, string> { { "Accept", "application/json" } };
            var response = await provider.PostAsync(
                    url: "https://postman-echo.com/post",
                    verb: "FAIL",
                    customHeaders: customHeaders,
                    parameters: parameters,
                    timeout: 3,
                    body: "This is expected to be sent back as part of response body.",
                    contentType: "text/plain",
                    contextProfileName: null)
                .ConfigureAwait(false);
            Assert.False(response.ActionSucceeded);
            Assert.Single(response.Messages);
            Assert.Equal("Method not supported", response.Messages.Single());
        }
    }
}
