// <copyright file="ImporterProviderBase.V1.Assignments.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the importer provider base class</summary>
// ReSharper disable NotAccessedVariable, UnusedVariable
namespace Clarity.Ecommerce.Providers.Importer
{
    using System;
    using System.Threading.Tasks;
    using Enums;
    using Interfaces.Models;
    using Interfaces.Providers.Importer;
    using Models;

    public abstract partial class ImporterProviderBase
    {
        /// <summary>Assign cell data by field enum.</summary>
        /// <param name="fileName">          Filename of the file.</param>
        /// <param name="field">             The field.</param>
        /// <param name="row">               The row.</param>
        /// <param name="product">           The product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{bool}.</returns>
        // ReSharper disable once CyclomaticComplexity, FunctionComplexityOverflow
        private async Task<bool> AssignCellDataByFieldEnumAsync(
            string fileName,
            ProductImportFieldEnum field,
            IRow row,
            IProductModel product,
            string? contextProfileName)
        {
            var productIsDirty = false;
            switch (field)
            {
                // ReSharper disable RedundantCaseLabel
                case ProductImportFieldEnum.DoNotImport:
                {
                    // Don't import
                    break;
                }
                case ProductImportFieldEnum.IsVisible:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = bool.TryParse(value, out var parsed) && parsed;
                    if (product.IsVisible != converted)
                    {
                        product.IsVisible = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.IsTaxable:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = !bool.TryParse(value, out var parsed) || parsed;
                    if (product.IsTaxable != converted)
                    {
                        product.IsTaxable = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.AllowBackOrder:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = !bool.TryParse(value, out var parsed) || parsed;
                    if (product.AllowBackOrder != converted)
                    {
                        product.AllowBackOrder = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.IsUnlimitedStock:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = bool.TryParse(value, out var parsed) && parsed;
                    if (product.IsUnlimitedStock != converted)
                    {
                        product.IsUnlimitedStock = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.IsDiscontinued:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = bool.TryParse(value, out var parsed) && parsed;
                    if (product.IsDiscontinued != converted)
                    {
                        product.IsDiscontinued = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.IsFreeShipping:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = bool.TryParse(value, out var parsed) && parsed;
                    if (product.IsFreeShipping != converted)
                    {
                        product.IsFreeShipping = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                // case ProductImportFieldEnum.PriceBase:
                // {
                //     var (hasValue, value, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                //     if (!hasValue)
                //     {
                //         return false;
                //     }
                //     var converted = decimal.TryParse(value, out var parsed) ? (decimal?)parsed : null;
                //     if (product.PriceBase != converted)
                //     {
                //         product.PriceBase = converted;
                //         productIsDirty = true;
                //     }
                //     break;
                // }
                // case ProductImportFieldEnum.PriceMsrp:
                // {
                //     var (hasValue, value, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                //     if (!hasValue)
                //     {
                //         return false;
                //     }
                //     var converted = decimal.TryParse(value, out var parsed) ? (decimal?)parsed : null;
                //     if (product.PriceMsrp != converted)
                //     {
                //         product.PriceMsrp = converted;
                //         productIsDirty = true;
                //     }
                //     break;
                // }
                // case ProductImportFieldEnum.PriceReduction:
                // {
                //     var (hasValue, value, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                //     if (!hasValue)
                //     {
                //         return false;
                //     }
                //     var converted = decimal.TryParse(value, out var parsed) ? (decimal?)parsed : null;
                //     if (product.PriceReduction != converted)
                //     {
                //         product.PriceReduction = converted;
                //         productIsDirty = true;
                //     }
                //     break;
                // }
                // case ProductImportFieldEnum.PriceSale:
                // {
                //     var (hasValue, value, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                //     if (!hasValue)
                //     {
                //         return false;
                //     }
                //     var converted = decimal.TryParse(value, out var parsed) ? (decimal?)parsed : null;
                //     if (product.PriceSale != converted)
                //     {
                //         product.PriceSale = converted;
                //         productIsDirty = true;
                //     }
                //     break;
                // }
                // case ProductImportFieldEnum.StockQuantity:
                // {
                //     var (hasValue, value, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                //     if (!hasValue)
                //     {
                //         return false;
                //     }
                //     var converted = decimal.TryParse(value, out var parsed) ? (decimal?)parsed : null;
                //     if (product.StockQuantity != converted)
                //     {
                //         product.StockQuantity = converted;
                //         productIsDirty = true;
                //     }
                //     break;
                // }
                case ProductImportFieldEnum.HandlingCharge:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? (decimal?)parsed : null;
                    if (product.HandlingCharge != converted)
                    {
                        product.HandlingCharge = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.MinimumPurchaseQuantity:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? (decimal?)parsed : null;
                    if (product.MinimumPurchaseQuantity != converted)
                    {
                        product.MinimumPurchaseQuantity = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.MinimumPurchaseQuantityIfPastPurchased:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? (decimal?)parsed : null;
                    if (product.MinimumPurchaseQuantityIfPastPurchased != converted)
                    {
                        product.MinimumPurchaseQuantityIfPastPurchased = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.MaximumPurchaseQuantity:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? (decimal?)parsed : null;
                    if (product.MaximumPurchaseQuantity != converted)
                    {
                        product.MaximumPurchaseQuantity = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.MaximumPurchaseQuantityIfPastPurchased:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? (decimal?)parsed : null;
                    if (product.MaximumPurchaseQuantityIfPastPurchased != converted)
                    {
                        product.MaximumPurchaseQuantityIfPastPurchased = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.Weight:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? (decimal?)parsed : null;
                    if (product.Weight != converted)
                    {
                        product.Weight = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.Width:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? (decimal?)parsed : null;
                    if (product.Width != converted)
                    {
                        product.Width = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.Depth:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? (decimal?)parsed : null;
                    if (product.Depth != converted)
                    {
                        product.Depth = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.Height:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? (decimal?)parsed : null;
                    if (product.Height != converted)
                    {
                        product.Height = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.KitBaseQuantityPriceMultiplier:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? (decimal?)parsed : null;
                    if (product.KitBaseQuantityPriceMultiplier != converted)
                    {
                        product.KitBaseQuantityPriceMultiplier = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.SortOrder:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = int.TryParse(value, out var parsed) ? (int?)parsed : null;
                    if (product.SortOrder != converted)
                    {
                        product.SortOrder = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.AvailableStartDate:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = DateTime.TryParse(value, out var parsed) ? (DateTime?)parsed : null;
                    if (product.AvailableStartDate != converted)
                    {
                        product.AvailableStartDate = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.AvailableEndDate:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = DateTime.TryParse(value, out var parsed) ? (DateTime?)parsed : null;
                    if (product.AvailableEndDate != converted)
                    {
                        product.AvailableEndDate = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.CustomKey:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    if (product.CustomKey != value)
                    {
                        product.CustomKey = value;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.Name:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    if (product.Name != value)
                    {
                        product.Name = value;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.Description:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    if (product.Description != value)
                    {
                        product.Description = value;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.SeoKeywords:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    if (product.SeoKeywords != value)
                    {
                        product.SeoKeywords = value;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.SeoUrl:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    if (product.SeoUrl != value)
                    {
                        product.SeoUrl = value;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.SeoPageTitle:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    if (product.SeoPageTitle != value)
                    {
                        product.SeoPageTitle = value;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.SeoDescription:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    if (product.SeoDescription != value)
                    {
                        product.SeoDescription = value;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.ManufacturerPartNumber:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    if (product.ManufacturerPartNumber != value)
                    {
                        product.ManufacturerPartNumber = value;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.ShortDescription:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    if (product.ShortDescription != value)
                    {
                        product.ShortDescription = value;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.UnitOfMeasure:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    if (product.UnitOfMeasure != value)
                    {
                        product.UnitOfMeasure = value;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.WeightUnitOfMeasure:
                {
                    var (hasValue, _, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    if (product.WeightUnitOfMeasure != unit)
                    {
                        product.WeightUnitOfMeasure = unit;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.DepthUnitOfMeasure:
                {
                    var (hasValue, _, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    if (product.DepthUnitOfMeasure != unit)
                    {
                        product.DepthUnitOfMeasure = unit;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.WidthUnitOfMeasure:
                {
                    var (hasValue, _, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    if (product.WidthUnitOfMeasure != unit)
                    {
                        product.WidthUnitOfMeasure = unit;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.HeightUnitOfMeasure:
                {
                    var (hasValue, _, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    if (product.HeightUnitOfMeasure != unit)
                    {
                        product.HeightUnitOfMeasure = unit;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.PackageWidth:
                {
                    var (hasValue, value, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? parsed : 0;
                    if (product.Package == null)
                    {
                        product.Package = new PackageModel { Active = true, IsCustom = true, TypeKey = "Package" };
                        productIsDirty = true;
                    }
                    if (product.Package.Width != converted && converted > 0)
                    {
                        product.Package.Width = converted;
                        productIsDirty = true;
                    }
                    if (product.Package.WidthUnitOfMeasure != unit)
                    {
                        product.Package.WidthUnitOfMeasure = unit;
                        productIsDirty = true;
                    }
                    break;
                }
                ////case ProductImportFieldEnum.PackageWidthUnitOfMeasure:
                ////    {
                ////        string value, unit;
                ////        if (!TryGetCellDataByFieldEnum(fileName, field, row, out value, out unit)) { return productIsDirty; }
                ////        if (product.Package == null) { product.Package = new PackageModel { Active = true, IsCustom = true, TypeKey = "Package" }; productIsDirty = true; }
                ////        if (product.Package.WidthUnitOfMeasure != unit)
                ////        {
                ////            product.Package.WidthUnitOfMeasure = unit;
                ////            productIsDirty = true;
                ////        }
                ////        break;
                ////    }
                case ProductImportFieldEnum.PackageDepth:
                {
                    var (hasValue, value, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? parsed : 0;
                    if (product.Package == null)
                    {
                        product.Package = new PackageModel { Active = true, IsCustom = true, TypeKey = "Package" };
                        productIsDirty = true;
                    }
                    if (product.Package.Depth != converted && converted > 0)
                    {
                        product.Package.Depth = converted;
                        productIsDirty = true;
                    }
                    if (product.Package.DepthUnitOfMeasure != unit)
                    {
                        product.Package.DepthUnitOfMeasure = unit;
                        productIsDirty = true;
                    }
                    break;
                }
                ////case ProductImportFieldEnum.PackageDepthUnitOfMeasure:
                ////    {
                ////        string value, unit;
                ////        if (!TryGetCellDataByFieldEnum(fileName, field, row, out value, out unit)) { return productIsDirty; }
                ////        if (product.Package == null) { product.Package = new PackageModel { Active = true, IsCustom = true, TypeKey = "Package" }; productIsDirty = true; }
                ////        if (product.Package.DepthUnitOfMeasure != unit)
                ////        {
                ////            product.Package.DepthUnitOfMeasure = unit;
                ////            productIsDirty = true;
                ////        }
                ////        break;
                ////    }
                case ProductImportFieldEnum.PackageHeight:
                {
                    var (hasValue, value, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? parsed : 0;
                    if (product.Package == null)
                    {
                        product.Package = new PackageModel { Active = true, IsCustom = true, TypeKey = "Package" };
                        productIsDirty = true;
                    }
                    if (product.Package.Height != converted && converted > 0)
                    {
                        product.Package.Height = converted;
                        productIsDirty = true;
                    }
                    if (product.Package.HeightUnitOfMeasure != unit)
                    {
                        product.Package.HeightUnitOfMeasure = unit;
                        productIsDirty = true;
                    }
                    break;
                }
                ////case ProductImportFieldEnum.PackageHeightUnitOfMeasure:
                ////    {
                ////        string value, unit;
                ////        if (!TryGetCellDataByFieldEnum(fileName, field, row, out value, out unit)) { return productIsDirty; }
                ////        if (product.Package == null) { product.Package = new PackageModel { Active = true, IsCustom = true, TypeKey = "Package" }; productIsDirty = true; }
                ////        if (product.Package.HeightUnitOfMeasure != unit)
                ////        {
                ////            product.Package.HeightUnitOfMeasure = unit;
                ////            productIsDirty = true;
                ////        }
                ////        break;
                ////    }
                case ProductImportFieldEnum.PackageWeight:
                {
                    var (hasValue, value, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? parsed : 0;
                    if (product.Package == null)
                    {
                        product.Package = new PackageModel { Active = true, IsCustom = true, TypeKey = "Package" };
                        productIsDirty = true;
                    }
                    if (product.Package.Weight != converted && converted > 0)
                    {
                        product.Package.Weight = converted;
                        productIsDirty = true;
                    }
                    if (product.Package.WeightUnitOfMeasure != unit)
                    {
                        product.Package.WeightUnitOfMeasure = unit;
                        productIsDirty = true;
                    }
                    break;
                }
                ////case ProductImportFieldEnum.PackageWeightUnitOfMeasure:
                ////    {
                ////        string value, unit;
                ////        if (!TryGetCellDataByFieldEnum(fileName, field, row, out value, out unit)) { return productIsDirty; }
                ////        if (product.Package == null) { product.Package = new PackageModel { Active = true, IsCustom = true, TypeKey = "Package" }; productIsDirty = true; }
                ////        if (product.Package.WeightUnitOfMeasure != unit)
                ////        {
                ////            product.Package.WeightUnitOfMeasure = unit;
                ////            productIsDirty = true;
                ////        }
                ////        break;
                ////    }
                case ProductImportFieldEnum.MasterPackWidth:
                {
                    var (hasValue, value, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? parsed : 0;
                    if (product.MasterPack == null)
                    {
                        product.MasterPack = new PackageModel { Active = true, IsCustom = true, TypeKey = "Master Pack" };
                        productIsDirty = true;
                    }
                    if (product.MasterPack.Width != converted && converted > 0)
                    {
                        product.MasterPack.Width = converted;
                        productIsDirty = true;
                    }
                    if (product.MasterPack.WidthUnitOfMeasure != unit)
                    {
                        product.MasterPack.WidthUnitOfMeasure = unit;
                        productIsDirty = true;
                    }
                    break;
                }
                ////case ProductImportFieldEnum.MasterPackWidthUnitOfMeasure:
                ////    {
                ////        string value, unit;
                ////        if (!TryGetCellDataByFieldEnum(fileName, field, row, out value, out unit)) { return productIsDirty; }
                ////        if (product.MasterPack == null) { product.MasterPack = new PackageModel { Active = true, IsCustom = true, TypeKey = "Master Pack" }; productIsDirty = true; }
                ////        if (product.MasterPack.WidthUnitOfMeasure != unit)
                ////        {
                ////            product.MasterPack.WidthUnitOfMeasure = unit;
                ////            productIsDirty = true;
                ////        }
                ////        break;
                ////    }
                case ProductImportFieldEnum.MasterPackDepth:
                {
                    var (hasValue, value, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? parsed : 0;
                    if (product.MasterPack == null)
                    {
                        product.MasterPack = new PackageModel { Active = true, IsCustom = true, TypeKey = "Master Pack" };
                        productIsDirty = true;
                    }
                    if (product.MasterPack.Depth != converted && converted > 0)
                    {
                        product.MasterPack.Depth = converted;
                        productIsDirty = true;
                    }
                    if (product.MasterPack.DepthUnitOfMeasure != unit)
                    {
                        product.MasterPack.DepthUnitOfMeasure = unit;
                        productIsDirty = true;
                    }
                    break;
                }
                ////case ProductImportFieldEnum.MasterPackDepthUnitOfMeasure:
                ////    {
                ////        string value, unit;
                ////        if (!TryGetCellDataByFieldEnum(fileName, field, row, out value, out unit)) { return productIsDirty; }
                ////        if (product.MasterPack == null) { product.MasterPack = new PackageModel { Active = true, IsCustom = true, TypeKey = "Master Pack" }; productIsDirty = true; }
                ////        if (product.MasterPack.DepthUnitOfMeasure != unit)
                ////        {
                ////            product.MasterPack.DepthUnitOfMeasure = unit;
                ////            productIsDirty = true;
                ////        }
                ////        break;
                ////    }
                case ProductImportFieldEnum.MasterPackHeight:
                {
                    var (hasValue, value, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? parsed : 0;
                    if (product.MasterPack == null)
                    {
                        product.MasterPack = new PackageModel { Active = true, IsCustom = true, TypeKey = "Master Pack" };
                        productIsDirty = true;
                    }
                    if (product.MasterPack.Height != converted && converted > 0)
                    {
                        product.MasterPack.Height = converted;
                        productIsDirty = true;
                    }
                    if (product.MasterPack.HeightUnitOfMeasure != unit)
                    {
                        product.MasterPack.HeightUnitOfMeasure = unit;
                        productIsDirty = true;
                    }
                    break;
                }
                ////case ProductImportFieldEnum.MasterPackHeightUnitOfMeasure:
                ////    {
                ////        string value, unit;
                ////        if (!TryGetCellDataByFieldEnum(fileName, field, row, out value, out unit)) { return productIsDirty; }
                ////        if (product.MasterPack == null) { product.MasterPack = new PackageModel { Active = true, IsCustom = true, TypeKey = "Master Pack" }; productIsDirty = true; }
                ////        if (product.MasterPack.HeightUnitOfMeasure != unit)
                ////        {
                ////            product.MasterPack.HeightUnitOfMeasure = unit;
                ////            productIsDirty = true;
                ////        }
                ////        break;
                ////    }
                case ProductImportFieldEnum.MasterPackWeight:
                {
                    var (hasValue, value, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? parsed : 0;
                    if (product.MasterPack == null)
                    {
                        product.MasterPack = new PackageModel { Active = true, IsCustom = true, TypeKey = "Master Pack" };
                        productIsDirty = true;
                    }
                    if (product.MasterPack.Weight != converted && converted > 0)
                    {
                        product.MasterPack.Weight = converted;
                        productIsDirty = true;
                    }
                    if (product.MasterPack.WeightUnitOfMeasure != unit)
                    {
                        product.MasterPack.WeightUnitOfMeasure = unit;
                        productIsDirty = true;
                    }
                    break;
                }
                ////case ProductImportFieldEnum.MasterPackWeightUnitOfMeasure:
                ////    {
                ////        string value, unit;
                ////        if (!TryGetCellDataByFieldEnum(fileName, field, row, out value, out unit)) { return productIsDirty; }
                ////        if (product.MasterPack == null) { product.MasterPack = new PackageModel { Active = true, IsCustom = true, TypeKey = "Master Pack" }; productIsDirty = true; }
                ////        if (product.MasterPack.WeightUnitOfMeasure != unit)
                ////        {
                ////            product.MasterPack.WeightUnitOfMeasure = unit;
                ////            productIsDirty = true;
                ////        }
                ////        break;
                ////    }
                case ProductImportFieldEnum.PalletWidth:
                {
                    var (hasValue, value, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? parsed : 0;
                    if (product.Pallet == null)
                    {
                        product.Pallet = new PackageModel { Active = true, IsCustom = true, TypeKey = "Pallet" };
                        productIsDirty = true;
                    }
                    if (product.Pallet.Width != converted && converted > 0)
                    {
                        product.Pallet.Width = converted;
                        productIsDirty = true;
                    }
                    if (product.Pallet.WidthUnitOfMeasure != unit)
                    {
                        product.Pallet.WidthUnitOfMeasure = unit;
                        productIsDirty = true;
                    }
                    break;
                }
                ////case ProductImportFieldEnum.PalletWidthUnitOfMeasure:
                ////    {
                ////        string value, unit;
                ////        if (!TryGetCellDataByFieldEnum(fileName, field, row, out value, out unit)) { return productIsDirty; }
                ////        if (product.Pallet == null) { product.Pallet = new PackageModel { Active = true, IsCustom = true, TypeKey = "Pallet" }; productIsDirty = true; }
                ////        if (product.Pallet.WidthUnitOfMeasure != unit)
                ////        {
                ////            product.Pallet.WidthUnitOfMeasure = unit;
                ////            productIsDirty = true;
                ////        }
                ////        break;
                ////    }
                case ProductImportFieldEnum.PalletDepth:
                {
                    var (hasValue, value, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? parsed : 0;
                    if (product.Pallet == null)
                    {
                        product.Pallet = new PackageModel { Active = true, IsCustom = true, TypeKey = "Pallet" };
                        productIsDirty = true;
                    }
                    if (product.Pallet.Depth != converted && converted > 0)
                    {
                        product.Pallet.Depth = converted;
                        productIsDirty = true;
                    }
                    if (product.Pallet.DepthUnitOfMeasure != unit)
                    {
                        product.Pallet.DepthUnitOfMeasure = unit;
                        productIsDirty = true;
                    }
                    break;
                }
                ////case ProductImportFieldEnum.PalletDepthUnitOfMeasure:
                ////    {
                ////        string value, unit;
                ////        if (!TryGetCellDataByFieldEnum(fileName, field, row, out value, out unit)) { return productIsDirty; }
                ////        if (product.Pallet == null) { product.Pallet = new PackageModel { Active = true, IsCustom = true, TypeKey = "Pallet" }; productIsDirty = true; }
                ////        if (product.Pallet.DepthUnitOfMeasure != unit)
                ////        {
                ////            product.Pallet.DepthUnitOfMeasure = unit;
                ////            productIsDirty = true;
                ////        }
                ////        break;
                ////    }
                case ProductImportFieldEnum.PalletHeight:
                {
                    var (hasValue, value, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? parsed : 0;
                    if (product.Pallet == null)
                    {
                        product.Pallet = new PackageModel { Active = true, IsCustom = true, TypeKey = "Pallet" };
                        productIsDirty = true;
                    }
                    if (product.Pallet.Height != converted && converted > 0)
                    {
                        product.Pallet.Height = converted;
                        productIsDirty = true;
                    }
                    if (product.Pallet.HeightUnitOfMeasure != unit)
                    {
                        product.Pallet.HeightUnitOfMeasure = unit;
                        productIsDirty = true;
                    }
                    break;
                }
                ////case ProductImportFieldEnum.PalletHeightUnitOfMeasure:
                ////    {
                ////        string value, unit;
                ////        if (!TryGetCellDataByFieldEnum(fileName, field, row, out value, out unit)) { return productIsDirty; }
                ////        if (product.Pallet == null) { product.Pallet = new PackageModel { Active = true, IsCustom = true, TypeKey = "Pallet" }; productIsDirty = true; }
                ////        if (product.Pallet.HeightUnitOfMeasure != unit)
                ////        {
                ////            product.Pallet.HeightUnitOfMeasure = unit;
                ////            productIsDirty = true;
                ////        }
                ////        break;
                ////    }
                case ProductImportFieldEnum.PalletWeight:
                {
                    var (hasValue, value, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? parsed : 0;
                    if (product.Pallet == null)
                    {
                        product.Pallet = new PackageModel { Active = true, IsCustom = true, TypeKey = "Pallet" };
                        productIsDirty = true;
                    }
                    if (product.Pallet.Weight != converted && converted > 0)
                    {
                        product.Pallet.Weight = converted;
                        productIsDirty = true;
                    }
                    if (product.Pallet.WeightUnitOfMeasure != unit)
                    {
                        product.Pallet.WeightUnitOfMeasure = unit;
                        productIsDirty = true;
                    }
                    break;
                }
                ////case ProductImportFieldEnum.PalletWeightUnitOfMeasure:
                ////    {
                ////        string value, unit;
                ////        if (!TryGetCellDataByFieldEnum(fileName, field, row, out value, out unit)) { return productIsDirty; }
                ////        if (product.Pallet == null) { product.Pallet = new PackageModel { Active = true, IsCustom = true, TypeKey = "Pallet" }; productIsDirty = true; }
                ////        if (product.Pallet.WeightUnitOfMeasure != unit)
                ////        {
                ////            product.Pallet.WeightUnitOfMeasure = unit;
                ////            productIsDirty = true;
                ////        }
                ////        break;
                ////    }

                case ProductImportFieldEnum.QuantityPerMasterPack:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? parsed : default(decimal?);
                    if (product.QuantityPerMasterPack != converted)
                    {
                        product.QuantityPerMasterPack = converted;
                        productIsDirty = true;
                    }
                    break;
                }

                case ProductImportFieldEnum.QuantityMasterPackPerPallet:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? parsed : default(decimal?);
                    if (product.QuantityMasterPackPerPallet != converted)
                    {
                        product.QuantityMasterPackPerPallet = converted;
                        productIsDirty = true;
                    }
                    break;
                }

                case ProductImportFieldEnum.QuantityPerPallet:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        return false;
                    }
                    var converted = decimal.TryParse(value, out var parsed) ? parsed : default(decimal?);
                    if (product.QuantityPerPallet != converted)
                    {
                        product.QuantityPerPallet = converted;
                        productIsDirty = true;
                    }
                    break;
                }
                case ProductImportFieldEnum.ProductType:
                {
                    var (hasValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
                    if (!hasValue)
                    {
                        break;
                    }
                    product.TypeID = await Workflows.ProductTypes.ResolveWithAutoGenerateToIDAsync(
                            byID: null,
                            byKey: null,
                            byName: value,
                            byDisplayName: null,
                            model: null,
                            contextProfileName: null)
                        .ConfigureAwait(false);
                    productIsDirty = true;
                    break;
                }
                // Associated Objects
                // ReSharper disable BadControlBracesLineBreaks, MissingLinebreak, MultipleStatementsOnOneLine, RedundantCaseLabel
                case ProductImportFieldEnum.Images:
                {
                    productIsDirty |= await LoopImagesAsync(fileName, row, product, contextProfileName).ConfigureAwait(false);
                    break;
                }
                case ProductImportFieldEnum.Vendors:
                {
                    productIsDirty |= await LoopVendorsAsync(fileName, field, row, product, contextProfileName).ConfigureAwait(false);
                    break;
                }
                case ProductImportFieldEnum.Manufacturers:
                {
                    productIsDirty |= await LoopManufacturersAsync(fileName, field, row, product, contextProfileName).ConfigureAwait(false);
                    break;
                }
                case ProductImportFieldEnum.Categories:
                { /*LoopCategories(field, row, product, ref productIsDirty);*/
                    break;
                } // Note: This this handled after-the-fact
                case ProductImportFieldEnum.RelatedProducts:
                { /*LoopRelatedProducts(field, row, product, ref productIsDirty);*/
                    break;
                } // Note: This is handled after-the-fact
                case ProductImportFieldEnum.Attribute:
                {
                    // Attributes handled elsewhere
                    break;
                }
                case ProductImportFieldEnum.PricePoints:
                { /*LoopPricePoints(field, row, product, ref productIsDirty);*/
                    break;
                }
                case ProductImportFieldEnum.InventoryLocationSections:
                { /*LoopLocationSections(field, row, product, ref productIsDirty);*/
                    break;
                }
                // ReSharper restore BadControlBracesLineBreaks, MissingLinebreak, MultipleStatementsOnOneLine, RedundantCaseLabel
                // ReSharper disable once RedundantEmptySwitchSection
                default:
                {
                    break;
                }
            }
            return productIsDirty;
        }
    }
}
