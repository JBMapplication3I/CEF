// <copyright file="PricingFactory.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pricing factory class</summary>
#if NET5_0_OR_GREATER
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Clarity.Ecommerce.Testing.Net5")]
#endif

namespace Clarity.Ecommerce.Providers.Pricing
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Pricing;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>A pricing factory.</summary>
    /// <seealso cref="IPricingFactory"/>
    public class PricingFactory : IPricingFactory
    {
        /// <summary>The pricing provider.</summary>
        private IPricingProviderBase? pricingProvider;

        /// <inheritdoc/>
        public IPricingFactoryContextModel DefaultPricingFactoryContext
        {
            get
            {
                var model = RegistryLoaderWrapper.GetInstance<IPricingFactoryContextModel>();
                model.Quantity = 1;
                model.PricePoint = Settings.DefaultPricePointKey;
                model.UserRoles = new();
                return model;
            }
        }

        /// <inheritdoc/>
        public IPricingFactorySettings Settings { get; }
            = RegistryLoaderWrapper.GetInstance<IPricingFactorySettings>();

        /// <summary>Gets the logger.</summary>
        /// <value>The logger.</value>
        private static ILogger Logger { get; }
            = RegistryLoaderWrapper.GetInstance<ILogger>();

        /// <summary>Gets the pricing provider.</summary>
        /// <value>The pricing provider.</value>
        private IPricingProviderBase PricingProvider
        {
            get
            {
                pricingProvider ??= RegistryLoaderWrapper.GetPricingProvider(contextProfileName: null);
                if (pricingProvider == null)
                {
                    // ReSharper disable once ThrowExceptionInUnexpectedLocation, UnthrowableException
                    throw new NullReferenceException(
                        "Error: No PricingProvider is available. Please check that a valid PricingProvider is in the"
                        + " plugins folder and that the path to the plugins folder has been configured."
                        + " Clarity.Providers.PluginsPath");
                }
                return pricingProvider;
            }
        }

        /// <inheritdoc/>
        public virtual async Task<ICalculatedPrice> CalculatePriceAsync(
            int productID,
            SerializableAttributesDictionary? salesItemAttributes,
            IPricingFactoryContextModel? pricingFactoryContext,
            string? contextProfileName,
            IPricingProviderBase? pricingProviderOverride = null,
            bool forceNoCache = false,
            bool? forCart = false)
        {
            return await CalculatePriceAsync(
                    pricingFactoryProduct: await GetPricingFactoryProductAsync(
                            productID: productID,
                            cartItemAttributes: salesItemAttributes,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false),
                    pricingFactoryContext: pricingFactoryContext,
                    contextProfileName: contextProfileName,
                    pricingProviderOverride: pricingProviderOverride,
                    forceNoCache: forceNoCache,
                    forCart: forCart)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task RemoveAllCachedPricesByProductIDAsync(int productID, string? contextProfileName)
        {
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName).ConfigureAwait(false);
            if (client is null)
            {
                return;
            }
            await client.RemoveByPatternAsync($":*PricingProvider:P={productID},*").ConfigureAwait(false);
        }

        /// <param name="contextProfileName"></param>
        /// <inheritdoc/>
        public virtual async Task RemoveAllCachedPricesAsync(string? contextProfileName)
        {
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName).ConfigureAwait(false);
            if (client is null)
            {
                return;
            }
            await client.RemoveByPatternAsync("*PricingProvider:*").ConfigureAwait(false);
        }

        /// <summary>Gets pricing factory context.</summary>
        /// <param name="quantity">          The quantity.</param>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="pricePoint">        The price point.</param>
        /// <param name="storeID">           Identifier for the store.</param>
        /// <param name="franchiseID">       Identifier for the franchise.</param>
        /// <param name="brandID">           Identifier for the brand.</param>
        /// <param name="currencyID">        Identifier for the currency.</param>
        /// <param name="sessionID">         Identifier for the session.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The pricing factory context.</returns>
        internal static IPricingFactoryContextModel GetPricingFactoryContext(
            decimal? quantity,
            int? accountID,
            int? userID,
            string pricePoint,
            int? storeID,
            int? franchiseID,
            int? brandID,
            int? currencyID,
            Guid? sessionID,
            string? contextProfileName)
        {
            var model = RegistryLoaderWrapper.GetInstance<IPricingFactoryContextModel>(contextProfileName);
            model.Quantity = quantity ?? 1;
            model.PricePoint = pricePoint;
            model.SessionID = sessionID ?? default;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            if (Contract.CheckValidID(userID))
            {
                var user = context.Users
                    .AsNoTracking()
                    .FilterByID(userID!.Value)
                    .Select(x => new
                    {
                        x.ID,
                        x.CustomKey,
                        x.AccountID,
                        Roles = x.Roles.Select(y => y.Role!.Name),
                        CountryID = x.Contact != null && x.Contact.Address != null
                            ? x.Contact.Address.CountryID
                            : null,
                    })
                    .Single();
                model.UserID = user.ID;
                model.UserKey = user.CustomKey;
                model.UserRoles = user.Roles.ToList();
                model.CountryID = user.CountryID;
                if (!Contract.CheckValidID(accountID) && user.AccountID != null)
                {
                    accountID = user.AccountID;
                }
            }
            if (Contract.CheckValidID(accountID))
            {
                var account = context.Accounts
                    .AsNoTracking()
                    .FilterByID(accountID!.Value)
                    .Select(x => new { x.ID, x.CustomKey, x.TypeID })
                    .Single();
                model.AccountID = account.ID;
                model.AccountKey = account.CustomKey;
                model.AccountTypeID = account.TypeID;
            }
            if (Contract.CheckValidID(currencyID))
            {
                var currency = context.Currencies
                    .AsNoTracking()
                    .FilterByID(currencyID!.Value)
                    .Select(x => new { x.ID, x.CustomKey })
                    .Single();
                model.CurrencyID = currency.ID;
                model.CurrencyKey = currency.CustomKey;
            }
            if (Contract.CheckValidID(storeID))
            {
                var store = context.Stores
                    .AsNoTracking()
                    .FilterByID(storeID!.Value)
                    .Select(x => new { x.ID, x.CustomKey })
                    .Single();
                model.StoreID = store.ID;
                model.StoreKey = store.CustomKey;
            }
            if (Contract.CheckValidID(franchiseID))
            {
                var franchise = context.Franchises
                    .AsNoTracking()
                    .FilterByID(franchiseID!.Value)
                    .Select(x => new { x.ID, x.CustomKey })
                    .Single();
                model.FranchiseID = franchise.ID;
                model.FranchiseKey = franchise.CustomKey;
            }
            // ReSharper disable once InvertIf
            if (Contract.CheckValidID(brandID))
            {
                var brand = context.Brands
                    .AsNoTracking()
                    .FilterByID(brandID!.Value)
                    .Select(x => new { x.ID, x.CustomKey })
                    .Single();
                model.BrandID = brand.ID;
                model.BrandKey = brand.CustomKey;
            }
            return model;
        }

        /// <summary>Calculates the price.</summary>
        /// <param name="pricingFactoryProduct">  The pricing factory product.</param>
        /// <param name="pricingFactoryContext">  Context for the pricing factory.</param>
        /// <param name="contextProfileName">     Name of the context profile.</param>
        /// <param name="pricingProviderOverride">The pricing provider override.</param>
        /// <param name="forceNoCache">           True to force no cache.</param>
        /// <param name="forCart">                Flag for cart item calculation.</param>
        /// <returns>The calculated price.</returns>
        // ReSharper disable once CyclomaticComplexity
        protected virtual async Task<ICalculatedPrice> CalculatePriceAsync(
            IPricingFactoryProductModel pricingFactoryProduct,
            IPricingFactoryContextModel? pricingFactoryContext,
            string? contextProfileName,
            IPricingProviderBase? pricingProviderOverride = null,
            bool forceNoCache = false,
            bool? forCart = false)
        {
            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            Contract.RequiresNotNull(pricingFactoryProduct, "pricingFactoryProduct is required");
            Contract.RequiresNotNull(pricingFactoryContext, "pricingFactoryContext is required");
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed
            if (!Contract.CheckValidKey(pricingFactoryContext!.PricePoint))
            {
                pricingFactoryContext.PricePoint = Settings.DefaultPricePointKey;
            }
            if (!Contract.CheckValidKey(pricingFactoryContext.PricePoint))
            {
                pricingFactoryContext.PricePoint = "WEB";
            }
            if (!Contract.CheckValidIDOrAnyValidKey(pricingFactoryContext.StoreID, pricingFactoryContext.StoreKey)
                && pricingFactoryProduct.CartItemSerializableAttributes?.ContainsKey("SelectedStoreID") == true
                && int.TryParse(pricingFactoryProduct.CartItemSerializableAttributes["SelectedStoreID"].Value, out var storeID))
            {
                pricingFactoryContext.StoreID = storeID;
            }
            if (!Contract.CheckValidIDOrAnyValidKey(pricingFactoryContext.FranchiseID, pricingFactoryContext.FranchiseKey)
                && pricingFactoryProduct.CartItemSerializableAttributes?.ContainsKey("SelectedFranchiseID") == true
                && int.TryParse(pricingFactoryProduct.CartItemSerializableAttributes["SelectedFranchiseID"].Value, out var franchiseID))
            {
                pricingFactoryContext.FranchiseID = franchiseID;
            }
            if (!Contract.CheckValidIDOrAnyValidKey(pricingFactoryContext.BrandID, pricingFactoryContext.BrandKey)
                && pricingFactoryProduct.CartItemSerializableAttributes?.ContainsKey("SelectedBrandID") == true
                && int.TryParse(pricingFactoryProduct.CartItemSerializableAttributes["SelectedBrandID"].Value, out var brandID))
            {
                pricingFactoryContext.BrandID = brandID;
            }
            ICalculatedPrice? calculatedPrice;
            // ReSharper disable once StyleCop.SA1008
            var providerPriceKey = await (pricingProviderOverride ?? PricingProvider)
                .GetPriceKeyAsync(pricingFactoryProduct, pricingFactoryContext)
                .ConfigureAwait(false);
            var usePriceCache = !string.IsNullOrWhiteSpace(providerPriceKey)
                && CEFConfigDictionary.CachingTimeoutTimeSpan >= TimeSpan.Zero
                && !forceNoCache;
            var priceCacheKey = providerPriceKey;
            if (usePriceCache)
            {
                calculatedPrice = await GetCachedPriceAsync(priceCacheKey, contextProfileName).ConfigureAwait(false);
                if (calculatedPrice != null && calculatedPrice.BasePrice != -1)
                {
                    return calculatedPrice;
                }
            }
            var provider = pricingProviderOverride ?? PricingProvider;
            calculatedPrice = await provider.CalculatePriceAsync(pricingFactoryProduct, pricingFactoryContext, contextProfileName, forCart).ConfigureAwait(false);
            if (calculatedPrice != null && string.IsNullOrEmpty(calculatedPrice.PricingProvider))
            {
                calculatedPrice.PricingProvider = provider.Name;
            }
            if ((calculatedPrice?.IsValid ?? false) && usePriceCache)
            {
                await AddCachedPriceAsync(priceCacheKey, (CalculatedPrice)calculatedPrice).ConfigureAwait(false);
            }
            return calculatedPrice ?? new CalculatedPrice(provider.Name, -1);
        }

        /// <summary>Gets cached price.</summary>
        /// <param name="priceCacheKey">     The price cache key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The cached price.</returns>
        protected virtual async Task<CalculatedPrice?> GetCachedPriceAsync(string priceCacheKey, string? contextProfileName)
        {
            try
            {
                var client = await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName).ConfigureAwait(false);
                if (client is null)
                {
                    return null;
                }
                return await client.GetAsync<CalculatedPrice?>(priceCacheKey).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(PricingFactory)}.{nameof(GetCachedPriceAsync)}.{ex.GetType().Name}",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return null;
            }
        }

        /// <summary>Adds a cached price to 'calculatedPrice'.</summary>
        /// <param name="priceCacheKey">  The price cache key.</param>
        /// <param name="calculatedPrice">The calculated price.</param>
        /// <returns>A Task.</returns>
        protected virtual async Task AddCachedPriceAsync(string priceCacheKey, CalculatedPrice calculatedPrice)
        {
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName: null).ConfigureAwait(false);
            if (client is null)
            {
                return;
            }
            await client.AddAsync(
                    key: priceCacheKey,
                    obj: calculatedPrice,
                    usePrefix: true,
                    timeToLive: CEFConfigDictionary.CachingTimeoutTimeSpan)
                .ConfigureAwait(false);
        }

        /// <summary>Removes the cached price described by priceCacheKey.</summary>
        /// <param name="priceCacheKey">The price cache key.</param>
        /// <returns>A Task.</returns>
        protected virtual async Task RemoveCachedPriceAsync(string priceCacheKey)
        {
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName: null).ConfigureAwait(false);
            if (client is null)
            {
                return;
            }
            await client.RemoveAsync(priceCacheKey).ConfigureAwait(false);
        }

        /// <summary>Gets pricing factory product.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="cartItemAttributes">The cart item attributes.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The pricing factory product.</returns>
        private static async Task<IPricingFactoryProductModel> GetPricingFactoryProductAsync(
            int productID,
            SerializableAttributesDictionary? cartItemAttributes,
            string? contextProfileName)
        {
            // TODO@JTG: Research putting these loaded products into an expiring memory cache and marry the
            // cartItemAttributes to the object outside of this function
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            IPricingFactoryProductModel retVal = (await Contract.RequiresNotNull(context.Products)
                    .AsNoTracking()
                    .FilterByID(productID)
                    .Select(product => new
                    {
                        product.ID,
                        product.TypeID,
                        product.CustomKey,
                        product.UnitOfMeasure,
                        product.PriceBase,
                        product.PriceSale,
                        product.KitBaseQuantityPriceMultiplier,
                        product.JsonAttributes,
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(product => new PricingFactoryProductModel
                {
                    ProductID = product.ID,
                    ProductTypeID = product.TypeID,
                    ProductKey = product.CustomKey,
                    ProductUnitOfMeasure = product.UnitOfMeasure ?? "EACH",
                    PriceBase = product.PriceBase,
                    PriceSale = product.PriceSale,
                    KitBaseQuantityPriceMultiplier = product.KitBaseQuantityPriceMultiplier,
                    SerializableAttributes = product.JsonAttributes.DeserializeAttributesDictionary(),
                    CartItemSerializableAttributes = cartItemAttributes,
                })
                .Single();
            if (CEFConfigDictionary.BrandsEnabled)
            {
                retVal.BrandIDs = await context.BrandProducts
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterIAmARelationshipTableBySlaveID<BrandProduct, Brand, Product>(productID)
                    .Select(c => c.MasterID)
                    .Distinct()
                    .ToListAsync()
                    .ConfigureAwait(false);
            }
            if (CEFConfigDictionary.CategoriesEnabled)
            {
                retVal.CategoryIDs = await context.ProductCategories
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterIAmARelationshipTableByMasterID<ProductCategory, Product, Category>(productID)
                    .Select(c => c.SlaveID)
                    .Distinct()
                    .ToListAsync()
                    .ConfigureAwait(false);
            }
            if (CEFConfigDictionary.FranchisesEnabled)
            {
                retVal.FranchiseIDs = await context.FranchiseProducts
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterIAmARelationshipTableBySlaveID<FranchiseProduct, Franchise, Product>(productID)
                    .Select(c => c.MasterID)
                    .Distinct()
                    .ToListAsync()
                    .ConfigureAwait(false);
            }
            if (CEFConfigDictionary.ManufacturersEnabled)
            {
                retVal.ManufacturerIDs = await context.ManufacturerProducts
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterIAmARelationshipTableBySlaveID<ManufacturerProduct, Manufacturer, Product>(productID)
                    .Select(c => c.MasterID)
                    .Distinct()
                    .ToListAsync()
                    .ConfigureAwait(false);
            }
            if (CEFConfigDictionary.StoresEnabled)
            {
                retVal.StoreIDs = await context.StoreProducts
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterIAmARelationshipTableBySlaveID<StoreProduct, Store, Product>(productID)
                    .Select(c => c.MasterID)
                    .Distinct()
                    .ToListAsync()
                    .ConfigureAwait(false);
            }
            if (CEFConfigDictionary.VendorsEnabled)
            {
                retVal.VendorIDs = await context.VendorProducts
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterIAmARelationshipTableBySlaveID<VendorProduct, Vendor, Product>(productID)
                    .Select(c => c.MasterID)
                    .Distinct()
                    .ToListAsync()
                    .ConfigureAwait(false);
            }
            return retVal;
        }
    }
}
