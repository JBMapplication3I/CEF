// <copyright file="ExcelSalesQuoteImporterProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the excel sales quote importer provider class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter.Excel
{
    /// <summary>An excel sales quote importer provider.</summary>
    /// <seealso cref="SalesQuoteImporterProviderBase"/>
    public partial class ExcelSalesQuoteImporterProvider : SalesQuoteImporterProviderBase
    {
        /// <summary>The alphabet.</summary>
        internal const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => ExcelSalesQuoteImporterProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;
    }
}
