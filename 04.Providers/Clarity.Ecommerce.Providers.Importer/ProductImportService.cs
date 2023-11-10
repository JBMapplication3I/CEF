// <copyright file="ProductImportService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product import service class</summary>
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;
    using Interfaces.Providers.Importer;
    using JetBrains.Annotations;
    using Models.Import;
    using Newtonsoft.Json;
    using Providers.Importer;
    using ServiceStack;
    using Utilities;

    [PublicAPI,
        Authenticate,
        Route("/Products/RetrieveExcelWorkbookInfo", "POST",
            Summary = "Import a list of products from an Excel Spreadsheet")]
    public class RetrieveExcelWorkbookInfo : IReturn<List<string>>
    {
        [ApiMember(
            Name = "FileName", DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Workbook Name")]
        public string FileName { get; set; } = null!;
    }

    [PublicAPI, UsedInStoreAdmin,
        Authenticate,
        Route("/Products/RetrieveExcelWorkbookInfoForStore", "POST",
            Summary = "Import a list of products from an Excel Spreadsheet")]
    public class RetrieveExcelWorkbookInfoForStore : IReturn<List<string>>
    {
        [ApiMember(
            Name = "FileName", DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Workbook Name")]
        public string FileName { get; set; } = null!;
    }

    [PublicAPI, UsedInBrandAdmin,
        Authenticate,
        Route("/Products/RetrieveExcelWorkbookInfoForBrand", "POST",
            Summary = "Import a list of products from an Excel Spreadsheet")]
    public class RetrieveExcelWorkbookInfoForBrand : IReturn<List<string>>
    {
        [ApiMember(
            Name = "FileName", DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Workbook Name")]
        public string FileName { get; set; } = null!;
    }

    [PublicAPI, UsedInVendorAdmin,
        Authenticate,
        Route("/Products/RetrieveExcelWorkbookInfoForVendor", "POST",
            Summary = "Import a list of products from an Excel Spreadsheet")]
    public class RetrieveExcelWorkbookInfoForVendor : IReturn<List<string>>
    {
        [ApiMember(
            Name = "FileName", DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Workbook Name")]
        public string FileName { get; set; } = null!;
    }

    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Products/ImportFromExcel", "POST",
            Summary = "Import a list of products from a Excel Spreadsheet")]
    public class ImportProductsFromExcel : IReturn<ImportResponse>
    {
        [ApiMember(
            Name = "FileName", DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "File name to be imported")]
        public string FileName { get; set; } = null!;
    }

    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Products/ImportKeywordsFromExcel", "POST",
            Summary = "Import a list of products from a Excel Spreadsheet")]
    public class ImportProductKeywordsFromExcel : IReturn<ImportResponse>
    {
        [ApiMember(
            Name = "FileName", DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "File name to be imported")]
        public string FileName { get; set; } = null!;
    }

    [PublicAPI, UsedInStoreAdmin,
        Authenticate,
        Route("/Products/ImportFromExcelForStore", "POST",
            Summary = "Import a list of products from an Excel Spreadsheet")]
    public class ImportProductsFromExcelForStore : IReturn<ImportResponse>
    {
        [DataMember(Name = "importMappings"), JsonProperty("importMappings"), ApiMember(
            Name = "importMappings", DataType = "Dictionary<string, ProductImportFieldEnum>", ParameterType = "body", IsRequired = true,
            Description = "List of import mappings")]
        public Dictionary<string, Enums.ProductImportFieldEnum> ImportMappings { get; set; } = null!;

        [ApiMember(
            Name = "UseCustomKeyForLookups", DataType = "bool", ParameterType = "body", IsRequired = true,
            Description = "Use the CustomKey to lookup existing products instead of by Name")]
        public bool UseCustomKeyForLookups { get; set; }

        [ApiMember(
            Name = "FileName", DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "File name to be imported")]
        public string FileName { get; set; } = null!;
    }

    [PublicAPI, UsedInBrandAdmin,
        Authenticate,
        Route("/Products/ImportFromExcelForBrand", "POST",
            Summary = "Import a list of products from an Excel Spreadsheet")]
    public class ImportProductsFromExcelForBrand : IReturn<ImportResponse>
    {
        [DataMember(Name = "importMappings"), JsonProperty("importMappings"), ApiMember(
            Name = "importMappings", DataType = "Dictionary<string, ProductImportFieldEnum>", ParameterType = "body", IsRequired = true,
            Description = "List of import mappings")]
        public Dictionary<string, Enums.ProductImportFieldEnum> ImportMappings { get; set; } = null!;

        [ApiMember(
            Name = "UseCustomKeyForLookups", DataType = "bool", ParameterType = "body", IsRequired = true,
            Description = "Use the CustomKey to lookup existing products instead of by Name")]
        public bool UseCustomKeyForLookups { get; set; }

        [ApiMember(
            Name = "FileName", DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "File name to be imported")]
        public string FileName { get; set; } = null!;
    }

    [PublicAPI, UsedInVendorAdmin,
        Authenticate,
        Route("/Products/ImportFromExcelForVendor", "POST",
            Summary = "Import a list of products from an Excel Spreadsheet")]
    public class ImportProductsFromExcelForVendor : IReturn<ImportResponse>
    {
        [DataMember(Name = "importMappings"), JsonProperty("importMappings"), ApiMember(
            Name = "importMappings", DataType = "Dictionary<string, ProductImportFieldEnum>", ParameterType = "body", IsRequired = true,
            Description = "List of import mappings")]
        public Dictionary<string, Enums.ProductImportFieldEnum> ImportMappings { get; set; } = null!;

        [ApiMember(
            Name = "UseCustomKeyForLookups", DataType = "bool", ParameterType = "body", IsRequired = true,
            Description = "Use the CustomKey to lookup existing products instead of by Name")]
        public bool UseCustomKeyForLookups { get; set; }

        [ApiMember(
            Name = "FileName", DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "File name to be imported")]
        public string FileName { get; set; } = null!;
    }

    [PublicAPI]
    public class ProductImportService : ClarityEcommerceServiceBase
    {
#pragma warning disable CA1822,IDE1006
        public async Task<object?> Post(RetrieveExcelWorkbookInfo request)
#pragma warning restore CA1822,IDE1006
        {
            if (string.IsNullOrWhiteSpace(request.FileName))
            {
                throw new ArgumentException("File name needs to be specified");
            }
            // var importer = new ExcelImporter(Workflows);
            var importer = RegistryLoaderWrapper.GetSpecificImportProvider("Excel", ServiceContextProfileName);
            if (importer == null)
            {
                throw new InvalidOperationException("Could load Excel importer provider");
            }
            return await importer.ReadWorkbookHeaderInfoAsync(request.FileName, ServiceContextProfileName).ConfigureAwait(false);
        }

#pragma warning disable CA1822,IDE1006
        public async Task<object?> Post(RetrieveExcelWorkbookInfoForStore request)
#pragma warning restore CA1822,IDE1006
        {
            Contract.RequiresValidKey<ArgumentException>(request.FileName, "File name needs to be specified");
            var importer = Contract.RequiresNotNull(
                RegistryLoaderWrapper.GetSpecificImportProvider("Excel", ServiceContextProfileName),
                "Could load Excel importer provider");
            importer.StoreID[request.FileName] = await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false);
            importer.AllowCreateCategories = false;
            return await importer.ReadWorkbookHeaderInfoAsync(request.FileName, ServiceContextProfileName).ConfigureAwait(false);
        }

#pragma warning disable CA1822,IDE1006
        public async Task<object?> Post(RetrieveExcelWorkbookInfoForBrand request)
#pragma warning restore CA1822,IDE1006
        {
            Contract.RequiresValidKey<ArgumentException>(request.FileName, "File name needs to be specified");
            var importer = Contract.RequiresNotNull(
                RegistryLoaderWrapper.GetSpecificImportProvider("Excel", ServiceContextProfileName),
                "Could load Excel importer provider");
            importer.BrandID[request.FileName] = await CurrentBrandForBrandAdminIDOrThrow401Async().ConfigureAwait(false);
            importer.AllowCreateCategories = false;
            return await importer.ReadWorkbookHeaderInfoAsync(request.FileName, ServiceContextProfileName).ConfigureAwait(false);
        }

#pragma warning disable CA1822,IDE1006
        public async Task<object?> Post(RetrieveExcelWorkbookInfoForVendor request)
#pragma warning restore CA1822,IDE1006
        {
            Contract.RequiresValidKey<ArgumentException>(request.FileName, "File name needs to be specified");
            var importer = Contract.RequiresNotNull(
                RegistryLoaderWrapper.GetSpecificImportProvider("Excel", ServiceContextProfileName),
                "Could load Excel importer provider");
            importer.VendorID[request.FileName] = await CurrentVendorForVendorAdminIDOrThrow401Async().ConfigureAwait(false);
            importer.AllowCreateCategories = false;
            return await importer.ReadWorkbookHeaderInfoAsync(request.FileName, ServiceContextProfileName).ConfigureAwait(false);
        }

#pragma warning disable CA1822,IDE1006
        public async Task<object?> Post(ImportProductsFromExcel request)
#pragma warning restore CA1822,IDE1006
        {
            if (string.IsNullOrEmpty(request.FileName))
            {
                throw new InvalidOperationException("File name needs to be specified");
            }
            var provider = RegistryLoaderWrapper.GetFilesProvider(ServiceContextProfileName);
            if (provider is null)
            {
                throw new InvalidOperationException("No files provider detected");
            }
            var rootPath = await provider.GetFileSaveRootPathFromFileEntityTypeAsync(Enums.FileEntityType.ImportProduct).ConfigureAwait(false);
            var filePath = Path.Combine(rootPath, request.FileName);
            var stream = File.OpenRead(filePath);
            var spreadsheetModel = new SpreadsheetImportModel
            {
                SpreadsheetStream = stream,
            };
            var extension = request.FileName.GetExtension();
            IImporterProviderBase? importer;
            switch (extension.ToLower())
            {
                case ".xls":
                {
                    spreadsheetModel.ImportType = Enums.ImportType.XLS;
                    importer = RegistryLoaderWrapper.GetSpecificImportProvider("Excel", ServiceContextProfileName);
                    break;
                }
                case ".xlsx":
                {
                    spreadsheetModel.ImportType = Enums.ImportType.XLSX;
                    importer = RegistryLoaderWrapper.GetSpecificImportProvider("Excel", ServiceContextProfileName);
                    break;
                }
                case ".csv":
                {
                    spreadsheetModel.ImportType = Enums.ImportType.XLSX;
                    importer = RegistryLoaderWrapper.GetSpecificImportProvider("CSV", ServiceContextProfileName);
                    break;
                }
                default:
                {
                    throw new InvalidOperationException("Extension not supported");
                }
            }
            if (importer == null)
            {
                throw new InvalidOperationException("Could load Excel Sheet importer provider");
            }
            var response = await importer.IntegrateProductsAsync(spreadsheetModel, ServiceContextProfileName).ConfigureAwait(false);
            var retVal = new ImportResponse();
            retVal.ErrorMessages.AddRange(response.Messages);
            return retVal;
        }

        public async Task<object?> Post(ImportProductKeywordsFromExcel request)
#pragma warning restore CA1822,IDE1006
        {
            if (string.IsNullOrEmpty(request.FileName))
            {
                throw new InvalidOperationException("File name needs to be specified");
            }
            var provider = RegistryLoaderWrapper.GetFilesProvider(ServiceContextProfileName);
            if (provider is null)
            {
                throw new InvalidOperationException("No files provider detected");
            }
            var rootPath = await provider.GetFileSaveRootPathFromFileEntityTypeAsync(Enums.FileEntityType.ImportProduct).ConfigureAwait(false);
            var filePath = Path.Combine(rootPath, request.FileName);
            var stream = File.OpenRead(filePath);
            var spreadsheetModel = new SpreadsheetImportModel
            {
                SpreadsheetStream = stream,
            };
            var extension = request.FileName.GetExtension();
            IImporterProviderBase? importer;
            switch (extension.ToLower())
            {
                case ".xls":
                {
                    spreadsheetModel.ImportType = Enums.ImportType.XLS;
                    importer = RegistryLoaderWrapper.GetSpecificImportProvider("Excel", ServiceContextProfileName);
                    break;
                }
                case ".xlsx":
                {
                    spreadsheetModel.ImportType = Enums.ImportType.XLSX;
                    importer = RegistryLoaderWrapper.GetSpecificImportProvider("Excel", ServiceContextProfileName);
                    break;
                }
                case ".csv":
                {
                    spreadsheetModel.ImportType = Enums.ImportType.XLSX;
                    importer = RegistryLoaderWrapper.GetSpecificImportProvider("CSV", ServiceContextProfileName);
                    break;
                }
                default:
                {
                    throw new InvalidOperationException("Extension not supported");
                }
            }
            if (importer == null)
            {
                throw new InvalidOperationException("Could load Excel Sheet importer provider");
            }
            var response = await importer.UpdateSeoKeywordsAsync(spreadsheetModel, ServiceContextProfileName).ConfigureAwait(false);
            var retVal = new ImportResponse();
            retVal.ErrorMessages.AddRange(response.Messages);
            return retVal;
        }

#pragma warning disable CA1822,IDE1006
        public async Task<object?> Post(ImportProductsFromExcelForStore request)
#pragma warning restore CA1822,IDE1006
        {
            if (string.IsNullOrWhiteSpace(request.FileName))
            {
                throw new ArgumentException("File name needs to be specified");
            }
            var importer = RegistryLoaderWrapper.GetSpecificImportProvider("Excel", ServiceContextProfileName);
            if (importer == null)
            {
                throw new InvalidOperationException("Could load Excel importer provider");
            }
            importer.StoreID[request.FileName] = await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false);
            importer.ImportMappings[request.FileName] = request.ImportMappings;
            importer.Responses[request.FileName] = new();
            importer.AllowCreateCategories = false;
            await importer.ImportAsync(request.FileName, null).ConfigureAwait(false);
            return importer.Responses[request.FileName];
        }

#pragma warning disable CA1822,IDE1006
        public async Task<object?> Post(ImportProductsFromExcelForBrand request)
#pragma warning restore CA1822,IDE1006
        {
            if (string.IsNullOrWhiteSpace(request.FileName))
            {
                throw new ArgumentException("File name needs to be specified");
            }
            var importer = RegistryLoaderWrapper.GetSpecificImportProvider("Excel", ServiceContextProfileName);
            if (importer == null)
            {
                throw new InvalidOperationException("Could load Excel importer provider");
            }
            importer.BrandID[request.FileName] = await CurrentBrandForBrandAdminIDOrThrow401Async().ConfigureAwait(false);
            importer.ImportMappings[request.FileName] = request.ImportMappings;
            importer.Responses[request.FileName] = new();
            importer.AllowCreateCategories = false;
            await importer.ImportAsync(request.FileName, null).ConfigureAwait(false);
            return importer.Responses[request.FileName];
        }

#pragma warning disable CA1822,IDE1006
        public async Task<object?> Post(ImportProductsFromExcelForVendor request)
#pragma warning restore CA1822,IDE1006
        {
            if (string.IsNullOrWhiteSpace(request.FileName))
            {
                throw new ArgumentException("File name needs to be specified");
            }
            var importer = RegistryLoaderWrapper.GetSpecificImportProvider("Excel", ServiceContextProfileName);
            if (importer == null)
            {
                throw new InvalidOperationException("Could load Excel importer provider");
            }
            importer.VendorID[request.FileName] = await CurrentVendorForVendorAdminIDOrThrow401Async().ConfigureAwait(false);
            importer.ImportMappings[request.FileName] = request.ImportMappings;
            importer.Responses[request.FileName] = new();
            importer.AllowCreateCategories = false;
            await importer.ImportAsync(request.FileName, null).ConfigureAwait(false);
            return importer.Responses[request.FileName];
        }
    }
}
