// <copyright file="ExcelSalesQuoteImporterProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the excel sales quote importer provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter.Excel
{
    using Interfaces.Providers;

    /// <summary>An excel sales quote importer provider configuration.</summary>
    internal static class ExcelSalesQuoteImporterProviderConfig
    {
        /// <summary>Gets a value indicating whether the create sales group.</summary>
        /// <value>True if create sales group, false if not.</value>
        internal static bool CreateSalesGroup { get; }
            = ProviderConfig.GetBooleanSetting("Clarity.Providers.SalesQuoteImporter.CreateSalesGroup");

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<ExcelSalesQuoteImporterProvider>() || isDefaultAndActivated;
    }
}
