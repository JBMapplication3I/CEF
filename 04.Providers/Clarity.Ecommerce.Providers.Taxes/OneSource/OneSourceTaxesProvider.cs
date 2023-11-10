// <copyright file="OneSourceTaxesProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the OneSource taxes provider class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.OneSource
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;
    using Ecommerce.Models;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Mapper;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>An OneSource taxes provider.</summary>
    /// <seealso cref="TaxesProviderBase"/>
    public class OneSourceTaxesProvider : TaxesProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration =>
            OneSourceTaxesProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

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

        /// <inheritdoc/>
        public override Task<bool> InitAsync(string? contextProfileName)
        {
            IsInitialized = true;
            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        public override async Task<TaxesResult> CalculateCartAsync(
            ICartModel cart,
            int? userId,
            string? contextProfileName,
            TargetGroupingKey? key = null,
            string? vatId = null)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override async Task<TaxesResult> CalculateCartAsync(
            ICartModel cart,
            int? userId,
            int? currentAccountId,
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
                var taxCalculationResponse = new taxCalculationResponse();
                if (cart.ShippingContact != null)
                {
                    taxCalculationResponse = await RequestTaxEstimateAsync(
                            carts: new List<ICartModel> { cart },
                            userId: userId,
                            currentAccountId: currentAccountId,
                            contextProfileName: contextProfileName,
                            context: context,
                            key: key)
                        .ConfigureAwait(false);
                    if (taxCalculationResponse?.OUTDATA == null)
                    {
                        return result;
                    }
                    result.TotalTaxes += taxCalculationResponse.OUTDATA.INVOICE.Sum(x => decimal.Parse(x.TOTAL_TAX_AMOUNT));
                }
                else if (cart.TypeKey == "Cart" && CEFConfigDictionary.SplitShippingEnabled)
                {
                    var targetCartIDs = await context.Carts
                        .AsNoTracking()
                        .FilterCartsByLookupKey(
                            lookupKey: new(
                                typeKey: "Target-Grouping-",
                                sessionID: cart.SessionID!.Value,
                                userID: cart.UserID,
                                accountID: cart.AccountID),
                            useStartsWithPrefix: true)
                        .Select(x => x.ID)
                        .ToListAsync()
                        .ConfigureAwait(false);
                    if (targetCartIDs.Count > 0)
                    {
                        #region Target Carts For Tax
                        List<ICartModel> carts = (await context.Carts
                                .FilterByIDs(targetCartIDs)
                                .Select(x => new
                                {
                                    // Base
                                    x.ID,
                                    // Cart Properties
                                    x.SessionID,
                                    x.ShippingSameAsBilling,
                                    // Related Objects
                                    x.UserID,
                                    x.AccountID,
                                    x.BrandID,
                                    x.FranchiseID,
                                    x.StoreID,
                                    x.TypeID,
                                    Type = new
                                    {
                                        x.Type!.CustomKey,
                                        x.Type.Name,
                                    },
                                    // Addresses/Contacts
                                    x.BillingContactID,
                                    BillingContact = new
                                    {
                                        Address = new
                                        {
                                            x.BillingContact!.Address!.Street1,
                                            x.BillingContact!.Address!.Street2,
                                            x.BillingContact!.Address!.Street3,
                                            x.BillingContact!.Address!.City,
                                            x.BillingContact!.Address!.PostalCode,
                                            Country = new
                                            {
                                                x.BillingContact!.Address!.Country!.Code,
                                            },
                                            Region = new
                                            {
                                                x.BillingContact!.Address!.Region!.Code,
                                            },
                                        },
                                    },
                                    x.ShippingContactID,
                                    ShippingContact = new
                                    {
                                        Address = new
                                        {
                                            x.ShippingContact!.Address!.Street1,
                                            x.ShippingContact!.Address!.Street2,
                                            x.ShippingContact!.Address!.Street3,
                                            x.ShippingContact!.Address!.City,
                                            x.ShippingContact!.Address!.PostalCode,
                                            Country = new
                                            {
                                                x.ShippingContact!.Address!.Country!.Code,
                                            },
                                            Region = new
                                            {
                                                x.ShippingContact!.Address!.Region!.Code,
                                            },
                                        },
                                    },
                                    // Line Items
                                    SalesItems = x.SalesItems!
                                        .Where(y => y.Active)
                                        .Select(y => new
                                        {
                                            // Overridden data
                                            y.Name,
                                            y.Description,
                                            y.Sku,
                                            // Related Items
                                            y.ProductID,
                                            Product = new
                                            {
                                                y.Product!.CustomKey,
                                                y.Product!.Description,
                                                y.Product!.IsTaxable,
                                                y.Product!.Name,
                                                y.Product!.UnitOfMeasure,
                                            },
                                            y.UnitOfMeasure,
                                            // SalesItem Properties
                                            y.Quantity,
                                            y.QuantityBackOrdered,
                                            y.QuantityPreSold,
                                            y.UnitCorePrice,
                                            y.UnitSoldPrice,
                                            Discounts = y.Discounts!
                                                .Where(z => z.Active)
                                                .Select(z => new
                                                {
                                                    // Applied Discount Properties
                                                    z.DiscountTotal,
                                                }
                                                !),
                                        }
                                        !),
                                    // Associated Objects
                                    Discounts = x.Discounts!
                                        .Where(y => y.Active)
                                        .Select(y => new
                                        {
                                            // Applied Discount Properties
                                            y.DiscountTotal,
                                        }
                                        !),
                                    RateQuotes = x.RateQuotes!
                                        .Where(y => y.Active)
                                        .Select(y => new
                                        {
                                            // RateQuote Properties
                                            y.Rate,
                                            y.Selected,
                                        }
                                        !),
                                }
                                !)
                                .ToListAsync()
                                .ConfigureAwait(false))
                            // ReSharper disable once CyclomaticComplexity
                            .Select(x => new CartModel
                            {
                                // Base
                                ID = x.ID,
                                // Cart Properties
                                SessionID = x.SessionID,
                                ShippingSameAsBilling = x.ShippingSameAsBilling,
                                // Related Objects
                                UserID = x.UserID,
                                AccountID = x.AccountID,
                                BrandID = x.BrandID,
                                FranchiseID = x.FranchiseID,
                                StoreID = x.StoreID,
                                TypeID = x.TypeID,
                                TypeName = x.Type.Name,
                                // Addresses/Contacts
                                BillingContactID = x.BillingContactID,
                                BillingContact = new ContactModel
                                {
                                    Address = new AddressModel
                                    {
                                        Street1 = x.BillingContact!.Address!.Street1,
                                        Street2 = x.BillingContact!.Address!.Street2,
                                        Street3 = x.BillingContact!.Address!.Street3,
                                        City = x.BillingContact!.Address!.City,
                                        PostalCode = x.BillingContact!.Address!.PostalCode,
                                        Country = new CountryModel
                                        {
                                            Code = x.BillingContact!.Address!.Country!.Code,
                                        },
                                        Region = new RegionModel
                                        {
                                            Code = x.BillingContact!.Address!.Region!.Code,
                                        },
                                    },
                                },
                                ShippingContactID = x.ShippingContactID,
                                ShippingContact = new ContactModel
                                {
                                    Address = new AddressModel
                                    {
                                        Street1 = x.ShippingContact!.Address!.Street1,
                                        Street2 = x.ShippingContact!.Address!.Street2,
                                        Street3 = x.ShippingContact!.Address!.Street3,
                                        City = x.ShippingContact!.Address!.City,
                                        PostalCode = x.ShippingContact!.Address!.PostalCode,
                                        Country = new CountryModel
                                        {
                                            Code = x.ShippingContact!.Address!.Country!.Code,
                                        },
                                        Region = new RegionModel
                                        {
                                            Code = x.ShippingContact!.Address!.Region!.Code,
                                        },
                                    },
                                },
                                // Line Items
                                SalesItems = x.SalesItems
                                    // ReSharper disable once CyclomaticComplexity
                                    .Select(y => new SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>
                                    {
                                        // Overridden data
                                        Name = y.Name,
                                        Description = y.Description,
                                        Sku = y.Sku,
                                        // Related Items
                                        ProductID = y.ProductID,
                                        ProductIsTaxable = y.Product.IsTaxable,
                                        ProductKey = y.Product?.CustomKey,
                                        ProductName = y.Product?.Name,
                                        ProductDescription = y.Product?.Description,
                                        ProductUnitOfMeasure = y.Product?.UnitOfMeasure,
                                        Quantity = y.Quantity,
                                        QuantityBackOrdered = y.QuantityBackOrdered ?? 0m,
                                        QuantityPreSold = y.QuantityPreSold ?? 0m,
                                        UnitCorePrice = y.UnitCorePrice,
                                        UnitSoldPrice = y.UnitSoldPrice ?? y.UnitCorePrice,
                                        Discounts = y.Discounts
                                            .Select(z => new AppliedCartItemDiscountModel
                                            {
                                                // Applied Discount Properties
                                                DiscountTotal = z.DiscountTotal,
                                            })
                                            .ToList(),
                                    })
                                    .ToList(),
                                // Associated Objects
                                Discounts = x.Discounts
                                    .Select(y => new AppliedCartDiscountModel
                                    {
                                        // Applied Discount Properties
                                        DiscountTotal = y.DiscountTotal,
                                    })
                                    .ToList(),
                                RateQuotes = x.RateQuotes
                                    .Select(y => new RateQuoteModel
                                    {
                                        // RateQuote's Properties
                                        Rate = y.Rate,
                                        Selected = y.Selected,
                                    })
                                    .OrderBy(y => y.Rate ?? 0m)
                                    .ToList(),
                            })
                            .ToList<ICartModel>();
                        #endregion Target Carts For Tax

                        taxCalculationResponse = await RequestTaxEstimateAsync(
                                carts: carts,
                                userId: userId,
                                currentAccountId: currentAccountId,
                                contextProfileName: contextProfileName,
                                context: context,
                                key: key)
                            .ConfigureAwait(false);
                        if (taxCalculationResponse?.OUTDATA == null)
                        {
                            return result;
                        }
                        result.TotalTaxes += taxCalculationResponse.OUTDATA.INVOICE.Sum(x => decimal.Parse(x.TOTAL_TAX_AMOUNT));
                    }
                }
                await Logger.LogInformationAsync(
                        name: $"{nameof(OneSourceTaxesProvider)}.{nameof(CalculateCartAsync)}.Success.{cart.ID}",
                        message: JsonConvert.SerializeObject(new { Transaction = taxCalculationResponse }),
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                /*if (transaction.OUTDATA.INVOICE.LINE != null)
                {
                    result.TaxLineItems.AddRange(transaction.OUTDATA.INVOICE.LINE.Select(x => new TaxLineItemResult
                    {
                        SKU = x.itemCode,
                        Tax = x.tax ?? 0m,
                    }));
                }*/
                return result;
            }
            catch (Exception e2)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(OneSourceTaxesProvider)}.{nameof(CalculateCartAsync)}.{e2.GetType().Name}",
                        message: e2.Message,
                        ex: e2,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return result;
            }
        }

        #region Unimplemented Methods
