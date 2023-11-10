// <copyright file="ProductCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
#if NET5_0_OR_GREATER
    using System.Data.Entity.Core;
#else
    using System.Data;
#endif
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Mapper;
    using Models;
    using Utilities;

    public partial class ProductWorkflow
    {
        /// <summary>The update resolved package from incoming model.</summary>
        private readonly Action<IPackageModel, IPackageModel, DateTime> updateResolvedPackageFromIncomingModel =
            (resolved, incoming, timestamp) =>
            {
                resolved.IsCustom = incoming.IsCustom;
                resolved.Name = incoming.Name;
                resolved.TypeID = incoming.TypeID;
                resolved.Width = incoming.Width;
                resolved.WidthUnitOfMeasure = incoming.WidthUnitOfMeasure;
                resolved.Depth = incoming.Depth;
                resolved.DepthUnitOfMeasure = incoming.DepthUnitOfMeasure;
                resolved.Height = incoming.Height;
                resolved.HeightUnitOfMeasure = incoming.HeightUnitOfMeasure;
                resolved.Weight = incoming.Weight;
                resolved.WeightUnitOfMeasure = incoming.WeightUnitOfMeasure;
                resolved.DimensionalWeight = incoming.DimensionalWeight;
                resolved.DimensionalWeightUnitOfMeasure = incoming.DimensionalWeightUnitOfMeasure;
                resolved.UpdatedDate = timestamp;
            };

        /// <summary>List of identifiers for the checked products.</summary>
        private List<int>? checkedProductIds;

        /// <inheritdoc/>
        public async Task<bool> CheckProductInMyStoreAsync(
            int productID,
            IUserModel currentUser,
            string? contextProfileName)
        {
            if (currentUser.Account?.Stores is null)
            {
                return false;
            }
            var product = await GetAsync(productID, contextProfileName).ConfigureAwait(false);
            if (product is null)
            {
                return false;
            }
            var accountStoreIDs = currentUser.Account.Stores.Select(store => store.ID).ToList();
            checkedProductIds = new();
            // Put 200 here because after 200 calls the method will most likely be stuck in an infinite loop.
            // We use the _checkedProductIds as well. The maxCount would be our last emergency exit.
            return await CheckIfProductInStoreAsync(product, accountStoreIDs, 200, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<IRawProductPricesModel>> GetRawPricesAsync(
            int id,
            string? contextProfileName)
        {
            Contract.RequiresValidID(id);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return (await context.Products
                    .AsNoTracking()
                    .FilterByID(id)
                    .Select(x => new
                    {
                        x.PriceBase,
                        x.PriceSale,
                        x.PriceMsrp,
                        x.PriceReduction,
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x =>
                {
                    var retVal = RegistryLoaderWrapper.GetInstance<IRawProductPricesModel>(contextProfileName);
                    retVal.ID = id;
                    retVal.PriceBase = x.PriceBase ?? 0m;
                    retVal.PriceSale = x.PriceSale;
                    retVal.PriceMsrp = x.PriceMsrp;
                    retVal.PriceReduction = x.PriceReduction;
                    return retVal;
                })
                .SingleOrDefault()
                .WrapInPassingCEFARIfNotNull();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> UpdateRawPricesAsync(
            IRawProductPricesModel rawPricesToPush,
            string? contextProfileName)
        {
            Contract.RequiresValidID(rawPricesToPush?.ID);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var product = await context.Products.FilterByID(rawPricesToPush!.ID).SingleAsync().ConfigureAwait(false);
            if (product.PriceBase == rawPricesToPush.PriceBase
                && product.PriceSale == rawPricesToPush.PriceSale
                && product.PriceMsrp == rawPricesToPush.PriceMsrp
                && product.PriceReduction == rawPricesToPush.PriceReduction)
            {
                // Do Nothing, already matches
                return CEFAR.PassingCEFAR();
            }
            product.PriceBase = rawPricesToPush.PriceBase;
            product.PriceSale = rawPricesToPush.PriceSale;
            product.PriceMsrp = rawPricesToPush.PriceMsrp;
            product.PriceReduction = rawPricesToPush.PriceReduction;
            product.UpdatedDate = DateExtensions.GenDateTime;
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> BulkUpdateRawPricesAsync(
            List<IRawProductPricesModel> rawPricesToPush,
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            IClarityEcommerceEntities? context = null;
            var counter = 0;
            foreach (var group in rawPricesToPush.GroupBy(x => x.ID))
            {
                if (context is null)
                {
                    context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Configuration.ValidateOnSaveEnabled = false;
                }
                var productID = group.Key;
                if (Contract.CheckInvalidID(productID))
                {
                    // TODO: Add message stating why skipped
                    continue;
                }
                if (!Contract.CheckValidID(
                    await Workflows.Products.CheckExistsAsync(productID, context).ConfigureAwait(false)))
                {
                    // TODO: Add message stating why skipped
                    continue;
                }
                var rawPricesToPushSingle = group.First();
                var product = await context.Products
                    .FilterByID(productID)
                    .SingleAsync()
                    .ConfigureAwait(false);
                if (product.PriceBase == rawPricesToPushSingle.PriceBase
                    && product.PriceSale == rawPricesToPushSingle.PriceSale
                    && product.PriceMsrp == rawPricesToPushSingle.PriceMsrp
                    && product.PriceReduction == rawPricesToPushSingle.PriceReduction)
                {
                    // All data matches already
                    continue;
                }
                product.PriceBase = rawPricesToPushSingle.PriceBase;
                product.PriceSale = rawPricesToPushSingle.PriceSale;
                product.PriceMsrp = rawPricesToPushSingle.PriceMsrp;
                product.PriceReduction = rawPricesToPushSingle.PriceReduction;
                product.UpdatedDate = timestamp;
                if (++counter % 250 != 0)
                {
                    continue;
                }
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                context.Dispose();
                context = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            if (context is null)
            {
                return CEFAR.PassingCEFAR();
            }
            context.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> CreateAsync(
            IProductModel model,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresInvalidID(Contract.RequiresNotNull(model).ID);
            if (!OverrideDuplicateCheck)
            {
                await DuplicateCheckAsync(model, context).ConfigureAwait(false);
            }
            // Create the base object
            var timestamp = DateExtensions.GenDateTime;
            var entity = (Product)model.CreateProductEntity(timestamp, context.ContextProfileName);
            if (Contract.CheckValidKey(entity.ShortDescription) && entity.ShortDescription!.Length > 255)
            {
                entity.ShortDescription = entity.ShortDescription[..255];
            }
            // Related Objects
            await RunLimitedRelateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
            if (!Contract.CheckValidID(entity.TypeID) && entity.Type is null)
            {
                if (Contract.CheckInvalidID(DefaultTypeID))
                {
                    DefaultTypeID = await Workflows.ProductTypes.ResolveWithAutoGenerateToIDAsync(
                            byID: null,
                            byKey: "GENERAL",
                            byName: "General",
                            byDisplayName: "General",
                            model: null,
                            context: context)
                        .ConfigureAwait(false);
                }
                entity.TypeID = DefaultTypeID!.Value;
            }
            await RunRetrieveProductImageFromUrlAsync(model, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
            // Save
            context.Products.Add(entity);
            var saveWorked = await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            if (!saveWorked)
            {
                throw new InvalidDataException("Something about updating this object and saving it failed");
            }
            // Secondary Workflows
            if (context.ContextProfileName is null)
            {
                await Workflows.PricingFactory.RemoveAllCachedPricesByProductIDAsync(
                        entity.ID,
                        context.ContextProfileName)
                    .ConfigureAwait(false);
            }
            return entity.ID.WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<int?> CreateLegacyProductWithKeyAsync(string key, string? name, string? contextProfileName)
        {
            Contract.RequiresValidKey(key);
            var productModel = new ProductModel
            {
                CustomKey = key.Trim(),
                Active = true,
                AllowBackOrder = false,
                Description = null,
                HandlingCharge = null,
                IsDiscontinued = true,
                IsTaxable = true,
                IsVisible = false,
                IsUnlimitedStock = false,
                ManufacturerPartNumber = key.Trim(),
                Name = name ?? key.Trim(),
                TypeID = 1,
                StatusID = 1,
                UnitOfMeasure = "EACH",
                Weight = 0,
            };
            return (await CreateAsync(productModel, contextProfileName).ConfigureAwait(false)).Result;
        }

        /// <inheritdoc/>
        public async Task<Dictionary<int, bool>> HasChildrenForTreeViewAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            // Get children of parent product
            var associatedProducts = await context.ProductAssociations
                .AsNoTracking()
                .Where(p => p.Active && p.MasterID == id)
                .Select(p => p.SlaveID)
                .ToListAsync()
                .ConfigureAwait(false);
            var res = new Dictionary<int, bool>();
            // for each child, check if it has children
            foreach (var assoc in associatedProducts)
            {
                res[assoc] = context.ProductAssociations.Any(p => p.Active && p.MasterID == assoc);
            }
            return res;
        }

        /// <inheritdoc/>
        public async Task<Dictionary<int, List<IProductImageModel>>> GetChildrenImagesForTreeViewAsync(
            int id,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            // Get children of parent product
            var associatedProducts = await context.ProductAssociations
                .AsNoTracking()
                .FilterByActive(true)
                .Where(x => x.MasterID == id)
                .Select(x => x.SlaveID)
                .ToListAsync()
                .ConfigureAwait(false);
            var res = new Dictionary<int, List<IProductImageModel>>();
            // for each child, get their image list
            foreach (var assocID in associatedProducts)
            {
                res[assocID] = context.ProductImages
                    .AsNoTracking()
                    .FilterByActive(true)
                    .Where(x => x.MasterID == assocID)
                    .SelectListProductImageAndMapToProductImageModel(null, null, null, contextProfileName)
                    .results
                    .ToList();
            }
            return res;
        }

        /// <inheritdoc/>
        // ReSharper disable once CyclomaticComplexity, FunctionComplexityOverflow
        public override async Task<CEFActionResponse<int>> UpdateAsync(
            IProductModel model,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresValidIDOrKey(Contract.RequiresNotNull(model).ID, model.CustomKey);
            var entity = Contract.CheckValidID(model.ID)
                ? await context.Products
                    .FilterByID(model.ID)
                    .SingleOrDefaultAsync()
                : null;
            if (entity is null && Contract.CheckValidKey(model.CustomKey))
            {
                entity = await context.Products
                    .FilterByActive(true)
                    .FilterByCustomKey(model.CustomKey, true)
                    .SingleOrDefaultAsync();
            }
            Contract.RequiresNotNull<ArgumentException, IProduct>(
                entity,
                "Must supply an ID or CustomKey that matches an existing record");
            if (entity!.CustomKey != model.CustomKey && !OverrideDuplicateCheck)
            {
                // This will throw if it finds another entity with this model's key
                await DuplicateCheckAsync(model, context).ConfigureAwait(false);
            }
            var timestamp = DateExtensions.GenDateTime;
            // Base Properties
            entity.Active = model.Active;
            entity.CustomKey = model.CustomKey;
            entity.UpdatedDate = timestamp;
            entity.Hash = model.Hash;
            entity.HCPCCode = model.HCPCCode;
            // NameableBase Properties
            entity.Name = model.Name;
            entity.Description = model.Description;
            // IHaveSeoBase Properties
            entity.SeoUrl = model.SeoUrl;
            entity.SeoPageTitle = model.SeoPageTitle;
            entity.SeoKeywords = model.SeoKeywords;
            entity.SeoDescription = model.SeoDescription;
            entity.SeoMetaData = model.SeoMetaData;
            // IHaveNullableDimensions Properties
            entity.Weight = model.Weight;
            entity.WeightUnitOfMeasure = model.WeightUnitOfMeasure;
            entity.Width = model.Width;
            entity.WidthUnitOfMeasure = model.WidthUnitOfMeasure;
            entity.Depth = model.Depth;
            entity.DepthUnitOfMeasure = model.DepthUnitOfMeasure;
            entity.Height = model.Height;
            entity.HeightUnitOfMeasure = model.HeightUnitOfMeasure;
            // Flags/Toggles
            entity.IsVisible = model.IsVisible;
            entity.IsDiscontinued = model.IsDiscontinued;
            entity.IsEligibleForReturn = model.IsEligibleForReturn;
            entity.IsTaxable = model.IsTaxable;
            entity.AllowBackOrder = model.AllowBackOrder;
            entity.IsUnlimitedStock = model.IsUnlimitedStock;
            entity.IsFreeShipping = model.IsFreeShipping;
            entity.NothingToShip = model.NothingToShip;
            entity.DropShipOnly = model.DropShipOnly;
            entity.ShippingLeadTimeIsCalendarDays = model.ShippingLeadTimeIsCalendarDays;
            // Descriptors
            entity.ShortDescription = model.ShortDescription;
            if (Contract.CheckValidKey(entity.ShortDescription) && entity.ShortDescription!.Length > 255)
            {
                entity.ShortDescription = entity.ShortDescription[..255];
            }
            entity.ManufacturerPartNumber = model.ManufacturerPartNumber;
            entity.BrandName = model.BrandName;
            entity.TaxCode = model.TaxCode;
            entity.UnitOfMeasure = model.UnitOfMeasure ?? "EACH";
            entity.SortOrder = model.SortOrder;
            // Pricing & Fees
            entity.HandlingCharge = model.HandlingCharge;
            entity.FlatShippingCharge = model.FlatShippingCharge;
            entity.RestockingFeePercent = model.RestockingFeePercent;
            entity.RestockingFeeAmount = model.RestockingFeeAmount;
            // Availability, Stock, Shipping Requirements
            entity.AvailableStartDate = model.AvailableStartDate;
            entity.AvailableEndDate = model.AvailableEndDate;
            entity.PreSellEndDate = model.PreSellEndDate;
            entity.AllowPreSale = model.AllowPreSale;
            entity.MaximumBackOrderPurchaseQuantity = model.MaximumBackOrderPurchaseQuantity;
            entity.MaximumBackOrderPurchaseQuantityIfPastPurchased = model.MaximumBackOrderPurchaseQuantityIfPastPurchased;
            entity.MaximumBackOrderPurchaseQuantityGlobal = model.MaximumBackOrderPurchaseQuantityGlobal;
            entity.MaximumPrePurchaseQuantity = model.MaximumPrePurchaseQuantity;
            entity.MaximumPrePurchaseQuantityIfPastPurchased = model.MaximumPrePurchaseQuantityIfPastPurchased;
            entity.MaximumPrePurchaseQuantityGlobal = model.MaximumPrePurchaseQuantityGlobal;
            entity.QuantityPerMasterPack = model.QuantityPerMasterPack;
            entity.QuantityMasterPackPerLayer = model.QuantityMasterPackPerLayer;
            entity.QuantityMasterPackLayersPerPallet = model.QuantityMasterPackLayersPerPallet;
            entity.QuantityMasterPackPerPallet = model.QuantityMasterPackPerPallet;
            entity.QuantityPerLayer = model.QuantityPerLayer;
            entity.QuantityLayersPerPallet = model.QuantityLayersPerPallet;
            entity.QuantityPerPallet = model.QuantityPerPallet;
            entity.KitBaseQuantityPriceMultiplier = model.KitBaseQuantityPriceMultiplier ?? 1m;
            entity.ShippingLeadTimeDays = model.ShippingLeadTimeDays;
            entity.RequiresRoles = model.RequiresRoles;
            entity.RequiresRolesAlt = model.RequiresRolesAlt;
            // Min/Max Purchase Per Order
            entity.MinimumPurchaseQuantity = model.MinimumPurchaseQuantity;
            entity.MinimumPurchaseQuantityIfPastPurchased = model.MinimumPurchaseQuantityIfPastPurchased;
            entity.MaximumPurchaseQuantity = model.MaximumPurchaseQuantity;
            entity.MaximumPurchaseQuantityIfPastPurchased = model.MaximumPurchaseQuantityIfPastPurchased;
            // Required Document
            entity.DocumentRequiredForPurchase = model.DocumentRequiredForPurchase;
            entity.DocumentRequiredForPurchaseMissingWarningMessage = model.DocumentRequiredForPurchaseMissingWarningMessage;
            entity.DocumentRequiredForPurchaseExpiredWarningMessage = model.DocumentRequiredForPurchaseExpiredWarningMessage;
            entity.DocumentRequiredForPurchaseOverrideFee = model.DocumentRequiredForPurchaseOverrideFee;
            entity.DocumentRequiredForPurchaseOverrideFeeIsPercent = model.DocumentRequiredForPurchaseOverrideFeeIsPercent;
            entity.DocumentRequiredForPurchaseOverrideFeeWarningMessage = model.DocumentRequiredForPurchaseOverrideFeeWarningMessage;
            entity.DocumentRequiredForPurchaseOverrideFeeAcceptedMessage = model.DocumentRequiredForPurchaseOverrideFeeAcceptedMessage;
            // Must Purchase In Multiples Of
            entity.MustPurchaseInMultiplesOfAmount = model.MustPurchaseInMultiplesOfAmount;
            entity.MustPurchaseInMultiplesOfAmountWarningMessage = model.MustPurchaseInMultiplesOfAmountWarningMessage;
            entity.MustPurchaseInMultiplesOfAmountOverrideFee = model.MustPurchaseInMultiplesOfAmountOverrideFee;
            entity.MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent = model.MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent;
            entity.MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage = model.MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage;
            entity.MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage = model.MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage;
            // Analytics filled data
            entity.TotalPurchasedAmount = model.TotalPurchasedAmount;
            entity.TotalPurchasedQuantity = model.TotalPurchasedQuantity;
            // Related Objects
            if (!Contract.CheckValidIDOrAnyValidKey(
                    model.Type?.ID ?? model.TypeID,
                    model.TypeKey,
                    model.TypeName,
                    model.Type?.CustomKey,
                    model.Type?.Name))
            {
                model.TypeKey = "GENERAL";
            }
            await RunLimitedRelateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
            if (!Contract.CheckValidID(entity.TypeID) && entity.Type is null)
            {
                if (Contract.CheckInvalidID(DefaultTypeID))
                {
                    DefaultTypeID = await Workflows.ProductTypes.ResolveWithAutoGenerateToIDAsync(
                            byID: null,
                            byKey: "GENERAL",
                            byName: "General",
                            byDisplayName: "General",
                            model: null,
                            context: context)
                        .ConfigureAwait(false);
                }
                entity.TypeID = DefaultTypeID!.Value;
            }
            await RunRetrieveProductImageFromUrlAsync(model, context.ContextProfileName).ConfigureAwait(false);
            if (model.IsVendorAdmin == true && Contract.CheckValidID(model.VendorAdminID))
            {
                // Don't modify any of these lists
                model.ProductAssociations = null;
                model.ProductsAssociatedWith = null;
                model.ProductMembershipLevels = null;
                model.ProductNotifications = null;
                model.ProductSubscriptionTypes = null;
                model.Accounts = null;
                model.Brands = null;
                model.Reviews = null;
                model.Stores = null;
            }
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
            // Secondary Workflows
            if (!await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
            {
                throw new InvalidDataException("Something about updating this object and saving it failed");
            }
            if (context.ContextProfileName is null)
            {
                await Workflows.PricingFactory.RemoveAllCachedPricesByProductIDAsync(
                        entity.ID,
                        context.ContextProfileName)
                    .ConfigureAwait(false);
            }
            return entity.ID.WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> UpsertAsync(
            IProductModel model,
            IClarityEcommerceEntities context)
        {
            int? id = null;
            var exists = Contract.CheckValidID(model.ID)
                && Contract.CheckValidID(id = await CheckExistsAsync(model.ID, context).ConfigureAwait(false))
                || Contract.CheckValidKey(model.CustomKey)
                && Contract.CheckValidID(id = await CheckExistsAsync(model.CustomKey!, context).ConfigureAwait(false))
                || CEFConfigDictionary.ImportProductsProductCategoriesAllowResolveByName
                && Contract.CheckValidKey(model.Name)
                && Contract.CheckValidID(id = await CheckExistsByNameAsync(model.Name!, context).ConfigureAwait(false));
            if (exists && !Contract.CheckValidID(model.ID))
            {
                model.ID = id!.Value;
            }
            return exists
                ? await UpdateAsync(model, context).ConfigureAwait(false)
                : await CreateAsync(model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<IProductModel>> ResolveAsync(
            int? byID,
            string? byKey,
            IProductModel? model,
            IClarityEcommerceEntities context,
            bool isInner = false)
        {
            if (Contract.CheckValidID(byID))
            {
                var attempt = await GetAsync(
                        id: byID!.Value,
                        context: context,
                        isVendorAdmin: model?.IsVendorAdmin ?? false,
                        vendorAdminID: model?.VendorAdminID,
                        previewID: null)
                    .ConfigureAwait(false);
                return attempt.WrapInPassingCEFARIfNotNull();
            }
            if (Contract.CheckValidKey(byKey))
            {
                var attempt = await GetAsync(
                        key: byKey!,
                        context: context,
                        isVendorAdmin: model?.IsVendorAdmin ?? false,
                        vendorAdminID: model?.VendorAdminID)
                    .ConfigureAwait(false);
                return attempt.WrapInPassingCEFARIfNotNull();
            }
            if (model is not null && Contract.CheckValidID(model.ID))
            {
                var attempt = await GetAsync(
                        id: model!.ID,
                        context: context,
                        isVendorAdmin: model.IsVendorAdmin ?? false,
                        vendorAdminID: model.VendorAdminID,
                        previewID: null)
                    .ConfigureAwait(false);
                return attempt.WrapInPassingCEFARIfNotNull();
            }
            if (model is not null && Contract.CheckValidKey(model.CustomKey))
            {
                var attempt = await GetAsync(
                        key: model!.CustomKey!,
                        context: context,
                        isVendorAdmin: model.IsVendorAdmin ?? false,
                        vendorAdminID: model.VendorAdminID)
                    .ConfigureAwait(false);
                return attempt.WrapInPassingCEFARIfNotNull();
            }
            if (isInner)
            {
                return CEFAR.FailingCEFAR<IProductModel>();
            }
            throw new ObjectNotFoundException(
                "ERROR! Could not locate a required record with the provided information.");
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<IProductModel>> ResolveAsync(
            int? byID,
            string? byKey,
            string? byName,
            IProductModel? model,
            IClarityEcommerceEntities context,
            bool isInner = false)
        {
            var existing = await ResolveAsync(byID, byKey, model, context, isInner: true).ConfigureAwait(false);
            if (existing.ActionSucceeded)
            {
                return existing;
            }
            if (Contract.CheckValidKey(byName))
            {
                var attempt = await GetByNameAsync(
                        name: byName!,
                        context: context,
                        isVendorAdmin: model?.IsVendorAdmin ?? false,
                        vendorAdminID: model?.VendorAdminID)
                    .ConfigureAwait(false);
                if (attempt is not null)
                {
                    return attempt.WrapInPassingCEFAR()!;
                }
            }
            if (Contract.CheckValidKey(model?.Name))
            {
                var attempt = await GetByNameAsync(
                        name: model!.Name!,
                        context: context,
                        isVendorAdmin: model.IsVendorAdmin ?? false,
                        vendorAdminID: model.VendorAdminID)
                    .ConfigureAwait(false);
                if (attempt is not null)
                {
                    return attempt.WrapInPassingCEFAR()!;
                }
            }
            if (isInner)
            {
                return CEFAR.FailingCEFAR<IProductModel>();
            }
            throw new ObjectNotFoundException(
                "ERROR! Could not locate a required record with the provided information.");
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<IProductModel>> ResolveWithAutoGenerateAsync(
            int? byID,
            string? byKey,
            string? byName,
            IProductModel? model,
            IClarityEcommerceEntities context)
        {
            var existing = await ResolveAsync(byID, byKey, byName, model, context, isInner: true).ConfigureAwait(false);
            if (existing.ActionSucceeded)
            {
                return existing;
            }
            if (model is null
                && (model = await GetInstanceAsync(byKey, byName, context.ContextProfileName).ConfigureAwait(false)) is null)
            {
                throw new InvalidDataException(
                    "Unable to auto-generate object entity with the provided information");
            }
            var createResponse = await CreateAsync(model, context).AwaitAndThrowIfFailedAsync().ConfigureAwait(false);
            return (await GetAsync(createResponse.Result, context).ConfigureAwait(false)).WrapInPassingCEFAR()!;
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeactivateAsync(
            Product? entity,
            IClarityEcommerceEntities context)
        {
            if (entity is null)
            {
                return CEFAR.FailingCEFAR("ERROR! Cannot Deactivate a null record");
            }
            if (!entity.Active)
            {
                // Already inactive
                return CEFAR.PassingCEFAR();
            }
            var timestamp = DateExtensions.GenDateTime;
            await ThrowIfAssociatedAsSalesItemObjectAsync<PurchaseOrderItem, AppliedPurchaseOrderItemDiscount, PurchaseOrderItemTarget>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSalesItemObjectAsync<SalesOrderItem, AppliedSalesOrderItemDiscount, SalesOrderItemTarget>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSalesItemObjectAsync<SalesQuoteItem, AppliedSalesQuoteItemDiscount, SalesQuoteItemTarget>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSalesItemObjectAsync<SalesInvoiceItem, AppliedSalesInvoiceItemDiscount, SalesInvoiceItemTarget>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSalesItemObjectAsync<SalesReturnItem, AppliedSalesReturnItemDiscount, SalesReturnItemTarget>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSalesItemObjectAsync<SampleRequestItem, AppliedSampleRequestItemDiscount, SampleRequestItemTarget>(entity.ID, context).ConfigureAwait(false);
            await DeactivateAssociatedImagesAsync<ProductImage>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<ProductAssociation>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<ProductCategory>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<ProductFile>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<ProductInventoryLocationSection>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<ProductMembershipLevel>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<ProductPricePoint>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<ProductShipCarrierMethod>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<ProductSubscriptionType>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsSlaveObjectsAsync<ProductAssociation>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsSlaveObjectsAsync<ManufacturerProduct>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsSlaveObjectsAsync<DiscountProduct>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsSlaveObjectsAsync<StoreProduct>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsSlaveObjectsAsync<VendorProduct>(entity.ID, timestamp, context).ConfigureAwait(false);
            var e = await context.Set<Product>().FilterByID(entity.ID).SingleAsync();
            e.UpdatedDate = timestamp;
            e.Active = false;
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR("ERROR! Could not save Deactivate record");
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeleteAsync(
            Product? entity,
            IClarityEcommerceEntities context)
        {
            if (entity is null)
            {
                return CEFAR.PassingCEFAR();
            }
            await ThrowIfAssociatedAsSalesItemObjectAsync<PurchaseOrderItem, AppliedPurchaseOrderItemDiscount, PurchaseOrderItemTarget>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSalesItemObjectAsync<SalesOrderItem, AppliedSalesOrderItemDiscount, SalesOrderItemTarget>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSalesItemObjectAsync<SalesQuoteItem, AppliedSalesQuoteItemDiscount, SalesQuoteItemTarget>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSalesItemObjectAsync<SalesInvoiceItem, AppliedSalesInvoiceItemDiscount, SalesInvoiceItemTarget>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSalesItemObjectAsync<SalesReturnItem, AppliedSalesReturnItemDiscount, SalesReturnItemTarget>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSalesItemObjectAsync<SampleRequestItem, AppliedSampleRequestItemDiscount, SampleRequestItemTarget>(entity.ID, context).ConfigureAwait(false);
            await DeleteAssociatedAsMasterObjectsAsync<ProductAssociation>(entity.ID, context).ConfigureAwait(false);
            await DeleteAssociatedAsSlaveObjectsAsync<ProductAssociation>(entity.ID, context).ConfigureAwait(false);
            await DeleteAssociatedImagesAsync<ProductImage>(entity.ID, context).ConfigureAwait(false);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (context.Reviews is not null)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.Reviews.Count(x => x.ProductID == entity.ID);)
                {
                    context.Reviews.Remove(context.Reviews.First(x => x.ProductID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (context.PageViews is not null)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.PageViews.Count(x => x.ProductID == entity.ID);)
                {
                    context.PageViews.Remove(context.PageViews.First(x => x.ProductID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse, InvertIf
            if (context.CartItems is not null)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.CartItems.Count(x => x.ProductID == entity.ID);)
                {
                    context.CartItems.Remove(context.CartItems.First(x => x.ProductID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            return await base.DeleteAsync(entity, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IProduct entity,
            IProductModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Set first image to primary if not done
            if (model.Images?.Any(img => img.Active) == true // Check if there are active images
                && !model.Images.Any(img => img.Active && img.IsPrimary))
            {
                // Check if none of the active images are set as primary
                model.Images.First(img => img.Active).IsPrimary = true;
            }
            // Update the base object (most of the properties handled)
            entity.UpdateProductFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            await RunLimitedRelateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
            if (!Contract.CheckValidID(entity.TypeID) && entity.Type is null)
            {
                if (Contract.CheckInvalidID(DefaultTypeID))
                {
                    DefaultTypeID = await Workflows.ProductTypes.ResolveWithAutoGenerateToIDAsync(
                            byID: null,
                            byKey: "GENERAL",
                            byName: "General",
                            byDisplayName: "General",
                            model: null,
                            context: context)
                        .ConfigureAwait(false);
                }
                entity.TypeID = DefaultTypeID!.Value;
            }
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            // Secondary Workflows
            if (context.ContextProfileName is null)
            {
                await Workflows.PricingFactory.RemoveAllCachedPricesByProductIDAsync(
                        productID: entity.ID,
                        contextProfileName: null)
                    .ConfigureAwait(false);
            }
        }

        /// <summary>Clears the package described by entity.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A Task.</returns>
        private static async Task ClearPackageAsync(IProduct entity)
        {
            Contract.RequiresNotNull(entity).PackageID = null;
            entity.Package = null;
        }

        /// <summary>Clears the master pack described by entity.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A Task.</returns>
        private static async Task ClearMasterPackAsync(IProduct entity)
        {
            Contract.RequiresNotNull(entity).MasterPackID = null;
            entity.MasterPack = null;
        }

        /// <summary>Clears the pallet described by entity.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A Task.</returns>
        private static async Task ClearPalletAsync(IProduct entity)
        {
            Contract.RequiresNotNull(entity).PalletID = null;
            entity.Pallet = null;
        }

        /// <summary>Executes the retrieve product image from URL.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task RunRetrieveProductImageFromUrlAsync(IProductModel model, string? contextProfileName)
        {
            if (model.Images is null)
            {
                return;
            }
            var provider = RegistryLoaderWrapper.GetFilesProvider(contextProfileName);
            if (provider is null)
            {
                return;
            }
            foreach (var productImageModel in model.Images)
            {
                if (productImageModel.SerializableAttributes is null
                    || !productImageModel.SerializableAttributes.ContainsKey("ImageLink"))
                {
                    continue;
                }
                var imageLink = productImageModel.SerializableAttributes["ImageLink"].Value;
                var index = imageLink.IndexOf("file_id", StringComparison.Ordinal);
                if (index <= 0)
                {
                    continue;
                }
                var imageFileName = imageLink[(index + 8)..];
                var path = await provider.GetFileSaveRootPathFromFileEntityTypeAsync(
                        Enums.FileEntityType.ImageProduct)
                    .ConfigureAwait(false);
                if (string.IsNullOrEmpty(path))
                {
                    continue;
                }
                var cdnDirectory = new DirectoryInfo(path);
                if (!cdnDirectory.Exists)
                {
                    cdnDirectory.Create();
                }
                var webRequest = WebRequest.Create(imageLink);
                using (var response = await webRequest.GetResponseAsync().ConfigureAwait(false))
                {
                    var ext = response.ContentType.Replace("image/", ".");
                    if (!imageFileName.Contains(ext))
                    {
                        imageFileName += ext;
                    }
                }
                var destination = path + imageFileName;
                productImageModel.Name = imageFileName;
                productImageModel.OriginalFileName = imageFileName;
                var file = new FileInfo(destination);
                if (file.Exists)
                {
                    continue;
                }
                using var webClient = new WebClient();
                try
                {
                    await webClient.DownloadFileTaskAsync(new Uri(imageLink), destination).ConfigureAwait(false);
                }
                catch
                {
                    // Do Nothing
                    // throw;
                }
            }
        }

        /// <summary>Throw if associated as sales item object asynchronous.</summary>
        /// <typeparam name="TSet">        Type of the set.</typeparam>
        /// <typeparam name="TSetDiscount">Type of the set discount.</typeparam>
        /// <typeparam name="TSetTarget">  Type of the set target.</typeparam>
        /// <param name="relatedID">Identifier for the related.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        private static async Task ThrowIfAssociatedAsSalesItemObjectAsync<TSet, TSetDiscount, TSetTarget>(
                int relatedID,
                IClarityEcommerceEntities context)
            where TSet : class, ISalesItemBase<TSet, TSetDiscount, TSetTarget>
            where TSetDiscount : IAppliedDiscountBase<TSet, TSetDiscount>
            where TSetTarget : ISalesItemTargetBase
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (context.Set<TSet>() is null)
            {
                // Must wrap in null check for unit tests
                return;
            }
            if (context.Set<TSet>().Any(x => x.Active && x.ProductID == relatedID))
            {
                throw new InvalidOperationException(
                    "Cannot delete or deactivate this record as it is tied by association to at least one record of"
                    + $" type {typeof(TSet).Name} which should not be deleted or deactivated. If you still wish to"
                    + " delete or deactivate this record, you must delete or deactivate the association first, which"
                    + " may require performing the action against the database directly.");
            }
        }

        /// <summary>Max call is specified to avoid having an infinite loop. If the product is too deep, we will return
        /// false to avoid harming the server.</summary>
        /// <param name="product">           The product.</param>
        /// <param name="accountStoreIDs">   The account store IDs.</param>
        /// <param name="maxCall">           The maximum call.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private async Task<bool> CheckIfProductInStoreAsync(
            IProductModel product,
            ICollection<int> accountStoreIDs,
            int maxCall,
            string? contextProfileName)
        {
            if (--maxCall <= 0)
            {
                return false;
            }
            checkedProductIds ??= new();
            // Already checked that product, we are in a loop
            if (checkedProductIds.Contains(product.ID) == false)
            {
                return false;
            }
            checkedProductIds.Add(product.ID);
            if (product.Stores!.Any(pc => accountStoreIDs.Contains(pc.StoreID)))
            {
                return true;
            }
            var associatedProducts = product.ProductsAssociatedWith;
            if (associatedProducts is null || associatedProducts.Count == 0)
            {
                return false;
            }
            foreach (var masterProductAssoc in associatedProducts)
            {
                var masterProduct = await GetAsync(masterProductAssoc.MasterID, contextProfileName).ConfigureAwait(false);
                if (masterProduct is null)
                {
                    continue;
                }
                var ret = await CheckIfProductInStoreAsync(
                        masterProduct,
                        accountStoreIDs,
                        maxCall,
                        contextProfileName)
                    .ConfigureAwait(false);
                if (ret)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Executes the limited relate workflows operation.</summary>
        /// <param name="entity">   The entity.</param>
        /// <param name="model">    The model.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task RunLimitedRelateWorkflowsAsync(
            IProduct entity,
            IProductModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            await RelateOptionalTotalPurchasedAmountCurrencyAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            await RelateOptionalRestockingFeeAmountCurrencyAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            await RelateRequiredTypeAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            await RelateRequiredStatusAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            await RelateShippingPackagesAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }

        /// <summary>Clean a package.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="typeName">          Name of the type.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private async Task<bool> CleanAPackageAsync(IPackageModel? model, string typeName, string? contextProfileName)
        {
            if (model is null)
            {
                // As we don't have an object to check, rely on flattening properties
                return true;
            }
            switch (model.Name)
            {
                case "No Package":
                case "No Master Pack":
                case "No Pallet":
                {
                    // Clear the object
                    return false;
                }
            }
            if (model.Width <= 0
                && model.Depth <= 0
                && model.Height <= 0
                && model.Weight <= 0
                && model.DimensionalWeight <= 0
                && model.Name != "Download")
            {
                // Invalid Package Model (all 0s)
                // Clear the object
                return false;
            }
            if (!Contract.CheckValidKey(model.Name) && model.IsCustom)
            {
                model.Name = $"Custom Package for {model.Name}";
            }
            if (Contract.CheckValidKey(model.Name)
                && model.Name!.Contains("custom")
                && !model.IsCustom)
            {
                model.IsCustom = true;
            }
            if (!Contract.CheckValidKey(model.WidthUnitOfMeasure))
            {
                model.WidthUnitOfMeasure = "in";
            }
            if (!Contract.CheckValidKey(model.DepthUnitOfMeasure))
            {
                model.DepthUnitOfMeasure = "in";
            }
            if (!Contract.CheckValidKey(model.HeightUnitOfMeasure))
            {
                model.HeightUnitOfMeasure = "in";
            }
            if (!Contract.CheckValidKey(model.WeightUnitOfMeasure))
            {
                model.WeightUnitOfMeasure = "lbs";
            }
            if (!Contract.CheckValidKey(model.DimensionalWeightUnitOfMeasure))
            {
                model.DimensionalWeightUnitOfMeasure = "lbs";
            }
            model.TypeID = await Workflows.PackageTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: model.TypeID,
                    byKey: null,
                    byName: null,
                    byDisplayName: typeName,
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            model.Type = null;
            return true;
        }

        /// <summary>Relate shipping packages.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task RelateShippingPackagesAsync(
            IProduct entity,
            IProductModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                await EnsurePackageTypeIDsAsync(context).ConfigureAwait(false);
            }
            if (await CleanAPackageAsync(model.Package, "Package", contextProfileName).ConfigureAwait(false))
            {
                await RelateNullablePackageAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            }
            else
            {
                await ClearPackageAsync(entity).ConfigureAwait(false);
            }
            // Master Pack
            if (await CleanAPackageAsync(model.MasterPack, "Master Pack", contextProfileName).ConfigureAwait(false))
            {
                await RelateNullableMasterPackAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            }
            else
            {
                await ClearMasterPackAsync(entity).ConfigureAwait(false);
            }
            // Pallet
            if (await CleanAPackageAsync(model.Pallet, "Pallet", contextProfileName).ConfigureAwait(false))
            {
                await RelateNullablePalletAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            }
            else
            {
                await ClearPalletAsync(entity).ConfigureAwait(false);
            }
            // Assign Counts
            entity.QuantityMasterPackPerPallet = model.QuantityMasterPackPerPallet;
            entity.QuantityPerPallet = model.QuantityPerPallet;
            entity.QuantityPerMasterPack = model.QuantityPerMasterPack;
            entity.QuantityMasterPackPerLayer = model.QuantityMasterPackPerLayer;
            entity.QuantityPerLayer = model.QuantityPerLayer;
            entity.QuantityLayersPerPallet = model.QuantityLayersPerPallet;
            entity.QuantityMasterPackLayersPerPallet = model.QuantityMasterPackLayersPerPallet;
        }

        /// <summary>Relate nullable package.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        // ReSharper disable once CognitiveComplexity, CyclomaticComplexity, FunctionComplexityOverflow
        private async Task RelateNullablePackageAsync(
            IProduct entity,
            IProductModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Validate the model isn't all zeros
            if (model.Package?.IsCustom == true
                && model.Package.Weight <= 0
                && model.Package.Width <= 0
                && model.Package.Depth <= 0
                && model.Package.Height <= 0
                && model.Package.DimensionalWeight <= 0)
            {
                // This is not a valid model
                model.Package = null;
            }
            var isNoPackage = model.PackageName == "No Package" || model.Package?.Name == "No Package";
            if (isNoPackage)
            {
                entity.PackageID = null;
                entity.Package = null;
                return;
            }
            // Look up the Package by multiple means and pick what we think is the best one, if any come back
            var flatKeyIsNull = !Contract.CheckValidKey(model.PackageKey);
            var modelCustomKeyIsNull = !Contract.CheckValidKey(model.Package?.CustomKey);
            var flatKeyMatchesModelCustomKey = !flatKeyIsNull && !modelCustomKeyIsNull && model.PackageKey == model.Package?.CustomKey;
            var flatNameIsNull = !Contract.CheckValidKey(model.PackageName);
            var modelNameIsNull = !Contract.CheckValidKey(model.Package?.Name);
            var flatNameMatchesModelName = !flatNameIsNull && !modelNameIsNull && model.PackageName == model.Package?.Name;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.Packages.FilterByActive(true).FilterByTypeID(PackageTypeID);
            var resolvedIDByFlatKey = !flatKeyIsNull ? await query.FilterByCustomKey(model.PackageKey, true).Select(x => (int?)x.ID).FirstOrDefaultAsync() : null;
            var resolvedIDByModelCustomKey = !modelCustomKeyIsNull ? await query.FilterByCustomKey(model.Package?.CustomKey, true).Select(x => (int?)x.ID).FirstOrDefaultAsync() : null;
            var resolvedIDByKey = flatKeyMatchesModelCustomKey ? resolvedIDByFlatKey ?? resolvedIDByModelCustomKey : resolvedIDByModelCustomKey ?? resolvedIDByFlatKey;
            var resolvedIDByKeyIsNull = !Contract.CheckValidID(resolvedIDByKey);
            var resolvedIDByFlatName = !flatNameIsNull ? await query.FilterByName(model.PackageName, true).Select(x => (int?)x.ID).FirstOrDefaultAsync() : null;
            var resolvedIDByModelName = !modelNameIsNull ? await query.FilterByName(model.Package?.Name, true).Select(x => (int?)x.ID).FirstOrDefaultAsync() : null;
            var resolvedIDByName = flatNameMatchesModelName ? resolvedIDByFlatName ?? resolvedIDByModelCustomKey : resolvedIDByModelName ?? resolvedIDByFlatName;
            var resolvedIDByNameIsNull = !Contract.CheckValidID(resolvedIDByName);
            var resolvedID = (!resolvedIDByKeyIsNull && !resolvedIDByNameIsNull && resolvedIDByKey == resolvedIDByName ? resolvedIDByKey : null)
                ?? (!resolvedIDByKeyIsNull ? resolvedIDByKey : null)
                ?? (!resolvedIDByNameIsNull ? resolvedIDByName : null);
            var resolvedIDIsNull = !Contract.CheckValidID(resolvedID);
            var resolvedEntity = resolvedIDIsNull ? null : await query.FilterByID(resolvedID).FirstOrDefaultAsync();
            if (!resolvedIDIsNull
                && resolvedEntity is not null
                && model.Package is not null
                && resolvedEntity.GetMatchCode() != model.Package.GetMatchCode())
            {
                // Update the found entity with new values since they are different
                resolvedEntity.UpdatePackageFromModel(model.Package, timestamp, timestamp);
            }
            var resolvedModel = !resolvedIDIsNull && resolvedEntity is not null
                ? resolvedEntity.CreatePackageModelFromEntityFull(contextProfileName)
                : model.Package;
            var entityIDIsNull = !Contract.CheckValidID(entity.PackageID);
            var modelIDIsNull = !Contract.CheckValidID(resolvedModel?.ID);
            var entityObjectIsNull = entity.Package is null;
            var modelObjectIsNull = resolvedModel is null;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.PackageID == resolvedModel?.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.Package?.ID == resolvedModel?.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // Scenario 2: They match IDs, just update the entity from the model if it is present
                if (entityObjectIsNull || modelObjectIsNull)
                {
                    return;
                }
                if (model.Package is not null)
                {
                    updateResolvedPackageFromIncomingModel(resolvedModel!, model.Package!, timestamp);
                }
                entity.Package!.UpdatePackageFromModel(resolvedModel!, timestamp, timestamp);
                entity.Package!.TypeID = PackageTypeID!.Value;
                return;
            }
            if (!entityIDIsNull && !modelIDIsNull)
            {
                // Scenario 3: We have IDs but they don't match, assign the model's Package ID to the entity's Package ID
                entity.PackageID = resolvedModel!.ID;
                return;
            }
            if (entityIDIsNull && !modelIDIsNull)
            {
                // Scenario 4: We have an ID on the model's side and a null one on the entity's side, assign the model's Package ID to the entity's Package ID
                entity.PackageID = resolvedModel!.ID;
                return;
            }
            var modelObjectIDIsNull = !modelObjectIsNull && !Contract.CheckValidID(resolvedModel!.ID);
            var modelObjectIsActive = !modelObjectIsNull && resolvedModel!.Active;
            if (!entityIDIsNull && !modelObjectIsNull && !modelObjectIDIsNull)
            {
                if (modelObjectIsActive)
                {
                    // Scenario 5: We have IDs but they don't match, assign the model's Package ID to the entity's Package ID (from the model object)
                    entity.PackageID = resolvedModel!.ID;
                    return;
                }
                // Scenario 6: We have IDs but they don't match and the Package model has been deactivated, remove the Package entity from it's master
                entity.PackageID = null;
                entity.Package = null;
                return;
            }
            if (entityIDIsNull && !modelObjectIsNull && !modelObjectIDIsNull)
            {
                if (modelObjectIsActive)
                {
                    // Scenario 7: We have IDs but they don't match, assign the model's Package ID to the entity's Package ID (from the model object)
                    entity.PackageID = resolvedModel!.ID;
                    return;
                }
                // Scenario 8: We have IDs but they don't match and the Package model has been deactivated, remove the Package entity from it's master
                entity.PackageID = null;
                entity.Package = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // Scenario 9: We have an entity id, but a new model, remove the id on the entity and assign the new model
                updateResolvedPackageFromIncomingModel(resolvedModel!, model.Package!, timestamp);
                resolvedModel!.TypeID = PackageTypeID!.Value;
                resolvedModel.Type = null;
                resolvedModel.TypeKey = resolvedModel.TypeName = resolvedModel.TypeDisplayName = null;
                entity.PackageID = (await Workflows.Packages.UpsertAsync(resolvedModel, contextProfileName).ConfigureAwait(false)).Result;
                return;
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // Scenario 10: We don't have an entity id, and we have a new model, assign the new model
                updateResolvedPackageFromIncomingModel(resolvedModel!, model.Package!, timestamp);
                entity.Package = (Package)resolvedModel!.CreatePackageEntity(timestamp, contextProfileName);
                if (entity.Package is not null)
                {
                    // Wrap in null check for tests
                    entity.Package.TypeID = PackageTypeID!.Value;
                }
                return;
            }
            if (entityObjectIsNull || !modelObjectIsActive || !entityObjectAndModelObjectHaveSameID)
            {
                return;
            }
            // TODO: Determine 'Equals' status between the objects so we only update if different
            // Scenario 11: We have data on both sides, update the object, assign the values using the Update action
            updateResolvedPackageFromIncomingModel(resolvedModel!, model.Package!, timestamp);
            entity.Package!.UpdatePackageFromModel(resolvedModel!, timestamp, timestamp);
        }

        /// <summary>Relate nullable master pack.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        // ReSharper disable once CognitiveComplexity, CyclomaticComplexity, FunctionComplexityOverflow
        private async Task RelateNullableMasterPackAsync(
            IProduct entity,
            IProductModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Validate the model isn't all zeros
            if (model.MasterPack?.IsCustom == true
                && model.MasterPack.Weight <= 0
                && model.MasterPack.Width <= 0
                && model.MasterPack.Depth <= 0
                && model.MasterPack.Height <= 0
                && model.MasterPack.DimensionalWeight <= 0)
            {
                // This is not a valid model
                model.MasterPack = null;
            }
            var isNoMasterPack = model.MasterPackName == "No Master Pack" || model.MasterPack?.Name == "No Master Pack";
            if (isNoMasterPack)
            {
                entity.MasterPackID = null;
                entity.MasterPack = null;
                return;
            }
            // Look up the MasterPack by multiple means and pick what we think is the best one, if any come back
            var flatKeyIsNull = !Contract.CheckValidKey(model.MasterPackKey);
            var modelCustomKeyIsNull = !Contract.CheckValidKey(model.MasterPack?.CustomKey);
            var flatKeyMatchesModelCustomKey = !flatKeyIsNull && !modelCustomKeyIsNull && model.MasterPackKey == model.MasterPack?.CustomKey;
            var flatNameIsNull = !Contract.CheckValidKey(model.MasterPackName);
            var modelNameIsNull = !Contract.CheckValidKey(model.MasterPack?.Name);
            var flatNameMatchesModelName = !flatNameIsNull && !modelNameIsNull && model.MasterPackName == model.MasterPack?.Name;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.Packages.FilterByActive(true);
            var resolvedIDByFlatKey = !flatKeyIsNull ? await query.FilterByCustomKey(model.MasterPackKey, true).Select(x => (int?)x.ID).FirstOrDefaultAsync() : null;
            var resolvedIDByModelCustomKey = !modelCustomKeyIsNull ? await query.FilterByCustomKey(model.MasterPack?.CustomKey, true).Select(x => (int?)x.ID).FirstOrDefaultAsync() : null;
            var resolvedIDByKey = flatKeyMatchesModelCustomKey ? resolvedIDByFlatKey ?? resolvedIDByModelCustomKey : resolvedIDByModelCustomKey ?? resolvedIDByFlatKey;
            var resolvedIDByKeyIsNull = !Contract.CheckValidID(resolvedIDByKey);
            var resolvedIDByFlatName = !flatNameIsNull ? await query.FilterByName(model.MasterPackName, true).Select(x => (int?)x.ID).FirstOrDefaultAsync() : null;
            var resolvedIDByModelName = !modelNameIsNull ? await query.FilterByName(model.MasterPack?.Name, true).Select(x => (int?)x.ID).FirstOrDefaultAsync() : null;
            var resolvedIDByName = flatNameMatchesModelName ? resolvedIDByFlatName ?? resolvedIDByModelCustomKey : resolvedIDByModelName ?? resolvedIDByFlatName;
            var resolvedIDByNameIsNull = !Contract.CheckValidID(resolvedIDByName);
            var resolvedID = (!resolvedIDByKeyIsNull && !resolvedIDByNameIsNull && resolvedIDByKey == resolvedIDByName ? resolvedIDByKey : null)
                ?? (!resolvedIDByKeyIsNull ? resolvedIDByKey : null)
                ?? (!resolvedIDByNameIsNull ? resolvedIDByName : null);
            var resolvedIDIsNull = !Contract.CheckValidID(resolvedID);
            var resolvedEntity = resolvedIDIsNull ? null : await query.FilterByID(resolvedID).FirstOrDefaultAsync();
            if (!resolvedIDIsNull
                && resolvedEntity is not null
                && model.MasterPack is not null
                && resolvedEntity.GetMatchCode() != model.MasterPack.GetMatchCode())
            {
                // Update the found entity with new values since they are different
                resolvedEntity.UpdatePackageFromModel(model.MasterPack, timestamp, timestamp);
            }
            var resolvedModel = !resolvedIDIsNull && resolvedEntity is not null
                ? resolvedEntity.CreatePackageModelFromEntityFull(contextProfileName)
                : model.MasterPack;
            var entityIDIsNull = !Contract.CheckValidID(entity.MasterPackID);
            var modelIDIsNull = !Contract.CheckValidID(resolvedModel?.ID);
            var entityObjectIsNull = entity.MasterPack is null;
            var modelObjectIsNull = resolvedModel is null;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.MasterPackID == resolvedModel?.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.MasterPack!.ID == resolvedModel!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // Scenario 2: They match IDs, just update the entity from the model if it is present
                if (entityObjectIsNull || modelObjectIsNull)
                {
                    return;
                }
                updateResolvedPackageFromIncomingModel(resolvedModel!, model.MasterPack!, timestamp);
                entity.MasterPack!.UpdatePackageFromModel(resolvedModel!, timestamp, timestamp);
                entity.MasterPack!.TypeID = MasterPackTypeID!.Value;
                return;
            }
            if (!entityIDIsNull && !modelIDIsNull)
            {
                // Scenario 3: We have IDs but they don't match, assign the model's MasterPack ID to the entity's MasterPack ID
                entity.MasterPackID = resolvedModel!.ID;
                return;
            }
            if (entityIDIsNull && !modelIDIsNull)
            {
                // Scenario 4: We have an ID on the model's side and a null one on the entity's side, assign the model's MasterPack ID to the entity's MasterPack ID
                entity.MasterPackID = resolvedModel!.ID;
                return;
            }
            var modelObjectIDIsNull = !modelObjectIsNull && !Contract.CheckValidID(resolvedModel!.ID);
            var modelObjectIsActive = !modelObjectIsNull && resolvedModel!.Active;
            if (!entityIDIsNull && !modelObjectIsNull && !modelObjectIDIsNull)
            {
                if (modelObjectIsActive)
                {
                    // Scenario 5: We have IDs but they don't match, assign the model's MasterPack ID to the entity's MasterPack ID (from the model object)
                    entity.MasterPackID = resolvedModel!.ID;
                    return;
                }
                // Scenario 6: We have IDs but they don't match and the MasterPack model has been deactivated, remove the MasterPack entity from it's master
                entity.MasterPackID = null;
                entity.MasterPack = null;
                return;
            }
            if (entityIDIsNull && !modelObjectIsNull && !modelObjectIDIsNull)
            {
                if (modelObjectIsActive)
                {
                    // Scenario 7: We have IDs but they don't match, assign the model's MasterPack ID to the entity's MasterPack ID (from the model object)
                    entity.MasterPackID = resolvedModel!.ID;
                    return;
                }
                // Scenario 8: We have IDs but they don't match and the MasterPack model has been deactivated, remove the MasterPack entity from it's master
                entity.MasterPackID = null;
                entity.MasterPack = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // Scenario 9: We have an entity id, but a new model, remove the id on the entity and assign the new model
                updateResolvedPackageFromIncomingModel(resolvedModel!, model.MasterPack!, timestamp);
                resolvedModel!.TypeID = MasterPackTypeID!.Value;
                resolvedModel.Type = null;
                resolvedModel.TypeKey = resolvedModel.TypeName = resolvedModel.TypeDisplayName = null;
                entity.MasterPackID = (await Workflows.Packages.UpsertAsync(resolvedModel, contextProfileName).ConfigureAwait(false)).Result;
                return;
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // Scenario 10: We don't have an entity id, and we have a new model, assign the new model
                updateResolvedPackageFromIncomingModel(resolvedModel!, model.MasterPack!, timestamp);
                entity.MasterPack = (Package)resolvedModel!.CreatePackageEntity(timestamp, contextProfileName);
                if (entity.MasterPack is not null)
                {
                    // Wrap in null check for tests
                    entity.MasterPack.TypeID = MasterPackTypeID!.Value;
                }
                return;
            }
            if (entityObjectIsNull || !modelObjectIsActive || !entityObjectAndModelObjectHaveSameID)
            {
                return;
            }
            // TODO: Determine 'Equals' status between the objects so we only update if different
            // Scenario 11: We have data on both sides, update the object, assign the values using the Update action
            updateResolvedPackageFromIncomingModel(resolvedModel!, model.MasterPack!, timestamp);
            entity.MasterPack!.UpdatePackageFromModel(resolvedModel!, timestamp, timestamp);
            entity.MasterPack!.TypeID = MasterPackTypeID!.Value;
        }

        /// <summary>Relate nullable pallet.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        // ReSharper disable once CognitiveComplexity, CyclomaticComplexity, FunctionComplexityOverflow
        private async Task RelateNullablePalletAsync(
            IProduct entity,
            IProductModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Validate the model isn't all zeros
            if (model.Pallet?.IsCustom == true
                && model.Pallet.Weight <= 0
                && model.Pallet.Width <= 0
                && model.Pallet.Depth <= 0
                && model.Pallet.Height <= 0
                && model.Pallet.DimensionalWeight <= 0)
            {
                // This is not a valid model
                model.Pallet = null;
            }
            var isNoPallet = model.PalletName == "No Pallet" || model.Pallet?.Name == "No Pallet";
            if (isNoPallet)
            {
                entity.PalletID = null;
                entity.Pallet = null;
                return;
            }
            // Look up the Pallet by multiple means and pick what we think is the best one, if any come back
            var flatKeyIsNull = !Contract.CheckValidKey(model.PalletKey);
            var modelCustomKeyIsNull = !Contract.CheckValidKey(model.Pallet?.CustomKey);
            var flatKeyMatchesModelCustomKey = !flatKeyIsNull && !modelCustomKeyIsNull && model.PalletKey == model.Pallet?.CustomKey;
            var flatNameIsNull = !Contract.CheckValidKey(model.PalletName);
            var modelNameIsNull = !Contract.CheckValidKey(model.Pallet?.Name);
            var flatNameMatchesModelName = !flatNameIsNull && !modelNameIsNull && model.PalletName == model.Pallet?.Name;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.Packages.FilterByActive(true);
            var resolvedIDByFlatKey = !flatKeyIsNull ? await query.FilterByCustomKey(model.PalletKey, true).Select(x => (int?)x.ID).FirstOrDefaultAsync() : null;
            var resolvedIDByModelCustomKey = !modelCustomKeyIsNull ? await query.FilterByCustomKey(model.Pallet?.CustomKey, true).Select(x => (int?)x.ID).FirstOrDefaultAsync() : null;
            var resolvedIDByKey = flatKeyMatchesModelCustomKey ? resolvedIDByFlatKey ?? resolvedIDByModelCustomKey : resolvedIDByModelCustomKey ?? resolvedIDByFlatKey;
            var resolvedIDByKeyIsNull = !Contract.CheckValidID(resolvedIDByKey);
            var resolvedIDByFlatName = !flatNameIsNull ? await query.FilterByName(model.PalletName, true).Select(x => (int?)x.ID).FirstOrDefaultAsync() : null;
            var resolvedIDByModelName = !modelNameIsNull ? await query.FilterByName(model.Pallet?.Name, true).Select(x => (int?)x.ID).FirstOrDefaultAsync() : null;
            var resolvedIDByName = flatNameMatchesModelName ? resolvedIDByFlatName ?? resolvedIDByModelCustomKey : resolvedIDByModelName ?? resolvedIDByFlatName;
            var resolvedIDByNameIsNull = !Contract.CheckValidID(resolvedIDByName);
            var resolvedID = (!resolvedIDByKeyIsNull && !resolvedIDByNameIsNull && resolvedIDByKey == resolvedIDByName ? resolvedIDByKey : null)
                ?? (!resolvedIDByKeyIsNull ? resolvedIDByKey : null)
                ?? (!resolvedIDByNameIsNull ? resolvedIDByName : null);
            var resolvedIDIsNull = !Contract.CheckValidID(resolvedID);
            var resolvedEntity = resolvedIDIsNull ? null : await query.FilterByID(resolvedID).FirstOrDefaultAsync();
            if (!resolvedIDIsNull
                && resolvedEntity is not null
                && model.Pallet is not null
                && resolvedEntity.GetMatchCode() != model.Pallet.GetMatchCode())
            {
                // Update the found entity with new values since they are different
                resolvedEntity.UpdatePackageFromModel(model.Pallet, timestamp, timestamp);
            }
            var resolvedModel = !resolvedIDIsNull && resolvedEntity is not null
                ? resolvedEntity.CreatePackageModelFromEntityFull(contextProfileName)
                : model.Pallet;
            var entityIDIsNull = !Contract.CheckValidID(entity.PalletID);
            var modelIDIsNull = !Contract.CheckValidID(resolvedModel?.ID);
            var entityObjectIsNull = entity.Pallet is null;
            var modelObjectIsNull = resolvedModel is null;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.PalletID == resolvedModel?.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.Pallet!.ID == resolvedModel!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // Scenario 2: They match IDs, just update the entity from the model if it is present
                if (entityObjectIsNull || modelObjectIsNull)
                {
                    return;
                }
                updateResolvedPackageFromIncomingModel(resolvedModel!, model.Pallet!, timestamp);
                entity.Pallet!.UpdatePackageFromModel(resolvedModel!, timestamp, timestamp);
                entity.Pallet!.TypeID = PalletTypeID!.Value;
                return;
            }
            if (!entityIDIsNull && !modelIDIsNull)
            {
                // Scenario 3: We have IDs but they don't match, assign the model's Pallet ID to the entity's Pallet ID
                entity.PalletID = resolvedModel!.ID;
                return;
            }
            if (entityIDIsNull && !modelIDIsNull)
            {
                // Scenario 4: We have an ID on the model's side and a null one on the entity's side, assign the model's Pallet ID to the entity's Pallet ID
                entity.PalletID = resolvedModel!.ID;
                return;
            }
            var modelObjectIDIsNull = !modelObjectIsNull && !Contract.CheckValidID(resolvedModel!.ID);
            var modelObjectIsActive = !modelObjectIsNull && resolvedModel!.Active;
            if (!entityIDIsNull && !modelObjectIsNull && !modelObjectIDIsNull)
            {
                if (modelObjectIsActive)
                {
                    // Scenario 5: We have IDs but they don't match, assign the model's Pallet ID to the entity's Pallet ID (from the model object)
                    entity.PalletID = resolvedModel!.ID;
                    return;
                }
                // Scenario 6: We have IDs but they don't match and the Pallet model has been deactivated, remove the Pallet entity from it's master
                entity.PalletID = null;
                entity.Pallet = null;
                return;
            }
            if (entityIDIsNull && !modelObjectIsNull && !modelObjectIDIsNull)
            {
                if (modelObjectIsActive)
                {
                    // Scenario 7: We have IDs but they don't match, assign the model's Pallet ID to the entity's Pallet ID (from the model object)
                    entity.PalletID = resolvedModel!.ID;
                    return;
                }
                // Scenario 8: We have IDs but they don't match and the Pallet model has been deactivated, remove the Pallet entity from it's master
                entity.PalletID = null;
                entity.Pallet = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // Scenario 9: We have an entity id, but a new model, remove the id on the entity and assign the new model
                updateResolvedPackageFromIncomingModel(resolvedModel!, model.Pallet!, timestamp);
                resolvedModel!.TypeID = PalletTypeID!.Value;
                resolvedModel.Type = null;
                resolvedModel.TypeKey = resolvedModel.TypeName = resolvedModel.TypeDisplayName = null;
                entity.PalletID = (await Workflows.Packages.UpsertAsync(resolvedModel, contextProfileName).ConfigureAwait(false)).Result;
                return;
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // Scenario 10: We don't have an entity id, and we have a new model, assign the new model
                updateResolvedPackageFromIncomingModel(resolvedModel!, model.Pallet!, timestamp);
                entity.Pallet = (Package)resolvedModel!.CreatePackageEntity(timestamp, contextProfileName);
                if (entity.Pallet is not null)
                {
                    // Wrap in null check for tests
                    entity.Pallet.TypeID = PalletTypeID!.Value;
                }
                return;
            }
            if (entityObjectIsNull || !modelObjectIsActive || !entityObjectAndModelObjectHaveSameID)
            {
                return;
            }
            // TODO: Determine 'Equals' status between the objects so we only update if different
            // Scenario 11: We have data on both sides, update the object, assign the values using the Update action
            updateResolvedPackageFromIncomingModel(resolvedModel!, model.Pallet!, timestamp);
            entity.Pallet!.UpdatePackageFromModel(resolvedModel!, timestamp, timestamp);
            entity.Pallet!.TypeID = PalletTypeID!.Value;
        }
    }
}
