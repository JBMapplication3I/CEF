// <copyright file="CategoryCRUDWorkflow.extended.Special.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the category workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Xml.Serialization;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;
    using File = System.IO.File;

    public partial class CategoryWorkflow
    {
        /// <inheritdoc/>
        public async Task<string> GenerateCategorySiteMapContentAsync(string? contextProfileName)
        {
            // TODO: Read CORS path for catalog route instead
            var root = CEFConfigDictionary.SiteRouteHostUrl;
            while (root.EndsWith("/"))
            {
                root = root.TrimEnd('/');
            }
            var relativePath = CEFConfigDictionary.CatalogRouteRelativePath;
            while (relativePath.StartsWith("/"))
            {
                relativePath = relativePath.TrimStart('/');
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var urlset = new CategorySiteMapUrlSet(
                (await context.Categories
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterCategoriesByIncludeInMenu(true)
                    .Where(x => x.SeoUrl != null && x.SeoUrl != string.Empty)
                    .Select(x => new
                    {
                        x.SeoUrl,
                        x.Name,
                        x.CustomKey,
                        UpdatedDate = x.UpdatedDate ?? x.CreatedDate,
                    })
                    .OrderBy(x => x.SeoUrl)
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new CategorySiteMapUrl
                {
                    ChangeFrequency = "daily",
                    UpdatedDate = x.UpdatedDate.ToString("yyyy-MM-dd"),
                    Location = $"{root}/{relativePath}"
                        + $"#!/c/p/Results/Format/{CEFConfigDictionary.CatalogDefaultFormat}/Page/1/Size/{CEFConfigDictionary.CatalogDefaultPageSize}/Sort/{CEFConfigDictionary.CatalogDefaultSort}"
                        + $"?categoriesAll={HttpUtility.UrlEncode(x.CustomKey)}%7C{HttpUtility.UrlEncode(x.Name)}",
                    Priority = 0.5,
                })
                .ToList());
            return urlset
                .ToXmlString()
                .Replace("utf-16", "utf-8")
                .Replace(
                    "<urlset xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">",
                    "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xhtml=\"http://www.w3.org/1999/xhtml\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">")
                .Replace(
                    "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">",
                    "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xhtml=\"http://www.w3.org/1999/xhtml\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">");
        }

        /// <inheritdoc/>
        public async Task<bool> SaveCategorySiteMapAsync(
            string content,
            string dropPath,
            string fileName = "CategorySiteMap.xml")
        {
            var fullPath = Path.Combine(dropPath, fileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            using var file = new StreamWriter(fullPath);
            await file.WriteAsync(content).ConfigureAwait(false);
            return true;
        }

        /// <inheritdoc/>
        public async Task<string> GenerateCustomSiteMapContentAsync(string? contextProfileName)
        {
            // TODO: Read CORS path for category route instead
            var root = CEFConfigDictionary.SiteRouteHostUrl;
            while (root.EndsWith("/"))
            {
                root = root.TrimEnd('/');
            }
            var urlFragment = ConfigurationManager.AppSettings["Clarity.Seo.CustomCategoryUrlFragments"] ?? "Category";
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var urlset = new CategorySiteMapUrlSet(
                (await context.Categories
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterCategoriesByIncludeInMenu(true)
                        .Where(x => x.SeoUrl != null && x.SeoUrl != string.Empty)
                        .Select(x => new
                        {
                            x.SeoUrl,
                            x.Name,
                            x.CustomKey,
                            UpdatedDate = x.UpdatedDate ?? x.CreatedDate,
                        })
                        .OrderBy(x => x.SeoUrl)
                        .ToListAsync()
                        .ConfigureAwait(false))
                    .Select(x => new CategorySiteMapUrl
                    {
                        ChangeFrequency = "daily",
                        UpdatedDate = x.UpdatedDate.ToString("yyyy-MM-dd"),
                        Location = $"{root}/{urlFragment}/{x.SeoUrl}",
                        Priority = 0.5,
                    })
                    .ToList());
            return urlset
                .ToXmlString()
                .Replace("utf-16", "utf-8")
                .Replace(
                    "<urlset xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">",
                    "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xhtml=\"http://www.w3.org/1999/xhtml\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">")
                .Replace(
                    "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">",
                    "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xhtml=\"http://www.w3.org/1999/xhtml\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">");
        }

        /// <summary>(Serializable) a category site map url.</summary>
        [Serializable, XmlType("url"), PublicAPI]
        public class CategorySiteMapUrl
        {
            /// <summary>Gets or sets the location.</summary>
            /// <value>The location.</value>
            [XmlElement("loc")]
            public string Location { get; set; } = null!;

            /// <summary>Gets or sets the updated date.</summary>
            /// <value>The updated date.</value>
            [XmlElement("lastmod")]
            public string UpdatedDate { get; set; } = null!;

            /// <summary>Gets or sets the change frequency.</summary>
            /// <value>The change frequency.</value>
            [XmlElement("changefreq")]
            public string ChangeFrequency { get; set; } = null!;

            /// <summary>Gets or sets the priority.</summary>
            /// <value>The priority.</value>
            [XmlElement("priority")]
            public double Priority { get; set; }
        }

        /// <summary>A category site map URL set.</summary>
        /// <seealso cref="List{CategorySiteMapUrl}"/>
        [Serializable, XmlType("urlset"), PublicAPI]
        public class CategorySiteMapUrlSet : List<CategorySiteMapUrl>
        {
            /// <summary>Initializes a new instance of the <see cref="CategorySiteMapUrlSet"/> class.</summary>
            /// <param name="source">Source for the.</param>
            public CategorySiteMapUrlSet(IEnumerable<CategorySiteMapUrl> source)
                : base(source)
            {
            }

            /// <summary>Prevents a default instance of the <see cref="CategorySiteMapUrlSet"/> class from being created.</summary>
            private CategorySiteMapUrlSet()
            {
            }
        }
    }
}
