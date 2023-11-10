// <copyright file="PageViewEventService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the page view event service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using ServiceStack;
    using Utilities;

    // No Authentication required, must be accessible to anonymous users
    [PublicAPI,
        Route("/Tracking/PageViewEvent/CreateFull", "POST",
            Summary = "Use to create a new page view event along with additional data points as needed (Event, Visit, Visitor)")]
    public partial class CreateFullPageViewEvent : EndUserEventModel, IReturn<CEFActionResponse<EndUserEventModel>>
    {
    }

    // No Authentication required, must be accessible to anonymous users
    [PublicAPI,
        Route("/Tracking/RecentlyViewedProducts", "POST",
            Summary = "Get Recently Viewed Products based on Tracking data")]
    public partial class GetRecentlyViewedProducts : IReturn<List<ProductModel>>
    {
        public Paging? Paging { get; set; }
    }

    public partial class PageViewEventService
    {
        public async Task<object?> Post(CreateFullPageViewEvent request)
        {
            var httpRequest = Request.ToHttpRequestBase();
            if (request.Visitor != null)
            {
                request.Visitor.IPAddress = httpRequest.UserHostAddress;
            }
            if (request.Visit != null)
            {
                request.Visit.IPAddress = httpRequest.UserHostAddress;
            }
            if (request.Event != null)
            {
                request.Event.IPAddress = httpRequest.UserHostAddress;
            }
            if (request.PageView != null)
            {
                request.PageView.IPAddress = httpRequest.UserHostAddress;
            }
            if (httpRequest.UserLanguages?.Any() == true)
            {
                if (request.Event != null)
                {
                    request.Event.Language = httpRequest.UserLanguages[0];
                }
                if (request.PageView != null)
                {
                    request.PageView.Language = httpRequest.UserLanguages[0];
                }
            }
            var browser = $"{httpRequest.Original.Browser.Browser} ({httpRequest.Original.Browser.Version})";
            if (browser.Length > 10)
            {
                browser = httpRequest.Original.Browser.Browser;
            }
            if (browser.Length > 10)
            {
                browser = browser[..10];
            }
            if (request.Visit != null)
            {
                request.Visit.Browser = browser;
            }
            if (request.Event != null)
            {
                request.Event.Browser = browser;
            }
            if (request.PageView != null)
            {
                request.PageView.Browser = browser;
            }
            if (request.Visit != null)
            {
                request.Visit.OperatingSystem = httpRequest.Original.Browser.Platform;
            }
            if (request.Event != null)
            {
                request.Event.OperatingSystem = httpRequest.Original.Browser.Platform;
            }
            if (request.PageView != null)
            {
                request.PageView.OperatingSystem = httpRequest.Original.Browser.Platform;
            }
            var productPath = ("/" + CEFConfigDictionary.ProductDetailRouteRelativePath + "/").Replace("//", "/");
            var catalogPath = ("/" + CEFConfigDictionary.CatalogRouteRelativePath + "#!").Replace("//", "/");
            // https://testing.keysource.com/Catalog#!/c/p/Results/Format/table/Page/1/Size/25/Sort/Relevance?term=Azeliac&category=Dermatology%7CDermatology
            var isProductPathInReferrer = httpRequest.UrlReferrer?.AbsoluteUri.Contains(productPath) == true;
            var isProductPathInPageView = request.PageView?.URI?.Contains(productPath) == true;
            var isCatalogPathInReferrer = httpRequest.UrlReferrer?.AbsoluteUri.Contains(catalogPath) == true;
            var isCatalogPathInPageView = request.PageView?.URI?.Contains(catalogPath) == true;
            if (request.PageView != null && (isProductPathInReferrer || isProductPathInPageView))
            {
                var urlToCheck = isProductPathInReferrer
                    ? httpRequest!.UrlReferrer!.AbsoluteUri
                    : request.PageView.URI;
                var seoUrl = urlToCheck![
                    (urlToCheck!.IndexOf(productPath, StringComparison.InvariantCultureIgnoreCase)
                    + productPath.Length)..];
                request.PageView.ProductID = await Workflows.Products.CheckExistsBySeoUrlAsync(
                        seoUrl,
                        contextProfileName: null)
                    .ConfigureAwait(false);
                if (Contract.CheckValidID(request.PageView.ProductID))
                {
                    var (results, _, _) = await Workflows.ProductCategories.SearchAsync(
                            new ProductCategorySearchModel
                            {
                                Active = true,
                                MasterID = request.PageView.ProductID!.Value,
                                Paging = new() { Size = 1, StartIndex = 1 },
                            },
                            true,
                            contextProfileName: null)
                        .ConfigureAwait(false);
                    if (results.Count == 1)
                    {
                        request.PageView.CategoryID = results[0].SlaveID;
                    }
                }
            }
            else if (request.PageView != null && (isCatalogPathInReferrer || isCatalogPathInPageView))
            {
                var urlToCheck = isCatalogPathInReferrer
                    ? httpRequest.UrlReferrer!.AbsoluteUri
                    : request.PageView.URI;
                var clippedPath = urlToCheck![
                    (urlToCheck!.IndexOf(catalogPath, StringComparison.InvariantCultureIgnoreCase)
                    + catalogPath.Length)..];
                if (clippedPath.ToLower().Contains("category="))
                {
                    var regex = new Regex(@"(^|\?|&)category=(?<name>[A-Za-z0-9%,-]+)%7c(?<key>[A-Za-z0-9%,-]+)($|&)", RegexOptions.IgnoreCase);
                    var match = regex.Match(clippedPath);
                    if (match.Success)
                    {
                        request.PageView.CategoryID = await Workflows.Categories.ResolveToIDOptionalAsync(
                                byID: null,
                                byKey: match.Groups["key"].Value,
                                byName: match.Groups["name"].Value,
                                model: null,
                                contextProfileName: null)
                            .ConfigureAwait(false);
                    }
                }
                if (clippedPath.ToLower().Contains("term="))
                {
                    var regex = new Regex(@"(^|\?|&)term=(?<term>[A-Za-z0-9%,-]+)($|&)");
                    var match = regex.Match(clippedPath);
                    if (match.Success)
                    {
                        request.PageView.Keywords = match.Groups["term"].Value;
                    }
                }
            }
            if (IsAuthenticated)
            {
                var userID = int.Parse(GetSession().UserAuthId);
                if (request.Visitor != null)
                {
                    request.Visitor.UserID = userID;
                }
                if (request.Visit != null)
                {
                    request.Visit.UserID = userID;
                }
                if (request.Event != null)
                {
                    request.Event.UserID = userID;
                }
                if (request.PageView != null)
                {
                    request.PageView.UserID = userID;
                }
            }
            // Run the Create
            return (await Workflows.PageViewEvents.CreateFromEndUserEventAsync(
                        request,
                        contextProfileName: null)
                    .ConfigureAwait(false))
                .ChangeCEFARType<IEndUserEventModel, EndUserEventModel>();
        }

        public async Task<object?> Post(GetRecentlyViewedProducts request)
        {
            if (!CEFConfigDictionary.TrackingEnabled)
            {
                throw new InvalidOperationException("Tracking has been disabled in this site");
            }
            await PickupVisitCookieAsync().ConfigureAwait(false);
            await PickupVisitorCookieAsync().ConfigureAwait(false);
            var productIDs = await Workflows.PageViewEvents.GetRecentlyViewedProductIDsForCurrentVisitorAsync(
                    Request.UserHostAddress,
                    await GetSessionVisitGuidAsync().ConfigureAwait(false),
                    await GetSessionVisitorGuidAsync().ConfigureAwait(false),
                    request.Paging ?? new(),
                    contextProfileName: null)
                .ConfigureAwait(false);
            if (productIDs?.Any() != true)
            {
                return new List<ProductModel>();
            }
            // The order of the products returned is not deterministic, so it needs to be made to match productIDs order
            var mappedProducts = await Workflows.Products.SearchRecentlyViewedProductsAsync(
                    productIDs.Select(x => x.ProductID).ToList(),
                    contextProfileName: null)
                .ConfigureAwait(false);
            return mappedProducts
                .OrderByDescending(x => productIDs.First(y => y.ProductID == x.ID).CreatedDate)
                .ToList();
        }
    }
}
