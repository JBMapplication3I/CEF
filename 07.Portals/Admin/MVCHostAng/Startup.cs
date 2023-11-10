// <copyright file="Startup.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the startup class</summary>
namespace Clarity.Ecommerce.UI.MVCHost
{
    using System.IdentityModel.Tokens.Jwt;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.OpenIdConnect;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>A startup.</summary>
    public class Startup
    {
        private readonly string? authority;
        private readonly string? clientId;

        /// <summary>Initializes a new instance of the <see cref="Startup"/> class.</summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            try
            {
                authority = configuration.GetValue<string>("Authority");
                clientId = configuration.GetValue<string>("ClientId");
            }
            catch
            {
                // Do Nothing
            }
        }

        /// <summary>Configure services.</summary>
        /// <param name="services">The services.</param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddMvc();
            if (string.IsNullOrWhiteSpace(authority) || string.IsNullOrWhiteSpace(clientId))
            {
                services
                    .AddAuthentication(options =>
                    {
                        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    })
                    .AddCookie(options => options.ForwardChallenge = CookieAuthenticationDefaults.AuthenticationScheme);
                return;
            }
            // Setup OIDC
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(options => options.ForwardChallenge = OpenIdConnectDefaults.AuthenticationScheme)
                .AddOpenIdConnect(options =>
                {
                    options.Authority = authority;
                    options.ClientId = clientId;
                    options.SaveTokens = true;
                });
        }

        /// <summary>Configures. This method gets called by the runtime. Use this method to configure the HTTP request
        /// pipeline.</summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()
                || env.EnvironmentName is "Local" or "Development" or "QA" or "Staging")
            {
                // app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app
                    .UseExceptionHandler("/Error")
                    .UseHsts();
            }
            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                    // endpoints.MapSpaFallbackRoute(
                    //     name: "spa-fallback",
                    //     defaults: new { controller = "Index", action = "Spa" });
                })
                .UseSpa(_ =>
                {
                    _.Options.DefaultPage = "spa-fallback";
                });
            // if (false /* if OIDC was turned on */)
            // {
            //     app.UseAuthentication();
            // }
            // Handle client side routes
            app.Run(context =>
            {
                context.Response.ContentType = "text/html";
                return context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));
            });
        }

        /// <summary>An ignore route middleware.</summary>
        // ReSharper disable once UnusedType.Global
        public class IgnoreRouteMiddleware
        {
            private readonly RequestDelegate next;

            /// <summary>Initializes a new instance of the <see cref="IgnoreRouteMiddleware"/> class.</summary>
            /// <param name="next">The next.</param>
            // You can inject a dependency here that gives you access to your ignored route configuration.
            public IgnoreRouteMiddleware(RequestDelegate next)
            {
                this.next = next;
            }

            /// <summary>Executes the given operation on a different thread, and waits for the result.</summary>
            /// <param name="context">The context.</param>
            /// <returns>A Task.</returns>
            // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting, UnusedMember.Global
#pragma warning disable IDE1006 // Naming Styles
            public async Task Invoke(HttpContext context)
#pragma warning restore IDE1006 // Naming Styles
            {
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/index.html";
                    context.Response.StatusCode = 200;
                    return;
                }
                await next(context).ConfigureAwait(false);
            }
        }
    }
}
