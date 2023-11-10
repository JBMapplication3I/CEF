// <copyright file="Logout.cshtml.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the logout.cshtml class</summary>
#pragma warning disable SA1649 // File name should match first type name
namespace Clarity.Ecommerce.UI.MVCHost.Pages
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    /// <summary>A data Model for the logout.</summary>
    /// <seealso cref="PageModel"/>
    public class LogoutModel : PageModel
    {
        private readonly string apiUri;
        // ReSharper disable once NotAccessedField.Local
        private readonly string logoutUrl;
        private readonly ILogger<LogoutModel> logger;

        /// <summary>Initializes a new instance of the <see cref="LogoutModel"/> class.</summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="logger">       The logger.</param>
        public LogoutModel(IConfiguration configuration, ILogger<LogoutModel> logger)
        {
            apiUri = configuration.GetValue<string>("ApiUri");
            logoutUrl = configuration.GetValue<string>("LogoutUrl");
            this.logger = logger;
        }

        /// <summary>Executes the get action.</summary>
        /// <returns>A Task{IActionResult}.</returns>
        public async Task<IActionResult> OnGetAsync()
        {
            using var tokenSource = new CancellationTokenSource();
            try
            {
                using var client = new HttpClient { Timeout = new(0, 5, 0) };
                var requestUri = new Uri($"{apiUri}/auth/logout");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new("application/json"));
                using var response = await client.GetAsync(requestUri, tokenSource.Token).ConfigureAwait(false);
                logger.LogInformation($"{nameof(LogoutModel)}.{nameof(OnGetAsync)} response awaited");
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                logger.LogInformation($"{nameof(LogoutModel)}.{nameof(OnGetAsync)}\r\n{responseString}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(responseString);
                }
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).ConfigureAwait(false);
                // if (false /* if OIDC was turned on */)
                // {
                //     await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme).ConfigureAwait(false);
                // }
                return new OkResult();
            }
            catch
            {
                tokenSource.Cancel();
                throw;
            }
        }
    }
}
