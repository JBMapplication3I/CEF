// <copyright file="BulkOrderService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the bulk order service class</summary>
namespace Clarity.Ecommerce.Providers.Importer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Importer;
    using Interfaces.Providers.Pricing;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using Models.Import;
    using Service;
    using ServiceStack;
    using Utilities;

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/Cart/GetHeaders", "POST",
            Summary = "Get Excel Spreadsheet headers for mapping")]
    public class GetFileHeaders : IReturn<BulkOrderHeadersModel>
    {
        [ApiMember(Name = nameof(FileName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "File name to be imported")]
        public string FileName { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/Cart/BulkOrder", "POST",
            Summary = "Parse excel file and add item to temp cart")]
    public class BulkOrder : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(FileName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "File name to be imported")]
        public string FileName { get; set; } = null!;

        [ApiMember(Name = nameof(CartType), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Cart type to add items to")]
        public string CartType { get; set; } = null!;

        [ApiMember(Name = nameof(VendorID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "ID of the Vendor")]
        public int VendorID { get; set; }
    }

    public partial class BulkOrderService : ClarityEcommerceServiceBase
    {
#pragma warning disable CA1822,IDE1006
        public async Task<object?> Post(GetFileHeaders request)
#pragma warning restore CA1822,IDE1006
        {
            if (!Contract.CheckValidKey(request.FileName))
            {
                throw new InvalidOperationException("File name needs to be specified");
            }
            return new BulkOrderHeadersModel
            {
                // ReSharper disable once StyleCop.SA1008
                FileHeaders = await (await LoadExcelFileAsync(request.FileName).ConfigureAwait(false)).GetHeadersAsync().ConfigureAwait(false),
                SystemHeaders = new List<string> { "Manufacturer Part Number", "Quantity", "Required Date" },
            };
        }

#pragma warning disable CA1822,IDE1006
        public async Task<object?> Post(BulkOrder request)
#pragma warning restore CA1822,IDE1006
        {
            if (!Contract.CheckValidKey(request.FileName))
            {
                return CEFAR.FailingCEFAR("File name needs to be specified");
            }
            if (!Contract.CheckValidID(request.VendorID))
            {
                return CEFAR.FailingCEFAR("Vendor needs to be specified");
            }
            var typeName = request.CartType ?? "Cart";
            var response = new CEFActionResponse();
            var importer = await LoadExcelFileAsync(request.FileName).ConfigureAwait(false);
            var items = await importer.ParseAsync(contextProfileName: null).ConfigureAwait(false);
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (cartResponse, updatedSessionID) = await Workflows.Carts.SessionGetAsync(
                    lookupKey: lookupKey,
                    await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: null)
                .ConfigureAwait(false);
            if (updatedSessionID != null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            if (cartResponse.Result?.SalesItems!.Count > 0)
            {
                // Empty cart if items are already there
                await Workflows.Carts.SessionClearAsync(
                        lookupKey: lookupKey,
                        await GetPricingFactoryContextAsync().ConfigureAwait(false),
                        contextProfileName: null)
                    .ConfigureAwait(false);
            }
            ////int? _ = cartResponse.Result?.ID ?? 0;
            var count = 0;
            var vendor = await Workflows.Stores.GetAsync(request.VendorID, contextProfileName: null).ConfigureAwait(false);
            foreach (var item in items!)
            {
                count++;
                var manufacturerPartNumber = item.Fields!.SingleOrDefault(f => f.Name == "Manufacturer Part Number")?.Value;
                if (!Contract.CheckValidKey(manufacturerPartNumber))
                {
                    response.Messages.Add($"Error while adding item #{count}: Manufacturer Part Number is empty");
                    continue;
                }
                // Get product by manufacturer part number
                var product = await Workflows.Products.GetAsync(
                        key: vendor!.CustomKey + "_" + manufacturerPartNumber,
                        contextProfileName: null,
                        isVendorAdmin: false,
                        vendorAdminID: null)
                    .ConfigureAwait(false);
                if (product == null)
                {
                    response.Messages.Add(
                        $"Error while adding item #{count}: Manufacturer Part Number {manufacturerPartNumber} was not found in the system");
                    continue;
                }
                // Grab quantity
                var quantityStr = item.Fields!.SingleOrDefault(f => f.Name == "Quantity")?.Value;
                // Default quantity to 1 if cannot parse
                if (!int.TryParse(quantityStr, out var quantity))
                {
                    quantity = 1;
                }
                // Add item to cart
                var cartItem = new SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>
                {
                    Active = true,
                    ProductID = product.ID,
                    Quantity = quantity,
                    MasterID = cartResponse.Result?.ID ?? 0,
                };
                try
                {
                    await Workflows.CartItems.UpsertAsync(
                            lookupKey: lookupKey,
                            model: cartItem,
                            pricingFactory: RegistryLoaderWrapper.GetInstance<IPricingFactory>(ServiceContextProfileName),
                            pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                            contextProfileName: ServiceContextProfileName)
                        .ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    response.Messages.Add($"Error while adding item #{count}: {ex.Message}");
                }
            }
            response.ActionSucceeded = true;
            return response;
        }

        private static async Task<IImporterProviderBase> LoadExcelFileAsync(string fileName)
        {
            var filePath = Path.Combine(CEFConfigDictionary.StoredFilesInternalLocalPath, fileName);
            var stream = File.OpenRead(filePath);
            var spreadsheetModel = new SpreadsheetImportModel
            {
                SpreadsheetStream = stream,
            };
            var extension = fileName.GetExtension();
            spreadsheetModel.ImportType = string.Equals(extension, ".xls", StringComparison.OrdinalIgnoreCase)
                ? Enums.ImportType.XLS
                : Enums.ImportType.XLSX;
            var importer = RegistryLoaderWrapper.GetSpecificImportProvider("Excel", contextProfileName: null);
            if (importer == null)
            {
                throw new InvalidOperationException("Could load Excel Sheet importer provider");
            }
            var loadRet = await importer.LoadAsync(spreadsheetModel).ConfigureAwait(false);
            if (!loadRet)
            {
                throw new InvalidOperationException("Error loading the spreadsheet");
            }
            return importer;
        }
    }
}
