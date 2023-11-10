// <copyright file="ProductService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using ServiceStack;
    using Utilities;

    [PublicAPI]
    public partial class ProductService
    {
        /// <inheritdoc/>
        protected override List<string> AdditionalUrnIDs => new()
        {
            ////UrnId.Create<SuggestProductCatalogWithProvider>(string.Empty),
            ////UrnId.Create<SearchProductCatalogWithProvider>(string.Empty),
            UrnId.Create<GetProductsForCurrentStore>(string.Empty),
            UrnId.Create<GetBestSellersProducts>(string.Empty),
            UrnId.Create<GetTrendingProducts>(string.Empty),
            UrnId.Create<GetCustomerFavoritesProducts>(string.Empty),
            UrnId.Create<GetLatestProducts>(string.Empty),
            UrnId.Create<GetProductsByIDs>(string.Empty),
            UrnId.Create<GetProductByURL>(string.Empty),
#pragma warning disable CS0618
            UrnId.Create<GetProductForMetaData>(string.Empty),
#pragma warning restore CS0618
            UrnId.Create<GetProductMetadataByURL>(string.Empty),
            UrnId.Create<GetPrimaryImageForProductID>(string.Empty),
            UrnId.Create<GetPersonalizationProductsForCurrentUser>(string.Empty),
            UrnId.Create<GetPersonalizedCategoriesForCurrentUser>(string.Empty),
            UrnId.Create<GetPersonalizedCategoryAndProductFeedForCurrentUser>(string.Empty),
            UrnId.Create<GetProductReview>(string.Empty),
        };

        public async Task<object?> Get(GetProductsAsExcelDoc _)
        {
            return await DoExportAsync().ConfigureAwait(false);
        }

        public Task<object?> Get(GetProductSiteMapContent _)
        {
            return Task.FromResult<object?>(
                new DownloadFileResult
                {
                    DownloadUrl = CEFConfigDictionary.SiteRouteHostUrl + "/ProductSiteMap.xml",
                });
        }

        public async Task<object?> Post(GetProductsByPreviouslyOrdered request)
        {
            // TODO: Cache Research
            var productIDs = (await Workflows.SalesOrders.GetSalesOrdersDistinctProductsForAccountAsync(
                        await LocalAdminAccountIDOrThrow401Async(CurrentAccountIDOrThrow401).ConfigureAwait(false),
                        contextProfileName: null)
                    .ConfigureAwait(false))
                ?.ToList();
            if (productIDs?.Any() != true)
            {
                return new PreviouslyOrderedProductPagedResults
                {
                    Results = new(),
                    CurrentCount = 0,
                    CurrentPage = 0,
                    TotalPages = 0,
                    TotalCount = 0,
                    Sorts = request.Sorts,
                    Groupings = request.Groupings,
                };
            }
            request.ProductIDs = productIDs.Select(p => p ?? 0).Distinct().ToArray();
            request.PricingFactoryContext = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            var (results, totalPages, totalCount) = await Workflows.Products.SearchPreviouslyOrderedAsync(
                    request,
                    asListing: false,
                    contextProfileName: null)
                .ConfigureAwait(false);
            return new PreviouslyOrderedProductPagedResults
            {
                Results = results.Cast<ProductModel>().ToList(),
                CurrentCount = request.Paging?.Size ?? totalCount,
                CurrentPage = request.Paging?.StartIndex ?? 1,
                TotalPages = totalPages,
                TotalCount = totalCount,
                Sorts = request.Sorts,
                Groupings = request.Groupings,
            };
        }

        public override async Task<object?> Get(GetProducts request)
        {
            request.PricingFactoryContext = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            return base.Get(request);
        }

        public async Task<object?> Get(GetProductsForCurrentStore request)
        {
            request.PricingFactoryContext = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            request.StoreID = await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false);
            request.Active = true;
            request.AsListing = true;
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Products.GetLastModifiedForResultSetAsync(request, contextProfileName: null),
                    async () =>
                    {
                        var (results, totalPages, totalCount) = await Workflows.Products.SearchAsync(
                                request,
                                request.AsListing,
                                contextProfileName: null)
                            .ConfigureAwait(false);
                        return new ProductPagedResults
                        {
                            Results = results.Cast<ProductModel>().ToList(),
                            CurrentCount = request.Paging?.Size ?? totalCount,
                            CurrentPage = request.Paging?.StartIndex ?? 1,
                            TotalPages = totalPages,
                            TotalCount = totalCount,
                            Sorts = request.Sorts,
                            Groupings = request.Groupings,
                        };
                    })
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(AdminGetProductsForPortal request)
        {
            request.PricingFactoryContext = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            switch (CurrentAPIKind)
            {
                case Enums.APIKind.BrandAdmin:
                {
                    request.BrandID = await CurrentBrandForBrandAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.FranchiseAdmin:
                {
                    request.FranchiseID = await CurrentFranchiseForFranchiseAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.ManufacturerAdmin:
                {
                    request.ManufacturerID = await CurrentManufacturerForManufacturerAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.StoreAdmin:
                {
                    request.StoreID = await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.VendorAdmin:
                {
                    request.VendorID = await CurrentVendorForVendorAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                default:
                {
                    throw HttpError.Forbidden("Invalid operation");
                }
            }
            request.Active = true;
            request.AsListing = true;
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Products.GetLastModifiedForResultSetAsync(request, contextProfileName: null),
                    async () =>
                    {
                        var (results, totalPages, totalCount) = await Workflows.Products.SearchAsync(
                                request,
                                request.AsListing,
                                contextProfileName: null)
                            .ConfigureAwait(false);
                        return new ProductPagedResults
                        {
                            Results = results.Cast<ProductModel>().ToList(),
                            CurrentCount = request.Paging?.Size ?? totalCount,
                            CurrentPage = request.Paging?.StartIndex ?? 1,
                            TotalPages = totalPages,
                            TotalCount = totalCount,
                            Sorts = request.Sorts,
                            Groupings = request.Groupings,
                        };
                    })
                .ConfigureAwait(false);
        }

        public override async Task<object?> Any(GetProductsForConnect request)
        {
            request.PricingFactoryContext = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            return base.Any(request);
        }

        public async Task<object?> Get(GetBestSellersProducts request)
        {
            var pfc = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Products.GetBestSellingProductsLastModifiedAsync(
                        request.Days,
                        request.CategorySeoUrl,
                        contextProfileName: null),
                    () => Workflows.Products.GetBestSellingProductsAsync(
                        request.Count,
                        request.Days,
                        pfc,
                        request.CategorySeoUrl,
                        null))
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetTrendingProducts request)
        {
            var pfc = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Products.GetTrendingProductsLastModifiedAsync(
                        request.Days,
                        request.CategorySeoUrl,
                        contextProfileName: null),
                    () => Workflows.Products.GetTrendingProductsAsync(
                        request.Count,
                        request.Days,
                        pfc,
                        request.CategorySeoUrl,
                        null))
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetCustomerFavoritesProducts request)
        {
            var pfc = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Products.GetCustomersFavoriteProductsLastModifiedAsync(
                        request.Days,
                        request.CategorySeoUrl,
                        contextProfileName: null),
                    () => Workflows.Products.GetCustomersFavoriteProductsAsync(
                        request.Count,
                        request.Days,
                        pfc,
                        request.CategorySeoUrl,
                        null))
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetLatestProducts request)
        {
            var pfc = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Products.GetLatestProductsLastModifiedAsync(
                        request.Days,
                        request.CategorySeoUrl,
                        contextProfileName: null),
                    () => Workflows.Products.GetLatestProductsAsync(
                        request.Count,
                        request.Days,
                        pfc,
                        request.CategorySeoUrl,
                        null))
                .ConfigureAwait(false);
        }

        public override async Task<object?> Get(GetProductByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Products.GetLastModifiedForResultAsync(request.ID, contextProfileName: null),
                    () => Workflows.Products.GetAsync(
                        id: request.ID,
                        isVendorAdmin: request.IsVendorAdmin ?? false,
                        vendorAdminID: request.VendorAdminID,
                        previewID: request.PreviewID,
                        contextProfileName: null))
                .ConfigureAwait(false);
        }

        public override async Task<object?> Get(GetProductByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Products.GetLastModifiedForResultAsync(request.Key, contextProfileName: null),
                    () => Workflows.Products.GetAsync(
                        key: request.Key,
                        isVendorAdmin: request.IsVendorAdmin ?? false,
                        vendorAdminID: request.VendorAdminID,
                        contextProfileName: null))
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetProductsByIDs request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Products.GetLastModifiedForByIDsResultAsync(
                        productIDs: Contract.RequiresNotEmpty(request.IDs).ToList(),
                        brandID: request.BrandID,
                        storeID: request.StoreID,
                        isVendorAdmin: request.IsVendorAdmin ?? false,
                        vendorAdminID: request.VendorAdminID,
                        contextProfileName: null),
                    async () => (await Workflows.Products.GetByIDsAsync(
                                productIDs: Contract.RequiresNotEmpty(request.IDs).ToList(),
                                brandID: request.BrandID,
                                storeID: request.StoreID,
                                isVendorAdmin: request.IsVendorAdmin ?? false,
                                vendorAdminID: request.VendorAdminID,
                                contextProfileName: null)
                            .ConfigureAwait(false))
                        .Cast<ProductModel>()
                        .ToList())
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetProductByURL request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Products.GetLastModifiedForBySeoUrlResultAsync(request.SeoUrl, contextProfileName: null),
                    () => Workflows.Products.GetProductBySeoUrlAsync(
                        seoUrl: request.SeoUrl,
                        isVendorAdmin: request.IsVendorAdmin ?? false,
                        vendorAdminID: request.VendorAdminID,
                        contextProfileName: null))
                .ConfigureAwait(false);
        }

        [Obsolete("Use GetProductMetadataByURL instead")]
        public async Task<object?> Get(GetProductForMetaData request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Products.GetLastModifiedForBySeoUrlForMetaDataResultAsync(
                        request.SeoUrl,
                        contextProfileName: null),
                    () => Workflows.Products.GetProductBySeoUrlForMetaDataAsync(
                        seoUrl: request.SeoUrl,
                        contextProfileName: null))
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetProductMetadataByURL request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Products.GetLastModifiedForBySeoUrlForMetaDataResultAsync(
                        request.SeoUrl,
                        contextProfileName: null),
                    () => Workflows.Products.GetProductMetadataBySeoUrlAsync(
                        seoUrl: request.SeoUrl,
                        contextProfileName: null))
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(AdminGetProductFull request)
        {
            // NOTE: Never cached, for admin only
            return await Workflows.Products.GetFullAsync(request.ID, null).ConfigureAwait(false);
        }

        public async Task<object?> Get(GetPrimaryImageForProductID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Products.GetPrimaryImageLastModifiedAsync(request.ID, null),
                    () => Workflows.Products.GetPrimaryImageAsync(request.ID, null))
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetProductsByCategory request)
        {
            // TODO: Cache research, this is only used by the bulk order form, highly customer specific
            return await Workflows.Products.GetProductsByCategoryAsync(
                    request.ProductTypeIDs,
                    await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    null)
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetProductInventoryHistory request)
        {
            return await Workflows.Products.GetInventoryLocationHistoryAsync(
                    Contract.RequiresValidID(request.ID),
                    null)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(RegenerateProductSiteMap _)
        {
            return await Workflows.Products.SaveProductSiteMapAsync(
                    await Workflows.Products.GenerateProductSiteMapContentAsync(null).ConfigureAwait(false),
                    Path.Combine(CEFConfigDictionary.StoredFilesInternalLocalPath, CEFConfigDictionary.SEOSiteMapsRelativePath))
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(CheckProductInMyStore request)
        {
            Contract.RequiresValidID(request.ID);
            var currentUser = await CurrentUserOrThrow401Async().ConfigureAwait(false);
            Contract.RequiresValidID(currentUser.AccountID);
            currentUser.Account = await Workflows.Accounts.GetAsync(currentUser.AccountID!.Value, contextProfileName: null).ConfigureAwait(false);
            return await Workflows.Products.CheckProductInMyStoreAsync(request.ID, currentUser, null).ConfigureAwait(false);
        }

        public async Task<object?> Get(GetPersonalizationProductsForCurrentUser request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Products.GetPersonalizationProductsForUserLastModifiedAsync(CurrentUserID, null),
                    () => Workflows.Products.GetPersonalizationProductsForUserAsync(CurrentUserID, null))
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetPersonalizedCategoriesForCurrentUser request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Products.GetPersonalizedCategoriesForUserLastModifiedAsync(CurrentUserID, null),
                    () => Workflows.Products.GetPersonalizedCategoriesForUserAsync(CurrentUserID, null))
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetPersonalizedCategoryAndProductFeedForCurrentUser request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Products.GetPersonalizedCategoryAndProductFeedForUserIDLastModifiedAsync(CurrentUserID, null),
                    async () => (await Workflows.Products.GetPersonalizedCategoryAndProductFeedForUserIDAsync(
                                CurrentUserID,
                                null)
                            .ConfigureAwait(false))
                        .Select(result => new KeyValuePair<CategoryModel, List<ProductModel>>(
                            (CategoryModel)result.Key,
                            result.Value.Cast<ProductModel>().ToList()))
                        .ToList())
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(ProcessProductNotifications _)
        {
            // TODO: Cache research
            return await Workflows.Products.ProductUpdateNotificationsAsync(
                    days: 2,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    categorySeoUrl: null,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        public async Task<object> Get(CheckProductExistsNonNullByKey request)
        {
            var model = new DigestModel();
            var id = await Workflows.Products.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
            model.ID = id ?? 0;
            return model;
        }

        private async Task<DownloadFileResult> DoExportAsync()
        {
            var ds = await Workflows.Products.ExportToExcelAsync(null).ConfigureAwait(false);
            var fileName = $"ProductExport-{DateExtensions.GenDateTime:yyyy-MM-dd-HH.mm.ss}.xlsx";
            var provider = RegistryLoaderWrapper.GetFilesProvider(contextProfileName: null);
            var rootPath = await provider!.GetFileSaveRootPathFromFileEntityTypeAsync(
                    Enums.FileEntityType.StoredFileProduct)
                .ConfigureAwait(false);
            var fullPath = Path.Combine(rootPath, fileName);
            if (!ExportToExcel.CreateExcelFile.CreateExcelDocument(ds, fullPath).ActionSucceeded)
            {
                throw new("Failed to build export file");
            }
            return new()
            {
                DownloadUrl = await provider.GetFileUrlAsync(
                        fileName,
                        Enums.FileEntityType.StoredFileProduct)
                    .ConfigureAwait(false),
            };
        }
    }
}
