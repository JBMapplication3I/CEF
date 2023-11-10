// <copyright file="Program.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the program class</summary>
namespace Clarity.Ecommerce.UI.XPortal.SharedLibrary
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AutoMapper;
    using Blazor.Extensions.Logging;
    using Blazored.LocalStorage;
    using Blazorise;
    using Blazorise.Bootstrap;
    using Blazorise.Icons.FontAwesome;
    using Blazorise.RichTextEdit;
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using MVC.Api.Callers;
    using MVC.Api.Endpoints;
    using MVC.Api.Models;
    using MVC.Api.Options;
    using MVC.Api.Service;
    using Syncfusion.Blazor;

    /// <summary>A program.</summary>
    public class Program
    {
        /// <summary>Main entry-point for this application.</summary>
        /// <param name="args">Array of command-line argument strings.</param>
        /// <returns>Exit-code for the process - 0 for success, else an error code.</returns>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
                "NDg0NTI0QDMxMzkyZTMyMmUzMFU2QWZaakt5K3ppV0J3QlRZRE1iNlY2WmNwT0taRENkcURvS1NZZEh4ZTQ9");
            builder.Services
                .AddLogging(b => b
                    .AddBrowserConsole()
                    .SetMinimumLevel(LogLevel.Trace));
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services
                .AddOptions()
                .AddScoped(_ => new HttpClient { BaseAddress = new(builder.HostEnvironment.BaseAddress) });
            await builder.Services.AddCEFServices(builder.Configuration).ConfigureAwait(false);
            builder.Services
                .AddAuthorizationCore()
                .AddBlazoredLocalStorage(config => config.JsonSerializerOptions.WriteIndented = true)
                .AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                    // Equivalent to the ng-options de-bounce 500
                    options.DelayTextOnKeyPress = true;
                    options.DelayTextOnKeyPressInterval = 500;
                })
                .AddBlazoriseRichTextEdit(options =>
                {
                    options.UseBubbleTheme = true;
                    // options.UseShowTheme = true;
                    options.DynamicallyLoadReferences = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons()
                .AddSyncfusionBlazor();
            await builder.Build().RunAsync().ConfigureAwait(false);
        }
    }

    /// <summary>An extensions.</summary>
    public static class Extensions
    {
        /// <summary>An IServiceCollection extension method that adds a cef services to
        /// 'webAssemblyHostConfiguration'.</summary>
        /// <param name="services">                    The services to act on.</param>
        /// <param name="webAssemblyHostConfiguration">The web assembly host configuration.</param>
        /// <returns>A list of.</returns>
        public static async Task<IServiceCollection> AddCEFServices(
            this IServiceCollection services,
            WebAssemblyHostConfiguration webAssemblyHostConfiguration)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateProduct, ProductModel>().ReverseMap();
                cfg.CreateMap<UpdateProduct, ProductModel>().ReverseMap();
                cfg.CreateMap<UpdateProductAssociation, ProductAssociationModel>().ReverseMap();
#if BRANDADMIN || FRANCHISEADMIN
                cfg.CreateMap<UpdateFranchiseProduct, FranchiseProductModel>().ReverseMap();
#endif
#if BRANDADMIN || FRANCHISEADMIN || STOREADMIN
                cfg.CreateMap<UpdateStoreProduct, StoreProductModel>().ReverseMap();
                cfg.CreateMap<UpdateVendorProduct, VendorProductModel>().ReverseMap();
#endif
            });
            var mapper = mapperConfig.CreateMapper();
            var config = webAssemblyHostConfiguration.Get<XPortalConfiguration>();
            var apiOptions = config.APIOptions ?? new();
            services
                .AddSingleton(apiOptions)
                .AddSingleton(config.PortalOptions ?? new())
                .AddSingleton(config.RoutingOptions ?? new())
                .AddSingleton(config.ProductEditorOptions ?? new())
                .AddSingleton<CEFService>()
                .AddSingleton<CEFAPI>()
                .AddSingleton<StaticMenuState>()
                .AddSingleton(_ => mapper);
            var cefService = new CEFService(config.APIOptions ?? new());
            var cvApi = new CEFAPI(cefService);
            var cefConfig = await (
                apiOptions.Kind switch
                {
#if BRANDADMIN
                    "BrandAdmin" => cvApi.GetBrandAdminCEFConfigAlt(),
#elif FRANCHISEADMIN
                    "FranchiseAdmin" => cvApi.GetFranchiseAdminCEFConfigAlt(),
#elif MANUFACTURERADMIN
                    "ManufacturerAdmin" => cvApi.GetManufacturerAdminCEFConfigAlt(),
#elif STOREADMIN
                    "StoreAdmin" => cvApi.GetStoreAdminCEFConfigAlt(),
#elif VENDORADMIN
                    "VendorAdmin" => cvApi.GetVendorAdminCEFConfigAlt(),
#endif
                    _ => throw new InvalidOperationException("No CEFConfig Kind defined"),
                })
                .ConfigureAwait(false);
            if (cefConfig is null)
            {
                throw new NullReferenceException("CEFConfig is null from endpoint");
            }
            services
                .AddSingleton(cefConfig.data ?? new())
                .AddScoped(serviceProvider => serviceProvider.GetService<CEFService>()!.Client);
            return services;
        }
    }
}

// NOTE: These are added where some namespaces can't show up in some projects so we don't get errors
// ReSharper disable EmptyNamespace
namespace Clarity.Ecommerce.UI.XPortal.SharedLibrary.Shared { }
namespace Clarity.Ecommerce.UI.XPortal.SharedLibrary.Shared.Associations { }
namespace Clarity.Ecommerce.UI.XPortal.SharedLibrary.Shared.Forms { }
namespace Clarity.Ecommerce.UI.XPortal.SharedLibrary.Shared.Forms.FormGroups { }
namespace Clarity.Ecommerce.UI.XPortal.SharedLibrary.Pages.Customers.Users.EditParts { }
