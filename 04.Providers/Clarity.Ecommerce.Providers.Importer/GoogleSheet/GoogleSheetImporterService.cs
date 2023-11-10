// <copyright file="GoogleSheetImporterService.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the google sheet importer service class</summary>
// ReSharper disable BadListLineBreaks, InconsistentNaming
#pragma warning disable 1591, IDE1006 // Naming Styles
#pragma warning disable SA1600 // Elements should be documented
namespace Clarity.Ecommerce.Providers.Importer.GoogleSheet
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models.Import;
    using Newtonsoft.Json;
    using Service;
    using ServiceStack;
    using Utilities;

    /// <summary>A google sheets request base.</summary>
    public abstract class GoogleSheetsRequestBase
    {
        /// <summary>Gets or sets the identifier of the client.</summary>
        /// <value>The identifier of the client.</value>
        [DataMember(Name = "google_client_id"), JsonProperty("google_client_id"),
         ApiMember(Name = "google_client_id", DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "Google API Client ID")]
        public string ClientId { get; set; }

        /// <summary>Gets or sets the access token.</summary>
        /// <value>The access token.</value>
        [DataMember(Name = "google_access_token"), JsonProperty("google_access_token"),
         ApiMember(Name = "google_access_token", DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "Google API Access Token")]
        public string AccessToken { get; set; }

        /// <summary>Gets or sets the identifier of the sheet.</summary>
        /// <value>The identifier of the sheet.</value>
        [DataMember(Name = "sheet_id"), JsonProperty("sheet_id"),
         ApiMember(Name = "sheet_id", DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "Google Sheet Id")]
        public string SheetId { get; set; }
    }

    /// <summary>Information about the retrieve google workbook.</summary>
    /// <seealso cref="GoogleSheetsRequestBase"/>
    /// <seealso cref="IReturn{List{String}}"/>
    [PublicAPI]
    [Authenticate]
    [Route("/Providers/Importer/GoogleSheets/RetrieveInfo", "POST",
        Summary = "Import a list of products from a Google Spreadsheet")]
    public class RetrieveGoogleWorkbookInfo : GoogleSheetsRequestBase, IReturn<List<string>>
    {
    }

    /// <summary>A retrieve google workbook information for store.</summary>
    /// <seealso cref="GoogleSheetsRequestBase"/>
    /// <seealso cref="IReturn{List{String}}"/>
    [PublicAPI]
    [Authenticate]
    [Route("/Providers/Importer/GoogleSheets/RetrieveInfoForStore", "POST",
        Summary = "Import a list of products from a Google Spreadsheet")]
    public class RetrieveGoogleWorkbookInfoForStore : GoogleSheetsRequestBase, IReturn<List<string>>
    {
    }

    /// <summary>An import products from google sheet.</summary>
    /// <seealso cref="GoogleSheetsRequestBase"/>
    /// <seealso cref="IReturn{ImportResponse}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate,
     Route("/Providers/Importer/GoogleSheets/Import", "POST",
        Summary = "Import a list of products from a Google Spreadsheet")]
    public class ImportProductsFromGoogleSheet : GoogleSheetsRequestBase, IReturn<ImportResponse>
    {
        ////[ApiMember(Name = nameof(importMappings), Description = "List of import mappings", IsRequired = true)]
        ////public Dictionary<string, ProductImportFieldEnum> importMappings { get; set; }

        /// <summary>Gets or sets a value indicating whether this ImportProductsFromGoogleSheet use custom key for
        /// lookups.</summary>
        /// <value>True if use custom key for lookups, false if not.</value>
        [ApiMember(Name = nameof(UseCustomKeyForLookups), DataType = "bool", ParameterType = "body", IsRequired = true,
            Description = "Use the CustomKey to lookup existing products instead of by Name")]
        public bool UseCustomKeyForLookups { get; set; }
    }

    /// <summary>An import products from google sheet for store.</summary>
    /// <seealso cref="GoogleSheetsRequestBase"/>
    /// <seealso cref="IReturn{ImportResponse}"/>
    [PublicAPI]
    [Authenticate]
    [Route("/Providers/Importer/GoogleSheets/ImportForStore", "POST",
        Summary = "Import a list of products from a Google Spreadsheet")]
    public class ImportProductsFromGoogleSheetForStore : GoogleSheetsRequestBase, IReturn<ImportResponse>
    {
        /// <summary>Gets or sets the import mappings.</summary>
        /// <value>The import mappings.</value>
        [DataMember(Name = "importMappings"), JsonProperty("importMappings"),
         ApiMember(Name = "importMappings", ParameterType = "body", IsRequired = true,
            Description = "List of import mappings")]
        public Dictionary<string, Enums.ProductImportFieldEnum> ImportMappings { get; set; }

        /// <summary>Gets or sets a value indicating whether this ImportProductsFromGoogleSheetForStore use custom key
        /// for lookups.</summary>
        /// <value>True if use custom key for lookups, false if not.</value>
        [ApiMember(Name = nameof(UseCustomKeyForLookups), DataType = "bool", ParameterType = "body", IsRequired = true,
            Description = "Use the CustomKey to lookup existing products instead of by Name")]
        public bool UseCustomKeyForLookups { get; set; }
    }

    /// <summary>A google sheet importer service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    [PublicAPI]
    public class GoogleSheetImporterService : ClarityEcommerceServiceBase
    {
        /// <summary>Post this message.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Post(RetrieveGoogleWorkbookInfo request)
        {
            Contract.RequiresValidKey(request.SheetId, "Sheet ID needs to be specified");
            var importer = new GoogleSheetImporterProvider
            {
                GoogleClientId = request.ClientId,
                GoogleAccessToken = request.AccessToken,
            };
            return await importer.ReadWorkbookHeaderInfoAsync(request.SheetId, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>Post this message.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Post(RetrieveGoogleWorkbookInfoForStore request)
        {
            Contract.RequiresValidKey(request.SheetId, "Sheet ID needs to be specified");
            var storeID = await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false);
            var importer = new GoogleSheetImporterProvider
            {
                GoogleClientId = request.ClientId,
                GoogleAccessToken = request.AccessToken,
                AllowCreateCategories = false,
            };
            importer.StoreID[request.SheetId] = storeID;
            return await importer.ReadWorkbookHeaderInfoAsync(request.SheetId, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>Post this message.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Post(ImportProductsFromGoogleSheet request)
        {
            var spreadsheetModel = new SpreadsheetImportModel
            {
                GoogleSheet = new GoogleSheetModel
                {
                    GoogleClientID = request.ClientId,
                    GoogleAccessToken = request.AccessToken,
                    SheetID = request.SheetId,
                },
            };
            var response = await new GoogleSheetImporterProvider()
                .IntegrateProductsAsync(spreadsheetModel, contextProfileName: null).ConfigureAwait(false);
            var retVal = new ImportResponse();
            retVal.ErrorMessages.AddRange(response.Messages);
            return retVal;
        }

        /// <summary>Post this message.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Post(ImportProductsFromGoogleSheetForStore request)
        {
            Contract.RequiresValidKey(request.SheetId, "Sheet ID needs to be specified");
            var storeID = await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false);
            var importer = new GoogleSheetImporterProvider
            {
                ImportMappings = { [request.SheetId] = request.ImportMappings },
                GoogleClientId = request.ClientId,
                GoogleAccessToken = request.AccessToken,
                AllowCreateCategories = false,
            };
            importer.StoreID[request.SheetId] = storeID;
            importer.Responses[request.SheetId] = new ImportResponse();
            await importer.ImportAsync(request.SheetId, contextProfileName: null).ConfigureAwait(false);
            return importer.Responses[request.SheetId];
        }
    }

    /// <summary>A google sheet importer feature.</summary>
    /// <seealso cref="IPlugin"/>
    [PublicAPI]
    public class GoogleSheetImporterFeature : IPlugin
    {
        /// <summary>Registers this CheckoutFeature.</summary>
        /// <param name="appHost">The application host.</param>
        public void Register(IAppHost appHost)
        {
            // This function intentionally left blank
        }
    }
}
