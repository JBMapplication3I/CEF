// <copyright file="Startup.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the startup class</summary>
namespace Clarity.Ecommerce.UI.XPortal.Server
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Portals.Shared.Environments;
    using ServiceStack;

    /// <summary>A startup.</summary>
    public class Startup : ModularStartup
    {
        /// <summary>Initializes a new instance of the <see cref="Startup"/> class.</summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>This method gets called by the runtime. Use this method to add services to the container. For more
        /// information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940.
        /// </summary>
        /// <param name="services">The services.</param>
        public new void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<MVC.Api.Options.XPortalConfiguration>(Configuration)
                .AddLogging()
                .AddHttpClients()
                .AddControllersWithViews()
                .Services
                .AddRazorPages();
        }

        /// <summary>This method gets called by the runtime. Use this method to configure the HTTP request pipeline.</summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsLocal() || env.IsQA())
            {
                app
                    .UseDeveloperExceptionPage()
                    .UseWebAssemblyDebugging();
            }
            else
            {
                app
                    .UseExceptionHandler("/Error")
                    .UseHsts();
            }
            app
                .UseHttpsRedirection()
                .UseBlazorFrameworkFiles()
                .UseStaticFiles()
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapRazorPages();
                    endpoints.MapControllers();
                    endpoints.MapFallbackToFile("index.html");
                });
        }
    }
}
