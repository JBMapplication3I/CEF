// <copyright file="AvalaraTaxesProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the avalara taxes provider class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Avalara.AvaTax.RestClient;
    using Ecommerce.Models;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Mapper;
    using Models;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>An Avalara taxes provider.</summary>
    /// <seealso cref="TaxesProviderBase"/>
    public class AvalaraTaxesProvider : TaxesProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration =>
            AvalaraTaxesProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <summary>Gets the origin address.</summary>
        /// <value>The origin address.</value>
        private static AddressModel OriginAddress => new()
        {
            Street1 = CEFConfigDictionary.ShippingOriginAddressStreet1,
            Street2 = CEFConfigDictionary.ShippingOriginAddressStreet2,
            Street3 = CEFConfigDictionary.ShippingOriginAddressStreet3,
            City = CEFConfigDictionary.ShippingOriginAddressCity,
            PostalCode = CEFConfigDictionary.ShippingOriginAddressPostalCode,
            RegionCode = CEFConfigDictionary.ShippingOriginAddressRegionCode,
            CountryCode = CEFConfigDictionary.ShippingOriginAddressCountryCode,
        };

        /// <summary>Gets the client.</summary>
        /// <value>The client.</value>
        private static AvaTaxClient Client => GetClient();

        /// <inheritdoc/>
        public override async Task<bool> InitAsync(string? contextProfileName)
        {
            try
            {
                await AvalaraTaxesProviderConfig.InitializeAsync(contextProfileName).ConfigureAwait(false);
                IsInitialized = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public override async Task<decimal> CalculateAsync(
            Enums.TaxEntityType taxEntityType, ICartModel cart, string? contextProfileName)
        {
            var response = await GetTaxAsync(taxEntityType, cart, contextProfileName).ConfigureAwait(false);
            if (response?.ResultCode == Models.SeverityLevel.Success != true)
            {
                return 0;
            }
            return response.TotalTax;
        }

        /// <inheritdoc/>
        public override async Task<TaxesResult> CalculateWithLineItemsAsync(
            Enums.TaxEntityType taxEntityType, ICartModel cart, string? contextProfileName)
        {
            var response = await GetTaxAsync(taxEntityType, cart, contextProfileName).ConfigureAwait(false);
            if (response?.ResultCode == Models.SeverityLevel.Success != true)
            {
                return new() { TotalTaxes = 0 };
            }
            return new()
            {
                CartID = cart.ID,
                CartSessionID = cart.SessionID,
                TotalTaxes = response.TotalTax,
                TaxLineItems = cart.SalesItems!
                    .Select(x => new TaxLineItemResult
                    {
                        CartItemID = x.ID,
                        ProductID = x.ProductID,
                        SKU = x.Sku,
                        Tax = response.TaxLines!.Single(y => y.LineNo == x.ProductKey).Tax,
                    })
                    .ToList(),
            };
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<string>> CommitAsync(
            Enums.TaxEntityType taxEntityType,
            ICartModel cartModel,
            string purchaseOrderNumber,
            string? contextProfileName)
        {
            var result = new CEFActionResponse<string>();
            if (!AvalaraTaxesProviderConfig.DocumentCommittingEnabled)
            {
                result.ActionSucceeded = false;
                return result;
            }
            var request = GetBaseTaxRequest(cartModel);
            request.PurchaseOrderNo = purchaseOrderNumber;
            // Now we log the transaction
            request.DocType = taxEntityType.ToAvalaraDocType(true);
            // Commit
            request.Commit = true;
            await LogEventAsync(contextProfileName, $"COMMIT REQUEST - {request}", "INFO").ConfigureAwait(false);
            var response = AvalaraTaxService.GetTax(request);
            await LogEventAsync(
                    contextProfileName,
                    $"COMMIT RESPONSE - {response}",
                    response.ResultCode == Models.SeverityLevel.Success ? "INFO" : "ERROR")
                .ConfigureAwait(false);
            if (response.ResultCode == Models.SeverityLevel.Success)
            {
                result.ActionSucceeded = true;
                result.Result = request.DocCode;
            }
            else
            {
                result.ActionSucceeded = false;
                foreach (var message in response.Messages)
                {
                    result.Messages.Add(message.Summary!);
                }
            }
            return result;
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<string>> VoidAsync(string taxTransactionID, string? contextProfileName)
        {
            var result = new CEFActionResponse<string>();
            var cancelRequest = new CancelTaxRequest
            {
                CancelCode = CancelCode.DocVoided,
                CompanyCode = AvalaraTaxesProviderConfig.CompanyCode,
                DocCode = taxTransactionID,
                DocType = DocType.SalesInvoice,
            };
            await LogEventAsync(contextProfileName, $"CALC REQUEST - {cancelRequest}", "INFO").ConfigureAwait(false);
            var response = AvalaraTaxService.CancelTax(cancelRequest);
            await LogEventAsync(contextProfileName, $"CALC REQUEST - {response}", "INFO").ConfigureAwait(false);
            if (response is not null)
            {
                if (response.ResultCode == Models.SeverityLevel.Success)
                {
                    result.ActionSucceeded = true;
                    result.Result = cancelRequest.DocCode;
                }
                else
                {
                    result.ActionSucceeded = false;
                    foreach (var message in response.Messages)
                    {
                        result.Messages.Add(message.Summary!);
                    }
                }
            }
            return result;
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> TestServiceAsync()
        {
            var response = AvalaraTaxService.Ping();
            return Task.FromResult(
                (response.ResultCode == Models.SeverityLevel.Success)
                    .BoolToCEFAR(response.Messages.Select(x => x.Summary).ToArray()));
        }

        /// <inheritdoc/>
        public override async Task<TaxesResult> CalculateCartAsync(
            ICartModel cart,
            int? userId,
            string? contextProfileName,
            TargetGroupingKey? key = null,
            string? vatId = null)
        {
            var result = new TaxesResult
            {
                TaxLineItems = new(),
                TotalTaxes = 0m,
            };
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                if (cart.SalesItems?.All(x => !x.Active) != false)
                {
                    return result;
                }
                var transaction = await GetTransactionAsync(
                        cart: cart,
                        userId: userId,
                        type: DocumentType.SalesOrder,
                        contextProfileName: contextProfileName,
                        context: context,
                        key: key)
                    .ConfigureAwait(false);
                if (transaction?.totalTax == null)
                {
                    return result;
                }
                result.TotalTaxes += transaction.totalTax.Value;
                await Logger.LogInformationAsync(
                        name: $"{nameof(AvalaraTaxesProvider)}.{nameof(CalculateCartAsync)}.Success.{cart.ID}",
                        message: JsonConvert.SerializeObject(new { Transaction = transaction }),
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                if (transaction.lines != null)
                {
                    result.TaxLineItems.AddRange(transaction.lines.Select(x => new TaxLineItemResult
                    {
                        SKU = x.itemCode,
                        Tax = x.tax ?? 0m,
                    }));
                }
                return result;
            }
            catch (AvaTaxError e)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(AvalaraTaxesProvider)}.{nameof(CalculateCartAsync)}.{nameof(AvaTaxError)}",
                        message: e.error!.error!.message,
                        ex: e,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return result;
            }
            catch (Exception e2)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(AvalaraTaxesProvider)}.{nameof(CalculateCartAsync)}.{e2.GetType().Name}",
                        message: e2.Message,
                        ex: e2,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return result;
            }
        }

        /// <inheritdoc/>
        public override async Task<TaxesResult> CommitCartAsync(
            ICartModel model,
            int? userId,
            string? contextProfileName,
            string? purchaseOrderNumber = null,
            string? vatId = null)
        {
            var result = new TaxesResult
            {
                TaxLineItems = new(),
                TotalTaxes = 0m,
            };
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                if (model.SalesItems?.All(x => !x.Active) != false)
                {
                    return result;
                }
                var transaction = await GetTransactionAsync(
                        cart: model,
                        userId: userId,
                        type: DocumentType.SalesInvoice,
                        contextProfileName: contextProfileName,
                        context: context,
                        key: null,
                        purchaseOrderNumber: purchaseOrderNumber,
                        commit: true)
                    .ConfigureAwait(false);
                if (transaction == null)
                {
                    return result;
                }
                if (transaction.totalTax.HasValue)
                {
                    result.TotalTaxes += transaction.totalTax.Value;
                }
                if (transaction.lines != null)
                {
                    result.TaxLineItems.AddRange(transaction.lines.Select(x => new TaxLineItemResult
                    {
                        SKU = x.itemCode,
                        Tax = x.tax ?? 0m,
                    }));
                }
                return result;
            }
            catch (AvaTaxError e)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(AvalaraTaxesProvider)}.{nameof(CommitCartAsync)}.{nameof(AvaTaxError)}",
                        message: e.error!.error!.message,
                        ex: e,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return result;
            }
            catch (Exception e2)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(AvalaraTaxesProvider)}.{nameof(CommitCartAsync)}.{e2.GetType().Name}",
                        message: e2.Message,
                        ex: e2,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return result;
            }
        }

        /// <inheritdoc/>
        public override async Task CommitReturnAsync(
            ISalesReturnModel salesReturn,
            string description,
            string? contextProfileName)
        {
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                await GetAdjustTransactionAsync(
                        salesReturn: salesReturn,
                        userId: salesReturn.UserID,
                        type: DocumentType.ReturnInvoice,
                        description: description,
                        reason: AdjustmentReason.ProductReturned,
                        contextProfileName: contextProfileName,
                        context: context)
                    .ConfigureAwait(false);
            }
            catch (AvaTaxError e)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(AvalaraTaxesProvider)}.{nameof(CommitReturnAsync)}.{nameof(AvaTaxError)}",
                        message: e.error!.error!.message,
                        ex: e,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            catch (Exception e2)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(AvalaraTaxesProvider)}.{nameof(CommitReturnAsync)}.{e2.GetType().Name}",
                        message: e2.Message,
                        ex: e2,
                        contextProfileName: null)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public override async Task VoidOrderAsync(ISalesOrderModel salesOrder, string? contextProfileName)
        {
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var transactionBuilder = await GetTransactionBuilderAsync(
                        model: salesOrder,
                        userID: salesOrder.UserID,
                        type: DocumentType.ReturnInvoice,
                        context: context,
                        purchaseOrderNumber: salesOrder.PurchaseOrderNumber,
                        commit: true,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                if (salesOrder.Discounts?.Any() == true)
                {
                    transactionBuilder.WithLine(amount: salesOrder.Discounts.Sum(x => x.DiscountTotal));
                }
                foreach (var salesItem in salesOrder.SalesItems!)
                {
                    transactionBuilder.WithLine(
                        amount: (salesItem.UnitSoldPrice ?? salesItem.UnitCorePrice) * salesItem.Quantity
                            + (salesItem.Discounts?.Sum(x => x.DiscountTotal) ?? 0m),
                        // ReSharper disable once StyleCop.SA1118
                        taxCode: Contract.CheckValidKey(salesItem.ProductTaxCode)
                            ? salesItem.ProductTaxCode
                            : !salesItem.ProductIsTaxable
                                ? "NT"
                                : null,
                        description: salesItem.Description,
                        itemCode: salesItem.ProductKey,
                        customerUsageType: salesOrder.Account?.TaxEntityUseCode);
                }
                transactionBuilder.Create();
            }
            catch (AvaTaxError e)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(AvalaraTaxesProvider)}.{nameof(VoidOrderAsync)}.{nameof(AvaTaxError)}",
                        message: e.error!.error!.message,
                        ex: e,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            catch (Exception e2)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(AvalaraTaxesProvider)}.{nameof(VoidOrderAsync)}.{e2.GetType().Name}",
                        message: e2.Message,
                        ex: e2,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public override async Task VoidReturnAsync(ISalesReturnModel salesReturn, string? contextProfileName)
        {
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var transactionBuilder = await GetTransactionBuilderAsync(
                        model: salesReturn,
                        userID: salesReturn.UserID,
                        type: DocumentType.SalesInvoice,
                        contextProfileName: contextProfileName,
                        context: context,
                        purchaseOrderNumber: salesReturn.PurchaseOrderNumber,
                        commit: true)
                    .ConfigureAwait(false);
                if (salesReturn.Discounts?.Any() == true)
                {
                    transactionBuilder.WithLine(amount: salesReturn.Discounts.Sum(x => x.DiscountTotal));
                }
                foreach (var salesItem in salesReturn.SalesItems!)
                {
                    transactionBuilder.WithLine(
                        amount: (salesItem.UnitSoldPrice ?? salesItem.UnitCorePrice) * salesItem.Quantity
                            + (salesItem.Discounts?.Sum(x => x.DiscountTotal) ?? 0m),
                        // ReSharper disable once StyleCop.SA1118
                        taxCode: Contract.CheckValidKey(salesItem.ProductTaxCode)
                            ? salesItem.ProductTaxCode
                            : !salesItem.ProductIsTaxable
                                ? "NT"
                                : null,
                        description: salesItem.Description,
                        itemCode: salesItem.ProductKey,
                        customerUsageType: salesReturn.Account?.TaxEntityUseCode);
                }
                transactionBuilder.Create();
            }
            catch (AvaTaxError e)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(AvalaraTaxesProvider)}.{nameof(VoidReturnAsync)}.{nameof(AvaTaxError)}",
                        message: e.error!.error!.message,
                        ex: e,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            catch (Exception e2)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(AvalaraTaxesProvider)}.{nameof(VoidReturnAsync)}.{e2.GetType().Name}",
                        message: e2.Message,
                        ex: e2,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
        }

        /// <summary>Gets base tax request.</summary>
        /// <param name="cartModel">The cart model.</param>
        /// <returns>The base tax request.</returns>
        private static GetTaxRequest GetBaseTaxRequest(ICartModel cartModel)
        {
            var request = new GetTaxRequest
            {
                DocDate = DateExtensions.GenDateTime.ToString("yyyy-MM-dd"),
                Client = AvalaraTaxesProviderConfig.EcommerceName,
                DetailLevel = DetailLevel.Tax,
                // Not a requirement, Avalara will assign a GUID, but will make searching more difficult, so assign
                // something, we don't have a Sales order ID, so Cart ID should work
                // Avalara Suggestion - Customer Unique code, lets try from the user ID first then the cartID
                DocCode = $"DOC-{DateTime.Today:yyMMdd}-{cartModel.ID}",
                CustomerCode = Contract.CheckValidID(cartModel.UserID)
                    ? $"U-{cartModel.UserID}"
                    : $"C-{cartModel.ID}",
                // Tax Code from Account (determines partial exemptions based on predefined values (govt, resale,
                // industrial, etc)
                CustomerUsageType = cartModel.Account?.IsTaxable ?? true
                    ? string.Empty
                    : cartModel.Account?.TaxEntityUseCode ?? string.Empty,
                // Exemption certificate value from the acct, if is taxable is selected and no exemption cert is
                // specified, supply a dummy value
                ExemptionNo = cartModel.Account?.IsTaxable ?? true
                    ? string.Empty
                    : cartModel.Account?.TaxExemptionNo ?? string.Empty,
                LocationCode = cartModel.StoreKey ?? "Default",
                CompanyCode = AvalaraTaxesProviderConfig.CompanyCode,
                Discount = Math.Abs(cartModel.Totals.Discounts),
                Lines = BuildRequestLineItems(
                    cartModel,
                    AvalaraTaxesProviderConfig.OriginCode,
                    AvalaraTaxesProviderConfig.DestinationCode),
            };
            var arr = new List<Address>();
            if (cartModel.ShippingContact?.Address is not null)
            {
                arr.Add(cartModel.ShippingContact.Address.ToAvalaraAddress(AvalaraTaxesProviderConfig.DestinationCode));
            }
            if (AvalaraTaxesProviderConfig.OriginAddress is not null)
            {
                AvalaraTaxesProviderConfig.OriginAddress.ToAvalaraAddress(AvalaraTaxesProviderConfig.OriginCode);
            }
            request.Addresses = arr.ToArray();
            if (!(cartModel.Account?.IsTaxable ?? true)
                && !Contract.CheckValidKey(request.ExemptionNo)
                && !Contract.CheckValidKey(request.CustomerUsageType))
            {
                request.ExemptionNo = "EXEMPT";
            }
            return request;
        }

        /// <summary>Builds request line items.</summary>
        /// <param name="cartModel">      The cart model.</param>
        /// <param name="originCode">     The origin code.</param>
        /// <param name="destinationCode">Destination code.</param>
        /// <returns>A Line[].</returns>
        private static Line[] BuildRequestLineItems(ICartModel cartModel, string originCode, string destinationCode)
        {
            var lines = new List<Line>();
            // With the way Avalara does discounts order level discounts need to be split among line items so if the
            // dollar amount for the order discounts are > 0, mark line items as discounted
            var orderDiscounts = cartModel.Discounts!.Where(x => x.Active).Sum(x => x.DiscountTotal) > 0.00m;
            ////var discountLineItems = Math.Abs(cartModel.Totals.Discounts) > 0.0m;
            // Sales Items
            lines.AddRange(cartModel.SalesItems!
                .Where(x => x.ItemType == Enums.ItemType.Item && x.ProductIsTaxable)
                .Select(i => i.ToAvalaraLine(originCode, destinationCode, orderDiscounts)));
            // Shipping
            if (cartModel.RateQuotes?.Any(x => x.Selected) == true)
            {
                var line = cartModel.RateQuotes
                    .First(x => x.Selected)
                    .ToAvalaraFreightLine(
                        AvalaraTaxesProviderConfig.OriginCode,
                        AvalaraTaxesProviderConfig.DestinationCode);
                if (line is not null)
                {
                    lines.Add(line);
                }
            }
            // Discounts - once there are BOGO style discounts, we'll need to do something
            return lines.ToArray();
        }

        /// <summary>Gets the client.</summary>
        /// <returns>The client.</returns>
        private static AvaTaxClient GetClient()
        {
            if (!CEFConfigDictionary.TaxesAvalaraAccountNumber.HasValue
                || !Contract.CheckValidKey(CEFConfigDictionary.TaxesAvalaraLicenseKey))
            {
                return null!;
            }
            return new AvaTaxClient(
                    appName: CEFConfigDictionary.APIName,
                    appVersion: string.Empty,
                    machineName: Environment.MachineName,
                    // ReSharper disable once StyleCop.SA1118
                    environment: CEFConfigDictionary.TaxesAvalaraEnableDevelopmentMode
                        ? AvaTaxEnvironment.Sandbox
                        : AvaTaxEnvironment.Production)
                .WithSecurity(
                    accountId: CEFConfigDictionary.TaxesAvalaraAccountNumber.Value,
                    licenseKey: CEFConfigDictionary.TaxesAvalaraLicenseKey!);
        }

        /// <summary>Gets origin address by originating store vendor warehouse.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <param name="key">    The key.</param>
        /// <returns>The origin address by originating store vendor warehouse.</returns>
        private static async Task<IAddressModel?> GetOriginAddressByOriginatingStoreVendorOrWarehouseAsync(
            IAmFilterableByNullableStoreModel model,
            IClarityEcommerceEntities context,
            TargetGroupingKey key)
        {
            if (!Enum.TryParse<Enums.TaxOrigins>(CEFConfigDictionary.TaxesUseOrigin, true, out var taxesUseOrigin))
            {
                return null;
            }
            switch (taxesUseOrigin)
            {
                case Enums.TaxOrigins.Store:
                {
                    return await OriginAddressByOriginatingStoreAsync(
                            model,
                            context,
                            key.StoreID)
                        .ConfigureAwait(false);
                }
                case Enums.TaxOrigins.Vendor:
                {
                    return await GetOriginAddressByOriginatingVendorAsync(
                            context,
                            key.VendorID)
                        .ConfigureAwait(false);
                }
                case Enums.TaxOrigins.InventoryLocation:
                {
                    return await GetOriginAddressByOriginatingWarehouseAsync(
                            context,
                            key.InventoryLocationID)
                        .ConfigureAwait(false);
                }
                default:
                {
                    return null;
                }
            }
        }

        private static async Task<IAddressModel?> OriginAddressByOriginatingStoreAsync(
            IAmFilterableByNullableStoreModel model,
            IClarityEcommerceEntities context,
            int? storeID)
        {
            if (storeID == null)
            {
                return model.Store?.Contact?.Address;
            }
            var storeProduct = await context.StoreProducts
                .SingleOrDefaultAsync(x => x.MasterID == storeID.Value)
                .ConfigureAwait(false);
            IAddressModel? originAddress = null;
            if (storeProduct != null)
            {
                originAddress = ModelMapperForAddress.MapAddressModelFromEntityFull(storeProduct.Master!.Contact?.Address, context.ContextProfileName);
            }
            return originAddress == null ? model.Store?.Contact?.Address : null;
        }

        private static Task<IAddressModel?> GetOriginAddressByOriginatingVendorAsync(
            IClarityEcommerceEntities context,
            int? vendorID)
        {
            if (Contract.CheckInvalidID(vendorID))
            {
                return Task.FromResult<IAddressModel?>(null);
            }
            return Task.FromResult(context.VendorProducts
                .AsNoTracking()
                .FilterByActive(true)
                .FilterIAmARelationshipTableByMasterID<DataModel.VendorProduct, DataModel.Vendor, DataModel.Product>(vendorID)
                .Select(x => x.Master!.Contact)
                .Where(x => x != null && x.Active)
                .Select(x => x!.Address!)
                .SelectSingleFullAddressAndMapToAddressModel(context.ContextProfileName));
        }

        private static async Task<IAddressModel?> GetOriginAddressByOriginatingWarehouseAsync(
            IClarityEcommerceEntities context,
            int? ilID)
        {
            if (Contract.CheckInvalidID(ilID))
            {
                return null;
            }
            var inventoryLocation = await context.InventoryLocationSections
                .SingleOrDefaultAsync(x => x.InventoryLocationID == ilID!.Value)
                .ConfigureAwait(false);
            if (inventoryLocation != null)
            {
                return ModelMapperForAddress.MapAddressModelFromEntityFull(
                    inventoryLocation.InventoryLocation!.Contact?.Address,
                    context.ContextProfileName);
            }
            return null;
        }

        /// <summary>Logs an event.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="message">           The message.</param>
        /// <param name="severity">          The severity.</param>
        /// <param name="key">               The key.</param>
        /// <returns>A Task.</returns>
        private static async Task LogEventAsync(
            string? contextProfileName,
            string message,
            string severity,
            string? key = null)
        {
            if (!AvalaraTaxesProviderConfig.DocumentCommittingEnabled && key == null)
            {
                return;
            }
            try
            {
                await Workflows.EventLogs.AddEventAsync(
                        message: message,
                        name: severity,
                        customKey: key,
                        dataID: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            catch
            {
                // Do Nothing
            }
        }

        /// <summary>Gets an event.</summary>
        /// <param name="request">           The request.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The event.</returns>
        private static async Task<GetTaxResult?> GetEventAsync(GetTaxRequest request, string? contextProfileName)
        {
            var hashCode = request.ToString().GetHashCode();
            var searchModel = RegistryLoaderWrapper.GetInstance<IEventLogSearchModel>(contextProfileName);
            searchModel.CustomKey = hashCode.ToString();
            searchModel.ModifiedSince = DateExtensions.GenDateTime.AddDays(-1);
            var response = await Workflows.EventLogs.GetLastAsync(searchModel, contextProfileName).ConfigureAwait(false);
            return response == null
                ? null
                : JsonConvert.DeserializeObject<GetTaxResult>(response.Description!.Replace("CALC RESPONSE - ", string.Empty));
        }

        /// <summary>Gets a tax.</summary>
        /// <param name="taxEntityType">     Type of the tax entity.</param>
        /// <param name="cartModel">         The cart model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The tax.</returns>
        private static async Task<GetTaxResult?> GetTaxAsync(
            Enums.TaxEntityType taxEntityType,
            ICartModel cartModel,
            string? contextProfileName)
        {
            if (!AvalaraTaxesProviderConfig.TaxServiceEnabled
                || !cartModel.SalesItems!.Any(x => x.ProductIsTaxable)
                || cartModel.ShippingContact?.Address.IsValidFull() != true)
            {
                return null;
            }
            var request = GetBaseTaxRequest(cartModel);
            // Determines if transaction is logged with Avalara, due to the way Avalara charges, we only log when we
            // commit (sales order is created)
            request.DocType = taxEntityType.ToAvalaraDocType();
            request.Commit = false;
            var response = await GetEventAsync(request, contextProfileName).ConfigureAwait(false);
            if (response?.ResultCode == Models.SeverityLevel.Success)
            {
                return response;
            }
            await LogEventAsync(
                    contextProfileName: contextProfileName,
                    message: $"CALC REQUEST - {request}",
                    severity: "INFO")
                .ConfigureAwait(false);
            response = AvalaraTaxService.GetTax(request);
            await LogEventAsync(
                    contextProfileName: contextProfileName,
                    message: $"CALC RESPONSE - {response}",
                    severity: response.ResultCode == Models.SeverityLevel.Success ? "INFO" : "ERROR",
                    key: request.ToString().GetHashCode().ToString())
                .ConfigureAwait(false);
            return response;
        }

        /// <summary>Gets transaction builder.</summary>
        /// <param name="model">              The model.</param>
        /// <param name="userID">             Identifier for the user.</param>
        /// <param name="type">               The type.</param>
        /// <param name="contextProfileName"> Name of the context profile.</param>
        /// <param name="context">            The context.</param>
        /// <param name="purchaseOrderNumber">The purchase order number.</param>
        /// <param name="key">                The key.</param>
        /// <param name="commit">             True to commit.</param>
        /// <returns>The transaction builder.</returns>
        private async Task<TransactionBuilder> GetTransactionBuilderAsync(
            ISalesCollectionBaseModel model,
            int? userID,
            DocumentType type,
            string? contextProfileName,
            IClarityEcommerceEntities context,
            string? purchaseOrderNumber = null,
            TargetGroupingKey? key = null,
            bool commit = false)
        {
            Contract.RequiresNotNull(Client);
            var customerCode = $"account{model.AccountID}user{userID}";
            var transactionBuilder = new TransactionBuilder(
                    Client, AvalaraTaxesProviderConfig.CompanyCodeSetting, type, customerCode)
                .WithDate(DateExtensions.GenDateTime);
            if (Contract.CheckValidKey(purchaseOrderNumber))
            {
                transactionBuilder.WithPurchaseOrderNumber(purchaseOrderNumber!);
            }
            if (Contract.CheckValidKey(model.Account?.TaxExemptionNo))
            {
                transactionBuilder.WithExemptionNumber(model.Account!.TaxExemptionNo!);
            }
            if (commit)
            {
                await Logger.LogInformationAsync(
                        name: $"{nameof(AvalaraTaxesProvider)}.{nameof(GetTransactionBuilderAsync)}.CommitRequest.{model.ID}",
                        message: $"{model.ID}",
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                transactionBuilder.WithCommit();
            }
            if (Contract.CheckValidKey(CEFConfigDictionary.TaxesAvalaraBusinessIdentificationNo))
            {
                transactionBuilder.WithBusinessIdentificationNumber(
                    CEFConfigDictionary.TaxesAvalaraBusinessIdentificationNo!);
            }
            if (Contract.CheckValidKey(AvalaraTaxesProviderConfig.CurrencyCode))
            {
                transactionBuilder.WithCurrencyCode(AvalaraTaxesProviderConfig.CurrencyCode);
            }
            // If Store.CustomKey is used, it needs to be added to Avalara as a location code
            // if (Contract.CheckValidKey(model.Store?.CustomKey))
            // {
            //     transactionBuilder.WithAddress(
            //         type: TransactionAddressType.ShipFrom,
            //         locationCode: model.Store.CustomKey);
            // }
            // else
            // {
            var originAddress = await GetOriginAddressAsync(model, contextProfileName, context, key).ConfigureAwait(false);
            transactionBuilder.WithAddress(
                type: TransactionAddressType.ShipFrom,
                line1: originAddress.Street1,
                line2: originAddress.Street2,
                line3: originAddress.Street3,
                city: originAddress.City,
                region: originAddress.RegionCode,
                postalCode: originAddress.PostalCode,
                // ReSharper disable once StyleCop.SA1118
                country: Contract.CheckValidKey(originAddress.CountryCode)
                    ? originAddress.CountryCode == "USA"
                        ? "US"
                        : originAddress.CountryCode
                    : originAddress.Country?.Code == "USA"
                        ? "US"
                        : originAddress.Country?.Code);
            // }
            if (model.ShippingContact?.Address != null)
            {
                transactionBuilder.WithAddress(
                    type: TransactionAddressType.ShipTo,
                    line1: model.ShippingContact.Address.Street1,
                    line2: model.ShippingContact.Address.Street2,
                    line3: model.ShippingContact.Address.Street3,
                    city: model.ShippingContact.Address.City,
                    region: model.ShippingContact.Address.RegionCode,
                    postalCode: model.ShippingContact.Address.PostalCode,
                    // ReSharper disable once StyleCop.SA1118
                    country: model.ShippingContact.Address.CountryCode == "USA"
                        ? "US"
                        : model.ShippingContact.Address.CountryCode);
            }
            return transactionBuilder;
        }

        /// <summary>Gets origin address.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="context">           The context.</param>
        /// <param name="key">               The key.</param>
        /// <returns>The origin address.</returns>
        private async Task<IAddressModel> GetOriginAddressAsync(
            IAmFilterableByNullableStoreModel model,
            string? contextProfileName,
            IClarityEcommerceEntities context,
            TargetGroupingKey? key = null)
        {
            IAddressModel? originAddress = null;
            try
            {
                if (model is ICartModel cart && cart.SalesItems!.All(x => x.ProductNothingToShip))
                {
                    originAddress = cart.BillingContact?.Address;
                }
                else if (Contract.CheckValidKey(CEFConfigDictionary.TaxesUseOrigin))
                {
                    originAddress = await GetOriginAddressByOriginatingStoreVendorOrWarehouseAsync(
                            model,
                            context,
                            key!)
                        .ConfigureAwait(false);
                }
                else if (Contract.CheckValidID(key?.StoreID))
                {
                    originAddress = await OriginAddressByOriginatingStoreAsync(
                            model,
                            context,
                            key!.StoreID)
                        .ConfigureAwait(false);
                }
                else if (Contract.CheckValidID(key?.VendorID))
                {
                    originAddress = await GetOriginAddressByOriginatingVendorAsync(
                            context,
                            key!.VendorID)
                        .ConfigureAwait(false);
                }
                else if (Contract.CheckValidID(key?.InventoryLocationID))
                {
                    originAddress = await GetOriginAddressByOriginatingWarehouseAsync(
                            context,
                            key!.InventoryLocationID)
                        .ConfigureAwait(false);
                }
            }
            catch (AvaTaxError e)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(AvalaraTaxesProvider)}.{nameof(GetOriginAddressAsync)}.{nameof(AvaTaxError)}",
                        message: e.error!.error!.message,
                        ex: e,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            catch (Exception e2)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(AvalaraTaxesProvider)}.{nameof(GetOriginAddressAsync)}.{e2.GetType().Name}",
                        message: e2.Message,
                        ex: e2,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            if (originAddress == null
                || string.IsNullOrEmpty(originAddress.PostalCode)
                || string.IsNullOrEmpty(originAddress.Street1)
                && string.IsNullOrEmpty(originAddress.City)
                && string.IsNullOrEmpty(originAddress.RegionCode ?? originAddress.Region?.Code))
            {
                originAddress = OriginAddress;
            }
            return originAddress;
        }

        /// <summary>Gets transaction.</summary>
        /// <param name="cart">               The cart.</param>
        /// <param name="userId">             Identifier for the user.</param>
        /// <param name="type">               The type.</param>
        /// <param name="contextProfileName"> Name of the context profile.</param>
        /// <param name="context">            The context.</param>
        /// <param name="key">                The key.</param>
        /// <param name="purchaseOrderNumber">The purchase order number.</param>
        /// <param name="commit">             True to commit.</param>
        /// <returns>The transaction.</returns>
        private async Task<TransactionModel?> GetTransactionAsync(
            ICartModel cart,
            int? userId,
            DocumentType type,
            string? contextProfileName,
            IClarityEcommerceEntities context,
            TargetGroupingKey? key = null,
            string? purchaseOrderNumber = null,
            bool commit = false)
        {
            TransactionModel? transaction = null;
            try
            {
                var transactionBuilder = await GetTransactionBuilderAsync(
                        model: cart,
                        userID: userId,
                        type: type,
                        context: context,
                        contextProfileName: contextProfileName,
                        purchaseOrderNumber: purchaseOrderNumber,
                        key: key,
                        commit: commit)
                    .ConfigureAwait(false);
                if (cart.Discounts?.Any() == true)
                {
                    transactionBuilder.WithLine(amount: cart.Discounts.Sum(x => x.DiscountTotal));
                }
                foreach (var salesItem in cart.SalesItems!)
                {
                    transactionBuilder.WithLine(
                        amount: (salesItem.UnitSoldPrice ?? salesItem.UnitCorePrice) * salesItem.TotalQuantity
                            + (salesItem.Discounts?.Sum(x => x.DiscountTotal) ?? 0m),
                        // ReSharper disable once StyleCop.SA1118
                        taxCode: Contract.CheckValidKey(salesItem.ProductTaxCode)
                            ? salesItem.ProductTaxCode
                            : !salesItem.ProductIsTaxable
                                ? "NT"
                                : null,
                        description: salesItem.Description,
                        itemCode: salesItem.ProductKey,
                        customerUsageType: cart.Account?.TaxEntityUseCode);
                }
                await Logger.LogInformationAsync(
                        $"{nameof(AvalaraTaxesProvider)}.{nameof(GetTransactionAsync)}.TransactionBuilder.{cart.ID}.Pre",
                        JsonConvert.SerializeObject(new { Builder = transactionBuilder }),
                        contextProfileName)
                    .ConfigureAwait(false);
                transaction = transactionBuilder.Create();
                await Logger.LogInformationAsync(
                        $"{nameof(AvalaraTaxesProvider)}.{nameof(GetTransactionAsync)}.TransactionBuilder.{cart.ID}.Post",
                        JsonConvert.SerializeObject(new { Transaction = transaction, Builder = transactionBuilder }),
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            catch (AvaTaxError e)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(AvalaraTaxesProvider)}.{nameof(GetTransactionAsync)}.{nameof(AvaTaxError)}",
                        message: e.error!.error!.message,
                        ex: e,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            catch (Exception e2)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(AvalaraTaxesProvider)}.{nameof(GetTransactionAsync)}.{e2.GetType().Name}",
                        message: e2.Message,
                        ex: e2,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            return transaction;
        }

        /// <summary>Gets adjust transaction.</summary>
        /// <param name="salesReturn">       The sales return.</param>
        /// <param name="userId">            Identifier for the user.</param>
        /// <param name="type">              The type.</param>
        /// <param name="description">       The description.</param>
        /// <param name="reason">            The reason.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="context">           The context.</param>
        /// <returns>The adjust transaction.</returns>
        private async Task<AdjustTransactionModel?> GetAdjustTransactionAsync(
            ISalesReturnModel salesReturn,
            int? userId,
            DocumentType type,
            string description,
            AdjustmentReason reason,
            string? contextProfileName,
            IClarityEcommerceEntities context)
        {
            AdjustTransactionModel? adjustTransaction = null;
            try
            {
                var transactionBuilder = await GetTransactionBuilderAsync(
                        model: salesReturn,
                        userID: userId,
                        type: type,
                        context: context,
                        contextProfileName: contextProfileName,
                        purchaseOrderNumber: salesReturn.PurchaseOrderNumber,
                        commit: true)
                    .ConfigureAwait(false);
                if (salesReturn.Discounts?.Any() == true)
                {
                    transactionBuilder.WithLine(amount: salesReturn.Discounts.Sum(x => x.DiscountTotal));
                }
                foreach (var salesItem in salesReturn.SalesItems!)
                {
                    transactionBuilder.WithLine(
                        amount: (salesItem.UnitSoldPrice ?? salesItem.UnitCorePrice) * salesItem.Quantity
                            + (salesItem.Discounts?.Sum(x => x.DiscountTotal) ?? 0m),
                        // ReSharper disable once StyleCop.SA1118
                        taxCode: Contract.CheckValidKey(salesItem.ProductTaxCode)
                            ? salesItem.ProductTaxCode
                            : !salesItem.ProductIsTaxable
                                ? "NT"
                                : null,
                        description: salesItem.Description,
                        itemCode: salesItem.ProductKey,
                        customerUsageType: salesReturn.Account?.TaxEntityUseCode);
                }
                adjustTransaction = transactionBuilder.CreateAdjustmentRequest(
                    desc: description,
                    reason: reason);
            }
            catch (AvaTaxError e)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(AvalaraTaxesProvider)}.{nameof(GetAdjustTransactionAsync)}.{nameof(AvaTaxError)}",
                        message: e.error!.error!.message,
                        ex: e,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            catch (Exception e2)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(AvalaraTaxesProvider)}.{nameof(GetAdjustTransactionAsync)}.{e2.GetType().Name}",
                        message: e2.Message,
                        ex: e2,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            return adjustTransaction;
        }

        public override Task<TaxesResult> CalculateCartAsync(ICartModel cart, int? userId, int? currentAccountId, string? contextProfileName, TargetGroupingKey? key = null, string? vatId = null)
        {
            throw new NotImplementedException();
        }
    }
}
