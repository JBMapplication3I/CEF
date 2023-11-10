// <copyright file="ProductMapper.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product mapper class</summary>
// ReSharper disable CyclomaticComplexity, MergeConditionalExpression
#pragma warning disable IDE0031 // Use null propagation
namespace Clarity.Ecommerce.Mapper
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using LinqKit;
    using Models;
    using Utilities;

    public static partial class ModelMapperForProduct
    {
        /// <summary>The map lite product old.</summary>
        public static readonly Func<IProduct, IProductModel> MapLiteProductOld = x => new ProductModel
        {
            // Base Properties
            ID = x.ID,
            CustomKey = x.CustomKey,
            Active = x.Active,
            CreatedDate = x.CreatedDate,
            UpdatedDate = x.UpdatedDate,
            Hash = x.Hash,
            // NameableBase Properties
            Name = x.Name,
            Description = x.Description,
            // IHaveSeoBase Properties
            SeoUrl = x.SeoUrl,
            SeoPageTitle = x.SeoPageTitle,
            SeoKeywords = x.SeoKeywords,
            SeoDescription = x.SeoDescription,
            SeoMetaData = x.SeoMetaData,
            // IHaveNullableDimensions Properties
            Weight = x.Weight,
            WeightUnitOfMeasure = x.WeightUnitOfMeasure,
            Width = x.Width,
            WidthUnitOfMeasure = x.WidthUnitOfMeasure,
            Depth = x.Depth,
            DepthUnitOfMeasure = x.DepthUnitOfMeasure,
            Height = x.Height,
            HeightUnitOfMeasure = x.HeightUnitOfMeasure,
            // Flags/Toggles
            IsVisible = x.IsVisible,
            IsDiscontinued = x.IsDiscontinued,
            IsEligibleForReturn = x.IsEligibleForReturn,
            IsTaxable = x.IsTaxable,
            AllowBackOrder = x.AllowBackOrder,
            AllowPreSale = x.AllowPreSale,
            IsUnlimitedStock = x.IsUnlimitedStock,
            IsFreeShipping = x.IsFreeShipping,
            NothingToShip = x.NothingToShip,
            DropShipOnly = x.DropShipOnly,
            ShippingLeadTimeIsCalendarDays = x.ShippingLeadTimeIsCalendarDays,
            // Descriptors
            ShortDescription = x.ShortDescription,
            ManufacturerPartNumber = x.ManufacturerPartNumber,
            BrandName = x.BrandName,
            TaxCode = x.TaxCode,
            UnitOfMeasure = x.UnitOfMeasure ?? "EACH",
            // ReSharper disable once ConstantNullCoalescingCondition
            SortOrder = /*x.ProductCategories?.Where(y => y.Active).FirstOrDefault(pc => pc.SortOrder.HasValue)?.SortOrder ??*/ x.SortOrder,
            // Fees
            HandlingCharge = x.HandlingCharge,
            FlatShippingCharge = x.FlatShippingCharge,
            RestockingFeePercent = x.RestockingFeePercent,
            RestockingFeeAmount = x.RestockingFeeAmount,
            // Availability, Stock, Shipping Requirements
            AvailableStartDate = x.AvailableStartDate,
            AvailableEndDate = x.AvailableEndDate,
            PreSellEndDate = x.PreSellEndDate,
            QuantityPerMasterPack = x.QuantityPerMasterPack,
            QuantityMasterPackPerLayer = x.QuantityMasterPackPerLayer,
            QuantityMasterPackLayersPerPallet = x.QuantityMasterPackLayersPerPallet,
            QuantityMasterPackPerPallet = x.QuantityMasterPackPerPallet,
            QuantityPerLayer = x.QuantityPerLayer,
            QuantityLayersPerPallet = x.QuantityLayersPerPallet,
            QuantityPerPallet = x.QuantityPerPallet,
            KitBaseQuantityPriceMultiplier = x.KitBaseQuantityPriceMultiplier ?? 1m,
            ShippingLeadTimeDays = x.ShippingLeadTimeDays,
            RequiresRoles = x.RequiresRoles,
            RequiresRolesAlt = x.RequiresRolesAlt,
            // Min/Max Purchase Per Order
            MinimumPurchaseQuantity = x.MinimumPurchaseQuantity,
            MinimumPurchaseQuantityIfPastPurchased = x.MinimumPurchaseQuantityIfPastPurchased,
            MaximumPurchaseQuantity = x.MaximumPurchaseQuantity,
            MaximumPurchaseQuantityIfPastPurchased = x.MaximumPurchaseQuantityIfPastPurchased,
            MaximumBackOrderPurchaseQuantity = x.MaximumBackOrderPurchaseQuantity,
            MaximumBackOrderPurchaseQuantityIfPastPurchased = x.MaximumBackOrderPurchaseQuantityIfPastPurchased,
            MaximumBackOrderPurchaseQuantityGlobal = x.MaximumBackOrderPurchaseQuantityGlobal,
            MaximumPrePurchaseQuantity = x.MaximumPrePurchaseQuantity,
            MaximumPrePurchaseQuantityIfPastPurchased = x.MaximumPrePurchaseQuantityIfPastPurchased,
            MaximumPrePurchaseQuantityGlobal = x.MaximumPrePurchaseQuantityGlobal,
            // Required Document
            DocumentRequiredForPurchase = x.DocumentRequiredForPurchase,
            DocumentRequiredForPurchaseMissingWarningMessage = x.DocumentRequiredForPurchaseMissingWarningMessage,
            DocumentRequiredForPurchaseExpiredWarningMessage = x.DocumentRequiredForPurchaseExpiredWarningMessage,
            DocumentRequiredForPurchaseOverrideFee = x.DocumentRequiredForPurchaseOverrideFee,
            DocumentRequiredForPurchaseOverrideFeeAcceptedMessage = x.DocumentRequiredForPurchaseOverrideFeeAcceptedMessage,
            DocumentRequiredForPurchaseOverrideFeeIsPercent = x.DocumentRequiredForPurchaseOverrideFeeIsPercent,
            DocumentRequiredForPurchaseOverrideFeeWarningMessage = x.DocumentRequiredForPurchaseOverrideFeeWarningMessage,
            // Must Purchase In Multiples Of
            MustPurchaseInMultiplesOfAmount = x.MustPurchaseInMultiplesOfAmount,
            MustPurchaseInMultiplesOfAmountWarningMessage = x.MustPurchaseInMultiplesOfAmountWarningMessage,
            MustPurchaseInMultiplesOfAmountOverrideFee = x.MustPurchaseInMultiplesOfAmountOverrideFee,
            MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent = x.MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent,
            MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage = x.MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage,
            MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage = x.MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage,
            // Analytics filled data
            TotalPurchasedAmount = x.TotalPurchasedAmount,
            TotalPurchasedAmountCurrencyID = x.TotalPurchasedAmountCurrencyID,
            TotalPurchasedQuantity = x.TotalPurchasedQuantity,
            // Convenience Properties
            PrimaryImageFileName = x.Images
                ?.Where(img => img.Active)
                .OrderByDescending(img => img.IsPrimary)
                .ThenByDescending(img => img.OriginalWidth)
                .Take(1)
                .Select(img => img.ThumbnailFileName ?? img.OriginalFileName)
                .FirstOrDefault(),
            // Related Objects
            TypeID = x.TypeID,
            TypeKey = x.Type != null ? x.Type.CustomKey : null,
            TypeName = x.Type != null ? x.Type.Name : null,
            TypeDisplayName = x.Type != null ? x.Type.DisplayName : null,
            TypeSortOrder = x.Type != null ? x.Type.SortOrder : null,
            StatusID = x.StatusID,
            StatusKey = x.Status != null ? x.Status.CustomKey : null,
            StatusName = x.Status != null ? x.Status.Name : null,
            StatusDisplayName = x.Status != null ? x.Status.DisplayName : null,
            StatusSortOrder = x.Status != null ? x.Status.SortOrder : null,
            PackageID = x.PackageID,
            PackageKey = x.Package != null ? x.Package.CustomKey : null,
            PackageName = x.Package != null ? x.Package.Name : null,
            MasterPackID = x.MasterPackID,
            MasterPackKey = x.MasterPack != null ? x.MasterPack.CustomKey : null,
            MasterPackName = x.MasterPack != null ? x.MasterPack.Name : null,
            PalletID = x.PalletID,
            PalletKey = x.Pallet != null ? x.Pallet.CustomKey : null,
            PalletName = x.Pallet != null ? x.Pallet.Name : null,
            // Associated Objects
            // JsonAttributes = x.JsonAttributes,
            SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary(),
        };

        /// <summary>The map listing product old.</summary>
        public static readonly Func<IProduct, IProductModel> MapListingProductOld = x => new ProductModel
        {
            ID = x.ID,
            CustomKey = x.CustomKey,
            Active = x.Active,
            CreatedDate = x.CreatedDate,
            Name = x.Name,
            ManufacturerPartNumber = x.ManufacturerPartNumber,
            BrandName = x.BrandName,
            IsTaxable = x.IsTaxable,
            IsVisible = x.IsVisible,
            SeoUrl = x.SeoUrl,
            TypeID = x.TypeID,
            StatusID = x.StatusID,
            // JsonAttributes = x.JsonAttributes,
            SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary(),
            PrimaryImageFileName = x.Images
                ?.Where(img => img.Active)
                .OrderByDescending(img => img.IsPrimary)
                .ThenByDescending(img => img.OriginalWidth)
                .Take(1)
                .Select(img => img.ThumbnailFileName ?? img.OriginalFileName)
                .FirstOrDefault(),
        };

        private static Func<Product, Product> ProductFullMapSelector { get; }
            = ProductSQLExtensions.GetProductFullMapSelector<Product>().Compile();

        /// <summary>Creates product model from entity for storefront.</summary>
        /// <param name="context">  The context.</param>
        /// <param name="productID">Identifier for the product.</param>
        /// <returns>The new product model from entity for storefront.</returns>
        public static IProductModel? CreateProductModelFromEntityForStorefront(
            IClarityEcommerceEntities context,
            int productID)
        {
            return context.Products
                .AsNoTracking()
                .FilterByID(productID)
                .AsExpandable()
                // ReSharper disable once PossibleUnintendedQueryableAsEnumerable (This is optimized)
                .Select(ProductFullMapSelector)
                .Take(1)
                .ToList()
                .Select(x =>
                {
                    var newModel = RegistryLoaderWrapper.GetInstance<IProductModel>(context.ContextProfileName);
                    {
                        // Base Properties
                        newModel.ID = x.ID;
                        newModel.CustomKey = x.CustomKey;
                        newModel.Active = x.Active;
                        newModel.CreatedDate = x.CreatedDate;
                        newModel.UpdatedDate = x.UpdatedDate;
                        newModel.Hash = x.Hash;
                        newModel.SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary();
                        // NameableBase Properties
                        newModel.Name = x.Name;
                        newModel.Description = x.Description;
                        // IHaveSeoBase Properties
                        newModel.SeoUrl = x.SeoUrl;
                        newModel.SeoPageTitle = x.SeoPageTitle;
                        newModel.SeoKeywords = x.SeoKeywords;
                        newModel.SeoDescription = x.SeoDescription;
                        newModel.SeoMetaData = x.SeoMetaData;
                        // IHaveNullableDimensions Properties
                        newModel.Weight = x.Weight;
                        newModel.WeightUnitOfMeasure = x.WeightUnitOfMeasure;
                        newModel.Width = x.Width;
                        newModel.WidthUnitOfMeasure = x.WidthUnitOfMeasure;
                        newModel.Depth = x.Depth;
                        newModel.DepthUnitOfMeasure = x.DepthUnitOfMeasure;
                        newModel.Height = x.Height;
                        newModel.HeightUnitOfMeasure = x.HeightUnitOfMeasure;
                        // Flags/Toggles
                        newModel.IsVisible = x.IsVisible;
                        newModel.IsDiscontinued = x.IsDiscontinued;
                        if (JSConfigs.CEFConfigDictionary.TaxesEnabled)
                        {
                            newModel.IsTaxable = x.IsTaxable;
                        }
                        if (JSConfigs.CEFConfigDictionary.InventoryEnabled)
                        {
                            newModel.IsUnlimitedStock = x.IsUnlimitedStock;
                        }
                        if (JSConfigs.CEFConfigDictionary.InventoryBackOrderEnabled)
                        {
                            newModel.AllowBackOrder = x.AllowBackOrder;
                        }
                        newModel.IsFreeShipping = x.IsFreeShipping;
                        newModel.NothingToShip = x.NothingToShip;
                        newModel.DropShipOnly = x.DropShipOnly;
                        newModel.ShippingLeadTimeIsCalendarDays = x.ShippingLeadTimeIsCalendarDays;
                        // Descriptors
                        newModel.ShortDescription = x.ShortDescription;
                        newModel.ManufacturerPartNumber = x.ManufacturerPartNumber;
                        newModel.RequiresRolesAlt = x.RequiresRolesAlt;
                        newModel.HCPCCode = x.HCPCCode;
                        newModel.BrandName = x.BrandName;
                        newModel.TaxCode = x.TaxCode;
                        newModel.UnitOfMeasure = x.UnitOfMeasure ?? "EACH";
                        newModel.SortOrder = x.SortOrder;
                        // Fees
                        newModel.HandlingCharge = x.HandlingCharge;
                        newModel.FlatShippingCharge = x.FlatShippingCharge;
                        // Availability, Shipping Requirements
                        newModel.PreSellEndDate = x.PreSellEndDate;
                        newModel.QuantityPerMasterPack = x.QuantityPerMasterPack;
                        newModel.QuantityMasterPackPerLayer = x.QuantityMasterPackPerLayer;
                        newModel.QuantityMasterPackLayersPerPallet = x.QuantityMasterPackLayersPerPallet;
                        newModel.QuantityMasterPackPerPallet = x.QuantityMasterPackPerPallet;
                        newModel.QuantityPerLayer = x.QuantityPerLayer;
                        newModel.QuantityLayersPerPallet = x.QuantityLayersPerPallet;
                        newModel.QuantityPerPallet = x.QuantityPerPallet;
                        newModel.KitBaseQuantityPriceMultiplier = x.KitBaseQuantityPriceMultiplier ?? 1m;
                        newModel.ShippingLeadTimeDays = x.ShippingLeadTimeDays;
                        newModel.RequiresRoles = x.RequiresRoles;
                        newModel.RequiresRolesAlt = x.RequiresRolesAlt;
                        if (JSConfigs.CEFConfigDictionary.SalesReturnsEnabled)
                        {
                            newModel.IsEligibleForReturn = x.IsEligibleForReturn;
                            newModel.RestockingFeePercent = x.RestockingFeePercent;
                            newModel.RestockingFeeAmount = x.RestockingFeeAmount;
                            newModel.RestockingFeeAmountCurrencyID = x.RestockingFeeAmountCurrencyID;
                        }
                        if (JSConfigs.CEFConfigDictionary.InventoryPreSaleEnabled)
                        {
                            newModel.AllowPreSale = x.AllowPreSale;
                        }
                        if (JSConfigs.CEFConfigDictionary.PurchasingMinMaxEnabled)
                        {
                            newModel.MinimumPurchaseQuantity = x.MinimumPurchaseQuantity;
                            newModel.MaximumPurchaseQuantity = x.MaximumPurchaseQuantity;
                            newModel.MaximumBackOrderPurchaseQuantity = x.MaximumBackOrderPurchaseQuantity;
                            newModel.MaximumBackOrderPurchaseQuantityGlobal = x.MaximumBackOrderPurchaseQuantityGlobal;
                            if (JSConfigs.CEFConfigDictionary.InventoryPreSaleEnabled)
                            {
                                newModel.MaximumPrePurchaseQuantity = x.MaximumPrePurchaseQuantity;
                            }
                            if (JSConfigs.CEFConfigDictionary.InventoryPreSaleMaxPerProductGlobalEnabled)
                            {
                                newModel.MaximumPrePurchaseQuantityGlobal = x.MaximumPrePurchaseQuantityGlobal;
                            }
                            if (JSConfigs.CEFConfigDictionary.PurchasingMinMaxAfterEnabled)
                            {
                                newModel.MinimumPurchaseQuantityIfPastPurchased = x.MinimumPurchaseQuantityIfPastPurchased;
                                newModel.MaximumPurchaseQuantityIfPastPurchased = x.MaximumPurchaseQuantityIfPastPurchased;
                                newModel.MaximumBackOrderPurchaseQuantityIfPastPurchased = x.MaximumBackOrderPurchaseQuantityIfPastPurchased;
                                if (JSConfigs.CEFConfigDictionary.InventoryPreSaleMaxPerProductGlobalEnabled)
                                {
                                    newModel.MaximumPrePurchaseQuantityIfPastPurchased = x.MaximumPrePurchaseQuantityIfPastPurchased;
                                }
                            }
                        }
                        if (JSConfigs.CEFConfigDictionary.PurchasingAvailabilityDatesEnabled)
                        {
                            newModel.AvailableStartDate = x.AvailableStartDate;
                            newModel.AvailableEndDate = x.AvailableEndDate;
                        }
                        if (JSConfigs.CEFConfigDictionary.PurchasingDocumentRequiredEnabled)
                        {
                            newModel.DocumentRequiredForPurchase = x.DocumentRequiredForPurchase;
                            newModel.DocumentRequiredForPurchaseMissingWarningMessage = x.DocumentRequiredForPurchaseMissingWarningMessage;
                            newModel.DocumentRequiredForPurchaseExpiredWarningMessage = x.DocumentRequiredForPurchaseExpiredWarningMessage;
                            if (JSConfigs.CEFConfigDictionary.PurchasingDocumentRequiredOverrideEnabled)
                            {
                                newModel.DocumentRequiredForPurchaseOverrideFee = x.DocumentRequiredForPurchaseOverrideFee;
                                newModel.DocumentRequiredForPurchaseOverrideFeeAcceptedMessage = x.DocumentRequiredForPurchaseOverrideFeeAcceptedMessage;
                                newModel.DocumentRequiredForPurchaseOverrideFeeIsPercent = x.DocumentRequiredForPurchaseOverrideFeeIsPercent;
                                newModel.DocumentRequiredForPurchaseOverrideFeeWarningMessage = x.DocumentRequiredForPurchaseOverrideFeeWarningMessage;
                            }
                        }
                        if (JSConfigs.CEFConfigDictionary.PurchasingMustPurchaseInMultiplesOfEnabled)
                        {
                            newModel.MustPurchaseInMultiplesOfAmount = x.MustPurchaseInMultiplesOfAmount;
                            newModel.MustPurchaseInMultiplesOfAmountWarningMessage = x.MustPurchaseInMultiplesOfAmountWarningMessage;
                            if (JSConfigs.CEFConfigDictionary.PurchasingMustPurchaseInMultiplesOfOverrideEnabled)
                            {
                                newModel.MustPurchaseInMultiplesOfAmountOverrideFee = x.MustPurchaseInMultiplesOfAmountOverrideFee;
                                newModel.MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage = x.MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage;
                                newModel.MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent = x.MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent;
                                newModel.MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage = x.MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage;
                            }
                        }
                        // Analytics filled data
                        newModel.TotalPurchasedAmount = x.TotalPurchasedAmount;
                        newModel.TotalPurchasedAmountCurrencyID = x.TotalPurchasedAmountCurrencyID;
                        newModel.TotalPurchasedQuantity = x.TotalPurchasedQuantity;
                        // Convenience Properties
                        newModel.PrimaryImageFileName = x.Images!
                            .Where(img => img.Active)
                            .OrderByDescending(img => img.IsPrimary)
                            .ThenByDescending(img => img.OriginalWidth)
                            .Take(1)
                            .Select(img => img.ThumbnailFileName ?? img.OriginalFileName)
                            .FirstOrDefault();
                        // Related Objects
                        newModel.TypeID = x.TypeID;
                        newModel.StatusID = x.StatusID;
                        newModel.PackageID = x.PackageID;
                        newModel.MasterPackID = x.MasterPackID;
                        newModel.PalletID = x.PalletID;
                        // Associated Objects
                        newModel.Images = x.Images!.Where(y => y.Active).Select(y => y.CreateProductImageModelFromEntityList(context.ContextProfileName)).ToList()!;
                        newModel.StoredFiles = x.StoredFiles!.Where(y => y.Active).Select(y => y.CreateProductFileModelFromEntityList(context.ContextProfileName)).ToList()!;
                        newModel.ProductAssociations = x.ProductAssociations!.Where(y => y.Active && y.Slave != null).OrderBy(pa => pa.Slave!.Name).Select(y => y.CreateProductAssociationModelFromEntityList(context.ContextProfileName)).ToList()!;
                        newModel.ProductsAssociatedWith = x.ProductsAssociatedWith!.Where(y => y.Active && y.Master != null).OrderBy(pa => pa.Master!.Name).Select(y => CreateProductAssociationModelFromEntityAlternateList(y, context.ContextProfileName)).ToList();
                        if (JSConfigs.CEFConfigDictionary.ManufacturersEnabled)
                        {
                            newModel.Manufacturers = x.Manufacturers!.Where(y => y.Active).Select(y => y.CreateManufacturerProductModelFromEntityList(context.ContextProfileName)).ToList()!;
                        }
                        if (JSConfigs.CEFConfigDictionary.VendorsEnabled)
                        {
                            newModel.Vendors = x.Vendors!.Where(y => y.Active).Select(y => y.CreateVendorProductModelFromEntityList(context.ContextProfileName)).ToList()!;
                        }
                        if (JSConfigs.CEFConfigDictionary.BrandsEnabled)
                        {
                            newModel.Brands = x.Brands!.Where(y => y.Active).Select(y => y.CreateBrandProductModelFromEntityLite(context.ContextProfileName)).ToList()!;
                        }
                        if (JSConfigs.CEFConfigDictionary.StoresEnabled)
                        {
                            newModel.Stores = x.Stores!.Where(y => y.Active).Select(y => y.CreateStoreProductModelFromEntityLite(context.ContextProfileName)).ToList()!;
                        }
                        if (JSConfigs.CEFConfigDictionary.LoginEnabled)
                        {
                            newModel.Accounts = x.Accounts!.Where(y => y.Active).Select(y => y.CreateAccountProductModelFromEntityList(context.ContextProfileName)).ToList()!;
                        }
                        if (JSConfigs.CEFConfigDictionary.CategoriesEnabled)
                        {
                            newModel.ProductCategories = x.Categories!.Where(y => y.Active).Select(y => y.CreateProductCategoryModelFromEntityList(context.ContextProfileName)).ToList()!;
                        }
                        if (JSConfigs.CEFConfigDictionary.ProductNotificationsEnabled)
                        {
                            newModel.ProductNotifications = x.ProductNotifications!.Where(y => y.Active).Select(y => y.CreateProductNotificationModelFromEntityList(context.ContextProfileName)).ToList()!;
                        }
                        if (JSConfigs.CEFConfigDictionary.PaymentsWithSubscriptionsEnabled)
                        {
                            newModel.ProductSubscriptionTypes = x.ProductSubscriptionTypes!.Where(y => y.Active).Select(y => y.CreateProductSubscriptionTypeModelFromEntityLite(context.ContextProfileName)).ToList()!;
                        }
                        if (JSConfigs.CEFConfigDictionary.ProductRestrictionsEnabled)
                        {
                            newModel.ProductRestrictions = x.ProductRestrictions!.Where(y => y.Active).Select(y => y.CreateProductRestrictionModelFromEntityLite(context.ContextProfileName)).ToList()!;
                        }
                    }
                    if (x.Type != null)
                    {
                        newModel.Type = (TypeModel?)x.Type.CreateProductTypeModelFromEntityLite(context.ContextProfileName);
                        newModel.TypeKey = x.Type.CustomKey;
                        newModel.TypeName = x.Type.Name;
                        newModel.TypeDisplayName = x.Type.DisplayName;
                        newModel.TypeSortOrder = x.Type.SortOrder;
                    }
                    if (x.Status != null)
                    {
                        newModel.Status = (StatusModel?)x.Status.CreateProductStatusModelFromEntityLite(context.ContextProfileName);
                        newModel.StatusKey = x.Status.CustomKey;
                        newModel.StatusName = x.Status.Name;
                        newModel.StatusDisplayName = x.Status.DisplayName;
                        newModel.StatusSortOrder = x.Status.SortOrder;
                    }
                    if (x.TotalPurchasedAmountCurrency != null)
                    {
                        newModel.TotalPurchasedAmountCurrency = x.TotalPurchasedAmountCurrency.CreateCurrencyModelFromEntityLite(context.ContextProfileName);
                        newModel.TotalPurchasedAmountCurrencyKey = x.TotalPurchasedAmountCurrency.CustomKey;
                        newModel.TotalPurchasedAmountCurrencyName = x.TotalPurchasedAmountCurrency.Name;
                    }
                    if (JSConfigs.CEFConfigDictionary.SalesReturnsEnabled && x.RestockingFeeAmountCurrency != null)
                    {
                        newModel.RestockingFeeAmountCurrency = x.RestockingFeeAmountCurrency.CreateCurrencyModelFromEntityLite(context.ContextProfileName);
                        newModel.RestockingFeeAmountCurrencyKey = x.RestockingFeeAmountCurrency.CustomKey;
                        newModel.RestockingFeeAmountCurrencyName = x.RestockingFeeAmountCurrency.Name;
                    }
                    if (x.Package != null)
                    {
                        newModel.Package = x.Package.CreatePackageModelFromEntityLite(context.ContextProfileName);
                        newModel.PackageKey = x.Package.CustomKey;
                        newModel.PackageName = x.Package.Name;
                    }
                    if (x.MasterPack != null)
                    {
                        newModel.MasterPack = x.MasterPack.CreatePackageModelFromEntityLite(context.ContextProfileName);
                        newModel.MasterPackKey = x.MasterPack.CustomKey;
                        newModel.MasterPackName = x.MasterPack.Name;
                    }
                    // ReSharper disable once InvertIf
                    if (x.Pallet != null)
                    {
                        newModel.Pallet = x.Pallet.CreatePackageModelFromEntityLite(context.ContextProfileName);
                        newModel.PalletKey = x.Pallet.CustomKey;
                        newModel.PalletName = x.Pallet.Name;
                    }
                    return newModel;
                })
                .SingleOrDefault();
        }

        /// <summary>Enumerates flatten in this collection.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="item">The item to act on.</param>
        /// <returns>An enumerator that allows foreach to be used to process flatten in this collection.</returns>
        public static IEnumerable<T> Flatten<T>(this T item)
            where T : class, IHaveAParentBase<T>
        {
            var stack = new Stack<T>();
            stack.Push(item);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                yield return current;
                var children = current.Children;
                if (children == null)
                {
                    continue;
                }
                foreach (var child in children)
                {
                    if (child.ParentID == child.ID)
                    {
                        // Parenting Itself, skip it
                        continue;
                    }
                    stack.Push(child);
                }
            }
        }

        /// <summary>A Product extension method that converts this object to a product catalog item alternate.</summary>
        /// <param name="x">                                    The Product to process.</param>
        /// <param name="productWorkflow">                      The product workflow.</param>
        /// <param name="disableSearchResultAssociatedProducts">True to disable, false to enable the search result
        ///                                                     associated products.</param>
        /// <param name="contextProfileName">                   Name of the context profile.</param>
        /// <returns>The given data converted to a ProductModel.</returns>
        public static async Task<IProductModel?> ToProductCatalogItemAltAsync(
            this Product? x,
            IProductWorkflow productWorkflow,
            bool disableSearchResultAssociatedProducts,
            string? contextProfileName)
        {
            // ReSharper disable MergeConditionalExpression
            if (x == null)
            {
                return null;
            }
            var item = RegistryLoaderWrapper.GetInstance<IProductModel>(contextProfileName);
            // Base Properties
            item.ID = x.ID;
            item.Active = x.Active;
            item.CustomKey = x.CustomKey;
            // JsonAttributes = x.JsonAttributes;
            item.SerializableAttributes = x.SerializableAttributes;
            // NameableBase Properties
            item.Name = x.Name;
            item.Description = x.Description;
            // IHaveSeoBase Properties
            item.SeoUrl = x.SeoUrl;
            // IHaveNullableDimensions Properties
            // Flags/Toggles
            item.AllowBackOrder = x.AllowBackOrder;
            item.AllowPreSale = x.AllowPreSale;
            item.IsUnlimitedStock = x.IsUnlimitedStock;
            // Descriptors
            item.ShortDescription = x.ShortDescription;
            item.ManufacturerPartNumber = x.ManufacturerPartNumber;
            item.BrandName = x.BrandName;
            item.UnitOfMeasure = x.UnitOfMeasure;
            item.HCPCCode = x.HCPCCode;
            // Fees
            // Availability, Shipping Requirements
            item.KitBaseQuantityPriceMultiplier = x.KitBaseQuantityPriceMultiplier ?? 1m;
            // Min/Max Purchase Per Order
            item.MinimumPurchaseQuantity = x.MinimumPurchaseQuantity;
            item.MinimumPurchaseQuantityIfPastPurchased = x.MinimumPurchaseQuantityIfPastPurchased;
            item.MaximumPurchaseQuantity = x.MaximumPurchaseQuantity;
            item.MaximumPurchaseQuantityIfPastPurchased = x.MaximumPurchaseQuantityIfPastPurchased;
            item.MaximumBackOrderPurchaseQuantity = x.MaximumBackOrderPurchaseQuantity;
            item.MaximumBackOrderPurchaseQuantityIfPastPurchased = x.MaximumBackOrderPurchaseQuantityIfPastPurchased;
            item.MaximumBackOrderPurchaseQuantityGlobal = x.MaximumBackOrderPurchaseQuantityGlobal;
            item.MaximumPrePurchaseQuantity = x.MaximumPrePurchaseQuantity;
            item.MaximumPrePurchaseQuantityIfPastPurchased = x.MaximumPrePurchaseQuantityIfPastPurchased;
            item.MaximumPrePurchaseQuantityGlobal = x.MaximumPrePurchaseQuantityGlobal;
            // Required Document
            // Must Purchase In Multiples Of
            // Analytics filled data
            // Convenience Properties
            item.PrimaryImageFileName = x.Images
                ?.Where(img => img.Active)
                .OrderByDescending(img => img.IsPrimary)
                .ThenByDescending(img => img.OriginalWidth)
                .Take(1)
                .Select(img => img.ThumbnailFileName ?? img.OriginalFileName)
                .FirstOrDefault();
            // Related Objects
            item.TypeID = x.TypeID;
            item.TypeKey = x.Type != null ? x.Type.CustomKey : null;
            item.TypeName = x.Type != null ? x.Type.Name : null;
            item.TypeDisplayName = x.Type != null ? x.Type.DisplayName : null;
            item.TypeSortOrder = x.Type != null ? x.Type.SortOrder : null;
            item.StatusID = x.StatusID;
            item.StatusKey = x.Status != null ? x.Status.CustomKey : null;
            item.StatusName = x.Status != null ? x.Status.Name : null;
            item.StatusDisplayName = x.Status != null ? x.Status.DisplayName : null;
            item.StatusSortOrder = x.Status != null ? x.Status.SortOrder : null;
            item.QuantityPerPallet = x.QuantityPerPallet;
            item.QuantityPerMasterPack = x.QuantityPerMasterPack;
            // Associated Objects
            item.ProductRestrictions = x.ProductRestrictions!
                .Where(y => y.Active)
                .Select(y => y.CreateProductRestrictionModelFromEntityLite(contextProfileName))
                .ToList()!;
            item.ProductAssociations = (disableSearchResultAssociatedProducts
                ? null
                : x.ProductAssociations!
                    .Where(y => y.Active && y.Slave is { Active: true })
                    .Select(y => y.CreateProductAssociationModelFromEntityList(contextProfileName))
                    .ToList())!;
            item.ProductNotifications = x.ProductNotifications!
                .Where(y => y.Active)
                .Select(y => y.CreateProductNotificationModelFromEntityList(contextProfileName))
                .ToList()!;
            item = await productWorkflow.RemoveHiddenFromStorefrontAttributesAsync(
                    item,
                    contextProfileName)
                .ConfigureAwait(false);
            var isShippingRestricted = await productWorkflow.IsShippingRestrictedAsync(
                    attributes: item!.SerializableAttributes,
                    state: null,
                    city: null)
                .ConfigureAwait(false);
            var productAssociationTypeID = await RegistryLoaderWrapper.GetInstance<IProductAssociationTypeWorkflow>(contextProfileName)
                .CheckExistsByNameAsync("Variant of Master", contextProfileName)
                .ConfigureAwait(false);
            // ReSharper disable once InvertIf
            if (!disableSearchResultAssociatedProducts && item.ProductAssociations?.Any() == true)
            {
                Contract.RequiresValidID(productAssociationTypeID);
                foreach (var ap in item.ProductAssociations)
                {
                    ap.Slave = await productWorkflow.RemoveHiddenFromStorefrontAttributesAsync(
                            ap.Slave!,
                            contextProfileName)
                        .ConfigureAwait(false);
                    if (ap.TypeID == productAssociationTypeID
                        && isShippingRestricted
                        && !await productWorkflow.IsShippingRestrictedAsync(
                                attributes: ap.Slave!.SerializableAttributes,
                                state: null,
                                city: null)
                            .ConfigureAwait(false))
                    {
                        ap.Slave.SerializableAttributes!.TryAdd("SKU-Restrictions", item.SerializableAttributes!["SKU-Restrictions"]);
                    }
                }
            }
            return item;
            // ReSharper restore MergeConditionalExpression
        }

        /// <summary>An IProduct extension method that map lite product old extent.</summary>
        /// <param name="x">The x to act on.</param>
        /// <returns>An IProductModel.</returns>
        internal static IProductModel? MapLiteProductOldExt(this IProduct? x)
        {
            return x == null ? null : MapLiteProductOld(x);
        }

        /// <summary>An IProductAssociation extension method that creates product association model from entity alternate
        /// list.</summary>
        /// <param name="entity">            The entity to act on.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new product association model from entity alternate list.</returns>
        private static IProductAssociationModel CreateProductAssociationModelFromEntityAlternateList(
            this IProductAssociation entity,
            string? contextProfileName)
        {
            var model = RegistryLoaderWrapper.GetInstance<IProductAssociationModel>(contextProfileName);
            // Map the Inherited Properties
            model.MapBaseEntityPropertiesToModel(
                Contract.RequiresNotNull(entity),
                MappingMode.Full,
                contextProfileName);
            // Map this level's Properties
            // IHaveANullableTypeBase Properties
            model.TypeID = entity.TypeID;
            if (entity.Type != null)
            {
                model.TypeKey = entity.Type.CustomKey;
                model.TypeName = entity.Type.Name;
                model.TypeDisplayName = entity.Type.DisplayName;
                model.TypeSortOrder = entity.Type.SortOrder;
            }
            // IHaveJsonAttributesBase Properties (Forced)
            // model.JsonAttributes = entity.JsonAttributes;
            model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
            // ProductAssociation's Properties
            model.Quantity = entity.Quantity;
            model.SortOrder = entity.SortOrder;
            model.UnitOfMeasure = entity.UnitOfMeasure;
            // ProductAssociation's Related Objects (Not Mapped unless Forced)
            model.StoreID = entity.StoreID;
            model.SlaveID = entity.SlaveID;
            model.MasterID = entity.MasterID;
            if (entity.Master != null)
            {
                model.MasterKey = entity.Master.CustomKey;
                model.MasterName = entity.Master.Name;
                model.MasterSeoUrl = entity.Master.SeoUrl;
                model.MasterIsVisible = entity.Master.IsVisible;
                model.MasterPrimaryImageFileName = entity.Master.Images!.Where(img => img.Active).OrderByDescending(img => img.IsPrimary).ThenByDescending(img => img.OriginalWidth).Take(1).Select(img => img.ThumbnailFileName ?? img.OriginalFileName).FirstOrDefault();
            }
            // ReSharper disable once InvertIf
            if (entity.Slave != null)
            {
                model.SlaveKey = entity.Slave.CustomKey;
                model.SlaveName = entity.Slave.Name;
                model.SlaveSeoUrl = entity.Slave.SeoUrl;
                model.SlaveIsVisible = entity.Slave.IsVisible;
                model.SlavePrimaryImageFileName = entity.Slave.Images!.Where(img => img.Active).OrderByDescending(img => img.IsPrimary).ThenByDescending(img => img.OriginalWidth).Take(1).Select(img => img.ThumbnailFileName ?? img.OriginalFileName).FirstOrDefault();
            }
            // Finished!
            return model;
        }
    }
}
