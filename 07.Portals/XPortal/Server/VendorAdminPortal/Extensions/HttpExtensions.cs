namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using Clarity.Ecommerce.MVC.Api.Options;
    using Clarity.Ecommerce.MVC.Utilities;
    using Options;

    /// <summary>A HTTP extensions.</summary>
    public static class HttpExtensions
    {
        /// <summary>An IServiceCollection extension method that adds a HTTP clients.</summary>
        /// <param name="services">The services to act on.</param>
        /// <returns>An IServiceCollection.</returns>
        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            services
                .AddHttpClient(
                    "APIService",
                    (sp, httpClient) =>
                    {
                        var options = sp.GetRequiredService<IOptions<APIOptions>>();
                        if (Contract.CheckValidKey(options.Value.BaseAddress))
                        {
                            httpClient.BaseAddress = new(options.Value.BaseAddress!);
                        }
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));
                        httpClient.DefaultRequestHeaders.Add("Cookie", $"cef_cart_shopping={Guid.NewGuid()};cef_cart_compare={Guid.NewGuid()}");
                        httpClient.Timeout = TimeSpan.FromHours(1);
                    })
                .ConfigurePrimaryHttpMessageHandler(messageHandler =>
                {
                    var handler = new HttpClientHandler { UseCookies = false };
                    // handler.CookieContainer = new CookieContainer { };
                    // handler.CookieContainer.Add(new Cookie("cef_cart_shopping", Guid.NewGuid().ToString(), "/", options.Value.Domain));
                    // handler.CookieContainer.Add(new Cookie("cef_cart_compare", Guid.NewGuid().ToString(), "/", options.Value.Domain));
                    // if (handler.SupportsAutomaticDecompression)
                    // {
                    //     handler.AutomaticDecompression = DecompressionMethods.Deflate;
                    // }
                    return handler;
                })
                .SetHandlerLifetime(Timeout.InfiniteTimeSpan);
            return services;
        }
    }
}
