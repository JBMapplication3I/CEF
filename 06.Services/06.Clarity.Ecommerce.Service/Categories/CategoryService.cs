// <copyright file="CategoryService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the category service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using ServiceStack;
    using Utilities;

    public partial class GetCategories
    {
        public GetCategories()
        {
            DisregardParents = true;
        }
    }

    public partial class GetCategoriesForConnect
    {
        public GetCategoriesForConnect()
        {
            DisregardParents = true;
        }
    }

    public partial class GetCategoryByID
    {
        [ApiMember(Name = nameof(ExcludeProductCategories), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "When true, don't map out the Product Categories (use when this would result in a lot of unnecessary data)")]
        public bool? ExcludeProductCategories { get; set; }
    }

    public partial class GetCategoryByKey
    {
        ////[ApiMember(Name = nameof(Key), DataType = "string", ParameterType = "query", IsRequired = true,
        ////    Description = "The CustomKey of the record to call")]
        ////public override string Key { get; set; }

        [ApiMember(Name = nameof(ExcludeProductCategories), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "When true, don't map out the Product Categories (use when this would result in a lot of unnecessary data)")]
        public bool? ExcludeProductCategories { get; set; }
    }

    public partial class GetCategoryBySeoUrl
    {
        [ApiMember(Name = nameof(ExcludeProductCategories), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "When true, don't map out the Product Categories (use when this would result in a lot of unnecessary data)")]
        public bool? ExcludeProductCategories { get; set; }
    }

    [PublicAPI,
        Route("/Categories/Category/Metadata", "GET",
            Summary = "Get Category by SEO URL for just the SEO Metadata")]
    public partial class GetCategoryForMetaData
    {
        /// <summary>Gets or sets the seo url.</summary>
        /// <value>The seo url.</value>
        [ApiMember(Name = nameof(SeoUrl), DataType = "string", ParameterType = "query", IsRequired = true,
            Description = "The SEO URL of the Category to locate")]
        public string SeoUrl { get; set; } = null!;
    }

    [PublicAPI,
        Route("/Categories/Levels/Three", "GET",
            Summary = "Use to get three levels of categories")]
    public partial class GetCategoriesThreeLevels : CategorySearchModel, IReturn<List<CategoryModel>>
    {
    }

    [PublicAPI,
        Route("/Categories/Tree", "GET",
            Summary = "Use to get a tree of categories. Include the ParentID and set IncludeChildrenInResults to false and this can function as lazy loading for Kendo Trees.")]
    public partial class GetCategoryTree : CategorySearchModel, IReturn<List<ProductCategorySelectorModel>>
    {
    }

    [PublicAPI,
        Route("/Categories/MenuLevels/Three", "GET",
            Summary = "Use to get three levels of categories")]
    public partial class GetMenuCategoriesThreeLevels : CategorySearchModel, IReturn<List<MenuCategoryModel>>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Categories/SiteMap", "GET", Summary = "Get the Category site map without replacing it")]
    public partial class GetCategorySiteMapContent : IReturn<DownloadFileResult>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Categories/SiteMap/Regenerate", "POST",
            Summary = "Generates a new Category site map and then replaces the existing one per the DropPath value from the web config")]
    public partial class RegenerateCategorySiteMap : IReturn<bool>
    {
    }

    [PublicAPI]
    public partial class CategoryService
    {
        protected override List<string> AdditionalUrnIDs => new()
        {
            UrnId.Create<GetCategoriesThreeLevels>(string.Empty),
            UrnId.Create<GetCategoryTree>(string.Empty),
        };

        /// <inheritdoc/>
        public override async Task<object?> Get(GetCategoryByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Categories.GetLastModifiedForResultAsync(request.ID, ServiceContextProfileName),
                    () => Workflows.Categories.GetWithOptionAsync(request.ID, request.ExcludeProductCategories, ServiceContextProfileName))
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override async Task<object?> Get(GetCategoryByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Categories.GetLastModifiedForResultAsync(request.Key, ServiceContextProfileName),
                    () => Workflows.Categories.GetWithOptionAsync(request.Key, request.ExcludeProductCategories, ServiceContextProfileName))
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override async Task<object?> Get(GetCategoryBySeoUrl request)
        {
            var id = await Workflows.Categories.CheckExistsBySeoUrlAsync(request.SeoUrl, ServiceContextProfileName).ConfigureAwait(false);
            if (Contract.CheckInvalidID(id))
            {
                return null;
            }
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Categories.GetLastModifiedForResultAsync(id!.Value, ServiceContextProfileName),
                    () => Workflows.Categories.GetWithOptionAsync(id!.Value, request.ExcludeProductCategories, ServiceContextProfileName))
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetCategoryForMetaData request)
        {
            if (request.SeoUrl == CEFConfigDictionary.CatalogRouteRelativePath)
            {
                return null;
            }
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Categories.GetLastModifiedForBySeoUrlForMetaDataResultAsync(
                        request.SeoUrl,
                        ServiceContextProfileName),
                    () => Workflows.Categories.GetCategoryBySeoUrlForMetaDataAsync(
                        seoUrl: request.SeoUrl,
                        ServiceContextProfileName))
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetCategoriesThreeLevels request)
        {
            request.CurrentRoles = GetSession()?.Roles?.ToArray();
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Categories.GetLastModifiedForResultSetAsync(request, ServiceContextProfileName),
                    () => Workflows.Categories.GetCategoriesThreeLevelsAsync(request, GetSession()?.Roles, ServiceContextProfileName))
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetMenuCategoriesThreeLevels request)
        {
            request.CurrentRoles = GetSession()?.Roles?.ToArray();
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Categories.GetLastModifiedForMenuCategoriesThreeLevelsAsync(request, ServiceContextProfileName),
                    async () => (await Workflows.Categories.GetMenuCategoriesThreeLevelsAsync(
                                request,
                                GetSession()?.Roles,
                                ServiceContextProfileName)
                            .ConfigureAwait(false))
                        ?.Cast<MenuCategoryModel>()
                        .ToList())
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetCategoryTree request)
        {
            request.CurrentRoles = GetSession()?.Roles?.ToArray();
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Categories.GetLastModifiedForResultTreeAsync(request, ServiceContextProfileName),
                    () => Workflows.Categories.GetCategoryTreeAsync(request, ServiceContextProfileName))
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override Task<object?> Get(GetCategories request)
        {
            request.CurrentRoles = GetSession()?.Roles?.ToArray();
            return base.Get(request);
        }

        public Task<object?> Get(GetCategorySiteMapContent _)
        {
            return Task.FromResult<object?>(
                new DownloadFileResult
                {
                    DownloadUrl = Path.Combine(CEFConfigDictionary.StoredFilesInternalLocalPath, CEFConfigDictionary.SEOSiteMapsRelativePath, "CategorySiteMap.xml"),
                });
        }

        public async Task<object?> Post(RegenerateCategorySiteMap _)
        {
            return await Workflows.Categories.SaveCategorySiteMapAsync(
                    await Workflows.Categories.GenerateCategorySiteMapContentAsync(ServiceContextProfileName).ConfigureAwait(false),
                    Path.Combine(CEFConfigDictionary.StoredFilesInternalLocalPath, CEFConfigDictionary.SEOSiteMapsRelativePath))
                .ConfigureAwait(false);
        }
    }
}
