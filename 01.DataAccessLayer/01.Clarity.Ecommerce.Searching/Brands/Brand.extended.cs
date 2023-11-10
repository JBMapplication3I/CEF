// <copyright file="Brand.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Linq;
    using DataModel;
    using Ecommerce.DataModel;
    using LinqKit;
    using Utilities;

    /// <summary>A brand search extensions.</summary>
    public static class BrandSearchExtensions
    {
        /// <summary>An IQueryable{TEntity} extension method that filter brands by host URL.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="query">  The query to act on.</param>
        /// <param name="hostUrl">URL of the host.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        public static IQueryable<TEntity> FilterBrandsByHostUrl<TEntity>(
                this IQueryable<TEntity> query,
                string? hostUrl)
            where TEntity : class, IBrand
        {
            if (!Contract.CheckValidKey(hostUrl))
            {
                return query;
            }
            var theHostUrl = hostUrl!;
            if (!theHostUrl.StartsWith("http://") && !theHostUrl.StartsWith("https://"))
            {
                theHostUrl = "http://" + theHostUrl;
            }
            if (theHostUrl.EndsWith("/"))
            {
                theHostUrl = theHostUrl.TrimEnd('/');
            }
            var uri = new Uri(theHostUrl.ToLower());
            var url = uri.ToString().Replace("https://", string.Empty).Replace("http://", string.Empty);
            var hostOnly = uri.Host.Replace("https://", string.Empty).Replace("http://", string.Empty);
            var urlWithHttp = "http://" + url;
            var urlWithHttps = "https://" + url;
            var hostWithHttp = "http://" + hostOnly;
            var hostWithHttps = "https://" + hostOnly;
            return Contract.RequiresNotNull(query)
                .Where(x => x.BrandSiteDomains!.Any(y => y.Active
                    && y.Slave!.Active
                    && (y.Slave.Url!.StartsWith(urlWithHttp)
                        || y.Slave.Url.StartsWith(urlWithHttps)
                        || y.Slave.Url.StartsWith(hostWithHttp)
                        || y.Slave.Url.StartsWith(hostWithHttps)
                        || y.Slave.AlternateUrl1!.StartsWith(urlWithHttp)
                        || y.Slave.AlternateUrl1.StartsWith(urlWithHttps)
                        || y.Slave.AlternateUrl1.StartsWith(hostWithHttp)
                        || y.Slave.AlternateUrl1.StartsWith(hostWithHttps)
                        || y.Slave.AlternateUrl2!.StartsWith(urlWithHttp)
                        || y.Slave.AlternateUrl2.StartsWith(urlWithHttps)
                        || y.Slave.AlternateUrl2.StartsWith(hostWithHttp)
                        || y.Slave.AlternateUrl2.StartsWith(hostWithHttps)
                        || y.Slave.AlternateUrl3!.StartsWith(urlWithHttp)
                        || y.Slave.AlternateUrl3.StartsWith(urlWithHttps)
                        || y.Slave.AlternateUrl3.StartsWith(hostWithHttp)
                        || y.Slave.AlternateUrl3.StartsWith(hostWithHttps))));
        }
    }
}
