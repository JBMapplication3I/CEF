// <copyright file="CartValidator.Products.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart validator class</summary>
namespace Clarity.Ecommerce.Providers.CartValidation
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>A cart validator.</summary>
    public partial class CartValidator
    {
        /// <summary>Check For Store Restrictions in the cart</summary>
        /// <param name="productID">         The product ID to check.</param>
        /// <param name="typeName">          The type of the cart.</param>
        /// <param name="sessID">            The Guid session ID.</param>
        /// <param name="pricingContext">    The pricing context.</param>
        /// <param name="taxesProvider">     The taxes provider.</param>
        /// <param name="contextProfileName">The context profile name.</param>
        /// <returns>True if product from store in cart, False if not.</returns>
        public static async Task<bool> CheckMultipleStoresInCartAsync(
            int? productID,
            string typeName,
            Guid sessID,
            IPricingFactoryContextModel pricingContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            var (cartResponse, _) = await Workflows.Carts.SessionGetAsync(
                    new SessionCartBySessionAndTypeLookupKey(sessID, typeName, pricingContext.UserID, pricingContext.AccountID, pricingContext.BrandID, pricingContext.FranchiseID, pricingContext.StoreID),
                    pricingFactoryContext: pricingContext,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            // Could not find a cart
            if (!cartResponse.ActionSucceeded && !cartResponse.Messages.Any(x => x.Contains("ERROR! This cart doesn't have any sales items and will be removed.")))
            {
                return false;
            }
            else if (!cartResponse.ActionSucceeded && cartResponse.Messages.Any(x => x.Contains("ERROR! This cart doesn't have any sales items and will be removed.")))
            {
                return true;
            }
            var currentCart = cartResponse.Result;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var storeIDs = new List<int>();
            // Retrieve the store ID(s)
            foreach (var item in currentCart!.SalesItems!)
            {
                storeIDs.Add(await context.StoreProducts
                    .AsNoTracking()
                    .Where(x => x.SlaveID == item.ProductID)
                    .Select(y => y.MasterID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false));
            }
            storeIDs = storeIDs.Distinct().ToList();
            var tempStoreForProduct = await context.StoreProducts
                .AsNoTracking()
                .Where(x => x.SlaveID == productID)
                .Select(x => x.MasterID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (storeIDs.Count > 1 || tempStoreForProduct != storeIDs[0])
            {
                return false;
            }
            return true;
        }

        /// <summary>Check cart total will not exceed the user's limit.</summary>
        /// <param name="quantity">          The requested quantity to add.</param>
        /// <param name="price">             The price per unit of measure.</param>
        /// <param name="typeName">          The type of the cart.</param>
        /// <param name="sessID">            The Guid session ID.</param>
        /// <param name="pricingContext">    The pricing context.</param>
        /// <param name="taxesProvider">     The taxes provider.</param>
        /// <param name="contextProfileName">The context profile name.</param>
        /// <returns>True if under limit, False if not.</returns>
        public static async Task<bool> CheckCartTotalUnderUserPurchasingLimitAsync(
            decimal? quantity,
            decimal? price,
            string typeName,
            Guid sessID,
            IPricingFactoryContextModel pricingContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(null);
            var cartInfo = await context.Carts
                .FilterCartsBySessionID(sessID)
                .Select(x => new
                {
                    x.UserID,
                    x.SubtotalItems,
                    x.SalesItems,
                })
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (!Contract.CheckNotNull(cartInfo))
            {
                return false;
            }
            var user = await context.Users
                .FilterByID(cartInfo.UserID)
                .Select(x => new { x.ID, x.JsonAttributes })
                .SingleAsync()
                .ConfigureAwait(false);
            var cartTotal = price * quantity;
            if (cartInfo.SalesItems.Any())
            {
                foreach (CartItem item in cartInfo.SalesItems!)
                {
                    cartTotal += item.UnitCorePrice * item.Quantity;
                }
            }
            decimal orderLimit;
            if (user!.JsonAttributes.DeserializeAttributesDictionary().TryGetValue("OrderAmountLimit", out var limit))
            {
                if (!Contract.CheckNotNull(limit))
                {
                    return true;
                }
                orderLimit = decimal.Parse(limit.Value);
                if (cartTotal > orderLimit)
                {
                    return false;
                }
                var result = await Workflows.SalesOrders.SearchAsync(
                        search: new SalesOrderSearchModel
                        {
                            Active = true,
                            HasSalesGroupAsMaster = true,
                            MinUpdatedOrCreatedDate = DateTime.Now.StripTime(),
                        },
                        asListing: true,
                        contextProfileName: context.ContextProfileName)
                    .ConfigureAwait(false);
                if (result.totalCount > 0 && cartTotal + result.results!.Select(x => x.Totals?.SubTotal ?? 0m).Sum() > orderLimit)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>Check For Store Restrictions in the cart</summary>
        /// <param name="productID">         The product ID to check.</param>
        /// <param name="sessID">            The Guid session ID.</param>
        /// <param name="contextProfileName">The context profile name.</param>
        /// <param name="typeName">          The cart type.</param>
        /// <returns>True if product from store in cart, False if not.</returns>
        public static async Task<bool> CheckProductRequiresLicenseAndValidateAccountLicenseExpirationDate(
            int? productID,
            Guid sessID,
            string? typeName,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var rawProductAttributes = await context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByID(productID)
                .Select(x => x.JsonAttributes)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            var productAttributes = rawProductAttributes.DeserializeAttributesDictionary();
            if (productAttributes.TryGetValue("PrescriptionDrug", out var prescriptionDrug)
                || productAttributes.TryGetValue("PrescriptionDevice", out var prescriptionDevice)
                && Contract.CheckAnyValidKey(prescriptionDrug?.Value, prescriptionDevice?.Value)
                && (prescriptionDrug?.Value?.ToLower() == "y" || prescriptionDevice?.Value?.ToLower() == "y"))
            {
                var accountID = await context.Carts
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterCartsBySessionID(sessID)
                    .FilterByTypeName<Cart, CartType>(typeName)
                    .Select(x => x.AccountID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
                if (Contract.CheckInvalidID(accountID))
                {
                    return false;
                }
                var account = (await context.Carts
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterCartsBySessionID(sessID)
                        .Select(x => new
                        {
                            x.Account!.MedicalLicenseNumber,
                            x.Account!.JsonAttributes,
                        })
                        .ToListAsync()
                    .ConfigureAwait(false))
                    .Select(x => new Account
                    {
                        MedicalLicenseNumber = x.MedicalLicenseNumber,
                        JsonAttributes = x.JsonAttributes,
                    })
                    .FirstOrDefault();
                var accountAttributes = account.JsonAttributes.DeserializeAttributesDictionary();
                if (Contract.CheckAllValidKeys(account.MedicalLicenseNumber, account.JsonAttributes)
                    && accountAttributes.TryGetValue("licenseExpiry", out var expiration)
                    && (DateTime.Parse(expiration.Value) > DateExtensions.GenDateTime.AddDays(-1)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>Check For SKU Restrictions.</summary>
        /// <param name="response">    The response.</param>
        /// <param name="salesItems">  The list of sales items.</param>
        /// <param name="addressModel">Address to validate the SKU restrictions against.</param>
        /// <returns>True if it changed the cart, false if it doesn't.</returns>
        protected virtual async Task<bool> CheckForSKURestrictionsAsync(
            CEFActionResponse response,
            IEnumerable<ISalesItemBaseModel> salesItems,
            IAddressModel? addressModel)
        {
            // Check for SKU restrictions
            var skuRestrictionsError = false;
            foreach (var cartSalesItem in salesItems)
            {
                if (cartSalesItem == null)
                {
                    continue;
                }
                if (!await Workflows.Products.IsShippingRestrictedAsync(
                            cartSalesItem.ProductSerializableAttributes,
                            addressModel?.RegionCode,
                            addressModel?.City)
                        .ConfigureAwait(false))
                {
                    continue;
                }
                skuRestrictionsError = true;
                // Set the cart's Product quantity to 0
                cartSalesItem.Quantity = 0m;
                cartSalesItem.QuantityBackOrdered = 0m;
                cartSalesItem.QuantityPreSold = 0m;
            }
            if (!skuRestrictionsError)
            {
                return false;
            }
            response.Messages.Add("ERROR! Some products have shipping restrictions.");
            return true;
        }

        /// <summary>Check for product role restrictions.</summary>
        /// <param name="response">          The response.</param>
        /// <param name="currentUserID">     Identifier for the current user.</param>
        /// <param name="salesItems">        The sales items.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it changed the cart, false if it doesn't.</returns>
        protected virtual async Task<bool> CheckForProductRoleRestrictionsAsync(
            CEFActionResponse response,
            int? currentUserID,
            IEnumerable<ISalesItemBaseModel> salesItems,
            string? contextProfileName)
        {
            // Check for product role restrictions
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var productsRequiresRolesError = false;
            var user = await context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ID == currentUserID)
                .ConfigureAwait(false);
            var roles = user?.Roles.Select(x => x.Role!.Name).ToList();
            foreach (var cartSalesItem in salesItems)
            {
                if (string.IsNullOrWhiteSpace(cartSalesItem.ProductRequiresRoles)
                        || roles?.Any(
                            x => cartSalesItem.ProductRequiresRoles!
                                .Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries)
                                    .Any(y => y.Trim() == x)) == true)
                {
                    continue;
                }
                productsRequiresRolesError = true;
                cartSalesItem.Quantity = 0m;
                cartSalesItem.QuantityBackOrdered = 0m;
                cartSalesItem.QuantityPreSold = 0m;
            }
            if (!productsRequiresRolesError)
            {
                return false;
            }
            response.Messages.Add("ERROR! Some products have role restrictions.");
            return true;
        }

        /// <summary>Check for product role restrictions alternate.</summary>
        /// <param name="response">          The response.</param>
        /// <param name="currentUserID">     Identifier for the current user.</param>
        /// <param name="salesItems">        The sales items.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it changed the cart, false if it doesn't.</returns>
        protected virtual async Task<bool> CheckForProductRoleRestrictionsAltAsync(
            CEFActionResponse response,
            int? currentUserID,
            IEnumerable<ISalesItemBaseModel> salesItems,
            string? contextProfileName)
        {
            // Check for product role restrictions
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var productsRequiresRolesError = false;
            var user = await context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.ID == currentUserID).ConfigureAwait(false);
            var roles = user?.Roles.Select(x => x.Role!.Name).ToList();
            foreach (var cartSalesItem in salesItems)
            {
                if (string.IsNullOrWhiteSpace(cartSalesItem.ProductRequiresRolesAlt)
                    || roles?.Any(
                           x => cartSalesItem.ProductRequiresRolesAlt!
                               .Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries)
                               .Any(y => y.Trim() == x)) == true)
                {
                    continue;
                }
                productsRequiresRolesError = true;
                cartSalesItem.Quantity = 0m;
                cartSalesItem.QuantityBackOrdered = 0m;
                cartSalesItem.QuantityPreSold = 0m;
            }
            if (!productsRequiresRolesError)
            {
                return false;
            }
            response.Messages.Add("ERROR! Some products have role restrictions (Alternate Check).");
            return true;
        }

        /// <summary>Process the products with must purchase multiples of amount.</summary>
        /// <param name="response">          The response.</param>
        /// <param name="cart">              The cart.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it changed the cart, false if it doesn't.</returns>
        protected virtual async Task<bool> ProcessProductsWithMustPurchaseMultiplesOfAmountAsync(
            CEFActionResponse response,
            ICartModel cart,
            string? contextProfileName)
        {
            if (!Config!.DoProductRestrictionsByMustPurchaseMultiplesOfAmount)
            {
                // SUCCESS! There are no Products with Multiples of Amount Restrictions
                return false;
            }
            // ReSharper disable once InconsistentNaming, StyleCop.SA1303
            const string kind = nameof(Product);
            var attrKey = $"MultiplesOf:IgnAcc:{kind}";
            var cacheKey = $"HardSoftStops:{attrKey}";
            var needsToBeMet = false;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName).ConfigureAwait(false);
            var linkHashesProducts = client is null ? null : await client.GetAsync<HashSet<int>>(cacheKey).ConfigureAwait(false);
            if (linkHashesProducts == null)
            {
                linkHashesProducts = new(
                    context.Products
                        .AsNoTracking()
                        .Where(x => x.Active && x.MustPurchaseInMultiplesOfAmount > 0m)
                        .OrderBy(x => x.ID)
                        .Select(x => x.ID));
                if (client is not null)
                {
                    await client.AddAsync(cacheKey, linkHashesProducts, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
                }
            }
            var requirementsToMeet = new Dictionary<int, Product>();
            foreach (var cartItem in cart.SalesItems!
                .Where(c => c.ProductID.HasValue
                    && linkHashesProducts.Contains(c.ProductID.Value)
                    && !requirementsToMeet.ContainsKey(c.ProductID.Value)))
            {
                var linkHashesStores = client is null ? null : await client.GetAsync<HashSet<(int?, string)>>($"{cacheKey}:{cartItem.ProductID!.Value}-Stores").ConfigureAwait(false);
                if (linkHashesStores == null)
                {
                    linkHashesStores = new(
                        (await context.StoreProducts
                            .AsNoTracking()
                            .Where(x => x.Active && x.SlaveID == cartItem.ProductID!.Value)
                            .OrderBy(x => x.SlaveID)
                            .Select(x => new { x.SlaveID, x.Slave!.Name }!)
                            .ToListAsync()
                            .ConfigureAwait(false))
                        .Select(x => ((int?)x.SlaveID, x.Name!)));
                    if (client is not null)
                    {
                        await client.AddAsync($"{cacheKey}:{cartItem.ProductID!.Value}-Stores", linkHashesStores, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
                    }
                }
                var requirementsInfo = (await context.Products
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterByID(cartItem.ProductID!.Value)
                        .Where(x => x.MustPurchaseInMultiplesOfAmount > 0)
                        .Take(1)
                        .Select(x => new
                        {
                            x.ID,
                            x.Name,
                            x.MustPurchaseInMultiplesOfAmount,
                            x.MustPurchaseInMultiplesOfAmountWarningMessage,
                            x.MustPurchaseInMultiplesOfAmountOverrideFee,
                            x.MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent,
                            x.MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage,
                            x.MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage,
                        }
                        !)
                        .ToListAsync()
                        .ConfigureAwait(false))
                    .Select(x => new Product
                    {
                        ID = x.ID,
                        Name = x.Name,
                        MustPurchaseInMultiplesOfAmount = x.MustPurchaseInMultiplesOfAmount,
                        MustPurchaseInMultiplesOfAmountWarningMessage = x.MustPurchaseInMultiplesOfAmountWarningMessage,
                        MustPurchaseInMultiplesOfAmountOverrideFee = x.MustPurchaseInMultiplesOfAmountOverrideFee,
                        MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent = x.MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent,
                        MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage = x.MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage,
                        MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage = x.MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage,
                    })
                    .SingleOrDefault();
                if (requirementsInfo == null)
                {
                    // Since time of cache, the requirement or product went away
                    continue;
                }
                needsToBeMet = true;
                requirementsToMeet[requirementsInfo.ID] = requirementsInfo;
            }
            if (!needsToBeMet)
            {
                // SUCCESS! There are no Products with Multiples of Amount Restrictions in the current cart
                return false;
            }
            foreach (var cartItem in cart.SalesItems!.Where(x => x.ProductID.HasValue && requirementsToMeet.ContainsKey(x.ProductID.Value)))
            {
                var quantity = cartItem.TotalQuantity;
                var summary = requirementsToMeet[cartItem.ProductID!.Value];
                var multiple = summary.MustPurchaseInMultiplesOfAmount;
                var modulus = quantity % multiple;
                if (modulus == 0)
                {
                    // Success! This product is already at the multiple
                    continue;
                }
                var overrideFee = summary.MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent
                    ? ((summary.MustPurchaseInMultiplesOfAmountOverrideFee ?? 0m) / 100).ToString("p0")
                    : (summary.MustPurchaseInMultiplesOfAmountOverrideFee ?? 0m).ToString("c2");
                var linkHashesStores = client is null ? null : await client.GetAsync<HashSet<(int?, string)>>($"{cacheKey}:{cartItem.ProductID.Value}-Stores").ConfigureAwait(false);
                if (linkHashesStores == null)
                {
                    linkHashesStores = new(
                        (await context.StoreProducts
                            .AsNoTracking()
                            .Where(x => x.Active && x.SlaveID == cartItem.ProductID.Value)
                            .OrderBy(x => x.SlaveID)
                            .Select(x => new { x.SlaveID, x.Slave!.Name }!)
                            .ToListAsync()
                            .ConfigureAwait(false))
                        .Select(x => ((int?)x.SlaveID, x.Name!)));
                    if (client is not null)
                    {
                        await client.AddAsync($"{cacheKey}:{cartItem.ProductID.Value}-Stores", linkHashesStores, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
                    }
                }
                var (relatedStoreID, relatedStoreName) = linkHashesStores.Count > 0
                    ? linkHashesStores.First()
                    : (null, null);
                if (cart.SerializableAttributes!.ContainsKey($"{attrKey}-{cartItem.ProductID.Value}")
                    && cart.SerializableAttributes.TryGetValue($"{attrKey}-{cartItem.ProductID.Value}", out var ignoreValue)
                    && bool.TryParse(ignoreValue.Value, out var ignoreValueParsed)
                    && ignoreValueParsed)
                {
                    if (Contract.CheckValidKey(summary.MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage))
                    {
                        response.Messages.Add(
                            "WARNING! "
                            + DoReplacementsMinOrders(
                                warningMessage: summary.MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage!,
                                ownerID: cartItem.ProductID.Value,
                                ownerName: summary.Name,
                                attrKey: $"{attrKey}-{cartItem.ProductID.Value}",
                                requiredAmount: $"{multiple:n0}",
                                missingAmount: $"{multiple - modulus:n0}",
                                overrideFeeWarningMessage: summary.MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage,
                                overrideFee: overrideFee,
                                relatedBrandID: relatedStoreID,
                                relatedBrandName: relatedStoreName,
                                bufferCategoryName: null,
                                bufferCategorySeoUrl: null,
                                bufferItemName: null,
                                bufferItemSeoUrl: null));
                        continue;
                    }
                    response.Messages.Add(
                        $"WARNING! The purchase requirements for Products in the Cart for \"{summary.Name}\""
                        + $" doesn't meet the requirements set by the store administrators for {nameof(Product)}s"
                        + $" as they must be purchased in multiples of {multiple:n0}, however the Override Fee of"
                        + $" {overrideFee} has been accepted.");
                    continue;
                }
                if (!Contract.CheckValidKey(summary.MustPurchaseInMultiplesOfAmountWarningMessage))
                {
                    if (summary.MustPurchaseInMultiplesOfAmountOverrideFee >= 0m
                        && !Contract.CheckValidKey(summary.MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage))
                    {
                        response.Messages.Add(
                            $"ERROR! The purchase requirements for Products in the cart for \"{summary.Name}\""
                            + $" require being purchased in multiples of {multiple:n0}! However, an override"
                            + $" option is available for a fee of {overrideFee}.");
                        continue;
                    }
                    response.Messages.Add(
                        $"ERROR! The purchase requirements for {nameof(Product)} '{summary.Name}'"
                        + $" require being purchased in multiples of {multiple:n0}!");
                    continue;
                }
                response.Messages.Add(
                    "ERROR! "
                    + DoReplacementsMinOrders(
                        warningMessage: summary.MustPurchaseInMultiplesOfAmountWarningMessage!,
                        ownerID: cartItem.ProductID.Value,
                        ownerName: summary.Name,
                        attrKey: $"{attrKey}-{cartItem.ProductID.Value}",
                        requiredAmount: $"{multiple:n0}",
                        missingAmount: $"{multiple - modulus:n0}",
                        overrideFeeWarningMessage: summary.MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage,
                        overrideFee: overrideFee,
                        relatedBrandID: relatedStoreID,
                        relatedBrandName: relatedStoreName,
                        bufferCategoryName: null,
                        bufferCategorySeoUrl: null,
                        bufferItemName: null,
                        bufferItemSeoUrl: null));
            }

            return false;
        }

        /// <summary>Process the products with must purchase multiples of amount.</summary>
        /// <param name="response">          The response.</param>
        /// <param name="cart">              The cart.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it changed the cart, false if it doesn't.</returns>
        protected virtual async Task<bool> ProcessProductsWithDocumentRequiredForPurchaseAsync(
            CEFActionResponse response,
            ICartModel cart,
            int? userID,
            string? contextProfileName)
        {
            if (!Config!.DoProductRestrictionsByDocumentRequiredForPurchase)
            {
                // SUCCESS! There are no Products with Documents Required for Purchase
                return false;
            }
            // ReSharper disable once InconsistentNaming, StyleCop.SA1303
            const string kind = nameof(Product);
            var attrKey = $"DocReqd:IgnAcc:{kind}";
            var cacheKey = $"HardSoftStops:{attrKey}";
            var needsToBeMet = false;
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName).ConfigureAwait(false);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var linkHashesProducts = client is null ? null : await client.GetAsync<HashSet<int>>(cacheKey).ConfigureAwait(false);
            if (linkHashesProducts == null)
            {
                linkHashesProducts = new(
                    context.Products
                        .AsNoTracking()
                        .Where(x => x.Active && x.DocumentRequiredForPurchase != null && x.DocumentRequiredForPurchase != string.Empty)
                        .OrderBy(x => x.ID)
                        .Select(x => x.ID));
                if (client is not null)
                {
                    await client.AddAsync(cacheKey, linkHashesProducts, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
                }
            }
            var requirementsToMeet = new Dictionary<int, Product>();
            foreach (var cartItem in cart.SalesItems!
                .Where(c => c.ProductID.HasValue
                    && linkHashesProducts.Contains(c.ProductID.Value)
                    && !requirementsToMeet.ContainsKey(c.ProductID.Value)))
            {
                var linkHashesStores = client is null ? null : await client.GetAsync<HashSet<(int?, string)>>($"{cacheKey}:{cartItem.ProductID!.Value}-Stores").ConfigureAwait(false);
                if (linkHashesStores == null)
                {
                    linkHashesStores = new(
                        (await context.StoreProducts
                            .AsNoTracking()
                            .Where(x => x.Active && x.SlaveID == cartItem.ProductID!.Value)
                            .OrderBy(x => x.SlaveID)
                            .Select(x => new { x.SlaveID, x.Slave!.Name }!)
                            .ToListAsync()
                            .ConfigureAwait(false))
                        .Select(x => ((int?)x.SlaveID, x.Name!)));
                    if (client is not null)
                    {
                        await client.AddAsync($"{cacheKey}:{cartItem.ProductID!.Value}-Stores", linkHashesStores, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
                    }
                }
                var requirementsInfo = (await context.Products
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterByID(cartItem.ProductID!.Value)
                        .Where(x => x.DocumentRequiredForPurchase != null && x.DocumentRequiredForPurchase != string.Empty)
                        .Take(1)
                        .Select(x => new
                        {
                            x.ID,
                            x.Name,
                            x.DocumentRequiredForPurchase,
                            x.DocumentRequiredForPurchaseExpiredWarningMessage,
                            x.DocumentRequiredForPurchaseMissingWarningMessage,
                            x.DocumentRequiredForPurchaseOverrideFee,
                            x.DocumentRequiredForPurchaseOverrideFeeIsPercent,
                            x.DocumentRequiredForPurchaseOverrideFeeAcceptedMessage,
                            x.DocumentRequiredForPurchaseOverrideFeeWarningMessage,
                        }
                        !)
                        .ToListAsync()
                        .ConfigureAwait(false))
                    .Select(x => new Product
                    {
                        ID = x.ID,
                        Name = x.Name,
                        DocumentRequiredForPurchase = x.DocumentRequiredForPurchase,
                        DocumentRequiredForPurchaseExpiredWarningMessage = x.DocumentRequiredForPurchaseExpiredWarningMessage,
                        DocumentRequiredForPurchaseMissingWarningMessage = x.DocumentRequiredForPurchaseMissingWarningMessage,
                        DocumentRequiredForPurchaseOverrideFee = x.DocumentRequiredForPurchaseOverrideFee,
                        DocumentRequiredForPurchaseOverrideFeeIsPercent = x.DocumentRequiredForPurchaseOverrideFeeIsPercent,
                        DocumentRequiredForPurchaseOverrideFeeAcceptedMessage = x.DocumentRequiredForPurchaseOverrideFeeAcceptedMessage,
                        DocumentRequiredForPurchaseOverrideFeeWarningMessage = x.DocumentRequiredForPurchaseOverrideFeeWarningMessage,
                    })
                    .SingleOrDefault();
                if (requirementsInfo == null)
                {
                    // Since time of cache, the requirement or product went away
                    continue;
                }
                needsToBeMet = true;
                requirementsToMeet[requirementsInfo.ID] = requirementsInfo;
            }
            if (!needsToBeMet)
            {
                // SUCCESS! There are no Products with Multiples of Amount Restrictions in the current cart
                return false;
            }
            var timestamp = DateExtensions.GenDateTime;
            foreach (var cartItem in cart.SalesItems!.Where(x => x.ProductID.HasValue && requirementsToMeet.ContainsKey(x.ProductID.Value)))
            {
                var summary = requirementsToMeet[cartItem.ProductID!.Value];
                if (Contract.CheckInvalidID(userID))
                {
                    response.Messages.Add($"ERROR! Please log in to verify the ability to purchase {summary.Name}");
                    continue;
                }
                var hasRoleCurrently = context.RoleUsers
                    .AsNoTracking()
                    .Any(x => x.Role!.Name == summary.DocumentRequiredForPurchase
                        && (!x.EndDate.HasValue || x.EndDate.Value > timestamp));
                // TODO: Add account assigned roles here too
                if (hasRoleCurrently)
                {
                    // Success! This user already has the necessary Documents to purchase this product
                    continue;
                }
                var linkHashesStores = client is null ? null : await client.GetAsync<HashSet<(int?, string)>>($"{attrKey}-{cartItem.ProductID.Value}-Stores").ConfigureAwait(false);
                if (linkHashesStores == null)
                {
                    linkHashesStores = new(
                        (await context.StoreProducts
                            .AsNoTracking()
                            .Where(x => x.Active && x.SlaveID == cartItem.ProductID!.Value)
                            .OrderBy(x => x.SlaveID)
                            .Select(x => new { x.SlaveID, x.Slave!.Name }!)
                            .ToListAsync()
                            .ConfigureAwait(false))
                        .Select(x => ((int?)x.SlaveID, x.Name!)));
                    if (client is not null)
                    {
                        await client.AddAsync($"{cacheKey}:{cartItem.ProductID.Value}-Stores", linkHashesStores, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
                    }
                }
                var overrideFee = summary.DocumentRequiredForPurchaseOverrideFeeIsPercent
                    ? ((summary.DocumentRequiredForPurchaseOverrideFee ?? 0m) / 100).ToString("p0")
                    : (summary.DocumentRequiredForPurchaseOverrideFee ?? 0m).ToString("c2");
                var (relatedStoreID, relatedStoreName) = linkHashesStores.Count > 0
                    ? linkHashesStores.First()
                    : (null, null);
                if (cart.SerializableAttributes!.ContainsKey($"{attrKey}-{cartItem.ProductID.Value}")
                    && cart.SerializableAttributes.TryGetValue($"{attrKey}-{cartItem.ProductID.Value}", out var ignoreValue)
                    && bool.TryParse(ignoreValue.Value, out var ignoreValueParsed)
                    && ignoreValueParsed)
                {
                    if (Contract.CheckValidKey(summary.DocumentRequiredForPurchaseOverrideFeeAcceptedMessage))
                    {
                        response.Messages.Add(
                            "WARNING! "
                            + DoReplacementsMinOrders(
                                warningMessage: summary.DocumentRequiredForPurchaseOverrideFeeAcceptedMessage!,
                                ownerID: cartItem.ProductID.Value,
                                ownerName: summary.Name,
                                attrKey: $"{attrKey}-{cartItem.ProductID.Value}",
                                requiredAmount: "Document",
                                missingAmount: "Document",
                                overrideFeeWarningMessage: summary.DocumentRequiredForPurchaseOverrideFeeWarningMessage,
                                overrideFee: overrideFee,
                                relatedBrandID: relatedStoreID,
                                relatedBrandName: relatedStoreName,
                                bufferCategoryName: null,
                                bufferCategorySeoUrl: null,
                                bufferItemName: null,
                                bufferItemSeoUrl: null));
                        continue;
                    }
                    response.Messages.Add(
                        $"WARNING! The purchase requirements for Products in the Cart for \"{summary.Name}\""
                        + $" doesn't meet the requirements set by the store administrators for {nameof(Product)}s"
                        + $" as they require specific Documents, however the Override Fee of {overrideFee} has"
                        + " been accepted.");
                    continue;
                }
                var warningMessage = context.RoleUsers
                    .AsNoTracking()
                    .Any(x => x.Role!.Name == summary.DocumentRequiredForPurchase
                        && x.EndDate.HasValue
                        && x.EndDate.Value <= timestamp)
                    ? summary.DocumentRequiredForPurchaseExpiredWarningMessage
                    : summary.DocumentRequiredForPurchaseMissingWarningMessage;
                if (!Contract.CheckValidKey(warningMessage))
                {
                    if (summary.DocumentRequiredForPurchaseOverrideFee >= 0m
                        && !Contract.CheckValidKey(summary.DocumentRequiredForPurchaseOverrideFeeWarningMessage))
                    {
                        response.Messages.Add(
                            $"ERROR! The purchase requirements for Products in the cart for \"{summary.Name}\""
                            + " require specific Documents! However, an override option is available for a fee"
                            + $"of {overrideFee}.");
                        continue;
                    }
                    response.Messages.Add(
                        $"ERROR! The purchase requirements for {nameof(Product)} '{summary.Name}'"
                        + " require specific Documents!");
                    continue;
                }
                response.Messages.Add(
                    "ERROR! "
                    + DoReplacementsMinOrders(
                        warningMessage: warningMessage!,
                        ownerID: cartItem.ProductID.Value,
                        ownerName: summary.Name,
                        attrKey: $"{attrKey}-{cartItem.ProductID.Value}",
                        requiredAmount: "Documents",
                        missingAmount: "Documents",
                        overrideFeeWarningMessage: summary.DocumentRequiredForPurchaseOverrideFeeWarningMessage,
                        overrideFee: overrideFee,
                        relatedBrandID: relatedStoreID,
                        relatedBrandName: relatedStoreName,
                        bufferCategoryName: null,
                        bufferCategorySeoUrl: null,
                        bufferItemName: null,
                        bufferItemSeoUrl: null));
            }

            return false;
        }

        /// <summary>Process the products with minimums.</summary>
        /// <param name="cart">                  The cart.</param>
        /// <param name="currentAccountTypeName">The current account type name.</param>
        /// <param name="currentAccountID">      Identifier for the current account.</param>
        /// <param name="currentUserID">         Identifier for the current user.</param>
        /// <param name="pricingFactoryContext"> Context for the pricing factory.</param>
        /// <param name="cartItem">              The cart item.</param>
        /// <param name="discontinuedCache">     The discontinued cache.</param>
        /// <param name="isUnlimitedCache">      The is unlimited cache.</param>
        /// <param name="allowPreSaleCache">     The allow pre sale cache.</param>
        /// <param name="allowBackOrderCache">   The allow backorder cache.</param>
        /// <param name="flatStockCache">        The flat stock cache.</param>
        /// <param name="response">              The response.</param>
        /// <param name="contextProfileName">    Name of the context profile.</param>
        /// <param name="context">               The context.</param>
        /// <returns>True if it changed the cart, false if it doesn't.</returns>
        protected virtual async Task<bool> ProcessProductWithMinimumsAsync(
            ICartModel cart,
            string? currentAccountTypeName,
            int? currentAccountID,
            int? currentUserID,
            IPricingFactoryContextModel pricingFactoryContext,
            ISalesItemBaseModel<IAppliedCartItemDiscountModel> cartItem,
            IDictionary<int, bool> discontinuedCache,
            IDictionary<int, bool> isUnlimitedCache,
            IDictionary<int, bool> allowPreSaleCache,
            IDictionary<int, bool> allowBackOrderCache,
            IDictionary<int, decimal> flatStockCache,
            CEFActionResponse response,
            string? contextProfileName,
            IClarityEcommerceEntities context)
        {
            if (!cartItem.ItemType.Equals(Enums.ItemType.Item))
            {
                return false;
            }
            if (!Contract.CheckValidID(cartItem.ProductID))
            {
                return true;
            }
            Contract.RequiresValidID(cartItem.ID);
            var doRemove = false;
            var doUpdate = false;
            var changedCart = false;
            var messages = new List<string>();
            // We add the back-ordered amount back in so we can see if there's a stock change that would affect how
            // much is back-ordered, same for Pre-Sold in case the values have changed
            var quantity = cartItem.Quantity;
            var backOrdered = cartItem.QuantityBackOrdered ?? 0m;
            var preSold = cartItem.QuantityPreSold ?? 0m;
            bool isDiscontinued;
            if (discontinuedCache.ContainsKey(cartItem.ProductID!.Value)
                && discontinuedCache.TryGetValue(cartItem.ProductID.Value, out var isDisc))
            {
                isDiscontinued = isDisc;
            }
            else
            {
                isDiscontinued = discontinuedCache[cartItem.ProductID.Value] = await context.Products
                    .AsNoTracking()
                    .FilterByID(cartItem.ProductID)
                    .Select(x => x.IsDiscontinued)
                    .SingleAsync();
            }
            if (isDiscontinued)
            {
                doRemove = true;
                messages.Add($"WARNING! The product \"{cartItem.ProductName}\" is discontinued. It has been removed"
                    + " from the current cart. Please contact support for assistance.");
            }
            if (!doRemove
                && Config!.DoProductRestrictionsByAccountType
                && !(await Workflows.CartItems.CheckIfProductIsPurchasableByCurrentAccountByAccountRestrictionsAsync(
                            currentAccountTypeName: currentAccountTypeName,
                            productID: cartItem.ProductID!.Value,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false))
                    .ActionSucceeded)
            {
                doRemove = true;
                messages.Add($"WARNING! This account is not allowed to purchase \"{cartItem.ProductName}\". It has been"
                    + " removed from the current cart. Please contact support for assistance.");
            }
            if (!doRemove /*&& Config.DoProductRestrictionsByMinMax*/)
            {
                var modificationResult = await Workflows.CartItems
                    .CheckIfProductQuantityMeetsMinimumAndMaximumPurchaseQuantitiesForAccountIDOrUserIDAsync(
                        lookupKey: CartByIDLookupKey.FromCart(cart),
                        entity: await context.CartItems.FilterByID(cartItem.ID).SingleOrDefaultAsync().ConfigureAwait(false),
                        quantity: quantity,
                        quantityBackOrdered: backOrdered,
                        quantityPreSold: preSold,
                        productID: cartItem.ProductID!.Value,
                        isUnlimitedCache: isUnlimitedCache,
                        allowPreSaleCache: allowPreSaleCache,
                        allowBackOrderCache: allowBackOrderCache,
                        flatStockCache: flatStockCache,
                        isForQuote: (cart.TypeName ?? cart.Type?.Name) == "Quote Cart",
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                messages.AddRange(modificationResult?.Messages!);
                if (modificationResult?.NeedToModify == true)
                {
                    if (modificationResult.NewQuantity + modificationResult.NewQuantityBackOrdered + modificationResult.NewQuantityPreSold == 0m)
                    {
                        doRemove = true;
                        ////message = "WARNING! This account has purchased the maximum allotted quantity of"
                        ////    + $" \"{cartItem.ProductName}\". It has been removed from the current cart. If you"
                        ////    + " believe this is in error, please contact support for assistance.";
                    }
                    else
                    {
                        quantity = modificationResult.NewQuantity;
                        backOrdered = modificationResult.NewQuantityBackOrdered;
                        preSold = modificationResult.NewQuantityPreSold;
                        doUpdate = true;
                        ////message = $"WARNING! The quantity of \"{cartItem.ProductName}\" did not meet the"
                        ////    + " requirements set by the store administrators and has been adjusted to"
                        ////    + $" {modificationResult.NewQuantity + modificationResult.NewQuantityBackOrdered + modificationResult.NewQuantityPreSold}.";
                    }
                }
                else
                {
                    // SUCCESS! This account is allowed to purchase \"{cartItem.ProductName}\" and the quantity meets the requirements set by the store administrators
                }
            }
            if (!doRemove)
            {
                // SUCCESS! This account is allowed to purchase \"{cartItem.ProductName}\" and the quantity meets the requirements set by the store administrators
            }
            response.Messages.AddRange(messages);
            if (doRemove)
            {
                changedCart = true;
                await Workflows.CartItems.DeactivateAsync(cartItem.ID, contextProfileName).ConfigureAwait(false);
            }
            else if (doUpdate)
            {
                changedCart = true;
                cartItem.Quantity = quantity;
                cartItem.QuantityBackOrdered = backOrdered;
                cartItem.QuantityPreSold = preSold;
                await Workflows.CartItems.UpdateAsync(
                        lookupKey: SessionCartBySessionAndTypeLookupKey.FromCart(cart),
                        model: cartItem,
                        pricingFactoryContext: pricingFactoryContext,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            return changedCart;
        }
    }
}
