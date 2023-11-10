// <copyright file="ServiceRouteMapper.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the service route mapper class</summary>
namespace Clarity.Ecommerce.DNN.Extensions.Services
{
    using System.Web.Http;
    using DotNetNuke.Web.Api;

    /// <summary>The ServiceRouteMapper tells the DNN Web API Framework what routes this module uses.</summary>
    /// <seealso cref="IServiceRouteMapper"/>
    public class ServiceRouteMapper : IServiceRouteMapper
    {
        /// <summary>RegisterRoutes is used to register the module's routes.</summary>
        /// <param name="mapRouteManager">Manager for map route.</param>
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute(
                moduleFolderName: "ClarityEcommerceDNN",
                routeName: "default",
                url: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                namespaces: new[] { "Clarity.Ecommerce.DNN.Extensions.Services" });
        }
    }
}
