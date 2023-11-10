// <copyright file="Index.cshtml.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the index.cshtml class</summary>
#pragma warning disable SA1649 // File name should match first type name
namespace Clarity.Ecommerce.UI.MVCHost.Pages
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Extensions;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    /// <summary>A data Model for the index.</summary>
    /// <seealso cref="PageModel"/>
    // [Microsoft.AspNetCore.Authorization.Authorize]
    public class IndexModel : PageModel
    {
        private readonly string apiUri;
        private readonly string cookieDomain;
        private readonly string logoutUrl;
        private readonly string[] requireAllRoles = { "CEF Global Administrator" };
        private readonly ILogger<IndexModel> logger;

        /// <summary>Initializes a new instance of the <see cref="IndexModel"/> class.</summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="logger">       The logger.</param>
        public IndexModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            this.logger = logger;
            apiUri = configuration.GetValue<string>("ApiUri");
            cookieDomain = configuration.GetValue<string>("CookieDomain");
            logoutUrl = configuration.GetValue<string>("LogoutUrl");
            this.logger.LogInformation($"{nameof(apiUri)}: {apiUri}");
            this.logger.LogInformation($"{nameof(cookieDomain)}: {cookieDomain}");
            this.logger.LogInformation($"{nameof(logoutUrl)}: {logoutUrl}");
        }

        /// <summary>SPA action.</summary>
        /// <returns>A Task{IActionResult}.</returns>
        public async Task<IActionResult> SpaAsync()
        {
            logger.LogInformation($"{nameof(SpaAsync)} entered");
            using var tokenSource = new CancellationTokenSource();
            logger.LogInformation($"{nameof(SpaAsync)} using tokenSource");
            try
            {
                logger.LogInformation($"{nameof(SpaAsync)} checking isAuthorized using SetupForSSO");
                var isAuthorized = await this.SetupForSSOAsync(
                        apiUri,
                        cookieDomain,
                        requireAllRoles,
                        logger,
                        tokenSource.Token)
                    .ConfigureAwait(false);
                if (!isAuthorized)
                {
                    logger.LogWarning($"{nameof(SpaAsync)} not authorized, logging out and kicking");
                    // This portal requires authorization with specific role(s), failing this check will trigger
                    // a log out and redirect
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).ConfigureAwait(false);
                    // if (false /* if OIDC was turned on */)
                    // {
                    //     await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme).ConfigureAwait(false);
                    // }
                    return Redirect(logoutUrl);
                }
                logger.LogInformation($"{nameof(SpaAsync)} is authorized, responding with index.html");
                return File("~/index.html", "text/html");
                ////return Page();
            }
            catch (Exception ex)
            {
                logger.LogCritical($"{nameof(SpaAsync)} had an exception, cancelling token source and rethrowing {ex.GetType().FullName} {ex.Message}");
                tokenSource.Cancel();
                throw;
            }
        }

        /// <summary>Executes the get action.</summary>
        /// <returns>A Task{IActionResult}.</returns>
        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogInformation($"{nameof(OnGetAsync)} entered");
            using var tokenSource = new CancellationTokenSource();
            logger.LogInformation($"{nameof(OnGetAsync)} using tokenSource");
            try
            {
                logger.LogInformation($"{nameof(OnGetAsync)} checking isAuthorized using SetupForSSO");
                var isAuthorized = await this.SetupForSSOAsync(
                        apiUri,
                        cookieDomain,
                        requireAllRoles,
                        logger,
                        tokenSource.Token)
                    .ConfigureAwait(false);
                if (!isAuthorized)
                {
                    logger.LogWarning($"{nameof(OnGetAsync)} not authorized, logging out and kicking");
                    // This portal requires authorization with specific role(s), failing this check will trigger
                    // a log out and redirect
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).ConfigureAwait(false);
                    // if (false /* if OIDC was turned on */)
                    // {
                    //     await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme).ConfigureAwait(false);
                    // }
                    return Redirect(logoutUrl);
                }
                logger.LogInformation($"{nameof(OnGetAsync)} is authorized, responding with index.html");
                return File("~/index.html", "text/html");
                ////return Page();
            }
            catch (Exception ex)
            {
                logger.LogCritical($"{nameof(OnGetAsync)} had an exception, cancelling token source and rethrowing {ex.GetType().FullName} {ex.Message}");
                tokenSource.Cancel();
                throw;
            }
        }
    }
}
