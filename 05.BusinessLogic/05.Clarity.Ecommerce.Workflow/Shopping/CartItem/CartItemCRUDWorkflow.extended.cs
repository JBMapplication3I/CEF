// <copyright file="CartItemCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart item workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.CartValidation;
    using Interfaces.Providers.Pricing;
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Mapper;
    using Models;
    using Utilities;

    public partial class CartItemWorkflow
    {
        /// <summary>Gets or sets the cart status identifier for new.</summary>
        /// <value>The cart status identifier for new.</value>
        protected static int CartStatusIDForNew { get; set; }

        /// <summary>Gets or sets the cart state identifier for work.</summary>
        /// <value>The cart state identifier for work.</value>
        protected static int CartStateIDForWork { get; set; }

        /// <summary>Gets or sets the configuration.</summary>
        /// <value>The configuration.</value>
        protected static ICartValidatorConfig Config { get; set; } = null!;

        /// <summary>Gets or sets the cart type IDs.</summary>
        /// <value>The cart type IDs.</value>
        protected Dictionary<string, int> CartTypeIDs { get; set; } = new();

        /// <inheritdoc/>
        public virtual async Task<(List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> results, int totalPages, int totalCount)> SearchAsync(
            ISalesItemBaseSearchModel search,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            (List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> results, int totalPages, int totalCount) results;
            results.results = (await context.CartItems
                    .AsNoTracking()
                    .FilterByBaseSearchModel(search)
                    .FilterCartItemsByProductActive()
                    .FilterCartItemsByHasQuantity()
                    .FilterCartItemsByCartID(search.MasterID)
                    .FilterCartItemsByCartKey(search.MasterKey)
                    .FilterCartItemsByCartUserID(search.CartUserID)
                    .FilterCartItemsByCartSessionID(search.CartSessionID)
                    .FilterCartItemsByCartTypeName(search.MasterTypeName)
                    .FilterCartItemsByForceUniqueLineItemKey(
                        search.ForceUniqueLineItemKey,
                        search.ForceUniqueLineItemKeyMatchNulls ?? true)
                    .FilterCartItemsBySku(search.Sku)
                    .FilterCartItemsByProductID(search.ProductID)
                    .FilterCartItemsByProductKey(search.ProductKey)
                    .FilterCartItemsByProductName(search.ProductName)
                    .FilterCartItemsByUserID(search.UserID)
                    .FilterCartItemsByUserKey(search.UserKey)
                    .FilterCartItemsByUserUsername(search.UserExternalID)
                    .FilterCartItemsByOriginalCurrencyID(search.OriginalCurrencyID)
                    .FilterCartItemsBySellingCurrencyID(search.SellingCurrencyID)
                    .ApplySorting(search.Sorts, search.Groupings, contextProfileName)
                    .FilterByPaging(search.Paging, out results.totalPages, out results.totalCount)
                    .Select(x => new
                    {
                        // Base Properties
                        x.ID,
                        x.CustomKey,
                        x.Active,
                        x.CreatedDate,
                        x.Hash,
                        x.JsonAttributes,
                        // Overridden data
                        x.Name,
                        x.Description,
                        x.Sku,
                        // Cart Items
                        x.ForceUniqueLineItemKey,
                        // Master ID
                        x.MasterID,
                        x.UserID,
                        x.ProductID,
                        // Product Info
                        x.UnitOfMeasure,
                        Product = x.Product == null
                            ? null
                            : new
                            {
                                x.Product.Name,
                                x.Product.CustomKey,
                                x.Product.Description,
                                x.Product.ShortDescription,
                                x.Product.SeoUrl,
                                x.Product.UnitOfMeasure,
                                x.Product.RequiresRoles,
                                x.Product.NothingToShip,
                                x.Product.DropShipOnly,
                                x.Product.MinimumPurchaseQuantity,
                                x.Product.MinimumPurchaseQuantityIfPastPurchased,
                                x.Product.MaximumPurchaseQuantity,
                                x.Product.MaximumPurchaseQuantityIfPastPurchased,
                                x.Product.MaximumBackOrderPurchaseQuantity,
                                x.Product.MaximumBackOrderPurchaseQuantityIfPastPurchased,
                                x.Product.MaximumBackOrderPurchaseQuantityGlobal,
                                x.Product.MaximumPrePurchaseQuantity,
                                x.Product.MaximumPrePurchaseQuantityIfPastPurchased,
                                x.Product.MaximumPrePurchaseQuantityGlobal,
                                x.Product.IsDiscontinued,
                                x.Product.IsUnlimitedStock,
                                x.Product.AllowBackOrder,
                                x.Product.AllowPreSale,
                                x.Product.PreSellEndDate,
                                x.Product.IsEligibleForReturn,
                                x.Product.RestockingFeePercent,
                                x.Product.RestockingFeeAmount,
                                x.Product.IsTaxable,
                                x.Product.TaxCode,
                                x.Product.JsonAttributes,
                                x.Product.TypeID,
                                x.Product.StatusID,
                                TypeKey = x.Product.Type != null ? x.Product.Type.CustomKey : null,
                                StatusKey = x.Product.Status != null ? x.Product.Status.CustomKey : null,
                                PrimaryImage = x.Product.Images!
                                    .Where(y => y.Active)
                                    .OrderByDescending(y => y.IsPrimary)
                                    .ThenBy(y => y.OriginalWidth)
                                    .ThenBy(y => y.OriginalHeight)
                                    .Take(1)
                                    .Select(y => y.ThumbnailFileName ?? y.OriginalFileName)
                                    .FirstOrDefault(),
                            },
                        // Calculate totals
                        x.Quantity,
                        x.QuantityBackOrdered,
                        x.QuantityPreSold,
                        x.UnitCorePrice,
                        x.UnitSoldPrice,
                        x.UnitSoldPriceModifier,
                        x.UnitSoldPriceModifierMode,
                        // User Info
                        User = x.User == null
                            ? null
                            : new
                            {
                                x.User.CustomKey,
                                x.User.UserName,
                            },
                        // Currency Info
                        x.OriginalCurrencyID,
                        OriginalCurrency = x.OriginalCurrency == null
                            ? null
                            : new
                            {
                                x.OriginalCurrency.CustomKey,
                                x.OriginalCurrency.Name,
                            },
                        x.SellingCurrencyID,
                        SellingCurrency = x.SellingCurrency == null
                            ? null
                            : new
                            {
                                x.SellingCurrency.CustomKey,
                                x.SellingCurrency.Name,
                            },
                        // Other Info
                        Discounts = x.Discounts!
                            .Where(y => y.Active)
                            .Select(y => new
                            {
                                y.ID,
                                y.CustomKey,
                                y.Active,
                                y.CreatedDate,
                                y.Hash,
                                y.DiscountTotal,
                                y.ApplicationsUsed,
                                y.MasterID,
                                y.SlaveID,
                                Slave = y.Slave == null
                                    ? null
                                    : new
                                    {
                                        SlaveKey = y.Slave.CustomKey,
                                        DiscountValue = y.Slave.Value,
                                        DiscountValueType = y.Slave.ValueType,
                                        DiscountPriority = y.Slave.Priority,
                                        y.Slave.DiscountTypeID,
                                        DiscountCanCombine = y.Slave.CanCombine,
                                        Discount = new
                                        {
                                            y.ID,
                                            y.CustomKey,
                                            y.Active,
                                            y.CreatedDate,
                                            y.Hash,
                                            y.Slave.JsonAttributes,
                                            y.Slave.BuyXValue,
                                            y.Slave.CanCombine,
                                            DiscountCompareOperator = y.Slave.DiscountCompareOperator ?? 0,
                                            y.Slave.DiscountTypeID,
                                            y.Slave.EndDate,
                                            y.Slave.GetYValue,
                                            y.Slave.IsAutoApplied,
                                            y.Slave.Priority,
                                            y.Slave.RoundingOperation,
                                            y.Slave.RoundingType,
                                            y.Slave.StartDate,
                                            y.Slave.ThresholdAmount,
                                            y.Slave.UsageLimitPerAccount,
                                            y.Slave.UsageLimitPerUser,
                                            y.Slave.UsageLimitPerCart,
                                            y.Slave.UsageLimitGlobally,
                                            y.Slave.Value,
                                            y.Slave.ValueType,
                                        },
                                    },
                            }),
                        Targets = x.Targets!
                            .Where(y => y.Active)
                            .Select(y => new
                            {
                                y.ID,
                                y.CustomKey,
                                y.Active,
                                y.CreatedDate,
                                y.UpdatedDate,
                                y.JsonAttributes,
                                y.Hash,
                                y.NothingToShip,
                                y.Quantity,
                                // Related Objects
                                y.MasterID,
                                y.TypeID,
                                Type = y.Type == null
                                    ? null
                                    : new
                                    {
                                        y.Type.CustomKey,
                                        y.Type.Name,
                                        y.Type.DisplayName,
                                        y.Type.SortOrder,
                                    },
                                y.DestinationContactID,
                                DestinationContact = y.DestinationContact == null
                                    ? null
                                    : new { y.DestinationContact.CustomKey, },
                                y.OriginProductInventoryLocationSectionID,
                                OriginProductInventoryLocationSection = y.OriginProductInventoryLocationSection == null
                                    ? null
                                    : new { y.OriginProductInventoryLocationSection.CustomKey, },
                                y.OriginStoreProductID,
                                OriginStoreProduct = y.OriginStoreProduct == null
                                    ? null
                                    : new { y.OriginStoreProduct.CustomKey, },
                                y.OriginVendorProductID,
                                OriginVendorProduct = y.OriginVendorProduct == null
                                    ? null
                                    : new { y.OriginVendorProduct.CustomKey, },
                                y.SelectedRateQuoteID,
                                SelectedRateQuote = y.SelectedRateQuote == null
                                    ? null
                                    : new
                                    {
                                        y.SelectedRateQuote.CustomKey,
                                        y.SelectedRateQuote.Name,
                                    },
                            }),
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>
                {
                    // Base Properties
                    ID = x.ID,
                    CustomKey = x.CustomKey,
                    Active = x.Active,
                    CreatedDate = x.CreatedDate,
                    Hash = x.Hash,
                    SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary(),
                    // SerializableAttributes = x.SerializableAttributes,
                    // Overridden data
                    Name = x.Name,
                    Description = x.Description,
                    Sku = x.Sku,
                    // Cart Items
                    ForceUniqueLineItemKey = x.ForceUniqueLineItemKey,
                    // Master ID
                    MasterID = x.MasterID,
                    UserID = x.UserID,
                    ProductID = x.ProductID,
                    // Cart Item Stuff
                    ItemType = Enums.ItemType.Item,
                    // Product Info
                    ProductName = x.Product?.Name,
                    ProductKey = x.Product?.CustomKey,
                    ProductDescription = x.Product?.Description,
                    ProductShortDescription = x.Product?.ShortDescription,
                    ProductSeoUrl = x.Product?.SeoUrl,
                    ProductUnitOfMeasure = x.Product?.UnitOfMeasure,
                    UnitOfMeasure = x.UnitOfMeasure ?? x.Product?.UnitOfMeasure,
                    ProductRequiresRoles = x.Product?.RequiresRoles,
                    ProductTypeID = x.Product?.TypeID,
                    ProductTypeKey = x.Product?.TypeKey,
                    ProductNothingToShip = x.Product?.NothingToShip ?? false,
                    ProductDropShipOnly = x.Product?.DropShipOnly ?? false,
                    ProductMaximumPurchaseQuantity = x.Product?.MaximumPurchaseQuantity,
                    ProductMaximumPurchaseQuantityIfPastPurchased = x.Product?.MaximumPurchaseQuantityIfPastPurchased,
                    ProductMinimumPurchaseQuantity = x.Product?.MinimumPurchaseQuantity,
                    ProductMinimumPurchaseQuantityIfPastPurchased = x.Product?.MinimumPurchaseQuantityIfPastPurchased,
                    ProductMaximumBackOrderPurchaseQuantity = x.Product?.MaximumBackOrderPurchaseQuantity,
                    ProductMaximumBackOrderPurchaseQuantityIfPastPurchased = x.Product?.MaximumBackOrderPurchaseQuantityIfPastPurchased,
                    ProductMaximumBackOrderPurchaseQuantityGlobal = x.Product?.MaximumBackOrderPurchaseQuantityGlobal,
                    ProductMaximumPrePurchaseQuantity = x.Product?.MaximumPrePurchaseQuantity,
                    ProductMaximumPrePurchaseQuantityIfPastPurchased = x.Product?.MaximumPrePurchaseQuantityIfPastPurchased,
                    ProductMaximumPrePurchaseQuantityGlobal = x.Product?.MaximumPrePurchaseQuantityGlobal,
                    ProductIsDiscontinued = x.Product?.IsDiscontinued ?? false,
                    ProductIsUnlimitedStock = x.Product?.IsUnlimitedStock ?? false,
                    ProductAllowBackOrder = x.Product?.AllowBackOrder ?? false,
                    ProductAllowPreSale = x.Product?.AllowPreSale ?? false,
                    ProductPreSellEndDate = x.Product?.PreSellEndDate,
                    ProductIsEligibleForReturn = x.Product?.IsEligibleForReturn ?? false,
                    ProductRestockingFeePercent = x.Product?.RestockingFeePercent,
                    ProductRestockingFeeAmount = x.Product?.RestockingFeeAmount,
                    ProductIsTaxable = x.Product?.IsTaxable ?? true,
                    ProductTaxCode = x.Product?.TaxCode,
                    ProductSerializableAttributes = x.Product?.JsonAttributes.DeserializeAttributesDictionary(),
                    ProductPrimaryImage = x.Product?.PrimaryImage,
                    // Calculate totals
                    Quantity = x.Quantity,
                    QuantityBackOrdered = x.QuantityBackOrdered,
                    QuantityPreSold = x.QuantityPreSold,
                    UnitCorePrice = x.UnitCorePrice,
                    UnitSoldPrice = x.UnitSoldPrice,
                    UnitSoldPriceModifier = x.UnitSoldPriceModifier,
                    UnitSoldPriceModifierMode = x.UnitSoldPriceModifierMode,
                    ExtendedShippingAmount = null, // Read in from Shipping info
                    ExtendedTaxAmount = null, // Read in from Taxes run
                    // User Info
                    UserKey = x.User?.CustomKey,
                    UserUserName = x.User?.UserName,
                    // Currency Info
                    OriginalCurrencyID = x.OriginalCurrencyID,
                    OriginalCurrencyKey = x.OriginalCurrency?.CustomKey,
                    OriginalCurrencyName = x.OriginalCurrency?.Name,
                    SellingCurrencyID = x.SellingCurrencyID,
                    SellingCurrencyKey = x.SellingCurrency?.CustomKey,
                    SellingCurrencyName = x.SellingCurrency?.Name,
                    UnitCorePriceInSellingCurrency = null,
                    UnitSoldPriceInSellingCurrency = null,
                    ExtendedPriceInSellingCurrency = null,
                    // Other Info
                    MaxAllowedInCart = null,
                    Discounts = x.Discounts
                        .Where(y => y.Active)
                        .Select(y => new AppliedCartItemDiscountModel
                        {
                            ID = y.ID,
                            CustomKey = y.CustomKey,
                            Active = y.Active,
                            CreatedDate = y.CreatedDate,
                            Hash = y.Hash,
                            ////SerializableAttributes = y.JsonAttributes.DeserializeAttributesDictionary(),
                            DiscountTotal = y.DiscountTotal,
                            ApplicationsUsed = y.ApplicationsUsed,
                            MasterID = y.MasterID,
                            SlaveID = y.SlaveID,
                            SlaveKey = y.Slave!.SlaveKey,
                            DiscountValue = y.Slave.DiscountValue,
                            DiscountValueType = y.Slave.DiscountValueType,
                            DiscountPriority = y.Slave.DiscountPriority,
                            DiscountTypeID = y.Slave.DiscountTypeID,
                            DiscountCanCombine = y.Slave.DiscountCanCombine,
                            Slave = new()
                            {
                                ID = y.ID,
                                CustomKey = y.CustomKey,
                                Active = y.Active,
                                CreatedDate = y.CreatedDate,
                                Hash = y.Hash,
                                SerializableAttributes = y.Slave.Discount.JsonAttributes.DeserializeAttributesDictionary(),
                                BuyXValue = y.Slave.Discount.BuyXValue,
                                CanCombine = y.Slave.Discount.CanCombine,
                                DiscountCompareOperator = (Enums.CompareOperator)y.Slave.Discount.DiscountCompareOperator,
                                DiscountTypeID = y.Slave.DiscountTypeID,
                                EndDate = y.Slave.Discount.EndDate,
                                GetYValue = y.Slave.Discount.GetYValue,
                                IsAutoApplied = y.Slave.Discount.IsAutoApplied,
                                Priority = y.Slave.Discount.Priority,
                                RoundingOperation = y.Slave.Discount.RoundingOperation,
                                RoundingType = y.Slave.Discount.RoundingType,
                                StartDate = y.Slave.Discount.StartDate,
                                ThresholdAmount = y.Slave.Discount.ThresholdAmount,
                                UsageLimitPerAccount = y.Slave.Discount.UsageLimitPerAccount,
                                UsageLimitPerUser = y.Slave.Discount.UsageLimitPerUser,
                                UsageLimitPerCart = y.Slave.Discount.UsageLimitPerCart,
                                UsageLimitGlobally = y.Slave.Discount.UsageLimitGlobally,
                                Value = y.Slave.Discount.Value,
                                ValueType = y.Slave.Discount.ValueType,
                            },
                        })
                        .ToList(),
                    Targets = x.Targets
                        .Where(y => y.Active)
                        .Select(y => new SalesItemTargetBaseModel
                        {
                            ID = y.ID,
                            CustomKey = y.CustomKey,
                            Active = y.Active,
                            CreatedDate = y.CreatedDate,
                            UpdatedDate = y.UpdatedDate,
                            SerializableAttributes = y.JsonAttributes.DeserializeAttributesDictionary(),
                            Hash = y.Hash,
                            MasterID = y.MasterID,
                            TypeID = y.TypeID,
                            TypeKey = y.Type!.CustomKey,
                            TypeName = y.Type.Name,
                            TypeDisplayName = y.Type.DisplayName,
                            TypeSortOrder = y.Type.SortOrder,
                            NothingToShip = y.NothingToShip,
                            Quantity = y.Quantity,
                            DestinationContactID = y.DestinationContactID,
                            DestinationContactKey = y.DestinationContact!.CustomKey,
                            // ReSharper disable ConstantConditionalAccessQualifier
                            SelectedRateQuoteID = y.SelectedRateQuoteID,
                            SelectedRateQuoteKey = y.SelectedRateQuote?.CustomKey,
                            SelectedRateQuoteName = y.SelectedRateQuote?.Name,
                            // ReSharper restore ConstantConditionalAccessQualifier
                        })
                        .ToList(),
                })
                .ToList<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>();
            return results;
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<int?>> AddCartItemAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            decimal quantity,
            List<ISalesItemTargetBaseModel>? targets,
            int productID,
            string? forceUniqueLineItemKey,
            SerializableAttributesDictionary serializableAttributes,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var productIDVerification = await Workflows.Products.CheckExistsAsync(
                    productID,
                    context)
                .ConfigureAwait(false);
            if (Contract.CheckInvalidID(productIDVerification))
            {
                return CEFAR.FailingCEFAR<int?>("ERROR! Could not locate product to add");
            }
            // Filter out products the current account isn't allowed to purchase
            if (Contract.CheckValidID(lookupKey.AccountID)
                && !(await CheckIfProductIsPurchasableByCurrentAccountByAccountRestrictionsAsync(
                            (await Workflows.Accounts.GetTypeNameForAccountAsync(
                                    lookupKey.AccountID!.Value,
                                    contextProfileName)
                                .ConfigureAwait(false))!,
                            productID,
                            contextProfileName)
                        .ConfigureAwait(false))
                    .ActionSucceeded)
            {
                return CEFAR.FailingCEFAR<int?>(
                    "ERROR! This account is not allowed to purchase this product. Please contact support for assistance.");
            }
            if (await IsOutOfStockAsync(lookupKey.TypeKey, productID, context).ConfigureAwait(false))
            {
                return CEFAR.FailingCEFAR<int?>("ERROR! This product is out of stock.");
            }
            var productIsExplodingKit = CEFConfigDictionary.ExplodeKitsAddedToCart
                && await context.Products
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByID(productID)
                    .FilterByTypeKeys<Product, ProductType>(new[] { "KIT", "VARIANT-KIT" })
                    .FilterObjectsWithJsonAttributesByValues(new()
                    {
                        ["ExplodeKitWhenAddingToCart"] = new[] { "true" },
                    })
                    .AnyAsync()
                    .ConfigureAwait(false);
            if (productIsExplodingKit)
            {
                return await AddExplodedKitComponentsAsync(
                        lookupKey: lookupKey,
                        productID: productID,
                        forceUniqueLineItemKey: forceUniqueLineItemKey,
                        pricingFactoryContext: pricingFactoryContext,
                        context: context,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            if (serializableAttributes?.ContainsKey("SelectedStoreID") == true
                && int.TryParse(serializableAttributes["SelectedStoreID"].Value, out var tempStoreID))
            {
                lookupKey.StoreID = tempStoreID;
            }
            if (CEFConfigDictionary.LicenseRequiredforProductPurchase)
            {
                var productLicenseCheck = await CheckForProductLicenseRestrictionsAsync(
                    currentUserID: lookupKey.UserID,
                    productID: productID,
                    contextProfileName: contextProfileName);
                if (!productLicenseCheck)
                {
                    return CEFAR.FailingCEFAR<int?>("ERROR! You do not have the license to purchase this product.");
                }
            }
            return await UpsertAsync(
                    lookupKey: lookupKey,
                    model: new SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>
                    {
                        Active = true,
                        SerializableAttributes = serializableAttributes!,
                        ProductID = productID,
                        Quantity = quantity,
                        Targets = targets?.Cast<SalesItemTargetBaseModel>().ToList(),
                        ForceUniqueLineItemKey = forceUniqueLineItemKey,
                    },
                    pricingFactory: RegistryLoaderWrapper.GetInstance<IPricingFactory>(contextProfileName),
                    pricingFactoryContext: pricingFactoryContext,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<int?>> AddByKeyAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            string productKey,
            string? forceUniqueLineItemKey,
            int quantity,
            IPricingFactoryContextModel pricingFactoryContext,
            List<ISalesItemTargetBaseModel>? targets,
            SerializableAttributesDictionary attributes,
            string? contextProfileName)
        {
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var productID = await context.Products
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByCustomKey(productKey, true)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
                if (Contract.CheckInvalidID(productID))
                {
                    return CEFAR.FailingCEFAR<int?>($"ERROR! Product not found for key: \"{productKey}\"");
                }
                if (attributes?.ContainsKey("SelectedStoreID") == true
                    && int.TryParse(attributes["SelectedStoreID"].Value, out var tempStoreID))
                {
                    lookupKey.StoreID = tempStoreID;
                }
                return await UpsertAsync(
                        lookupKey: lookupKey,
                        model: new SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>
                        {
                            Active = true,
                            CreatedDate = DateExtensions.GenDateTime,
                            ProductID = productID!.Value,
                            Quantity = quantity,
                            SerializableAttributes = attributes!,
                            Targets = targets?.Cast<SalesItemTargetBaseModel>().ToList(),
                            ForceUniqueLineItemKey = forceUniqueLineItemKey,
                        },
                        pricingFactory: RegistryLoaderWrapper.GetInstance<IPricingFactory>(contextProfileName),
                        pricingFactoryContext: pricingFactoryContext,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(CartItemWorkflow)}.{nameof(AddByKeyAsync)}",
                        message: e.Message,
                        ex: e,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR<int?>("Unable to add item to cart");
            }
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<List<int?>>> AddByKeysAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            List<string> productKeys,
            string? forceUniqueLineItemKey,
            int quantity,
            IPricingFactoryContextModel pricingFactoryContext,
            List<ISalesItemTargetBaseModel>? targets,
            SerializableAttributesDictionary attributes,
            string? contextProfileName)
        {
            try
            {
                var retVal = new List<int?>();
                foreach (var productKey in productKeys)
                {
                    var indRetVal = await AddByKeyAsync(
                            lookupKey: lookupKey,
                            productKey: productKey.Trim(),
                            forceUniqueLineItemKey: forceUniqueLineItemKey,
                            quantity: quantity,
                            pricingFactoryContext: pricingFactoryContext,
                            targets: targets,
                            attributes: attributes,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    retVal.Add(indRetVal.Result);
                }
                return retVal.WrapInPassingCEFAR()!;
            }
            catch (Exception e)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(CartItemWorkflow)}.{nameof(AddByKeysAsync)}",
                        message: e.Message,
                        ex: e,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR<List<int?>>("Unable to add item to cart");
            }
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<int?>> UpsertAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ISalesItemBaseModel<IAppliedCartItemDiscountModel> model,
            IPricingFactory pricingFactory,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await UpsertInnerAsync(
                    lookupKey: lookupKey,
                    model: model,
                    pricingFactory: pricingFactory,
                    pricingFactoryContext: pricingFactoryContext,
                    isUnlimitedCache: new Dictionary<int, bool>(),
                    allowPreSaleCache: new Dictionary<int, bool>(),
                    allowBackOrderCache: new Dictionary<int, bool>(),
                    flatStockCache: new Dictionary<int, decimal>(),
                    contextProfileName: contextProfileName,
                    context: context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<List<int?>>> UpsertMultipleAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> models,
            IPricingFactory pricingFactory,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            if (Contract.CheckEmpty(models))
            {
                return CEFAR.FailingCEFAR<List<int?>>("ERROR! The list of items to add was null or empty");
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            ////var discontinuedCache = new Dictionary<int, bool>();
            var isUnlimitedCache = new Dictionary<int, bool>();
            var allowPreSaleCache = new Dictionary<int, bool>();
            var allowBackOrderCache = new Dictionary<int, bool>();
            var flatStockCache = new Dictionary<int, decimal>();
            return await CEFAR.AggregateAsync(
                    models,
                    x => UpsertInnerAsync(
                        lookupKey: lookupKey,
                        model: x!,
                        pricingFactory: pricingFactory,
                        pricingFactoryContext: pricingFactoryContext,
                        isUnlimitedCache: isUnlimitedCache,
                        allowPreSaleCache: allowPreSaleCache,
                        allowBackOrderCache: allowBackOrderCache,
                        flatStockCache: flatStockCache,
                        contextProfileName: contextProfileName,
                        // ReSharper disable once AccessToDisposedClosure
                        context: context))
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<List<int>>> UpdateMultipleAsync(
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> models,
            string? contextProfileName)
        {
            if (!Contract.CheckNotEmpty(models))
            {
                return CEFAR.FailingCEFAR<List<int>>("ERROR! The list of items to update was null or empty");
            }
            return await CEFAR.AggregateAsync(
                    models,
                    async x => await UpdateAsync(x!, contextProfileName).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual Task<CEFActionResponse<List<int?>>> UpdateMultipleAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> models,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            if (!Contract.CheckNotEmpty(models))
            {
                return Task.FromResult(CEFAR.FailingCEFAR<List<int?>>(
                    "ERROR! The list of items to update was null or empty"));
            }
            return CEFAR.AggregateAsync(
                models,
                x => UpdateAsync(
                    lookupKey: lookupKey,
                    model: x!,
                    pricingFactoryContext: pricingFactoryContext,
                    contextProfileName: contextProfileName));
        }

        /// <inheritdoc/>
        // ReSharper disable once CognitiveComplexity, CyclomaticComplexity
        public virtual async Task<CEFActionResponse<int?>> UpdateAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ISalesItemBaseModel<IAppliedCartItemDiscountModel> model,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            if (!CartTypeIDs.ContainsKey(lookupKey.TypeKey))
            {
                CartTypeIDs[lookupKey.TypeKey] = await Workflows.CartTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: lookupKey.TypeKey,
                        byName: lookupKey.TypeKey,
                        byDisplayName: lookupKey.TypeKey,
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            CartItem? entity = null;
            if (Contract.CheckValidID(model.ID))
            {
                entity = await context.CartItems
                    .FilterByActive(true)
                    .FilterByID(model.ID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
            }
            var timestamp = DateExtensions.GenDateTime;
            if (entity == null)
            {
                // var typeID = CartTypeIDs[lookupKey.TypeKey]; // Linq can't read dictionary value
                var query = context.CartItems
                    .FilterByActive(true)
                    .FilterSalesItemsByMasterActive<CartItem, AppliedCartItemDiscount, CartItemTarget>()
                    .FilterCartItemsByCartLookupKey(lookupKey)
                    .FilterCartItemsByProductID(model.ProductID)
                    .FilterCartItemsByForceUniqueLineItemKey(
                        model.ForceUniqueLineItemKey,
                        !Contract.CheckValidKey(model.ForceUniqueLineItemKey));
                entity = await query.SingleOrDefaultAsync().ConfigureAwait(false);
                if (entity != null)
                {
                    model.ID = entity.ID;
                    if (model.Targets?.Any() == true)
                    {
                        foreach (var target in model.Targets.Where(x => x.MasterID != entity.ID))
                        {
                            target.MasterID = entity.ID;
                        }
                    }
                    if (model.Discounts?.Any() == true)
                    {
                        foreach (var discount in model.Discounts.Where(x => x.MasterID != entity.ID))
                        {
                            discount.MasterID = entity.ID;
                        }
                    }
                    if (model.Notes?.Any() == true)
                    {
                        foreach (var note in model.Notes.Where(x => x.CartItemID != entity.ID))
                        {
                            note.CartItemID = entity.ID;
                        }
                    }
                }
            }
            if (entity == null)
            {
                return CEFAR.FailingCEFAR<int?>("Could not locate Cart Item");
            }
            if (Contract.CheckValidID(model.ProductID))
            {
                var result = await AdjustForProductAsync(
                        lookupKey: lookupKey,
                        model: model,
                        entity: entity,
                        timestamp: timestamp,
                        pricingFactoryContext: pricingFactoryContext,
                        isUnlimitedCache: new Dictionary<int, bool>(),
                        allowPreSaleCache: new Dictionary<int, bool>(),
                        allowBackOrderCache: new Dictionary<int, bool>(),
                        flatStockCache: new Dictionary<int, decimal>(),
                        contextProfileName: contextProfileName,
                        context: context)
                    .ConfigureAwait(false);
                if (!result.ActionSucceeded)
                {
                    return result.ChangeFailingCEFARType<int?>();
                }
            }
            if (model.Targets?.Any() == true)
            {
                // Check for and collapse duplicate targets
                CollapseTargets(model);
            }
            entity.UpdatedDate = timestamp;
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, contextProfileName).ConfigureAwait(false);
            if (model.Targets?.Any() == true)
            {
                await Workflows.CartItemWithTargetsAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            }
            if (model.Notes?.Any() == true)
            {
                if (Contract.CheckValidID(entity.ID))
                {
                    foreach (var note in model.Notes)
                    {
                        note.CartItemID = entity.ID;
                    }
                }
                await Workflows.CartItemWithNotesAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            }
            // NOTE: This section is for unit testing only so the table contents all get generated correctly in the
            // mock context, it is not called in normal site run-time as contextProfileName would be null
            if (Contract.CheckValidKey(context.ContextProfileName))
            {
                var tempTargets = entity.Targets;
                foreach (var target in tempTargets!)
                {
                    if (!Contract.CheckValidID(target.MasterID))
                    {
                        target.MasterID = entity.ID;
                    }
                    if (!Contract.CheckValidID(target.ID))
                    {
                        context.CartItemTargets.Add(target);
                    }
                    _ = Contract.RequiresValidID(target.ID);
                }
                _ = Contract.RequiresAllValidIDs(
                    await context.CartItems.Select(x => (int?)x.ID).ToArrayAsync().ConfigureAwait(false));
                _ = Contract.RequiresAllValidIDs(
                    await context.CartItems
                        .Where(x => x.ID == entity.ID)
                        .SelectMany(x => x.Targets!
                            .Where(y => y.Active)
                            .Select(y => (int?)y.ID))
                        .ToArrayAsync()
                        .ConfigureAwait(false));
            }
            if (!await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
            {
                return CEFAR.FailingCEFAR<int?>("ERROR! Something about updating this record failed to save");
            }
            await Workflows.Carts.RemoveCartsThatAreEmptyAsync(contextProfileName).ConfigureAwait(false);
            return ((int?)entity.ID).WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>> UpdateTargetsAsync(
            int cartItemID,
            List<ISalesItemTargetBaseModel> targets,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.CartItems
                .FilterByActive(true)
                .FilterByID(cartItemID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (entity == null)
            {
                return CEFAR.FailingCEFAR<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>(
                    "Could not locate Cart Item");
            }
            var timestamp = DateExtensions.GenDateTime;
            var model = RegistryLoaderWrapper.GetInstance<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>();
            model.Targets = targets;
            await Workflows.CartItemWithTargetsAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            if (!await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
            {
                return CEFAR.FailingCEFAR<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>(
                    "ERROR! Something about updating this record failed to save");
            }
            return (await GetAsync(cartItemID, contextProfileName).ConfigureAwait(false)).WrapInPassingCEFARIfNotNull();
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<decimal>> UpdateQuantityAsync(
            int cartItemID,
            decimal quantity,
            decimal quantityBackOrdered,
            decimal quantityPreSold,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.CartItems
                .FilterByID(cartItemID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (entity == null)
            {
                return CEFAR.FailingCEFAR<decimal>($"ERROR! Unable to locate cart item by id '{cartItemID}'");
            }
            if (entity.Quantity + (entity.QuantityBackOrdered ?? 0m) + (entity.QuantityPreSold ?? 0m)
                == quantity + quantityBackOrdered + quantityPreSold)
            {
                return quantity.WrapInPassingCEFAR("SUCCESS! The value is already at this quantity");
            }
            entity.Quantity = quantity;
            entity.QuantityBackOrdered = quantityBackOrdered;
            entity.QuantityPreSold = quantityPreSold;
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            await Workflows.DiscountManager.VerifyCurrentDiscountsAsync(
                    cartID: entity.MasterID,
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return quantity.WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> DeactivateAsync(
            int id,
            IClarityEcommerceEntities context)
        {
            return DeleteAsync(id, context);
            /*
            Contract.RequiresValidID(id);
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var cartItem = context.CartItems
                    .Include(x => x.Targets)
                    .Include(x => x.Master)
                    .Include(x => x.Master.Discounts)
                    .Include(x => x.Master.RateQuotes)
                    .Include(x => x.Master.SalesItems.Select(y => y.Discounts))
                    .SingleOrDefault(x => x.ID == id);
                if (cartItem == null) { return false; }
                if (!cartItem.Active) { return true; }
                if (cartItem.Master.SalesItems.Any(x => x.ID != cartItem.ID && x.Active))
                {
                    return Deactivate(cartItem, contextProfileName);
                }
                foreach (var target in cartItem.Targets.Where(x => x.Active)) { target.Active = false; }
                foreach (var discount in cartItem.Master.Discounts.Where(x => x.Active)) { discount.Active = false; }
                foreach (var discount in cartItem.Master.SalesItems.SelectMany(x => x.Discounts).Where(x => x.Active)) { discount.Active = false; }
                foreach (var rateQuote in cartItem.Master.RateQuotes.Where(x => x.Active)) { rateQuote.Active = false; }
                context.SaveUnitOfWork();
                var result = Deactivate(cartItem, contextProfileName);
                await Workflows.Carts.RemoveCartsThatAreEmptyAsync(contextProfileName);
                return result;
            }
            */
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> DeactivateAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            int productID,
            string? forceUniqueLineItemKey,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entityIDs = await context.CartItems
                .AsNoTracking()
                .FilterByActive(true)
                .FilterCartItemsByCartLookupKey(lookupKey)
                .FilterCartItemsByProductID(productID)
                .FilterCartItemsByForceUniqueLineItemKey(
                    forceUniqueLineItemKey,
                    Contract.CheckValidKey(forceUniqueLineItemKey))
                .Select(x => x.ID)
                .ToListAsync()
                .ConfigureAwait(false);
            var result = await CEFAR.AggregateAsync(entityIDs, x => DeactivateAsync(x, contextProfileName)).ConfigureAwait(false);
            await Workflows.Carts.RemoveCartsThatAreEmptyAsync(contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> CheckIfProductIsPurchasableByCurrentAccountByAccountRestrictionsAsync(
            string? currentAccountTypeName,
            int productID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await CheckIfProductIsPurchasableByCurrentAccountByAccountRestrictionsAsync(
                    currentAccountTypeName: currentAccountTypeName,
                    productID: productID,
                    context: context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<ICartValidatorItemModificationResult?> CheckIfProductQuantityMeetsMinimumAndMaximumPurchaseQuantitiesForAccountIDOrUserIDAsync(
            CartByIDLookupKey lookupKey,
            ICartItem entity,
            decimal quantity,
            decimal quantityBackOrdered,
            decimal quantityPreSold,
            int productID,
            IDictionary<int, bool> isUnlimitedCache,
            IDictionary<int, bool> allowPreSaleCache,
            IDictionary<int, bool> allowBackOrderCache,
            IDictionary<int, decimal> flatStockCache,
            bool isForQuote,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var product = (await context.Products
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByID(productID)
                    .Select(x => new
                    {
                        x.ID,
                        x.Name,
                        x.IsUnlimitedStock,
                        x.MaximumPurchaseQuantity,
                        x.MinimumPurchaseQuantity,
                        x.MaximumPurchaseQuantityIfPastPurchased,
                        x.MinimumPurchaseQuantityIfPastPurchased,
                        x.AllowBackOrder,
                        x.MaximumBackOrderPurchaseQuantity,
                        x.MaximumBackOrderPurchaseQuantityIfPastPurchased,
                        x.MaximumBackOrderPurchaseQuantityGlobal,
                        x.AllowPreSale,
                        x.PreSellEndDate,
                        x.MaximumPrePurchaseQuantity,
                        x.MaximumPrePurchaseQuantityIfPastPurchased,
                        x.MaximumPrePurchaseQuantityGlobal,
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new Product
                {
                    ID = x.ID,
                    Name = x.Name,
                    IsUnlimitedStock = x.IsUnlimitedStock,
                    MaximumPurchaseQuantity = x.MaximumPurchaseQuantity,
                    MinimumPurchaseQuantity = x.MinimumPurchaseQuantity,
                    MaximumPurchaseQuantityIfPastPurchased = x.MaximumPurchaseQuantityIfPastPurchased,
                    MinimumPurchaseQuantityIfPastPurchased = x.MinimumPurchaseQuantityIfPastPurchased,
                    AllowBackOrder = x.AllowBackOrder,
                    MaximumBackOrderPurchaseQuantity = x.MaximumBackOrderPurchaseQuantity,
                    MaximumBackOrderPurchaseQuantityIfPastPurchased = x.MaximumBackOrderPurchaseQuantityIfPastPurchased,
                    MaximumBackOrderPurchaseQuantityGlobal = x.MaximumBackOrderPurchaseQuantityGlobal,
                    AllowPreSale = x.AllowPreSale,
                    PreSellEndDate = x.PreSellEndDate,
                    MaximumPrePurchaseQuantity = x.MaximumPrePurchaseQuantity,
                    MaximumPrePurchaseQuantityIfPastPurchased = x.MaximumPrePurchaseQuantityIfPastPurchased,
                    MaximumPrePurchaseQuantityGlobal = x.MaximumPrePurchaseQuantityGlobal,
                })
                .SingleOrDefault();
            if (product is null)
            {
                return null;
            }
            return await GetModifiedQuantityForProductByMinMaxAllowForAccountIDOrUserIDAsync(
                    cartItem: entity,
                    product: product,
                    newQuantity: quantity,
                    newQuantityBackOrder: quantityBackOrdered,
                    newQuantityPreSold: quantityPreSold,
                    currentQuantity: null,
                    currentQuantityBackOrder: null,
                    currentQuantityPreSold: null,
                    accountID: lookupKey.AccountID,
                    userID: lookupKey.UserID,
                    matrix: await Workflows.Stores.GetStoreInventoryLocationsMatrixAsync(contextProfileName).ConfigureAwait(false),
                    isUnlimitedCache: isUnlimitedCache,
                    allowPreSaleCache: allowPreSaleCache,
                    allowBackOrderCache: allowBackOrderCache,
                    flatStockCache: flatStockCache,
                    isForQuote: isForQuote,
                    contextProfileName: contextProfileName,
                    context: context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> AddBufferSkuCartItemAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            string bufferSkuSeoUrl,
            string amountToFill,
            IPricingFactory pricingFactory,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            if (!decimal.TryParse(amountToFill.Replace("$", string.Empty), out var amount))
            {
                return CEFAR.FailingCEFAR();
            }
            var product = await Workflows.Products.GetProductBySeoUrlAsync(
                    bufferSkuSeoUrl,
                    contextProfileName,
                    false,
                    null)
                .ConfigureAwait(false);
            if (product?.ID == null)
            {
                return CEFAR.FailingCEFAR();
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return (await UpsertInnerAsync(
                            lookupKey: lookupKey,
                            model: new SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>
                            {
                                Active = true,
                                CreatedDate = DateExtensions.GenDateTime,
                                ProductID = product.ID,
                                // TODO@JTG: Calc price first, pending client funding
                                Quantity = (int)Math.Ceiling(amount / 0m),
                            },
                            pricingFactory: pricingFactory,
                            pricingFactoryContext: pricingFactoryContext,
                            isUnlimitedCache: new Dictionary<int, bool>(),
                            allowPreSaleCache: new Dictionary<int, bool>(),
                            allowBackOrderCache: new Dictionary<int, bool>(),
                            flatStockCache: new Dictionary<int, decimal>(),
                            contextProfileName: contextProfileName,
                            context: context)
                        .ConfigureAwait(false)
                    != null)
                .BoolToCEFAR();
        }

        /// <summary>Query if 'productID' is out of stock.</summary>
        /// <param name="cartTypeName">The cart type name.</param>
        /// <param name="productID">   Identifier for the product.</param>
        /// <param name="context">     The context.</param>
        /// <returns>True if out of stock, false if not.</returns>
        protected virtual async Task<bool> IsOutOfStockAsync(
            string cartTypeName,
            int productID,
            IClarityEcommerceEntities context)
        {
            // Check inventory only for regular cart, valid products and if inventory is enabled
            if (cartTypeName != "Cart"
                || !Contract.CheckValidID(productID)
                || !CEFConfigDictionary.InventoryEnabled)
            {
                return false;
            }
            if (CEFConfigDictionary.InventoryAdvancedEnabled)
            {
                var product = await context.Products
                    .AsNoTracking()
                    .FilterByID(productID)
                    .Select(x => new
                    {
                        x.IsUnlimitedStock,
                        x.AllowBackOrder,
                        ProductInventoryLocationSections = x.ProductInventoryLocationSections!
                            .Where(y => y.Active)
                            .Select(y => new
                            {
                                y.Quantity,
                                y.QuantityAllocated,
                                y.QuantityBroken,
                            }),
                    })
                    .SingleAsync()
                    .ConfigureAwait(false);
                // Check if back orders or unlimited stock
                if (product.IsUnlimitedStock || product.AllowBackOrder)
                {
                    return false;
                }
                // Check the product location quantities
                return product.ProductInventoryLocationSections
                    .Sum(s => (s.Quantity ?? 0) - (s.QuantityAllocated ?? 0) - (s.QuantityBroken ?? 0))
                    <= 0;
                // && !Workflows.ProductInventoryLocationSections.CheckProductInventory(request.ProductID)
                // && !Workflows.ProductInventoryLocationSections.CheckKitComponentInventory(request.ProductID)
            }
            else
            {
                var product = await context.Products
                    .AsNoTracking()
                    .FilterByID(productID)
                    .Select(x => new
                    {
                        x.IsUnlimitedStock,
                        x.AllowBackOrder,
                        x.StockQuantity,
                        x.StockQuantityAllocated,
                    })
                    .SingleAsync()
                    .ConfigureAwait(false);
                // Check if back orders or unlimited stock
                if (product.IsUnlimitedStock || product.AllowBackOrder)
                {
                    return false;
                }
                // Check if there is quantity on hand
                return (product.StockQuantity ?? 0) - (product.StockQuantityAllocated ?? 0) <= 0;
            }
        }

        /// <summary>Query if 'productID' is out of stock.</summary>
        /// <param name="cartID">   Identifier for the cart.</param>
        /// <param name="productID">Identifier for the product.</param>
        /// <param name="context">  The context.</param>
        /// <returns>True if out of stock, false if not.</returns>
        protected virtual async Task<bool> IsOutOfStockAsync(
            int cartID,
            int productID,
            IClarityEcommerceEntities context)
        {
            // Check inventory only for regular cart, valid products and if inventory is enabled
            if (Contract.CheckInvalidID(cartID)
                || !Contract.CheckValidID(productID)
                || !CEFConfigDictionary.InventoryEnabled)
            {
                return false;
            }
            if (CEFConfigDictionary.InventoryAdvancedEnabled)
            {
                var product = await context.Products
                    .AsNoTracking()
                    .FilterByID(productID)
                    .Select(x => new
                    {
                        x.IsUnlimitedStock,
                        x.AllowBackOrder,
                        ProductInventoryLocationSections = x.ProductInventoryLocationSections!
                            .Where(y => y.Active)
                            .Select(y => new
                            {
                                y.Quantity,
                                y.QuantityAllocated,
                                y.QuantityBroken,
                            }),
                    })
                    .SingleAsync()
                    .ConfigureAwait(false);
                // Check if back orders or unlimited stock
                if (product.IsUnlimitedStock || product.AllowBackOrder)
                {
                    return false;
                }
                // Check the product location quantities
                return product.ProductInventoryLocationSections
                    .Sum(s => (s.Quantity ?? 0) - (s.QuantityAllocated ?? 0) - (s.QuantityBroken ?? 0))
                    <= 0;
                // && !Workflows.ProductInventoryLocationSections.CheckProductInventory(request.ProductID)
                // && !Workflows.ProductInventoryLocationSections.CheckKitComponentInventory(request.ProductID)
            }
            else
            {
                var product = await context.Products
                    .AsNoTracking()
                    .FilterByID(productID)
                    .Select(x => new
                    {
                        x.IsUnlimitedStock,
                        x.AllowBackOrder,
                        x.StockQuantity,
                        x.StockQuantityAllocated,
                    })
                    .SingleAsync()
                    .ConfigureAwait(false);
                // Check if back orders or unlimited stock
                if (product.IsUnlimitedStock || product.AllowBackOrder)
                {
                    return false;
                }
                // Check if there is quantity on hand
                return (product.StockQuantity ?? 0) - (product.StockQuantityAllocated ?? 0) <= 0;
            }
        }

        /// <summary>Check if product is purchasable by current account by account restrictions.</summary>
        /// <param name="currentAccountTypeName">The current account type name.</param>
        /// <param name="productID">             Identifier for the product.</param>
        /// <param name="context">               The context.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        protected virtual async Task<CEFActionResponse> CheckIfProductIsPurchasableByCurrentAccountByAccountRestrictionsAsync(
            string? currentAccountTypeName,
            int productID,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresValidID(productID);
            if (!Contract.CheckValidKey(currentAccountTypeName))
            {
                return CEFAR.PassingCEFAR();
            }
            Config ??= RegistryLoader.NamedContainerInstance(context.ContextProfileName).GetInstance<ICartValidatorConfig>();
            if (Config.ProductRestrictionsKeys == null
                || Config.ProductRestrictionsKeys.Count == 0
                || Config.ProductRestrictionsKeys.Keys.All(v => v != currentAccountTypeName))
            {
                // There are no restrictions from the web config for this account type
                return CEFAR.PassingCEFAR();
            }
            var json = await context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByID(productID)
                .Select(x => x.JsonAttributes)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (!Contract.CheckValidKey(json) || json == "{}")
            {
                return CEFAR.FailingCEFAR("Could not find product");
            }
            var attrs = json.DeserializeAttributesDictionary();
            if (!attrs.Any())
            {
                // No restrictions
                return CEFAR.PassingCEFAR();
            }
            return Config.ProductRestrictionsKeys
                .All(kvp => !attrs.TryGetValue(kvp.Key, out var value) || Convert.ToBoolean(value.Value))
                .BoolToCEFAR();
            // Couldn't find an active attribute on the product for this that says it can't be ordered by this
            // account type
        }

        /// <summary>Collapse targets.</summary>
        /// <param name="model">The model.</param>
        protected virtual void CollapseTargets(ISalesItemBaseModel model)
        {
            // ReSharper disable StyleCop.SA1008, StyleCop.SA1009 - Can't satisfy
            var grouped = model.Targets!
                .GroupBy(x => (
                    typeKey: x.Type?.CustomKey ?? x.TypeKey,
                    storeID: (int?)null,
                    vendorID: (int?)null,
                    ilID: (int?)null,
                    nothingToShip: x.NothingToShip,
                    hashedDestination: x.DestinationContact!.ConvertContactToComparableHashedValue()))
                .ToList();
            // ReSharper restore StyleCop.SA1008, StyleCop.SA1009 - Can't satisfy
            if (!grouped.Any(x => x.Count() > 1))
            {
                return;
            }
            var replacementList = new List<ISalesItemTargetBaseModel>();
            foreach (var group in grouped)
            {
                replacementList.AddRange(group);
            }
            model.Targets = replacementList;
        }

        /// <summary>Gets quantity purchased of product by account or user identifier.</summary>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The quantity purchased of product by account or user identifier or null if the account and user or
        /// product could not be found.</returns>
        protected virtual async Task<decimal?> GetQuantityPurchasedOfProductByAccountOrUserIDAsync(
            int? accountID,
            int? userID,
            int productID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var accountIDIsValid = Contract.CheckValidID(accountID)
                && await context.Accounts
                    .AsNoTracking()
                    .AnyAsync(x => x.Active && x.ID == accountID!.Value)
                    .ConfigureAwait(false);
            var userIDIsValid = Contract.CheckValidID(userID)
                && await context.Users
                    .AsNoTracking()
                    .AnyAsync(x => x.Active && x.ID == userID!.Value)
                    .ConfigureAwait(false);
            var productIDIsValid = Contract.CheckValidID(productID)
                && !await context.Products
                    .AsNoTracking()
                    .AnyAsync(x => x.Active && x.ID == productID)
                    .ConfigureAwait(false);
            if (!accountIDIsValid && !userIDIsValid || !productIDIsValid)
            {
                // No valid account or user specified or Couldn't find product
                return null;
            }
            return await context.SalesOrders
                .AsNoTracking()
                .FilterByActive(true)
                .FilterSalesCollectionsByAccountID<SalesOrder, SalesOrderStatus, SalesOrderType, SalesOrderItem, AppliedSalesOrderDiscount, SalesOrderState, SalesOrderFile, SalesOrderContact, SalesOrderEvent, SalesOrderEventType>(
                    accountIDIsValid ? accountID!.Value : null)
                .FilterSalesCollectionsByUserID<SalesOrder, SalesOrderStatus, SalesOrderType, SalesOrderItem, AppliedSalesOrderDiscount, SalesOrderState, SalesOrderFile, SalesOrderContact, SalesOrderEvent, SalesOrderEventType>(
                    userIDIsValid ? userID!.Value : null)
                .SelectMany(x => x.SalesItems!)
                .Where(x => x.Active && x.ProductID == productID)
                .Select(x => x.Quantity + (x.QuantityBackOrdered ?? 0m) + (x.QuantityPreSold ?? 0m))
                .DefaultIfEmpty(0m)
                .SumAsync()
                .ConfigureAwait(false);
        }

        /// <summary>Gets quantity purchased of product globally back ordered.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The quantity purchased of product globally back ordered.</returns>
        protected virtual async Task<decimal?> GetQuantityPurchasedOfProductGloballyBackOrderedAsync(
            int productID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var productIDIsValid = Contract.CheckValidID(productID)
                && !await context.Products
                    .AsNoTracking()
                    .AnyAsync(x => x.Active && x.ID == productID)
                    .ConfigureAwait(false);
            if (!productIDIsValid)
            {
                // Couldn't find product
                return null;
            }
            return await context.SalesOrders
                .AsNoTracking()
                .FilterByActive(true)
                .SelectMany(x => x.SalesItems!)
                .Where(x => x.Active && x.ProductID == productID)
                .Select(x => x.QuantityBackOrdered ?? 0m)
                .DefaultIfEmpty(0m)
                .SumAsync()
                .ConfigureAwait(false);
        }

        /// <summary>Gets quantity purchased of product globally pre-sold.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The quantity purchased of product globally pre-sold.</returns>
        protected virtual async Task<decimal?> GetQuantityPurchasedOfProductGloballyPreSoldAsync(
            int productID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var productIDIsValid = Contract.CheckValidID(productID)
                && !await context.Products
                    .AsNoTracking()
                    .AnyAsync(x => x.Active && x.ID == productID)
                    .ConfigureAwait(false);
            if (!productIDIsValid)
            {
                // Couldn't find product
                return null;
            }
            return await context.SalesOrders
                .AsNoTracking()
                .FilterByActive(true)
                .SelectMany(x => x.SalesItems!)
                .Where(x => x.Active && x.ProductID == productID)
                .Select(x => x.QuantityPreSold ?? 0m)
                .DefaultIfEmpty(0m)
                .SumAsync()
                .ConfigureAwait(false);
        }

        /// <summary>Gets modified quantity.</summary>
        /// <param name="cartItem">                The cart item.</param>
        /// <param name="product">                 The product.</param>
        /// <param name="newQuantity">             The new quantity.</param>
        /// <param name="newQuantityBackOrder">    The new quantity back order.</param>
        /// <param name="newQuantityPreSold">      The new quantity pre sold.</param>
        /// <param name="currentQuantity">         The current quantity.</param>
        /// <param name="currentQuantityBackOrder">The current quantity back order.</param>
        /// <param name="currentQuantityPreSold">  The current quantity pre sold.</param>
        /// <param name="accountID">               Identifier for the account.</param>
        /// <param name="userID">                  Identifier for the user.</param>
        /// <param name="matrix">                  The matrix.</param>
        /// <param name="isUnlimitedCache">        The is unlimited cache.</param>
        /// <param name="allowPreSaleCache">       The allow pre sale cache.</param>
        /// <param name="allowBackOrderCache">     The allow back order cache.</param>
        /// <param name="flatStockCache">          The flat stock cache.</param>
        /// <param name="isForQuote">              True if this is for a quote, which should ignore inventory requirements.</param>
        /// <param name="contextProfileName">      Name of the context profile.</param>
        /// <param name="context">                 The context.</param>
        /// <returns>The modified quantity.</returns>
        // ReSharper disable once CyclomaticComplexity, FunctionComplexityOverflow
        protected virtual async Task<ICartValidatorItemModificationResult> GetModifiedQuantityForProductByMinMaxAllowForAccountIDOrUserIDAsync(
            ICartItem? cartItem,
            IProduct product,
            decimal newQuantity,
            decimal newQuantityBackOrder,
            decimal newQuantityPreSold,
            decimal? currentQuantity,
            decimal? currentQuantityBackOrder,
            decimal? currentQuantityPreSold,
            int? accountID,
            int? userID,
            IEnumerable<IStoreInventoryLocationsMatrixModel> matrix,
            IDictionary<int, bool> isUnlimitedCache,
            IDictionary<int, bool> allowPreSaleCache,
            IDictionary<int, bool> allowBackOrderCache,
            IDictionary<int, decimal> flatStockCache,
            bool isForQuote,
            string? contextProfileName,
            IClarityEcommerceEntities context)
        {
            // Business Rules: If the product does not allow more/less than a specified quantity in the cart at a given time:
            // * cap the quantity being set down to that limiting value on the cart item
            // * bring the quantity being set up to that limiting value on the cart item
            // * ...because the customer has previously purchased a quantity of this product:
            // * cap the quantity being set down to that past limiting value on the cart item
            // * bring the quantity being set up to that past limiting value on the cart item
            var timestamp = DateExtensions.GenDateTime;
            var messages = new List<string>();
            // The quantity we want to set
            var originalSumQuantity = newQuantity + (currentQuantity ?? 0m);
            var originalSumQuantityBackOrder = newQuantityBackOrder + (currentQuantityBackOrder ?? 0m);
            var originalSumQuantityPreSold = newQuantityPreSold + (currentQuantityPreSold ?? 0m);
            var sumQuantity = originalSumQuantity;
            var sumQuantityBackOrder = originalSumQuantityBackOrder;
            var sumQuantityPreSold = originalSumQuantityPreSold;
            // Was this previously purchased
            var purchasedQuantity = await GetQuantityPurchasedOfProductByAccountOrUserIDAsync(accountID, userID, product.ID, contextProfileName).ConfigureAwait(false);
            var havePurchasedQuantity = purchasedQuantity > 0;
            // See if any rules are in effect
            var productExists = Contract.CheckNotNull(product);
            var productNameToUse = productExists ? product.Name : cartItem?.Name ?? cartItem?.Sku;
            var productHasMax = !isForQuote && CEFConfigDictionary.PurchasingMinMaxEnabled && productExists && product.MaximumPurchaseQuantity is >= 0;
            var productHasMin = !isForQuote && CEFConfigDictionary.PurchasingMinMaxEnabled && productExists && product.MinimumPurchaseQuantity is >= 0;
            var productHasMaxPast = !isForQuote && CEFConfigDictionary.PurchasingMinMaxAfterEnabled && productExists && product.MaximumPurchaseQuantityIfPastPurchased is >= 0;
            var productHasMinPast = !isForQuote && CEFConfigDictionary.PurchasingMinMaxAfterEnabled && productExists && product.MinimumPurchaseQuantityIfPastPurchased is >= 0;
            var productIsUnlimitedStock = (CEFConfigDictionary.OverrideProductIsUnlimitedStock.HasValue && CEFConfigDictionary.OverrideProductIsUnlimitedStock.Value) || !CEFConfigDictionary.InventoryEnabled || productExists && product.IsUnlimitedStock;
            if (productExists && !isUnlimitedCache.ContainsKey(product.ID))
            {
                isUnlimitedCache[product.ID] = productIsUnlimitedStock;
            }
            var productAllowsBackOrder = isForQuote || CEFConfigDictionary.InventoryBackOrderEnabled && productExists && !productIsUnlimitedStock && product.AllowBackOrder;
            if (productExists && !allowBackOrderCache.ContainsKey(product.ID))
            {
                allowBackOrderCache[product.ID] = productAllowsBackOrder;
            }
            var productHasMaxBO = !isForQuote && CEFConfigDictionary.InventoryBackOrderMaxPerProductPerAccountEnabled && productExists && product.MaximumBackOrderPurchaseQuantity is >= 0;
            var productHasMaxBOPast = !isForQuote && CEFConfigDictionary.InventoryBackOrderMaxPerProductPerAccountAfterEnabled && productExists && productHasMaxBO && product.MaximumBackOrderPurchaseQuantityIfPastPurchased is >= 0;
            var productHasMaxBOGlobal = !isForQuote && CEFConfigDictionary.InventoryBackOrderMaxPerProductGlobalEnabled && productExists && productHasMaxBO && product.MaximumBackOrderPurchaseQuantityGlobal is >= 0;
            var productTotalBOGlobal = !isForQuote && CEFConfigDictionary.InventoryBackOrderMaxPerProductGlobalEnabled && productExists ? await GetQuantityPurchasedOfProductGloballyBackOrderedAsync(product.ID, contextProfileName).ConfigureAwait(false) : null;
            var productAllowsPreSale = !isForQuote && CEFConfigDictionary.InventoryPreSaleEnabled && productExists && !productIsUnlimitedStock && product.AllowPreSale && (!product.PreSellEndDate.HasValue || timestamp < product.PreSellEndDate.Value);
            if (productExists && !allowPreSaleCache.ContainsKey(product.ID))
            {
                allowPreSaleCache[product.ID] = productAllowsPreSale;
            }
            var productHasMaxPre = !isForQuote && CEFConfigDictionary.InventoryPreSaleMaxPerProductPerAccountEnabled && productExists && productAllowsPreSale && product.MaximumPrePurchaseQuantity is >= 0;
            var productHasMaxPrePast = !isForQuote && CEFConfigDictionary.InventoryPreSaleMaxPerProductPerAccountAfterEnabled && productExists && productAllowsPreSale && product.MaximumPrePurchaseQuantityIfPastPurchased is >= 0;
            var productHasMaxPreGlobal = !isForQuote && CEFConfigDictionary.InventoryPreSaleMaxPerProductGlobalEnabled && productExists && productAllowsPreSale && product.MaximumPrePurchaseQuantityGlobal is >= 0;
            var productTotalPreGlobal = !isForQuote && CEFConfigDictionary.InventoryPreSaleMaxPerProductGlobalEnabled && productExists ? await GetQuantityPurchasedOfProductGloballyPreSoldAsync(product.ID, contextProfileName).ConfigureAwait(false) : null;
            decimal? floorConstraint = null;
            decimal? ceilingConstraint = null;
            decimal? ceilingConstraintBackOrder = null;
            decimal? ceilingConstraintPreSale = null;
            ICartValidatorItemModificationResult ReturnValueFn()
            {
                var result = RegistryLoaderWrapper.GetInstance<ICartValidatorItemModificationResult>();
                result.Messages.AddRange(messages);
                // Floor/Ceiling
                result.FloorAllowed = floorConstraint;
                result.CeilingAllowed = ceilingConstraint;
                result.CeilingAllowedBackOrdered = ceilingConstraintBackOrder;
                result.CeilingAllowedPreSold = ceilingConstraintPreSale;
                // New Values
                result.NewQuantity = sumQuantity;
                result.NewQuantityBackOrdered = sumQuantityBackOrder;
                result.NewQuantityPreSold = sumQuantityPreSold;
                // Old Values
                result.OldQuantity = originalSumQuantity;
                result.OldQuantityBackOrdered = originalSumQuantityBackOrder;
                result.OldQuantityPreSold = originalSumQuantityPreSold;
                // Does it need to change?
                result.NeedToModify = originalSumQuantity != sumQuantity
                    || originalSumQuantityBackOrder != sumQuantityBackOrder
                    || originalSumQuantityPreSold != sumQuantityPreSold;
                return result;
            }
            if (productAllowsPreSale)
            {
                // When Pre-Sale, must put all data to Pre-Sale quantity
                if (sumQuantity > 0m)
                {
                    // messages.Add($"WARNING! Normal Sales Quantity of {sumQuantity} for item '{productNameToUse}' converted to Pre-Sales Quantity.");
                    sumQuantityPreSold += sumQuantity;
                    sumQuantity = 0m;
                }
                if (sumQuantityBackOrder > 0m)
                {
                    // messages.Add($"WARNING! Back-Order Quantity of {sumQuantityBackOrder} for item '{productNameToUse}' converted to Pre-Sales Quantity.");
                    sumQuantityPreSold += sumQuantityBackOrder;
                    sumQuantityBackOrder = 0m;
                }
            }
            else
            {
                if (sumQuantityPreSold > 0m)
                {
                    // Not allowed to pre-sell, so push the value onto the main quantity
                    // messages.Add($"WARNING! Pre-Sales Quantity of {sumQuantityPreSold} for item '{productNameToUse}' converted to Normal Sales Quantity.");
                    sumQuantity += sumQuantityPreSold;
                    sumQuantityPreSold = 0m;
                }
                if (!productAllowsBackOrder && sumQuantityBackOrder > 0m)
                {
                    // Not allowed to back order, so push the value onto the main quantity
                    // messages.Add($"WARNING! Back-Order Quantity of {sumQuantityBackOrder} for item '{productNameToUse}' converted to Normal Sales Quantity.");
                    sumQuantity += sumQuantityBackOrder;
                    sumQuantityBackOrder = 0m;
                }
            }
            /* Can't do this or normal back-order process doesn't work
            || !productHasMax
            && !productHasMin
            && !productHasMaxPast
            && !productHasMinPast
            && !productHasMaxBO
            && !productHasMaxBOPast
            && !productHasMaxBOGlobal
            && !productHasMaxPre
            && !productHasMaxPrePast
            && !productHasMaxPreGlobal*/
            if (!productExists)
            {
                // There are no rules in effect, just apply the sums where they need to go
                return ReturnValueFn();
            }
            // Get modified values by applicable rule
            if (havePurchasedQuantity && productHasMaxPast)
            {
                // The Past Max value overrides the non-Past Max value when set
                ceilingConstraint = product.MaximumPurchaseQuantityIfPastPurchased!.Value;
            }
            else if (productHasMax)
            {
                ceilingConstraint = product.MaximumPurchaseQuantity!.Value;
            }
            if (ceilingConstraint <= 0)
            {
                // We already know we can't buy any now, skip any remaining checks
                messages.Add($"ERROR! Total Sales Quantity of {sumQuantity + sumQuantityBackOrder + sumQuantityPreSold}"
                    + $" for item '{productNameToUse}' removed from Cart as this account is unable to purchase any additional units.");
                sumQuantity = 0m;
                sumQuantityBackOrder = 0m;
                sumQuantityPreSold = 0m;
                return ReturnValueFn();
            }
            if (havePurchasedQuantity && productHasMinPast)
            {
                // The Past Min value overrides the non-Past Min value when set
                floorConstraint = product.MinimumPurchaseQuantityIfPastPurchased!.Value;
            }
            else if (productHasMin)
            {
                floorConstraint = product.MinimumPurchaseQuantity!.Value;
            }
            if (floorConstraint.HasValue
                && ceilingConstraint.HasValue
                && floorConstraint.Value > ceilingConstraint.Value)
            {
                // There's no way to satisfy the conditions as you have to purchase more than you are allowed to,
                // remove the item
                messages.Add($"ERROR! Total Sales Quantity of {sumQuantity + sumQuantityBackOrder + sumQuantityPreSold}"
                    + $" for item '{productNameToUse}' removed from Cart as this account would be required a minimum"
                    + $" of {floorConstraint.Value} which is less than the current maximum of {ceilingConstraint.Value}.");
                sumQuantity = 0m;
                sumQuantityBackOrder = 0m;
                sumQuantityPreSold = 0m;
                return ReturnValueFn();
            }
            // Apply the ceiling constraint if there is one
            if (ceilingConstraint.HasValue)
            {
                if (productAllowsPreSale)
                {
                    if (sumQuantityPreSold > ceilingConstraint.Value)
                    {
                        messages.Add($"WARNING! Pre-Sales Quantity of {sumQuantityPreSold} for item '{productNameToUse}'"
                            + $" reduced to {ceilingConstraint.Value} to meet the limits set by store administrators.");
                        sumQuantityPreSold = ceilingConstraint.Value;
                    }
                }
                else
                {
                    // We have to combine the two values to ensure the total is not exceeded, inventory checking will
                    // enure back-order gets filled appropriately
                    if (sumQuantity + sumQuantityBackOrder > ceilingConstraint.Value)
                    {
                        messages.Add($"WARNING! Normal Sales Quantity of {sumQuantity}"
                            + $"{(sumQuantityBackOrder > 0m ? $" and Back-Order Quantity of {sumQuantityBackOrder}" : string.Empty)}"
                            + $" for item '{productNameToUse}' reduced to {ceilingConstraint.Value} to meet the limits"
                            + " set by store administrators.");
                        sumQuantity = ceilingConstraint.Value;
                        sumQuantityBackOrder = 0m;
                    }
                }
            }
            // Apply the floor constraint if there is one
            if (floorConstraint.HasValue)
            {
                // We already know this is less than or equal to the ceiling
                if (productAllowsPreSale)
                {
                    if (sumQuantityPreSold < floorConstraint.Value)
                    {
                        messages.Add($"WARNING! Pre-Sales Quantity of {sumQuantityPreSold} for item '{productNameToUse}'"
                            + $" increased to {floorConstraint.Value} to meet the minimums set by store administrators.");
                        sumQuantityPreSold = floorConstraint.Value;
                    }
                }
                else
                {
                    // We have to combine the two values to ensure the total is not surpassed, inventory checking will
                    // enure back-order gets filled appropriately
                    if (sumQuantity + sumQuantityBackOrder < floorConstraint.Value)
                    {
                        messages.Add($"WARNING! Normal Sales Quantity of {sumQuantity}"
                            + $"{(sumQuantityBackOrder > 0m ? $" and Back-Order Quantity of {sumQuantityBackOrder}" : string.Empty)}"
                            + $" for item '{productNameToUse}' increased to {floorConstraint.Value} to meet the minimums"
                            + " set by store administrators.");
                        sumQuantity = floorConstraint.Value;
                        sumQuantityBackOrder = 0m;
                    }
                }
            }
            // Finalize Pre-Sales checks
            if (productAllowsPreSale)
            {
                if (havePurchasedQuantity && productHasMaxPrePast)
                {
                    // The Past Max value overrides the non-Past Max value when set
                    ceilingConstraintPreSale = product.MaximumPrePurchaseQuantityIfPastPurchased!.Value;
                }
                else if (productHasMaxPre)
                {
                    ceilingConstraintPreSale = product.MaximumPrePurchaseQuantity!.Value;
                }
                if (ceilingConstraintPreSale.HasValue && productHasMaxPreGlobal)
                {
                    var temp = product.MaximumPrePurchaseQuantityGlobal!.Value;
                    if (productTotalPreGlobal > 0m)
                    {
                        temp -= productTotalPreGlobal.Value;
                    }
                    // The lesser between the two
                    ceilingConstraintPreSale = Math.Min(ceilingConstraintPreSale.Value, temp);
                }
                else if (productHasMaxPreGlobal)
                {
                    var temp = product.MaximumPrePurchaseQuantityGlobal!.Value;
                    if (productTotalPreGlobal > 0m)
                    {
                        temp -= productTotalPreGlobal.Value;
                    }
                    ceilingConstraintPreSale = temp;
                }
                if (ceilingConstraintPreSale <= 0)
                {
                    // We already know we can't buy any now, skip any remaining checks
                    messages.Add($"ERROR! Total Sales Quantity of {sumQuantity + sumQuantityBackOrder + sumQuantityPreSold}"
                        + $" for item '{productNameToUse}' removed from Cart as this account has purchased the current"
                        + $" maximum of {ceilingConstraintPreSale.Value} for Pre-Sales.");
                    sumQuantity = 0m;
                    sumQuantityBackOrder = 0m;
                    sumQuantityPreSold = 0m;
                    return ReturnValueFn();
                }
                if (floorConstraint.HasValue
                    && ceilingConstraintPreSale.HasValue
                    && floorConstraint.Value > ceilingConstraintPreSale.Value)
                {
                    // There's no way to satisfy the conditions as you have to purchase more than you are allowed to,
                    // remove the item
                    messages.Add($"ERROR! Total Sales Quantity of {sumQuantity + sumQuantityBackOrder + sumQuantityPreSold}"
                        + $" for item '{productNameToUse}' removed from Cart as this account would be required a"
                        + $" minimum of {floorConstraint.Value} which is less than the current maximum of"
                        + $" {ceilingConstraintPreSale.Value} for Pre-Sales.");
                    sumQuantity = 0m;
                    sumQuantityBackOrder = 0m;
                    sumQuantityPreSold = 0m;
                    return ReturnValueFn();
                }
                // Apply the pre-sale ceiling constraint if there is one
                if (ceilingConstraintPreSale < sumQuantityPreSold)
                {
                    sumQuantityPreSold = ceilingConstraintPreSale.Value;
                }
                // There is no inventory checking for Pre-Sales
                // Return with our PreSale values set appropriately (non-pre-sale values are already 0)
                return ReturnValueFn();
            }
            // If the product has unlimited stock, we don't need to count inventory and determine a quantity to
            // back-order
            if (productIsUnlimitedStock)
            {
                return ReturnValueFn();
            }
            // Finalize Non-Pre-Sales checks
            // Find the total quantity on hand at all warehouses (advanced) or on the product record itself (simple)
            decimal inventoryCount;
            if (CEFConfigDictionary.InventoryAdvancedEnabled)
            {
                SerializableAttributesDictionary sad;
                bool doUpdate;
                (inventoryCount, doUpdate, sad) = await ProcessInventoryCountWithPILSAndCustomSettingsAsync(
                        sad: cartItem?.SerializableAttributes ?? new SerializableAttributesDictionary(),
                        productID: product.ID,
                        matrix: matrix,
                        context: context,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                if (doUpdate)
                {
                    if (cartItem != null)
                    {
                        cartItem.JsonAttributes = sad.SerializeAttributesDictionary();
                    }
                    await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                }
            }
            else if (Contract.CheckValidID(product?.ID))
            {
                flatStockCache ??= new Dictionary<int, decimal>();
                // Simple Inventory from the Product itself
                if (flatStockCache.ContainsKey(product!.ID)
                    && flatStockCache.TryGetValue(product.ID, out var stock))
                {
                    inventoryCount = isForQuote ? int.MaxValue : stock;
                }
                else
                {
                    var p = await context.Products
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterByID(product.ID)
                        .Select(x => (x.StockQuantity ?? 0m) - (x.StockQuantityAllocated ?? 0m) - (x.StockQuantityPreSold ?? 0m))
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false);
                    inventoryCount = flatStockCache[product.ID] = isForQuote ? int.MaxValue : Math.Max(0, p);
                }
            }
            else
            {
                inventoryCount = 0m;
            }
            if (inventoryCount >= sumQuantity + sumQuantityBackOrder)
            {
                // We have enough stock, so no need to back-order anything
                // ReSharper disable once InvertIf
                if (sumQuantityBackOrder > 0m)
                {
                    messages.Add($"WARNING! Back-Order Quantity of {sumQuantityBackOrder:0.####} for item '{productNameToUse}'"
                        + " converted to Normal Sales Quantity as there is enough stock to not need to Back-Order any"
                        + " amount.");
                    sumQuantity += sumQuantityBackOrder;
                    sumQuantityBackOrder = 0m;
                }
                return ReturnValueFn();
            }
            if (!productAllowsBackOrder)
            {
                // We don't allow back-order, so limit to the amount of the inventory only
                if (sumQuantity > inventoryCount)
                {
                    messages.Add($"WARNING! Total Sales Quantity of {sumQuantity + sumQuantityBackOrder + sumQuantityPreSold:0.####}"
                        + $" for item '{productNameToUse}' converted to Normal Sales Quantity and reduced to the"
                        + $" amount available in stock ({inventoryCount}) as the item is not allowed to be Back-Ordered.");
                    sumQuantity = inventoryCount;
                }
                sumQuantityBackOrder = 0m;
                return ReturnValueFn();
            }
            // Since we have to back-order something, determine what the caps are, if any
            if (havePurchasedQuantity && productHasMaxBOPast)
            {
                // The Past Max value overrides the non-Past Max value when set
                ceilingConstraintBackOrder = product!.MaximumBackOrderPurchaseQuantityIfPastPurchased!.Value;
            }
            else if (productHasMaxBO)
            {
                ceilingConstraintBackOrder = product!.MaximumBackOrderPurchaseQuantity!.Value;
            }
            if (ceilingConstraintBackOrder.HasValue && productHasMaxBOGlobal)
            {
                var temp = product!.MaximumBackOrderPurchaseQuantityGlobal!.Value;
                if (productTotalBOGlobal > 0m)
                {
                    temp -= productTotalBOGlobal.Value;
                }
                // The lesser between the two
                ceilingConstraintBackOrder = Math.Min(ceilingConstraintBackOrder.Value, temp);
            }
            else if (productHasMaxBOGlobal)
            {
                var temp = product!.MaximumBackOrderPurchaseQuantityGlobal!.Value;
                if (productTotalBOGlobal > 0m)
                {
                    temp -= productTotalBOGlobal.Value;
                }
                ceilingConstraintBackOrder = temp;
            }
            var missingInventoryQuantity = sumQuantity + sumQuantityBackOrder - inventoryCount;
            // Set the amount to order now as what is left in stock, if possible
            sumQuantity = sumQuantity + sumQuantityBackOrder - missingInventoryQuantity;
            // Override the amount being back-ordered
            sumQuantityBackOrder = missingInventoryQuantity;
            // Cap the amount back-ordered if necessary
            var capped = false;
            if (ceilingConstraintBackOrder > 0m)
            {
                if (sumQuantityBackOrder > ceilingConstraintBackOrder.Value)
                {
                    sumQuantityBackOrder = ceilingConstraintBackOrder.Value;
                    capped = true;
                }
            }
            // ReSharper disable once InvertIf
            if (originalSumQuantityBackOrder != sumQuantityBackOrder)
            {
                // Don't add a message unless it changed
                if (sumQuantity <= 0m)
                {
                    messages.Add($"WARNING! Total Sales Quantity of {sumQuantity + sumQuantityBackOrder + sumQuantityPreSold:0.####}"
                        + $" for item '{productNameToUse}' converted to Back-Order Sales Quantity of {sumQuantityBackOrder:0.####}"
                        + " as there is not enough current stock to supply the item. You will be notified when your order"
                        + " has shipped, after more stock arrives."
                        + (capped ? " The Back-Order amount was capped due to a limit set by the store administrators." : string.Empty));
                }
                else
                {
                    messages.Add($"WARNING! Total Sales Quantity of {sumQuantity + sumQuantityBackOrder + sumQuantityPreSold:0.####}"
                        + $" for item '{productNameToUse}' converted to a Normal Sales Quantity of {sumQuantity} (the"
                        + $" amount available in stock) and a Back-Order Sales Quantity of {sumQuantityBackOrder:0.####} as there"
                        + " is not enough current stock to supply the item. You will be notified when your order has"
                        + " shipped, after more stock arrives. We may perform a partial shipment for the units we have"
                        + " available."
                        + (capped ? " The Back-Order amount was capped due to a limit set by the store administrators." : string.Empty));
                }
            }
            // Pre-Sale is already at 0
            return ReturnValueFn();
        }

        /// <summary>Process the inventory count with PILS and custom settings.</summary>
        /// <param name="sad">               The SerializableAttributesDictionary.</param>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="matrix">            The store inventory matrix.</param>
        /// <param name="context">           The context.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{(decimal inventoryCount,bool doUpdate)}.</returns>
        protected virtual async Task<(decimal inventoryCount, bool doUpdate, SerializableAttributesDictionary sad)> ProcessInventoryCountWithPILSAndCustomSettingsAsync(
            SerializableAttributesDictionary sad,
            int productID,
            IEnumerable<IStoreInventoryLocationsMatrixModel> matrix,
            IClarityEcommerceEntities context,
            string? contextProfileName)
        {
            Config ??= RegistryLoader.NamedContainerInstance(contextProfileName).GetInstance<ICartValidatorConfig>();
            var doUpdate = false;
            decimal inventoryCount;
            var foundShipOption = sad.TryGetValue("ShipOption", out var shipOption);
            if (Config.OverrideAndForceShipToHomeOptionIfNoShipOptionSelected
                && (!foundShipOption || shipOption?.Value != null && string.IsNullOrWhiteSpace(shipOption.Value)))
            {
                // We are enforcing at least Ship To Home when not found or not set up properly, set that up in the cart item
                doUpdate = true;
                sad["ShipOption"] = shipOption = new()
                {
                    Key = "ShipOption",
                    Value = "ShipToHome",
                };
                foundShipOption = true;
            }
            else if (foundShipOption && Config.OverrideAndForceNoShipToOptionIfWhenShipOptionSelected)
            {
                // We are enforcing no Ship To Option, remove it from the cart item
                doUpdate = true;
                sad.TryRemove("ShipOption", out var dummy);
                shipOption = null;
                foundShipOption = false;
            }
            var query = context.ProductInventoryLocationSections
                .AsNoTracking()
                .FilterByActive(true)
                .FilterPILSByProductID(productID);
            // ReSharper disable once PossibleMultipleEnumeration
            if (foundShipOption)
            {
                // Advanced Inventory from PILS
                inventoryCount = await ProcessInventoryCountWithPILSAndCustomSettingsInnerAsync(
                        shipOption!,
                        query,
                        matrix)
                    .ConfigureAwait(false);
                return (inventoryCount, doUpdate, sad);
            }
            // ReSharper disable once PossibleMultipleEnumeration
            inventoryCount = await query
                .Select(x => (x.Quantity ?? 0m) - (x.QuantityAllocated ?? 0m) - (x.QuantityPreSold ?? 0m) - (x.QuantityBroken ?? 0m)) // Quantity on Hand formula
                .DefaultIfEmpty(0m)
                .SumAsync()
                .ConfigureAwait(false);
            return (inventoryCount, doUpdate, sad);
        }

        /// <summary>Process the inventory count with PILS and custom settings inner.</summary>
        /// <param name="shipOption">        The ship option.</param>
        /// <param name="query">             The query.</param>
        /// <param name="matrix">            The matrix.</param>
        /// <returns>A decimal.</returns>
        protected virtual async Task<decimal> ProcessInventoryCountWithPILSAndCustomSettingsInnerAsync(
            SerializableAttributeObject shipOption,
            IQueryable<ProductInventoryLocationSection> query,
            IEnumerable<IStoreInventoryLocationsMatrixModel> matrix)
        {
            var inventoryCount = 0m;
            // Advanced Inventory with special case overrides
            switch (shipOption.Value)
            {
                case "ShipToHome":
                {
                    if (Config.UseShipToHomeFromAnyDCStockCheck)
                    {
                        // Use the DC with the Highest Inventory Count
                        var distributionCenterIDs = matrix
                            .Where(x => x.DistributionCenterInventoryLocationID > 0)
                            .Select(x => x.DistributionCenterInventoryLocationID)
                            .Distinct();
                        var distributionCenterInventories = new Dictionary<int, decimal>();
                        foreach (var distributionCenterID in distributionCenterIDs)
                        {
                            // ReSharper disable once PossibleMultipleEnumeration
                            distributionCenterInventories[distributionCenterID] = await query
                                .FilterPILSByILSInventoryLocationID(distributionCenterID)
                                .Select(x => (x.Quantity ?? 0m) + (x.QuantityAllocated ?? 0m) + (x.QuantityPreSold ?? 0m) + (x.QuantityBroken ?? 0m))
                                .DefaultIfEmpty(0m)
                                .SumAsync()
                                .ConfigureAwait(false);
                        }
                        inventoryCount = distributionCenterInventories.Aggregate((l, r) => l.Value > r.Value ? l : r).Value;
                    }
                    break;
                }
                case "ShipToStore":
                {
                    if (Config.UseShipToStoreFromStoreDCStockCheck)
                    {
                        ////// Use the store's assigned DC's inventory count
                        ////// NOTE: There should only be one, but doing it as an array for extendability
                        ////var dcIDs = matrix
                        ////    .Where(x => x.StoreID == cartItem.StoreID && x.DistributionCenterInventoryLocationID > 0)
                        ////    .Select(x => x.DistributionCenterInventoryLocationID)
                        ////    .Distinct();
                        ////var dcInventories = new Dictionary<int, decimal>();
                        ////foreach (var dcID in dcIDs)
                        ////{
                        ////    // ReSharper disable once PossibleMultipleEnumeration
                        ////    dcInventories[dcID] = query.FilterPILSByILSInventoryLocationID(dcID)
                        ////        .Select(x => (x.Quantity ?? 0m) + (x.QuantityAllocated ?? 0m) + (x.QuantityPreSold ?? 0m) + (x.QuantityBroken ?? 0m))
                        ////        .DefaultIfEmpty(0m)
                        ////        .SumAsync()
                        ////        .ConfigureAwait(false);
                        ////}
                        ////var storesHighestDCInventoryCount = dcInventories.Aggregate((l, r) => l.Value > r.Value ? l : r).Value;
                        ////inventoryCount = storesHighestDCInventoryCount;
                    }
                    break;
                }
                case "PickupInStore":
                {
                    if (Config.UsePickupInStoreStockCheck)
                    {
                        ////// Use the store's internal inventory count
                        ////// NOTE: There should only be one, but doing it as an array for extendability
                        ////var sIDs = matrix
                        ////    .Where(x => x.StoreID == cartItem.StoreID && x.InternalInventoryLocationID > 0)
                        ////    .Select(x => x.InternalInventoryLocationID)
                        ////    .Distinct();
                        ////var sInventories = new Dictionary<int, decimal>();
                        ////foreach (var sID in sIDs)
                        ////{
                        ////    // ReSharper disable once PossibleMultipleEnumeration
                        ////    sInventories[sID] = query.FilterPILSByILSInventoryLocationID(sID)
                        ////        .Select(x => (x.Quantity ?? 0m) + (x.QuantityAllocated ?? 0m) + (x.QuantityPreSold ?? 0m) + (x.QuantityBroken ?? 0m))
                        ////        .DefaultIfEmpty(0m)
                        ////        .SumAsync()
                        ////        .ConfigureAwait(false);
                        ////}
                        ////var storesHighestInventoryCount = sInventories.Aggregate((l, r) => l.Value > r.Value ? l : r).Value;
                        ////inventoryCount = storesHighestInventoryCount;
                    }
                    break;
                }
            }
            return inventoryCount;
        }

        /// <summary>Create a cart item and add it to the current cart.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="model">                A model of the core cart item attributes.</param>
        /// <param name="pricingFactory">       The pricing factory.</param>
        /// <param name="pricingFactoryContext">The pricing factory context.</param>
        /// <param name="isUnlimitedCache">     The is unlimited cache.</param>
        /// <param name="allowPreSaleCache">    The allow pre-sale cache.</param>
        /// <param name="allowBackOrderCache">  The allow back order cache.</param>
        /// <param name="flatStockCache">       The flat stock cache.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <param name="context">              The context.</param>
        /// <returns>The identifier of the existing or new cart item wrapped in a <see cref="CEFActionResponse"/>.</returns>
        protected virtual async Task<CEFActionResponse<int?>> UpsertInnerAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ISalesItemBaseModel<IAppliedCartItemDiscountModel> model,
            IPricingFactory pricingFactory,
            IPricingFactoryContextModel pricingFactoryContext,
            IDictionary<int, bool> isUnlimitedCache,
            IDictionary<int, bool> allowPreSaleCache,
            IDictionary<int, bool> allowBackOrderCache,
            IDictionary<int, decimal> flatStockCache,
            string? contextProfileName,
            IClarityEcommerceEntities context)
        {
            if (!Contract.CheckNotNull(model))
            {
                return CEFAR.FailingCEFAR<int?>("ERROR! The model was null");
            }
            if (!Contract.CheckAnyValidID(model.ProductID)
                && !Contract.CheckAnyValidKey(model.ProductKey, model.ProductName)
                && !Contract.CheckAnyValidKey(model.ForceUniqueLineItemKey, model.Sku))
            {
                return CEFAR.FailingCEFAR<int?>("ERROR! There was no product information to utilize");
            }
            var productID = await Workflows.Products.ResolveToIDOptionalAsync(
                    model.ProductID,
                    model.ProductKey,
                    model.ProductName,
                    null,
                    contextProfileName)
                .ConfigureAwait(false);
            var uom = model.SerializableAttributes.TryGetValue("SelectedUOM", out var selectedUOM)
                ? Contract.CheckNotNull(selectedUOM)
                    ? selectedUOM.Value
                    : await context.Products.FilterByID(productID).Select(x => x.UnitOfMeasure).SingleOrDefaultAsync().ConfigureAwait(false)
                : string.Empty;
            if (Contract.CheckValidKey(uom))
            {
                model.UnitOfMeasure = uom;
            }
            if (!Contract.CheckValidID(productID)
                && Contract.CheckAnyValidKey(model.ForceUniqueLineItemKey, model.Sku))
            {
                // This is a custom item not in the system, let's make a dummy product
                var dummyProductModel = RegistryLoaderWrapper.GetInstance<IProductModel>(contextProfileName);
                dummyProductModel.Active = true;
                dummyProductModel.TypeKey = "Custom Quote Item";
                dummyProductModel.StatusKey = "Normal";
                dummyProductModel.Package = (await Workflows.Packages.ResolveAsync(null, "Default", null, contextProfileName).ConfigureAwait(false)).Result;
                // Don't show the item in the catalog
                dummyProductModel.IsVisible = false;
                dummyProductModel.IsDiscontinued = false;
                // Don't look for inventory
                dummyProductModel.AllowBackOrder = true;
                dummyProductModel.IsUnlimitedStock = true;
                // The custom info we have, find a product key that would make this truly unique
                var rawKeyToUse = model.ForceUniqueLineItemKey ?? model.Sku;
                var keyToCheck = rawKeyToUse;
                var i = 0;
                while (Contract.CheckValidKey(keyToCheck)
                    && Contract.CheckValidID(await Workflows.Products.CheckExistsAsync(keyToCheck!, contextProfileName).ConfigureAwait(false)))
                {
                    keyToCheck = $"{rawKeyToUse} ({++i})";
                }
                dummyProductModel.CustomKey = keyToCheck;
                dummyProductModel.Name = model.Name;
                dummyProductModel.SeoUrl = model.Name!.ToSeoUrl();
                // Run the create and read it for the ID
                productID = (await Workflows.Products.CreateAsync(dummyProductModel, contextProfileName).ConfigureAwait(false)).Result;
            }
            var product = (await context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByID(productID)
                .Take(1)
                .Select(x => new
                {
                    x.ID,
                    x.Name,
                    x.IsUnlimitedStock,
                    x.MaximumPurchaseQuantity,
                    x.MinimumPurchaseQuantity,
                    x.MaximumPurchaseQuantityIfPastPurchased,
                    x.MinimumPurchaseQuantityIfPastPurchased,
                    x.AllowBackOrder,
                    x.MaximumBackOrderPurchaseQuantity,
                    x.MaximumBackOrderPurchaseQuantityIfPastPurchased,
                    x.MaximumBackOrderPurchaseQuantityGlobal,
                    x.AllowPreSale,
                    x.PreSellEndDate,
                    x.MaximumPrePurchaseQuantity,
                    x.MaximumPrePurchaseQuantityIfPastPurchased,
                    x.MaximumPrePurchaseQuantityGlobal,
                })
                .ToListAsync()
                .ConfigureAwait(false))
                .Select(x => new Product
                {
                    ID = x.ID,
                    Name = x.Name,
                    IsUnlimitedStock = x.IsUnlimitedStock,
                    MaximumPurchaseQuantity = x.MaximumPurchaseQuantity,
                    MinimumPurchaseQuantity = x.MinimumPurchaseQuantity,
                    MaximumPurchaseQuantityIfPastPurchased = x.MaximumPurchaseQuantityIfPastPurchased,
                    MinimumPurchaseQuantityIfPastPurchased = x.MinimumPurchaseQuantityIfPastPurchased,
                    AllowBackOrder = x.AllowBackOrder,
                    MaximumBackOrderPurchaseQuantity = x.MaximumBackOrderPurchaseQuantity,
                    MaximumBackOrderPurchaseQuantityIfPastPurchased = x.MaximumBackOrderPurchaseQuantityIfPastPurchased,
                    MaximumBackOrderPurchaseQuantityGlobal = x.MaximumBackOrderPurchaseQuantityGlobal,
                    AllowPreSale = x.AllowPreSale,
                    PreSellEndDate = x.PreSellEndDate,
                    MaximumPrePurchaseQuantity = x.MaximumPrePurchaseQuantity,
                    MaximumPrePurchaseQuantityIfPastPurchased = x.MaximumPrePurchaseQuantityIfPastPurchased,
                    MaximumPrePurchaseQuantityGlobal = x.MaximumPrePurchaseQuantityGlobal,
                })
                .SingleOrDefault();
            if (!Contract.CheckNotNull(product))
            {
                return CEFAR.FailingCEFAR<int?>("ERROR! Invalid Product");
            }
            model.ProductID = productID;
            if (!Contract.CheckValidKey(lookupKey.TypeKey))
            {
                // Auto-assign default
                lookupKey.TypeKey = "Cart";
            }
            var timestamp = DateExtensions.GenDateTime;
            var existingCartItem = (await context.CartItems
                    .FilterByActive(true)
                    .FilterSalesItemsByMasterActive<CartItem, AppliedCartItemDiscount, CartItemTarget>()
                    .FilterCartItemsByCartLookupKey(lookupKey.ButIgnoreUserAndAccountID())
                    .FilterCartItemsByProductID(productID)
                    .FilterCartItemsByForceUniqueLineItemKey(
                        model.ForceUniqueLineItemKey,
                        Contract.CheckValidKey(model.ForceUniqueLineItemKey))
                    .Select(x => new
                    {
                        x.ID,
                        x.Quantity,
                        QuantityBackOrdered = x.QuantityBackOrdered ?? 0m,
                        QuantityPreSold = x.QuantityPreSold ?? 0m,
                    })
                    .Take(1)
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new CartItem
                {
                    ID = x.ID,
                    Quantity = x.Quantity,
                    QuantityBackOrdered = x.QuantityBackOrdered,
                    QuantityPreSold = x.QuantityPreSold,
                })
                .SingleOrDefault();
            if (product is null)
            {
                return CEFAR.FailingCEFAR<int?>();
            }
            var result = await GetModifiedQuantityForProductByMinMaxAllowForAccountIDOrUserIDAsync(
                    cartItem: existingCartItem,
                    product: product,
                    newQuantity: model.Quantity,
                    newQuantityBackOrder: model.QuantityBackOrdered ?? 0m,
                    newQuantityPreSold: model.QuantityPreSold ?? 0m,
                    currentQuantity: existingCartItem?.Quantity,
                    currentQuantityBackOrder: existingCartItem?.QuantityBackOrdered,
                    currentQuantityPreSold: existingCartItem?.QuantityPreSold,
                    accountID: lookupKey.AccountID,
                    userID: lookupKey.UserID,
                    matrix: await Workflows.Stores.GetStoreInventoryLocationsMatrixAsync(contextProfileName).ConfigureAwait(false),
                    isUnlimitedCache: isUnlimitedCache,
                    allowPreSaleCache: allowPreSaleCache,
                    allowBackOrderCache: allowBackOrderCache,
                    flatStockCache: flatStockCache,
                    isForQuote: lookupKey.TypeKey == "Quote Cart",
                    contextProfileName: contextProfileName,
                    context: context)
                .ConfigureAwait(false);
            model.Quantity = result.NewQuantity;
            model.QuantityBackOrdered = result.NewQuantityBackOrdered;
            model.QuantityPreSold = result.NewQuantityPreSold;
            // Note: Modifying by Min/Max Purchasing limits, but not Inventory, intentionally
            if (model.TotalQuantity <= 0)
            {
                return CEFAR.FailingCEFAR<int?>(result.Messages.ToArray());
            }
            if (Contract.CheckValidID(productID))
            {
                pricingFactoryContext.Quantity = model.TotalQuantity;
                var calculatedPrice = await pricingFactory.CalculatePriceAsync(
                        productID!.Value,
                        model.SerializableAttributes!,
                        pricingFactoryContext,
                        contextProfileName,
                        forceNoCache: true,
                        forCart: true)
                    .ConfigureAwait(false);
                if (CEFConfigDictionary.UseCustomPriceConversionForCartItems
                    && model.SerializableAttributes.TryGetValue("SoldPrice", out var rawPrice)
                    && decimal.TryParse(rawPrice!.Value, out decimal soldPrice))
                {
                    model.UnitSoldPrice = soldPrice;
                    model.UnitCorePrice = soldPrice;
                }
                else
                {
                    model.UnitSoldPrice = calculatedPrice.SalePrice;
                    model.UnitCorePrice = calculatedPrice.BasePrice;
                }
                model.SerializableAttributes!["PriceKey"] = new()
                {
                    Key = "PriceKey",
                    Value = calculatedPrice.PriceKey!,
                };
                model.SerializableAttributes["PriceKeyAlt"] = new()
                {
                    Key = "PriceKeyAlt",
                    Value = calculatedPrice.PriceKeyAlt!,
                };
                model.SerializableAttributes["PriceKeyRelevantRules"] = new()
                {
                    Key = "PriceKeyRelevantRules",
                    Value = Newtonsoft.Json.JsonConvert.SerializeObject(calculatedPrice.RelevantRules),
                };
                model.SerializableAttributes["PriceKeyUsedRules"] = new()
                {
                    Key = "PriceKeyUsedRules",
                    Value = Newtonsoft.Json.JsonConvert.SerializeObject(calculatedPrice.UsedRules),
                };
            }
            if (existingCartItem != null)
            {
                var retVal = await UpdateAsync(
                        lookupKey: lookupKey,
                        model: model,
                        pricingFactoryContext: pricingFactoryContext,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                if (Contract.CheckNotEmpty(result.Messages))
                {
                    retVal.Messages.AddRange(result.Messages);
                }
                return retVal;
            }
            if (!CartTypeIDs.ContainsKey(lookupKey.TypeKey))
            {
                CartTypeIDs[lookupKey.TypeKey] = await Workflows.CartTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: lookupKey.TypeKey,
                        byName: lookupKey.TypeKey,
                        byDisplayName: lookupKey.TypeKey,
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            var existingCart = await context.Carts
                .FilterByActive(true)
                .FilterByTypeID(CartTypeIDs[lookupKey.TypeKey])
                .FilterCartsBySessionID(lookupKey.SessionID)
                .FilterCartsByBrandID(lookupKey.BrandID)
                .FilterCartsByFranchiseID(lookupKey.FranchiseID)
                .FilterCartsByStoreID(lookupKey.StoreID)
                .OrderBy(x => x.ID)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            var newCartItem = (CartItem)model.CreateCartItemEntity(timestamp, contextProfileName);
            newCartItem.ProductID = productID;
            if (existingCart != null)
            {
                // Use existing cart
                if (!Contract.CheckValidID(existingCart.UserID)
                    && Contract.CheckValidID(pricingFactoryContext.UserID))
                {
                    existingCart.UserID = pricingFactoryContext.UserID;
                }
                newCartItem.MasterID = existingCart.ID;
                context.CartItems.Add(newCartItem);
            }
            else
            {
                if (Contract.CheckInvalidID(CartStatusIDForNew))
                {
                    CartStatusIDForNew = await Workflows.CartStatuses.ResolveWithAutoGenerateToIDAsync(
                            byID: null,
                            byKey: "New",
                            byName: "New",
                            byDisplayName: "New",
                            model: null,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                }
                if (Contract.CheckInvalidID(CartStateIDForWork))
                {
                    CartStateIDForWork = await Workflows.CartStates.ResolveWithAutoGenerateToIDAsync(
                            byID: null,
                            byKey: "WORK",
                            byName: "Work",
                            byDisplayName: "Work",
                            model: null,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                }
                var cart = new Cart
                {
                    Active = true,
                    CreatedDate = timestamp,
                    TypeID = CartTypeIDs[lookupKey.TypeKey],
                    StatusID = CartStatusIDForNew,
                    StateID = CartStateIDForWork,
                    SessionID = lookupKey.SessionID,
                    UserID = lookupKey.UserID,
                    AccountID = lookupKey.AccountID,
                    SalesItems = new List<CartItem> { newCartItem },
                };
                await Workflows.Carts.AddCartAndSaveSafelyAsync(context, cart, true).ConfigureAwait(false);
            }
            // We already took care of Product
            // We already took care of Status
            await RelateOptionalUserAsync(newCartItem, model, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateOptionalOriginalCurrencyAsync(newCartItem, model, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateOptionalSellingCurrencyAsync(newCartItem, model, timestamp, contextProfileName).ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(newCartItem, model, timestamp, contextProfileName).ConfigureAwait(false);
            if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
            {
                var failedResult = CEFAR.FailingCEFAR<int?>(
                    "ERROR! Something about creating and saving this record failed");
                if (Contract.CheckNotEmpty(result.Messages))
                {
                    failedResult.Messages.AddRange(result.Messages);
                }
                return failedResult;
            }
            var successResult = ((int?)newCartItem.ID).WrapInPassingCEFAR();
            if (Contract.CheckNotEmpty(result.Messages))
            {
                successResult.Messages.AddRange(result.Messages);
            }
            return successResult;
        }

        /// <summary>Adds a exploded kit components to the specified cart.</summary>
        /// <param name="lookupKey">             The lookup key.</param>
        /// <param name="productID">             Identifier for the product.</param>
        /// <param name="forceUniqueLineItemKey">The force unique line item key.</param>
        /// <param name="pricingFactoryContext"> Context for the pricing factory.</param>
        /// <param name="context">               The context.</param>
        /// <param name="contextProfileName">    Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse{int?}}.</returns>
        protected virtual async Task<CEFActionResponse<int?>> AddExplodedKitComponentsAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            int productID,
            string? forceUniqueLineItemKey,
            IPricingFactoryContextModel pricingFactoryContext,
            IClarityEcommerceEntities context,
            string? contextProfileName)
        {
            // ReSharper disable once PossibleInvalidOperationException : Validated Above
            var kitProducts = await context.ProductAssociations
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByTypeKey<ProductAssociation, ProductAssociationType>("KIT-COMPONENT", true)
                .Where(x => x.Quantity > 0m)
                .Select(x => new
                {
                    ProductID = x.SlaveID,
                    Quantity = x.Quantity!.Value,
                })
                .ToListAsync()
                .ConfigureAwait(false);
            var multipleCartItems = kitProducts.Select(
                p => new SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>
                {
                    Active = true,
                    CreatedDate = DateExtensions.GenDateTime,
                    ProductID = p.ProductID,
                    Quantity = p.Quantity,
                    ForceUniqueLineItemKey = Contract.CheckValidKey(forceUniqueLineItemKey)
                        ? forceUniqueLineItemKey
                        : null,
                })
                .ToList<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>();
            await UpsertMultipleAsync(
                    lookupKey: lookupKey,
                    models: multipleCartItems,
                    pricingFactory: RegistryLoaderWrapper.GetInstance<IPricingFactory>(contextProfileName),
                    pricingFactoryContext: pricingFactoryContext,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            var (id2, _) = await Workflows.Carts.SessionGetAsIDAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: pricingFactoryContext,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            var cartID = id2;
            var attrResponse = await Workflows.Carts.GetAttributesAsync(
                    lookupKey.ToIDLookupKey(cartID!.Value),
                    contextProfileName)
                .ConfigureAwait(false);
            if (!attrResponse.ActionSucceeded)
            {
                return attrResponse.ChangeFailingCEFARType<int?>();
            }
            attrResponse.Result!["ExplodingKitAdded-" + productID] = new()
            {
                Key = "ExplodingKitAdded-" + productID,
                Value = true.ToString(),
            };
            var updateResponse = await Workflows.Carts.UpdateAttributesAsync(
                    lookupKey.ToIDLookupKey(cartID.Value),
                    attrResponse.Result,
                    contextProfileName)
                .ConfigureAwait(false);
            return !updateResponse.ActionSucceeded
                ? updateResponse.ChangeFailingCEFARType<int?>()
                : CEFAR.PassingCEFAR<int?>(0);
        }

        /// <summary>Adjust for product.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="model">                The model.</param>
        /// <param name="entity">               The entity.</param>
        /// <param name="timestamp">            The timestamp Date/Time.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="isUnlimitedCache">     The is unlimited cache.</param>
        /// <param name="allowPreSaleCache">    The allow pre sale cache.</param>
        /// <param name="allowBackOrderCache">  The allow back order cache.</param>
        /// <param name="flatStockCache">       The flat stock cache.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <param name="context">              The context.</param>
        /// <returns>A Tuple.</returns>
        protected virtual async Task<CEFActionResponse> AdjustForProductAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ISalesItemBaseModel model,
            CartItem entity,
            DateTime timestamp,
            IPricingFactoryContextModel pricingFactoryContext,
            IDictionary<int, bool> isUnlimitedCache,
            IDictionary<int, bool> allowPreSaleCache,
            IDictionary<int, bool> allowBackOrderCache,
            IDictionary<int, decimal> flatStockCache,
            string? contextProfileName,
            IClarityEcommerceEntities context)
        {
            // Validate the Quantity
            var product = (await context.Products
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByID(model.ProductID)
                    .Select(x => new
                    {
                        x.ID,
                        x.CustomKey,
                        x.IsDiscontinued,
                        x.Name,
                        x.NothingToShip,
                        x.MaximumPurchaseQuantity,
                        x.MinimumPurchaseQuantity,
                        x.MaximumPurchaseQuantityIfPastPurchased,
                        x.MinimumPurchaseQuantityIfPastPurchased,
                        x.IsUnlimitedStock,
                        x.AllowBackOrder,
                        x.MaximumBackOrderPurchaseQuantity,
                        x.MaximumBackOrderPurchaseQuantityIfPastPurchased,
                        x.MaximumBackOrderPurchaseQuantityGlobal,
                        x.AllowPreSale,
                        x.PreSellEndDate,
                        x.MaximumPrePurchaseQuantity,
                        x.MaximumPrePurchaseQuantityIfPastPurchased,
                        x.MaximumPrePurchaseQuantityGlobal,
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new Product
                {
                    ID = x.ID,
                    CustomKey = x.CustomKey,
                    Name = x.Name,
                    IsDiscontinued = x.IsDiscontinued,
                    NothingToShip = x.NothingToShip,
                    IsUnlimitedStock = x.IsUnlimitedStock,
                    MaximumPurchaseQuantity = x.MaximumPurchaseQuantity,
                    MinimumPurchaseQuantity = x.MinimumPurchaseQuantity,
                    MaximumPurchaseQuantityIfPastPurchased = x.MaximumPurchaseQuantityIfPastPurchased,
                    MinimumPurchaseQuantityIfPastPurchased = x.MinimumPurchaseQuantityIfPastPurchased,
                    AllowBackOrder = x.AllowBackOrder,
                    MaximumBackOrderPurchaseQuantity = x.MaximumBackOrderPurchaseQuantity,
                    MaximumBackOrderPurchaseQuantityIfPastPurchased = x.MaximumBackOrderPurchaseQuantityIfPastPurchased,
                    MaximumBackOrderPurchaseQuantityGlobal = x.MaximumBackOrderPurchaseQuantityGlobal,
                    AllowPreSale = x.AllowPreSale,
                    PreSellEndDate = x.PreSellEndDate,
                    MaximumPrePurchaseQuantity = x.MaximumPrePurchaseQuantity,
                    MaximumPrePurchaseQuantityIfPastPurchased = x.MaximumPrePurchaseQuantityIfPastPurchased,
                    MaximumPrePurchaseQuantityGlobal = x.MaximumPrePurchaseQuantityGlobal,
                })
                .SingleOrDefault();
            if (product == null)
            {
                return CEFAR.FailingCEFAR("ERROR! Could not locate Product");
            }
            if (product.IsDiscontinued)
            {
                entity.Quantity = 0m;
                entity.QuantityBackOrdered = entity.QuantityPreSold = 0m;
                await DeactivateAsync(entity, contextProfileName).ConfigureAwait(false);
                return CEFAR.FailingCEFAR("WARNING! Removed discontinued product");
            }
            var result = await GetModifiedQuantityForProductByMinMaxAllowForAccountIDOrUserIDAsync(
                    cartItem: entity,
                    product: product,
                    newQuantity: model.Quantity,
                    newQuantityBackOrder: model.QuantityBackOrdered ?? 0m,
                    newQuantityPreSold: model.QuantityPreSold ?? 0m,
                    currentQuantity: null,
                    currentQuantityBackOrder: null,
                    currentQuantityPreSold: null,
                    accountID: lookupKey.AccountID,
                    userID: lookupKey.UserID,
                    matrix: await Workflows.Stores.GetStoreInventoryLocationsMatrixAsync(contextProfileName).ConfigureAwait(false),
                    isUnlimitedCache: isUnlimitedCache,
                    allowPreSaleCache: allowPreSaleCache,
                    allowBackOrderCache: allowBackOrderCache,
                    flatStockCache: flatStockCache,
                    isForQuote: lookupKey.TypeKey == "Quote Cart",
                    contextProfileName: contextProfileName,
                    context: context)
                .ConfigureAwait(false);
            entity.Quantity = result.NewQuantity;
            entity.QuantityBackOrdered = result.NewQuantityBackOrdered;
            entity.QuantityPreSold = result.NewQuantityPreSold;
            if (entity.Quantity + (entity.QuantityBackOrdered ?? 0m) + (entity.QuantityPreSold ?? 0m) <= 0m)
            {
                // Take the item out of the cart and return
                await DeactivateAsync(entity, contextProfileName).ConfigureAwait(false);
                return CEFAR.FailingCEFAR(
                    $"WARNING! Removed product '{product.Name}' with quantity 0",
                    result.Messages.ToArray());
            }
            if (!product.NothingToShip
                || model.Targets?.Any(
                    x => !x.NothingToShip
                        || !Contract.CheckValidIDOrAnyValidKey(
                                x.DestinationContact?.ID ?? x.DestinationContactID,
                                x.DestinationContactKey,
                                x.DestinationContact?.CustomKey))
                != true)
            {
                var skippedResult = CEFAR.PassingCEFAR("Skipped");
                if (Contract.CheckNotEmpty(result.Messages))
                {
                    skippedResult.Messages.AddRange(result.Messages);
                }
                return skippedResult;
            }
            var billingContactID = entity.Master!.BillingContactID ?? 0;
            int? accountID = null;
            if (!Contract.CheckValidID(billingContactID) && Contract.CheckValidID(pricingFactoryContext.UserID))
            {
                // Try resolve to account's default billing
                accountID = await Workflows.Accounts.GetIDByUserIDAsync(
                        id: pricingFactoryContext.UserID!.Value,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                if (Contract.CheckValidID(accountID))
                {
                    billingContactID = (await Workflows.AddressBooks.GetAddressBookPrimaryBillingAsync(
                                accountID: accountID!.Value,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false))
                        ?.SlaveID
                        ?? 0;
                }
            }
            var shippingContactID = entity.Master.ShippingContactID;
            if (!Contract.CheckValidID(shippingContactID) && Contract.CheckValidID(pricingFactoryContext.UserID))
            {
                // Try resolve to account's default shipping
                accountID ??= await Workflows.Accounts.GetIDByUserIDAsync(
                        pricingFactoryContext.UserID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false);
                if (Contract.CheckValidID(accountID))
                {
                    shippingContactID = (await Workflows.AddressBooks.GetAddressBookPrimaryShippingAsync(
                                accountID: accountID!.Value,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false))
                        ?.SlaveID;
                }
            }
            var idToUse = Contract.CheckValidID(shippingContactID) ? shippingContactID : billingContactID;
            if (Contract.CheckInvalidID(idToUse))
            {
                // We tried using account and other info and still don't have something to use, let's make a
                // dummy contact to assign for these NothingToShip targets
                Contact contact = new()
                {
                    Active = true,
                    FullName = "Nothing to Ship Contact",
                    CreatedDate = timestamp,
                    TypeID = await Workflows.ContactTypes.ResolveWithAutoGenerateToIDAsync(
                            byID: null,
                            byKey: "SHIPPING",
                            byName: "Shipping",
                            byDisplayName: "Shipping",
                            model: null,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false),
                };
                context.Contacts.Add(contact);
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                idToUse = contact.ID;
            }
            foreach (var target in model.Targets)
            {
                target.NothingToShip = true;
                target.DestinationContactID = idToUse!.Value;
            }
            return CEFAR.PassingCEFAR(result.Messages.ToArray());
        }

        private async Task<bool> CheckForProductLicenseRestrictionsAsync(
           int? currentUserID,
           int? productID,
           string? contextProfileName)
        {
            // Check for product role restrictions
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var user = await context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ID == currentUserID)
                .ConfigureAwait(false);
            var userHasLicense = !string.IsNullOrWhiteSpace(user.Account!.MedicalLicenseNumber);
            var product = await Workflows.Products.ResolveAsync(productID, null, null, contextProfileName).ConfigureAwait(false);
            var productRequiresLicense = product.Result!.SerializableAttributes.Any(y => y.Key == "LicenseRequired" && y.Value.Value == "true");
            if (productRequiresLicense && !userHasLicense)
            {
                return false;
            }
            return true;
        }
    }
}
