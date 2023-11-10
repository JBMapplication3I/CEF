// <copyright file="LocalFilesProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the local files provider configuration class</summary>
// TODO: The settings loading from this file needs to be changed to read from the CEFConfigDictionary as the app
// settings file won't normally have these values in them anymore
namespace Clarity.Ecommerce.Providers.Files.LocalFileSystem
{
    using System;
    using DataModel;
    using Enums;
    using Interfaces.Providers;
    using JSConfigs;
    using Utilities;

    /// <summary>A local files provider configuration.</summary>
    internal static class LocalFilesProviderConfig
    {
        /// <summary>Gets a path.</summary>
        /// <param name="fileEntityType">Type of the file entity. If not set, will give you the raw root path.</param>
        /// <returns>The path.</returns>
        internal static string GetPath(FileEntityType? fileEntityType)
        {
            if (!fileEntityType.HasValue)
            {
                return CEFConfigDictionary.StoredFilesInternalLocalPath;
            }
            return CEFConfigDictionary.StoredFilesInternalLocalPath.Replace('/', '\\')
                + GetRelativePath(fileEntityType.Value)?.Replace('/', '\\');
        }

        /// <summary>Gets relative path.</summary>
        /// <param name="fileEntityType">Type of the file entity.</param>
        /// <returns>The relative path.</returns>
        internal static string? GetRelativePath(FileEntityType fileEntityType)
        {
            switch (fileEntityType)
            {
                // Images
                case FileEntityType.ImageAccount:
                {
                    return GetSettingValue(nameof(Account), "Images");
                }
                case FileEntityType.ImageAd:
                {
                    return GetSettingValue(nameof(Ad), "Images");
                }
                case FileEntityType.ImageBrand:
                {
                    return GetSettingValue(nameof(Brand), "Images");
                }
                case FileEntityType.ImageCalendarEvent:
                {
                    return GetSettingValue(nameof(CalendarEvent), "Images");
                }
                case FileEntityType.ImageCategory:
                {
                    return GetSettingValue(nameof(Category), "Images");
                }
                case FileEntityType.ImageCountry:
                {
                    return GetSettingValue(nameof(Country), "Images");
                }
                case FileEntityType.ImageCurrency:
                {
                    return GetSettingValue(nameof(Currency), "Images");
                }
                case FileEntityType.ImageLanguage:
                {
                    return GetSettingValue(nameof(Language), "Images");
                }
                case FileEntityType.ImageManufacturer:
                {
                    return GetSettingValue(nameof(Manufacturer), "Images");
                }
                case FileEntityType.ImageProduct:
                {
                    return GetSettingValue(nameof(Product), "Images");
                }
                case FileEntityType.ImageRegion:
                {
                    return GetSettingValue(nameof(Region), "Images");
                }
                case FileEntityType.ImageStore:
                {
                    return GetSettingValue(nameof(Store), "Images");
                }
                case FileEntityType.ImageUser:
                {
                    return GetSettingValue(nameof(User), "Images");
                }
                case FileEntityType.ImageVendor:
                {
                    return GetSettingValue(nameof(Vendor), "Images");
                }
                // Stored Files
                case FileEntityType.StoredFileAccount:
                {
                    return GetSettingValue(nameof(Account), "Files");
                }
                case FileEntityType.StoredFileCalendarEvent:
                {
                    return GetSettingValue(nameof(CalendarEvent), "Files");
                }
                case FileEntityType.StoredFileCart:
                {
                    return GetSettingValue(nameof(Cart), "Files");
                }
                case FileEntityType.StoredFileCategory:
                {
                    return GetSettingValue(nameof(Category), "Files");
                }
                case FileEntityType.StoredFileEmailQueueAttachment:
                {
                    return GetSettingValue(nameof(EmailQueueAttachment), "Files");
                }
                case FileEntityType.StoredFileMessageAttachment:
                {
                    return GetSettingValue(nameof(MessageAttachment), "Files");
                }
                case FileEntityType.StoredFileProduct:
                {
                    return GetSettingValue(nameof(Product), "Files");
                }
                case FileEntityType.StoredFilePurchaseOrder:
                {
                    return GetSettingValue(nameof(PurchaseOrder), "Files");
                }
                case FileEntityType.StoredFileSalesInvoice:
                {
                    return GetSettingValue(nameof(SalesInvoice), "Files");
                }
                case FileEntityType.StoredFileSalesOrder:
                {
                    return GetSettingValue(nameof(SalesOrder), "Files");
                }
                case FileEntityType.StoredFileSalesQuote:
                {
                    return GetSettingValue(nameof(SalesQuote), "Files");
                }
                case FileEntityType.StoredFileSalesReturn:
                {
                    return GetSettingValue(nameof(SalesReturn), "Files");
                }
                case FileEntityType.StoredFileSampleRequest:
                {
                    return GetSettingValue(nameof(SampleRequest), "Files");
                }
                case FileEntityType.StoredFileUser:
                {
                    return GetSettingValue(nameof(User), "Files");
                }
                // Imports
                case FileEntityType.ImportExcel:
                {
                    return GetSettingValue("Excel", "Imports");
                }
                case FileEntityType.ImportProduct:
                {
                    return GetSettingValue(nameof(Product), "Imports");
                }
                case FileEntityType.ImportProductPricePoint:
                {
                    return GetSettingValue(nameof(ProductPricePoint), "Imports");
                }
                case FileEntityType.ImportSalesQuote:
                {
                    return GetSettingValue(nameof(SalesQuote), "Imports");
                }
                case FileEntityType.ImportUser:
                {
                    return GetSettingValue(nameof(User), "Imports");
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(fileEntityType), fileEntityType, null);
                }
            }
        }

        /// <summary>Query if this LocalFilesProviderConfig is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this LocalFilesProviderConfig is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<LocalFilesProvider>() || isDefaultAndActivated)
            && Contract.CheckValidKey(CEFConfigDictionary.StoredFilesInternalLocalPath);

        /// <summary>Gets a path.</summary>
        /// <param name="area">The area (e.g.- Product).</param>
        /// <param name="type">The type (e.g.- Images).</param>
        /// <returns>The path.</returns>
        private static string? GetSettingValue(string area, string type)
        {
            return ProviderConfig.GetStringSetting($"Clarity.Uploads.{type}.{area}", $"\\{area}\\{type}\\");
        }
    }
}
