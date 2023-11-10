// <copyright file="IImporterProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IImporterProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Importer
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Ecommerce.Providers.Importer;
    using Models;
    using Models.Import;

    /// <summary>Interface for importer provider base.</summary>
    public interface IImporterProviderBase : IProviderBase
    {
        /// <summary>Gets or sets the google access token.</summary>
        /// <value>The google access token.</value>
        string? GoogleAccessToken { get; set; }

        /// <summary>Gets or sets the identifier of the google client.</summary>
        /// <value>The identifier of the google client.</value>
        string? GoogleClientId { get; set; }

        /// <summary>Gets or sets a value indicating whether we allow create categories.</summary>
        /// <value>True if allow create categories, false if not.</value>
        bool AllowCreateCategories { get; set; }

        /// <summary>Gets or sets the responses.</summary>
        /// <value>The responses.</value>
        Dictionary<string /*fileName*/, ImportResponse> Responses { get; set; }

        /// <summary>Gets the name of the column by.</summary>
        /// <value>The name of the column by.</value>
        Dictionary<string /*fileName*/, Dictionary<string /*columnName*/, uint /*columnNumber*/>> ColumnByName { get; }

        /// <summary>Gets the column by name uof milliseconds.</summary>
        /// <value>The column by name uof milliseconds.</value>
        Dictionary<string /*fileName*/, Dictionary<string /*columnName*/, uint /*columnNumber*/>> ColumnByNameUofMs { get; }

        /// <summary>Gets or sets the import mappings.</summary>
        /// <value>The import mappings.</value>
        Dictionary<string /*fileName*/, Dictionary<string /*columnName*/, Enums.ProductImportFieldEnum>> ImportMappings { get; set; }

        /// <summary>Gets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        Dictionary<string /*fileName*/, int?> StoreID { get; }

        /// <summary>Gets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        Dictionary<string /*fileName*/, int?> BrandID { get; }

        /// <summary>Gets the identifier of the vendor.</summary>
        /// <value>The identifier of the vendor.</value>
        Dictionary<string /*fileName*/, int?> VendorID { get; }

        /// <summary>Loads the given Spreadsheet model.</summary>
        /// <param name="spsModel">The Spreadsheet model to load.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> LoadAsync(ISpreadsheetImportModel spsModel);

        /// <summary>Enumerates parse in this collection.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An enumerator that allows foreach to be used to process parse in this collection.</returns>
        Task<IEnumerable<IImportItem>?> ParseAsync(string? contextProfileName);

        /// <summary>Gets the headers in this collection.</summary>
        /// <returns>An enumerator that allows foreach to be used to process the headers in this collection.</returns>
        Task<IEnumerable<string>?> GetHeadersAsync();

        /// <summary>Integrate products.</summary>
        /// <param name="spreadsheetModel">  The spreadsheet model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> IntegrateProductsAsync(
            ISpreadsheetImportModel spreadsheetModel,
            string? contextProfileName);

        /// <summary>Update SEO Kewords.</summary>
        /// <param name="spreadsheetModel">  The spreadsheet model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> UpdateSeoKeywordsAsync(
            ISpreadsheetImportModel spreadsheetModel,
            string? contextProfileName);

        /// <summary>Gets parsing error.</summary>
        /// <returns>The parsing error.</returns>
        Task<List<string>> GetParsingErrorAsync();

        /// <summary>Resolves out to products to upsert.</summary>
        /// <param name="items">             The items.</param>
        /// <param name="getModelByKey">     The get model by key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A List{(IProductModel, IRawProductPricesModel, IUpdateInventoryForProduct)}.</returns>
        Task<List<(IProductModel? model, IRawProductPricesModel? pricing, IUpdateInventoryForProduct? inventory)>> ResolveAsync(
            IEnumerable<IImportItem> items,
            Func<string, Task<IProductModel?>> getModelByKey,
            string? contextProfileName);

        /// <summary>Imports the given file.</summary>
        /// <param name="fileName">          Filename of the file.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        Task ImportAsync(string fileName, string? contextProfileName);

        /// <summary>Reads workbook header information.</summary>
        /// <param name="fileName">          Filename of the file.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The workbook header information.</returns>
        Task<List<string>> ReadWorkbookHeaderInfoAsync(string fileName, string? contextProfileName);
    }
}
