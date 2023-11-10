// <copyright file="CookieAuthenticationMiddleware.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cookie authentication middleware class</summary>
namespace Microsoft.Owin.Security.Cookies
{
    using DataHandler;
    using DataProtection;
    using Infrastructure;
    using Logging;
    using Owin.Infrastructure;

    /// <summary>Cookie based authentication middleware.</summary>
    /// <seealso cref="AuthenticationMiddleware{CookieAuthenticationOptions}"/>
    /// <seealso cref="AuthenticationMiddleware{TOptions}"/>
    public class CookieAuthenticationMiddleware : AuthenticationMiddleware<CookieAuthenticationOptions>
    {
        /// <summary>The logger.</summary>
        private readonly ILogger _logger;

        /// <summary>Initializes a new instance of <see cref="CookieAuthenticationMiddleware" /></summary>
        /// <param name="next">   The next middleware in the OWIN pipeline to invoke.</param>
        /// <param name="app">    The OWIN application.</param>
        /// <param name="options">Configuration options for the middleware.</param>
        public CookieAuthenticationMiddleware(
            OwinMiddleware next,
            global::Owin.IAppBuilder app,
            CookieAuthenticationOptions options) : base(next, options)
        {
            if (Options.Provider == null)
            {
                Options.Provider = new CookieAuthenticationProvider();
            }
            if (string.IsNullOrEmpty(Options.CookieName))
            {
                Options.CookieName = string.Concat(".AspNet.", Options.AuthenticationType);
            }
            _logger = app.CreateLogger<CookieAuthenticationMiddleware>();
            if (Options.TicketDataFormat == null)
            {
                var dataProtector = app.CreateDataProtector(
                    typeof(CookieAuthenticationMiddleware).FullName,
                    Options.AuthenticationType,
                    "v1");
                Options.TicketDataFormat = new TicketDataFormat(dataProtector);
            }
            if (Options.CookieManager == null)
            {
                Options.CookieManager = new ChunkingCookieManager();
            }
        }

        /// <summary>Provides the <see cref="AuthenticationHandler" /> object for processing authentication-related
        /// requests.</summary>
        /// <returns>An <see cref="AuthenticationHandler" /> configured with the
        /// <see cref="CookieAuthenticationOptions" /> supplied to the constructor.</returns>
        protected override AuthenticationHandler<CookieAuthenticationOptions> CreateHandler()
        {
            return new CookieAuthenticationHandler(_logger);
        }
    }
}
