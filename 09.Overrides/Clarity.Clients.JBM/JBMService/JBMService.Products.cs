// <copyright file="JBMService.Products.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Ecommerce;
    using Ecommerce.DataModel;
    using Ecommerce.Interfaces.Models;
    using Ecommerce.Models;
    using Ecommerce.Service;
    using Ecommerce.Utilities;
    using ServiceStack;
    using ServiceStack.Support.Markdown;
    using RestSharpSerializer = RestSharp.Serialization.Json.JsonSerializer;

    public partial class JBMService : ClarityEcommerceServiceBase
    {
        public async Task<CEFActionResponse> Post(InventoryItem _)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var rawKeys = await context.Products.Select(x => new { CustomKey = x.CustomKey!, JsonAttributes = x.JsonAttributes, }).ToListAsync().ConfigureAwait(false);
            var productKeys = rawKeys.Where(x => string.IsNullOrWhiteSpace(x.JsonAttributes) || !x!.JsonAttributes!.Contains("InventoryItemId")).Select(y => y.CustomKey).ToList();
            foreach (var key in productKeys)
            {
                var product = await Workflows.Products.GetAsync(key!, ServiceContextProfileName).ConfigureAwait(false);
                if (product is null)
                {
                    continue;
                }
                if (product!.SerializableAttributes.ContainsKey("InventoryItemId"))
                {
                    continue;
                }
                var iii = await GetResponseAsync<InventoryItems>(
                        resource: $"{JBMConfig.JBMSalesAPI}/inventoryOnhandBalances",
                        queryParams: CreateQueryParams(onlyData: true, fields: new string[] { "InventoryItemId" }, fieldQuery: ("ItemNumber", key)))
                    .ConfigureAwait(false);
                if (iii is null || iii.Count == 0)
                {
                    continue;
                }
                if (product!.SerializableAttributes.TryGetValue("InventoryItemId", out var _))
                {
                    product!.SerializableAttributes["InventoryItemId"].Value = iii!.Items!.First()!.InventoryItemId!.ToString()!;
                }
                else
                {
                    product!.SerializableAttributes.TryAdd(
                        "InventoryItemId",
                        new SerializableAttributeObject { Key = "InventoryItemId", Value = iii!.Items.First().InventoryItemId.ToString() });
                }
                await Workflows.Products.UpdateAsync(product, ServiceContextProfileName).ConfigureAwait(false);
            }
            return CEFAR.PassingCEFAR();
        }

        public async Task<CEFActionResponse> Post(ProductBrandAndCategorySetup request)
        {
            if (string.IsNullOrWhiteSpace(request.CategoryString) || string.IsNullOrWhiteSpace(request.ItemNumber))
            {
                return CEFAR.FailingCEFAR("ERROR! Not all data was present in the request.");
            }
            using var context = RegistryLoaderWrapper.GetContext(null);
            var product = await Workflows.Products.GetAsync(
                    key: request.ItemNumber!,
                    context: context,
                    isVendorAdmin: false,
                    vendorAdminID: null)
                .ConfigureAwait(false);
            if (product is null)
            {
                return CEFAR.FailingCEFAR($"ERROR! Could not find product with key: {request.ItemNumber}");
            }
            var rawCategories = request!.CategoryString!.Split(',').ToList();
            (var brand, var categoryInfo) = await EnsureCategoriesAndBrandsExistAsync(
                    brand: rawCategories.First().ToLower().Trim() == "medsurg" ? "medsurg" : "ems",
                    categories: rawCategories.Select(c => c.Trim()).ToList(),
                    contextProfileName: context.ContextProfileName)
                .ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(brand.name) && brand.id == null)
            {
                return CEFAR.FailingCEFAR($"ERROR! Could not find brand for `{product.CustomKey}`");
            }
            if (categoryInfo is null)
            {
                return CEFAR.FailingCEFAR($"ERROR! Could not resolve categories for `{product.CustomKey}`");
            }
            product.Brands = new()
            {
                new BrandProductModel
                {
                    CustomKey = $"{brand.id}|{product.ID}",
                    CreatedDate = DateExtensions.GenDateTime,
                    MasterID = (int)brand.id!,
                    SlaveID = product.ID,
                    SlaveKey = product.CustomKey,
                    Active = true,
                    IsVisibleIn = true,
                },
            };
            product.Categories = new(
                categoryInfo.Select(cat => new ProductCategoryModel
                {
                    Active = true,
                    CustomKey = $"{product.ID}|{cat}",
                    MasterID = product.ID,
                    SlaveID = (int)cat!,
                }));
            var res = await Workflows.Products.UpdateAsync(product, context).ConfigureAwait(false);
            return CEFAR.BoolToCEFAR(res.ActionSucceeded, $"ERROR! Could not update `{request.ItemNumber}` with brand/categories");
        }

        public async Task<CEFActionResponse> Post(UpdatePrimaryUOM request)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var queryParams = CreateQueryParams(onlyData: true, fields: new string[] { "UOMCode" }, fieldQuery: ("UOM", request.UOM!));
            var res = await GetResponseAsync<UOMRespsonse>($"{JBMConfig.JBMSalesAPI}/unitsOfMeasure", queryParams).ConfigureAwait(false);
            var uomCode = res?.items.FirstOrDefault()?.UOMCode;
            if (!Contract.CheckNotNull(uomCode))
            {
                return CEFAR.FailingCEFAR();
            }
            var product = await Workflows.Products.GetAsync(request.ProductKey!, context).ConfigureAwait(false);
            if (!Contract.CheckNotNull(product))
            {
                return CEFAR.FailingCEFAR();
            }
            product = JBMWorkflow.AddOrUpdatePrimaryUnitOfMeasure(product!, uomCode!);
            var updateRes = await Workflows.Products.UpdateAsync(product, context).ConfigureAwait(false);
            return updateRes.ActionSucceeded
                ? CEFAR.PassingCEFAR()
                : CEFAR.FailingCEFAR();
        }

        public async Task<CEFActionResponse> Post(AddOrUpdateProductUOMs request)
        {
            using var context = RegistryLoaderWrapper.GetContext(null);
            var product = await Workflows.Products.GetAsync(request.ProductKey!, context.ContextProfileName).ConfigureAwait(false);
            if (Contract.CheckNull(product))
            {
                return CEFAR.FailingCEFAR();
            }
            if (Contract.CheckAnyInvalidKey(request.UOMCode, request.UOMConversion))
            {
                return CEFAR.FailingCEFAR();
            }
            product = JBMWorkflow.AddOrUpdateAvailableUnitsOfMeasure(product!, request.UOMCode!, request.UOMConversion!, request.UOMName);
            var updateRes = await Workflows.Products.UpdateAsync(product, context).ConfigureAwait(false);
            return updateRes.ActionSucceeded
                ? CEFAR.PassingCEFAR()
                : CEFAR.FailingCEFAR();
        }

     /*   public async Task<object?> Post(UpdateInventory _)
        {
            // Pull digest of all CustomKeys in Products
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var itemKeys = await context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterProductsByIsVisible(true)
                .Select(x => new
                {
                    x.ID,
                    x.CustomKey,
                    x.StockQuantity,
                })
                .ToListAsync()
                .ConfigureAwait(false);
            // Create ConcurrentDictionary<string, int> to hold the inventory for each item.
            ConcurrentDictionary<string, decimal> inventory = new ConcurrentDictionary<string, decimal>();
            List<Task> itemQueries = new List<Task>();
            itemKeys.Select(x => inventory.TryAdd(x.CustomKey!, 0m));
            // Populate dictionary with parallel loop calling the invetory endpoint filtered using the custom key of the item.
            foreach (var key in itemKeys.Select(x => x.CustomKey))
            {
                itemQueries.Add(Task.Run(() =>
                {
                    try
                    {
                        inventory[key!] = GetResponseAsync<InventoryItems>(
                        resource: "inventoryOnhandBalances",
                        queryParams: CreateQueryParams(onlyData: true, fieldQuery: ("ItemNumber", System.Web.HttpUtility.UrlEncode(key!))),
                        overrideUrl: $"{JBMConfig.JBMFusionBaseURL}/{JBMConfig.JBMSalesAPI}").Result?.Items.Sum(x => x.PrimaryQuantity) ?? 0m;
                    }
                    catch (Exception)
                    {
                        inventory[key!] = 0m;
                    }
                }));
            }
            Task t = Task.WhenAll(itemQueries);
            t.Wait();
            // Update the StockQuanity of each item using EF.
            var updatedDate = DateExtensions.GenDateTime;
            if (t.Status == TaskStatus.RanToCompletion)
            {
                foreach (var item in itemKeys)
                {
                    var currentInventory = inventory[item.CustomKey!];
                    if (currentInventory == item.StockQuantity)
                    {
                        continue;
                    }
                    var product = new Product();
                    product.ID = item.ID;
                    context.Products.Attach(product);
                    product.StockQuantity = inventory[item.CustomKey!];
                    product.UpdatedDate = updatedDate;
                    context.Entry(product).Property(y => y.StockQuantity).IsModified = true;
                    context.Entry(product).Property(y => y.UpdatedDate).IsModified = true;
                }
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            return CEFAR.PassingCEFAR();
        }
*/
        private async Task<((string? name, int? id) brand, List<int?>? categories)> EnsureCategoriesAndBrandsExistAsync(
            string brand,
            List<string> categories,
            string? contextProfileName)
        {
            var foundBrand = await Workflows.Brands.GetByNameAsync(brand, contextProfileName).ConfigureAwait(false);
            if (foundBrand is null)
            {
                return ((null, null), null);
            }
            if (foundBrand is not null && Contract.CheckEmpty(categories))
            {
                return ((brand, foundBrand?.ID), null);
            }
            var upsertedCategories = new List<int?>();
            int? masterCategoryID = null;
            if (categories is null)
            {
                return ((brand, foundBrand?.ID), null);
            }
            foreach (var cat in categories)
            {
                ICategoryModel newOrExisting = new CategoryModel
                {
                    Active = true,
                    CustomKey = cat,
                    ParentID = masterCategoryID is not null ? masterCategoryID : null,
                    IncludeInMenu = true,
                    IsVisible = true,
                    Description = cat,
                    DisplayName = cat,
                    HasChildren = categories.FindIndex(x => x == cat) + 1 < categories.Count ? true : false,
                    Name = cat,
                    TypeID = 1,
                    SeoUrl = cat.ToLower().RemoveAllWhiteSpace().Replace('/', '-').Replace('&', '-'),
                };
                try
                {
                    var categoryResponse = await Workflows.Categories.ResolveWithAutoGenerateToIDAsync(
                            byID: null,
                            byKey: newOrExisting.CustomKey,
                            model: newOrExisting,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    masterCategoryID = categoryResponse;
                    upsertedCategories.Add(categoryResponse);
                }
                catch (Exception)
                {
                    return ((null, null), null);
                }
            }
            await AddCategoriesToBrandAsync(foundBrand!, upsertedCategories, contextProfileName).ConfigureAwait(false);
            return ((brand, foundBrand?.ID), upsertedCategories);
        }

        private async Task AddCategoriesToBrandAsync(
            IBrandModel brand,
            List<int?> upsertedCategories,
            string? contextProfileName)
        {
            foreach (var c in upsertedCategories)
            {
                var brandCategory = new BrandCategoryModel
                {
                    Active = true,
                    CustomKey = $"{brand.ID}|{c}",
                    IsVisibleIn = true,
                    MasterID = brand.ID,
                    SlaveID = (int)c!,
                };
                await Workflows.BrandCategories.ResolveWithAutoGenerateAsync(
                        byID: null,
                        byKey: brandCategory.CustomKey,
                        model: brandCategory,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
        }
    }
}