#pragma warning disable 1998
        /// <inheritdoc/>
        public override async Task<TaxesResult> CommitCartAsync(
            ICartModel model,
            int? userId,
            string? contextProfileName,
            string? purchaseOrderNumber = null,
            string? vatId = null)
        {
            return new()
            {
                TaxLineItems = new(),
                TotalTaxes = 0,
            };
        }

        /// <inheritdoc/>
        public override async Task<TaxesResult> CalculateWithLineItemsAsync(
            Enums.TaxEntityType taxEntityType, ICartModel cart, string? contextProfileName)
        {
            return new()
            {
                TaxLineItems = new(),
                TotalTaxes = 0,
            };
        }

        /// <inheritdoc/>
        public override async Task CommitReturnAsync(
            ISalesReturnModel salesReturn,
            string description,
            string? contextProfileName)
        {
        }

        /// <inheritdoc/>
        public override async Task VoidOrderAsync(ISalesOrderModel salesOrder, string? contextProfileName)
        {
        }

        /// <inheritdoc/>
        public override async Task VoidReturnAsync(ISalesReturnModel salesReturn, string? contextProfileName)
        {
        }

        /// <inheritdoc/>
        public override async Task<decimal> CalculateAsync(
            Enums.TaxEntityType taxEntityType, ICartModel cart, string? contextProfileName)
        {
            return 0;
        }
