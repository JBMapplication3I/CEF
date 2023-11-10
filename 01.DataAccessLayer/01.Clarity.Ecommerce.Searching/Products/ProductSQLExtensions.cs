// <copyright file="ProductSQLExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product SQL extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using DataModel;
    using Ecommerce.DataModel;

    /// <summary>A product SQL extensions.</summary>
    public static class ProductSQLExtensions
    {
        /// <summary>Gets kit composition stock quantity.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <returns>The kit composition stock quantity.</returns>
        public static Expression<Func<IQueryable<T>, decimal?>> GetKitCompositionStockQuantity<T>()
            where T : class, IProductAssociation
        {
            return x => x
                .Where(pa => pa.Active && pa.Type != null && pa.Type.Name == "Kit Component" && (pa.Quantity ?? 1) > 0)
                .Select(pa => new
                {
                    Quantity = pa.Quantity ?? 1,
                    Stock = pa.Slave == null
                        ? null
                        : pa.Slave.ProductInventoryLocationSections!
                            .Where(pils => pils.Active
                                && pils.Slave!.Active
                                && pils.Slave.InventoryLocation!.Active
                                && pils.Quantity.HasValue
                                && pils.Quantity > 0)
                            .Select(pils => pils.Quantity)
                            .DefaultIfEmpty(0)
                            .Sum(),
                })
                .Select(pa => (pa.Stock ?? 0) / pa.Quantity)
                .DefaultIfEmpty(0)
                .Min();
        }

        /// <summary>Gets kit composition stock quantity allocated.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <returns>The kit composition stock quantity allocated.</returns>
        public static Expression<Func<IQueryable<T>, decimal?>> GetKitCompositionStockQuantityAllocated<T>()
            where T : class, IProductAssociation
        {
            return x => x
                .Where(pa => pa.Active && pa.Type != null && pa.Type.Name == "Kit Component" && (pa.Quantity ?? 1) > 0)
                .Select(pa => new
                {
                    Quantity = pa.Quantity ?? 1,
                    StockAllocated = pa.Slave == null
                        ? null
                        : pa.Slave.ProductInventoryLocationSections!
                            .Where(pils => pils.Active
                                && pils.Slave!.Active
                                && pils.Slave.InventoryLocation!.Active
                                && pils.QuantityAllocated.HasValue
                                && pils.QuantityAllocated > 0)
                            .Select(pils => pils.QuantityAllocated)
                            .DefaultIfEmpty(0)
                            .Sum(),
                })
                .Select(pa => pa.StockAllocated / pa.Quantity)
                .DefaultIfEmpty(0)
                .Min();
        }

        /// <summary>Gets kit composition stock quantity pre sold.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <returns>The kit composition stock quantity pre sold.</returns>
        public static Expression<Func<IQueryable<T>, decimal?>> GetKitCompositionStockQuantityPreSold<T>()
            where T : class, IProductAssociation
        {
            return x => x
                .Where(pa => pa.Active && pa.Type != null && pa.Type.Name == "Kit Component" && (pa.Quantity ?? 1) > 0)
                .Select(pa => new
                {
                    Quantity = pa.Quantity ?? 1,
                    StockPreSold = pa.Slave == null
                        ? null
                        : pa.Slave.ProductInventoryLocationSections!
                            .Where(pils => pils.Active
                                && pils.Slave!.Active
                                && pils.Slave.InventoryLocation!.Active
                                && pils.QuantityPreSold.HasValue
                                && pils.QuantityPreSold > 0)
                            .Select(pils => pils.QuantityPreSold)
                            .DefaultIfEmpty(0)
                            .Sum(),
                })
                .Select(pa => pa.StockPreSold / pa.Quantity)
                .DefaultIfEmpty(0)
                .Min();
        }

        /// <summary>Gets kit composition stock quantity broken.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <returns>The kit composition stock quantity broken.</returns>
        public static Expression<Func<IQueryable<T>, decimal?>> GetKitCompositionStockQuantityBroken<T>()
            where T : class, IProductAssociation
        {
            return x => x
                .Where(pa => pa.Active && pa.Type != null && pa.Type.Name == "Kit Component" && (pa.Quantity ?? 1) > 0)
                .Select(pa => new
                {
                    Quantity = pa.Quantity ?? 1,
                    StockBroken = pa.Slave == null
                        ? null
                        : pa.Slave.ProductInventoryLocationSections!
                            .Where(pils => pils.Active
                                && pils.Slave!.Active
                                && pils.Slave.InventoryLocation!.Active
                                && pils.QuantityBroken.HasValue
                                && pils.QuantityBroken > 0)
                            .Select(pils => pils.QuantityBroken)
                            .DefaultIfEmpty(0)
                            .Sum(),
                })
                .Select(pa => pa.StockBroken / pa.Quantity)
                .DefaultIfEmpty(0)
                .Min();
        }

        /// <summary>Gets product full map selector.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <returns>The product full map selector.</returns>
        public static Expression<Func<T, T>> GetProductFullMapSelector<T>()
            where T : class, IProduct, new()
        {
            var getKitCompositionStockQuantity = GetKitCompositionStockQuantity<ProductAssociation>().Compile();
            var getKitCompositionStockQuantityAllocated = GetKitCompositionStockQuantityAllocated<ProductAssociation>().Compile();
            var getKitCompositionStockQuantityPreSold = GetKitCompositionStockQuantityPreSold<ProductAssociation>().Compile();
            // var getKitCompositionStockQuantityBroken = GetKitCompositionStockQuantityBroken<ProductAssociation>().Compile();
            return x => new()
            {
                // Base Properties
                ID = x.ID,
                CustomKey = x.CustomKey,
                Active = x.Active,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                JsonAttributes = x.JsonAttributes,
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
                HCPCCode = x.HCPCCode,
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
                ShippingLeadTimeIsCalendarDays = x.ShippingLeadTimeIsCalendarDays,
                // Descriptors
                ShortDescription = x.ShortDescription,
                ManufacturerPartNumber = x.ManufacturerPartNumber,
                BrandName = x.BrandName,
                TaxCode = x.TaxCode,
                UnitOfMeasure = x.UnitOfMeasure ?? "EACH",
                SortOrder = x.SortOrder,
                // Pricing & Fees
                PriceBase = x.PriceBase,
                PriceSale = x.PriceSale,
                PriceMsrp = x.PriceMsrp,
                PriceReduction = x.PriceReduction,
                HandlingCharge = x.HandlingCharge,
                FlatShippingCharge = x.FlatShippingCharge,
                RestockingFeePercent = x.RestockingFeePercent,
                RestockingFeeAmount = x.RestockingFeeAmount,
                // Availability, Stock, Shipping Requirements
                AvailableStartDate = x.AvailableStartDate,
                AvailableEndDate = x.AvailableEndDate,
                PreSellEndDate = x.PreSellEndDate,
                StockQuantity = x.IsUnlimitedStock || x.Type != null && x.Type.Name == "Variant Master"
                    ? null
                    : x.Type != null && x.Type.Name == "Kit" || x.Type != null && x.Type.Name == "Variant Kit"
                        ? getKitCompositionStockQuantity(x.ProductAssociations!.AsQueryable())
                        : x.StockQuantity,
                StockQuantityAllocated = x.IsUnlimitedStock || x.Type != null && x.Type.Name == "Variant Master"
                    ? null
                    : x.Type != null && x.Type.Name == "Kit" || x.Type != null && x.Type.Name == "Variant Kit"
                        ? getKitCompositionStockQuantityAllocated(x.ProductAssociations!.AsQueryable())
                        : x.StockQuantityAllocated,
                StockQuantityPreSold = x.IsUnlimitedStock || x.Type != null && x.Type.Name == "Variant Master"
                    ? null
                    : x.Type != null && x.Type.Name == "Kit" || x.Type != null && x.Type.Name == "Variant Kit"
                        ? getKitCompositionStockQuantityPreSold(x.ProductAssociations!.AsQueryable())
                        : x.StockQuantityPreSold,
                // StockQuantityBroken = x.IsUnlimitedStock || x.Type != null && x.Type.Name == "Variant Master"
                //     ? null
                //     : x.Type != null && x.Type.Name == "Kit" || x.Type != null && x.Type.Name == "Variant Kit"
                //         ? getKitCompositionStockQuantityBroken(x.ProductAssociations!.AsQueryable())
                //         : null,
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
                MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage = x.MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage,
                MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent = x.MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent,
                MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage = x.MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage,
                // Analytics filled data
                TotalPurchasedAmount = x.TotalPurchasedAmount,
                TotalPurchasedAmountCurrencyID = x.TotalPurchasedAmountCurrencyID,
                TotalPurchasedAmountCurrency = x.TotalPurchasedAmountCurrency,
                TotalPurchasedQuantity = x.TotalPurchasedQuantity,
                // Convenience Properties
                // Related Objects
                TypeID = x.TypeID,
                Type = x.Type,
                StatusID = x.StatusID,
                Status = x.Status,
                RestockingFeeAmountCurrencyID = x.RestockingFeeAmountCurrencyID,
                RestockingFeeAmountCurrency = x.RestockingFeeAmountCurrency,
                PackageID = x.PackageID,
                Package = x.Package,
                MasterPackID = x.MasterPackID,
                MasterPack = x.MasterPack,
                PalletID = x.PalletID,
                Pallet = x.Pallet,
                // Associated Objects
                Images = x.Images,
                StoredFiles = x.StoredFiles,
                Manufacturers = x.Manufacturers,
                Vendors = x.Vendors,
                Brands = x.Brands,
                Stores = x.Stores,
                Accounts = x.Accounts,
                Categories = x.Categories,
                ProductPricePoints = x.ProductPricePoints,
                ProductAssociations = x.ProductAssociations,
                ProductsAssociatedWith = x.ProductsAssociatedWith,
                ProductInventoryLocationSections = x.ProductInventoryLocationSections,
                ProductSubscriptionTypes = x.ProductSubscriptionTypes,
                ProductRestrictions = x.ProductRestrictions,
                ProductNotifications = x.ProductNotifications,
            };
        }
    }
}
