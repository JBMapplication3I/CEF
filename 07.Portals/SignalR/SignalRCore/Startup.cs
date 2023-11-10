// <copyright file="Startup.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the SignalRCore Startup class</summary>
namespace Clarity.Ecommerce.SignalRCore
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;

    /// <summary>A startup.</summary>
    public class Startup
    {
        /// <summary>Initializes a new instance of the <see cref="Startup"/> class.</summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>Gets the configuration.</summary>
        /// <value>The configuration.</value>
        public IConfiguration Configuration { get; }

        /// <summary>This method gets called by the runtime. Use this method to add services to the container.</summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddCors();
            services.AddSignalR();
            services.Configure<CefOptions>(Configuration.GetSection(nameof(CefOptions)));
            services.AddTransient<IService, Service>();
            services.AddHttpClient(nameof(Service), (sp, httpClient) =>
            {
                var options = sp.GetRequiredService<IOptions<CefOptions>>();
                httpClient.BaseAddress = new(options.Value.BaseAddress!);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));
                httpClient.Timeout = TimeSpan.FromMinutes(10);
                var auth = JsonConvert.SerializeObject(new
                {
                    options.Value.Username,
                    options.Value.Password,
                });
                using var request = new StringContent(auth, Encoding.UTF8, "application/json");
                httpClient
                    .PostAsync($"{httpClient.BaseAddress}/Authenticate/{options.Value.AuthProvider ?? "Identity"}", request)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
        }

        /// <summary>This method gets called by the runtime. Use this method to configure the HTTP request pipeline.</summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios,
                // see https://aka.ms/aspnetcore-hsts
                app.UseHsts();
            }
            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthorization()
                .UseCors(builder =>
                {
                    builder.WithOrigins(Configuration.GetValue<string>("CorsAllowOriginsUrls").Split(","))
                        .AllowAnyHeader()
                        .WithMethods("GET", "POST")
                        .AllowCredentials();
                })
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapRazorPages();
                    endpoints.MapHub<Hubs>("hubs");
                });
        }
    }
}