#pragma warning restore 1998

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<string>> CommitAsync(
            Enums.TaxEntityType taxEntityType,
            ICartModel cartModel,
            string purchaseOrderNumber,
            string? contextProfileName)
        {
            return await Task.FromResult<CEFActionResponse<string>>(
                string.Empty.WrapInPassingCEFAR(
                    "NOTE! OneSourceTaxesProvider does not implement this functionality as it is not needed.")!);
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<string>> VoidAsync(string taxTransactionID, string? contextProfileName)
        {
            return await Task.FromResult<CEFActionResponse<string>>(
                string.Empty.WrapInPassingCEFAR(
                    "NOTE! OneSourceTaxesProvider does not implement this functionality as it is not needed.")!);
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> TestServiceAsync()
        {
            return Task.FromResult(CEFAR.PassingCEFAR());
        }
        #endregion Unimplemented Methods

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

        private static async Task<taxCalculationResponse?> PostOneSourceRequest(string requestXml)
        {
            var serviceUrl = OneSourceTaxesProviderConfig.ProviderMode == Enums.PaymentProviderMode.Production
                ? OneSourceTaxesProviderConfig.ServiceUrlProduction
                : OneSourceTaxesProviderConfig.ServiceUrlUat;

            using var httpClient = new HttpClient();
            var httpContent = new StringContent(requestXml, Encoding.UTF8, "text/xml");
            var response = await httpClient.PostAsync(serviceUrl, httpContent);

            if (response.IsSuccessStatusCode)
            {
                var xmlResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlResponse);
                if (xmlDoc.DocumentElement != null)
                {
                    using TextReader textReader = new StringReader(xmlDoc.DocumentElement.ChildNodes[0]!.InnerXml);
                    using var reader = new XmlTextReader(textReader);
                    reader.Namespaces = false;
                    var serializer = new XmlSerializer(typeof(taxCalculationResponse));
                    return (taxCalculationResponse?)serializer.Deserialize(reader);
                }
            }

            return null;
        }

        /// <summary>Gets transaction.</summary>
        /// <param name="carts">               The carts.</param>
        /// <param name="userId">             Identifier for the user.</param>
        /// <param name="contextProfileName"> Name of the context profile.</param>
        /// <param name="context">            The context.</param>
        /// <param name="key">                The key.</param>
        /// <returns>The transaction.</returns>
        private async Task<taxCalculationResponse?> RequestTaxEstimateAsync(
            List<ICartModel> carts,
            int? userId,
            int? currentAccountId,
            string? contextProfileName,
            IClarityEcommerceEntities context,
            TargetGroupingKey? key = null)
        {
            try
            {
                var soapEnvelope = @$"
                    <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                       <soap:Header>
                          <wsse:Security soap:mustUnderstand=""1"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"">
                             <wsse:UsernameToken>
                                <wsse:Username>{OneSourceTaxesProviderConfig.Username}</wsse:Username>
                                <wsse:Password Type=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText"">{OneSourceTaxesProviderConfig.Password}</wsse:Password>
                             </wsse:UsernameToken>
                          </wsse:Security>
                       </soap:Header>
                       <soap:Body>
                          <taxCalculationRequest xmlns=""http://www.sabrix.com/services/taxcalculationservice/2011-09-01"">
                             <INDATA version=""G"">
                                *INVOICES*
                             </INDATA>
                          </taxCalculationRequest>
                       </soap:Body>
                    </soap:Envelope>";
                var invoices = new List<string>();
                foreach (var cart in carts)
                {
                    var request = await GetInvoiceRequestAsync(
                            model: cart,
                            context: context,
                            contextProfileName: contextProfileName,
                            key: key,
                            currentAccountId: currentAccountId)
                        .ConfigureAwait(false);
                    decimal discountPercent = 0;
                    if (cart.Discounts?.Any() == true)
                    {
                        discountPercent = cart.Discounts.Sum(x => x.DiscountTotal) / cart.Totals.Total;
                    }
                    var lines = new List<string>();
                    var count = 1;
                    foreach (var salesItem in cart.SalesItems!)
                    {
                        var amount = (salesItem.UnitSoldPrice ?? salesItem.UnitCorePrice) * salesItem.TotalQuantity
                            + (salesItem.Discounts?.Sum(x => x.DiscountTotal) ?? 0m);
                        if (discountPercent > 0)
                        {
                            amount -= discountPercent * amount;
                        }
                        var taxCode = Contract.CheckValidKey(salesItem.ProductTaxCode)
                            ? salesItem.ProductTaxCode
                            : !salesItem.ProductIsTaxable
                                ? "NT"
                                : string.Empty;
                        var description = string.IsNullOrEmpty(salesItem.Description)
                            ? salesItem.Name
                            : salesItem.Description;
                        var line =
                            $@"<LINE ID=""{salesItem.ProductKey}"">
                                <LINE_NUMBER>{count}</LINE_NUMBER>
                                <GROSS_AMOUNT>{amount:0.00}</GROSS_AMOUNT>
                                <TAX_CODE>{taxCode}</TAX_CODE>
                                <DESCRIPTION>{description}</DESCRIPTION>
                                <PRODUCT_CODE>{salesItem.ProductKey}</PRODUCT_CODE>
                                <QUANTITY>{Math.Round(salesItem.TotalQuantity, 0, MidpointRounding.AwayFromZero)}</QUANTITY>
                            </LINE>";
                        lines.Add(line);
                        count++;
                    }
                    if (cart.RateQuotes?.Any(x => x.Selected) == true)
                    {
                        var rateQuote = cart.RateQuotes.First(x => x.Selected);
                        var line =
                            $@"<LINE ID=""FR000000"">
                                <LINE_NUMBER>{count}</LINE_NUMBER>
                                <GROSS_AMOUNT>{rateQuote.Rate:0.00}</GROSS_AMOUNT>
                                <TAX_CODE>FR000000</TAX_CODE>
                                <DESCRIPTION>FREIGHT</DESCRIPTION>
                                <QUANTITY>1</QUANTITY>
                            </LINE>";
                        if (line is not null)
                        {
                            lines.Add(line);
                        }
                    }
                    request = request.Replace("*LINE*", lines.Aggregate((i, j) => i + j).ToString());
                    invoices.Add(request);
                }

                soapEnvelope = soapEnvelope.Replace("*INVOICES*", invoices.Aggregate((i, j) => i + j).ToString());
                await Logger.LogInformationAsync(
                        $"{nameof(OneSourceTaxesProvider)}.{nameof(RequestTaxEstimateAsync)}.Pre",
                        soapEnvelope,
                        contextProfileName)
                    .ConfigureAwait(false);

                // Submit tax request
                var response = await PostOneSourceRequest(soapEnvelope).ConfigureAwait(false);

                await Logger.LogInformationAsync(
                        $"{nameof(OneSourceTaxesProvider)}.{nameof(RequestTaxEstimateAsync)}.Post",
                        JsonConvert.SerializeObject(new { Response = response, Request = soapEnvelope }),
                        contextProfileName)
                    .ConfigureAwait(false);

                return response;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(OneSourceTaxesProvider)}.{nameof(RequestTaxEstimateAsync)}.{ex.GetType().Name}",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                throw;
            }
        }

        /// <summary>Gets tax request.</summary>
        /// <param name="model">              The model.</param>
        /// <param name="contextProfileName"> Name of the context profile.</param>
        /// <param name="context">            The context.</param>
        /// <param name="key">                The key.</param>
        /// <returns>The transaction builder.</returns>
        private async Task<string> GetInvoiceRequestAsync(
            ISalesCollectionBaseModel model,
            string? contextProfileName,
            IClarityEcommerceEntities context,
            TargetGroupingKey? key = null,
            int? currentAccountId = null)
        {
            var acctAttrs = string.Empty;
            if (Contract.CheckValidID(currentAccountId))
            {
                acctAttrs = await context.Accounts.FilterByID(currentAccountId).Select(x => x.JsonAttributes).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            var originAddress = await GetOriginAddressAsync(model, contextProfileName, context, key).ConfigureAwait(false);
            var requestString = string.IsNullOrWhiteSpace(acctAttrs)
                ? @$"
                <INVOICE>
                    <EXTERNAL_COMPANY_ID>{OneSourceTaxesProviderConfig.ExternalCompanyID}</EXTERNAL_COMPANY_ID>
                    <CALCULATION_DIRECTION>F</CALCULATION_DIRECTION>
                    <COMPANY_ROLE>S</COMPANY_ROLE>
                    <CURRENCY_CODE>{OneSourceTaxesProviderConfig.CurrencyCode}</CURRENCY_CODE>
                    <INVOICE_DATE>{model.CreatedDate:yyyy-MM-dd}</INVOICE_DATE>
                    <IS_AUDITED>FALSE</IS_AUDITED>
                    <TRANSACTION_TYPE>GS</TRANSACTION_TYPE>
                    *SHIP_FROM*
                    *SHIP_TO*
                    *LINE*
                </INVOICE>"
                : @$"
                <INVOICE>
                    <EXTERNAL_COMPANY_ID>{OneSourceTaxesProviderConfig.ExternalCompanyID}</EXTERNAL_COMPANY_ID>
                    <CALCULATION_DIRECTION>F</CALCULATION_DIRECTION>
                    <CUSTOMER_NUMBER>{acctAttrs.DeserializeAttributesDictionary()["accountNumber"]?.Value}</CUSTOMER_NUMBER>
                    <COMPANY_ROLE>S</COMPANY_ROLE>
                    <CURRENCY_CODE>{OneSourceTaxesProviderConfig.CurrencyCode}</CURRENCY_CODE>
                    <INVOICE_DATE>{model.CreatedDate:yyyy-MM-dd}</INVOICE_DATE>
                    <IS_AUDITED>FALSE</IS_AUDITED>
                    <TRANSACTION_TYPE>GS</TRANSACTION_TYPE>
                    *SHIP_FROM*
                    *SHIP_TO*
                    *LINE*
                </INVOICE>";

            var originCountry = Contract.CheckValidKey(originAddress.CountryCode)
                ? originAddress.CountryCode == "USA"
                    ? "US"
                    : originAddress.CountryCode
                : originAddress.Country?.Code == "USA"
                    ? "US"
                    : originAddress.Country?.Code;
            var shipFrom =
                @$"<SHIP_FROM>
                    <ADDRESS_1>{originAddress.Street1}</ADDRESS_1>
                    <ADDRESS_2>{originAddress.Street2}</ADDRESS_2>
                    <ADDRESS_3>{originAddress.Street3}</ADDRESS_3>
                    <CITY>{originAddress.City}</CITY>
                    <STATE>{originAddress.RegionCode}</STATE>
                    <POSTCODE>{originAddress.PostalCode}</POSTCODE>
                    <COUNTRY>{originCountry}</COUNTRY>"
                + "</SHIP_FROM>";
            requestString = requestString.Replace("*SHIP_FROM*", shipFrom);

            var shipTo = string.Empty;
            if (model.ShippingContact != null)
            {
                var shipToCountry = Contract.CheckValidKey(model.ShippingContact.Address?.CountryCode)
                ? model.ShippingContact.Address?.CountryCode == "USA"
                    ? "US"
                    : model.ShippingContact.Address?.CountryCode
                : model.ShippingContact.Address?.Country?.Code == "USA"
                    ? "US"
                    : model.ShippingContact.Address?.Country?.Code;
                shipTo =
                    @$"<SHIP_TO>
                        <ADDRESS_1>{model.ShippingContact.Address?.Street1}</ADDRESS_1>
                        <ADDRESS_2>{model.ShippingContact.Address?.Street2}</ADDRESS_2>
                        <ADDRESS_3>{model.ShippingContact.Address?.Street3}</ADDRESS_3>
                        <CITY>{model.ShippingContact.Address?.City}</CITY>
                        <STATE>{model.ShippingContact.Address?.RegionCode}</STATE>
                        <POSTCODE>{model.ShippingContact.Address?.PostalCode}</POSTCODE>
                        <COUNTRY>{shipToCountry}</COUNTRY>"
                    + "</SHIP_TO>";
            }
            requestString = requestString.Replace("*SHIP_TO*", shipTo);

            return requestString;
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
            catch (Exception e2)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(OneSourceTaxesProvider)}.{nameof(GetOriginAddressAsync)}.{e2.GetType().Name}",
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
    }
}
