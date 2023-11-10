// <copyright file="OraclePricingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Oracle pricing provider class</summary>
namespace Clarity.Ecommerce.Providers.Pricing.Oracle
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Pricing;
    using Models;
    using Newtonsoft.Json;
    using PSTRequest;
    using PSTResponse;
    using ServiceStack.Auth;
    using Utilities;

    /// <summary>A Oracle pricing provider.</summary>
    /// <seealso cref="PricingProviderBase"/>
    public class OraclePricingProvider : PricingProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => OraclePricingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        public async Task<List<ICalculatedPrice>> CalculatePricesAsync(
            List<Tuple<IPricingFactoryProductModel, IPricingFactoryContextModel>> factoryProductsAndContexts,
            string? contextProfileName,
            bool? forCart = false)
        {
            var response = await PriceSalesTransactionAsync(
                    factoryProductsAndContexts,
                    contextProfileName,
                    forCart: forCart)
                .ConfigureAwait(false);
            if (response is null)
            {
                return new();
            }
            var unitPrices = response.ChargeComponent
                .Where(x => x.PriceElementUsageCode == "QP_BASE_LIST_PRICE")
                .Select(x => x.ExtendedAmount?.Value ?? 0)
                .ToList();
            var salePrices = response.ChargeComponent
                .Where(x => x.PriceElementUsageCode == "QP_NET_PRICE")
                .Select(x => x.ExtendedAmount?.Value ?? 0)
                .ToList();
            List<ICalculatedPrice> result = new();
            for (var i = 0; i < factoryProductsAndContexts.Count; i++)
            {
                result.Add(
                    new CalculatedPrice(
                        Name,
                        (double)(unitPrices![i] / factoryProductsAndContexts[i].Item2.Quantity),
                        (double?)(unitPrices![i] / factoryProductsAndContexts[i].Item2.Quantity))
                    {
                        PriceKey = await GetPriceKeyAsync(factoryProductsAndContexts[i].Item1, factoryProductsAndContexts[i].Item2).ConfigureAwait(false),
                        PriceKeyAlt = await GetPriceKeyAltAsync(factoryProductsAndContexts[i].Item1, factoryProductsAndContexts[i].Item2).ConfigureAwait(false),
                    });
            }
            return result;
        }

        /// <inheritdoc/>
        public override async Task<ICalculatedPrice> CalculatePriceAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext,
            string? contextProfileName,
            bool? forCart = false)
        {
            var response = await PriceSalesTransactionAsync(
                    new List<Tuple<IPricingFactoryProductModel, IPricingFactoryContextModel>>
                    {
                        Tuple.Create(factoryProduct, factoryContext),
                    },
                    contextProfileName,
                    forCart: forCart)
                .ConfigureAwait(false);
            if (response is null || response?.ChargeComponent is null)
            {
                return new CalculatedPrice(Name, -1);
            }
            var prices = new List<MultiUOMCalculatedPrice>();
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var priceList = await context.ProductPricePoints.AsNoTracking().FilterByActive(true).Where(x => x.MasterID == factoryProduct.ProductID).Select(y => y.Slave!.Name).ToListAsync().ConfigureAwait(false);
            foreach (var line in response.Line!)
            {
                var chargeId = response.Charge.SingleOrDefault(x => x.LineId == line.LineId)?.ChargeId;
                if (chargeId is null)
                {
                    continue;
                }
                var unitPrice = response.ChargeComponent.SingleOrDefault(x => x.ChargeId == chargeId && x.PriceElementCode == "QP_BASE_LIST_PRICE")?.ExtendedAmount?.Value ?? 0m;
                var salePrice = response.ChargeComponent.SingleOrDefault(x => x.ChargeId == chargeId && x.PriceElementCode == "QP_NET_PRICE")?.ExtendedAmount?.Value ?? 0m;
                var uom = response.Charge.SingleOrDefault(x => x.ChargeId == chargeId)?.PricedQuantityUOMCode;
                var priceListName = response.ChargeComponent.FirstOrDefault(x => x.ExplanationMessageName == "QP_BASE_LIST_PRICE_CHARGE").Explanation?.Replace("Base List Price Applied from ", string.Empty);
                //if (Contract.CheckNotNull(factoryProduct.CartItemSerializableAttributes)
                //    && factoryProduct.CartItemSerializableAttributes!.TryGetValue("SelectedUOM", out var selectedUOM)
                //    && Contract.CheckNotNull(selectedUOM?.Value))
                //{
                //    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                //    var productAttributes = await context.Products
                //        .FilterByID(factoryProduct.ProductID)
                //        .Select(x => x.JsonAttributes)
                //        .SingleOrDefaultAsync()
                //        .ConfigureAwait(false);
                //    if (productAttributes.DeserializeAttributesDictionary().TryGetValue(selectedUOM!.Value, out var conversion))
                //    {
                //        unitPrice *= decimal.Parse(conversion.Value);
                //    }
                //}
                prices.Add(new MultiUOMCalculatedPrice()
                {
                    PricingProvider = Name,
                    BasePrice = unitPrice / factoryContext.Quantity,
                    SalePrice = salePrice / factoryContext.Quantity,
                    PriceKey = await GetPriceKeyAsync(factoryProduct, factoryContext).ConfigureAwait(false),
                    PriceKeyAlt = await GetPriceKeyAltAsync(factoryProduct, factoryContext).ConfigureAwait(false),
                    ProductUnitOfMeasure = uom,
                    PriceListName = priceListName?.ToLower() != "medsurg retail price list" && priceListName?.ToLower() != "ems retail price list"
                        ? priceListName
                        : string.Empty,
                });
            }
            string? selectedUOM = null;
            if (Contract.CheckNotNull(factoryProduct.CartItemSerializableAttributes)
                && factoryProduct.CartItemSerializableAttributes!.TryGetValue("SelectedUOM", out var selectedUOMObj))
            {
                selectedUOM = selectedUOMObj.Value;
            }
            return selectedUOM is null
                ? new CalculatedPrice(
                    Name,
                    (double)prices[0].BasePrice,
                    (double)prices[0].SalePrice!.Value)
                {
                    PriceKey = prices[0].PriceKey,
                    PriceKeyAlt = prices[0].PriceKeyAlt,
                    MultiUOMPrices = prices,
                }
                : new CalculatedPrice(
                    Name,
                    (double)prices.Where(x => x.ProductUnitOfMeasure!.ToLower() == selectedUOM.ToLower()).Select(y => y.BasePrice).SingleOrDefault(),
                    (double)prices.Where(x => x.ProductUnitOfMeasure!.ToLower() == selectedUOM.ToLower()).Select(y => y.SalePrice ?? 0m).SingleOrDefault())
                {
                    PriceKey = prices[0].PriceKey,
                    PriceKeyAlt = prices[0].PriceKeyAlt,
                    MultiUOMPrices = prices,
                };
        }

        /// <inheritdoc/>
        public override Task<string> GetPriceKeyAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext)
        {
            return Task.FromResult(
                nameof(OraclePricingProvider)
                    + $":P={factoryProduct.ProductID}");
        }

        /// <inheritdoc/>
        public override Task<string> GetPriceKeyAltAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext)
        {
            return Task.FromResult(
                JsonConvert.SerializeObject(new
                {
                    PriceAltKeyProvider = nameof(OraclePricingProvider),
                    PriceAltKeyProductID = $"{factoryProduct.ProductID}",
                }));
        }

        /// <summary>Request the sales transaction pricing.</summary>
        /// <param name="factoryProductsAndContexts">The factory products.</param>
        /// <param name="contextProfileName">        Name of the context profile.</param>
        /// <param name="forCart">                   Flag for cart item price calculation.</param>
        /// <returns>The response from PriceSalesTransaction.</returns>
        // ReSharper disable once UnusedParameter.Local
        private static async Task<PriceSalesTransactionResponse?> PriceSalesTransactionAsync(
            List<Tuple<IPricingFactoryProductModel, IPricingFactoryContextModel>> factoryProductsAndContexts,
            string? contextProfileName,
            bool? forCart = false)
        {
            var transactionResults = new List<PriceSalesTransactionResponse?>();
            var firstFactoryContext = factoryProductsAndContexts.First().Item2;
            var customerID = long.TryParse(firstFactoryContext.AccountKey, out long tempCustomerID)
                ? tempCustomerID
                : 0;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var accountJsonAttributes = firstFactoryContext.AccountID == null
                ? null
                : await context.Accounts
                    .AsNoTracking()
                    .FilterByID(firstFactoryContext.AccountID)
                    .Select(x => x.JsonAttributes)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
            var accountSerializableAttributes = accountJsonAttributes.DeserializeAttributesDictionary();
            var transactionOn = DateTime.Now;
            PriceSalesTransactionRequest pstRequest = new()
            {
                Header = new List<PSTRequest.Header>
                {
                    new PSTRequest.Header
                    {
                        HeaderId = 1,
                        AppliedCurrencyCode = OraclePricingProviderConfig.AppliedCurrencyCode,
                        CalculatePricingChargesFlag = true,
                        CalculateShippingChargesFlag = false,
                        CalculateTaxFlag = false,
                        CustomerId = customerID,
                        TransactionOn = transactionOn,
                        TransactionTypeCode = "ORA_SALES_ORDER",
                        SellingBusinessUnitName = OraclePricingProviderConfig.SellingBusinessUnitName,
                    },
                },
                Line = new List<PSTRequest.Line>(),
                PricingServiceParameter = new PSTRequest.PricingServiceParameter
                {
                    PricingContext = "SALES",
                    PerformValueIdConversionsFlag = true,
                },
            };
            #region JBM specific logic
            var jnbPricingTier = accountSerializableAttributes.ContainsKey("division")
                ? accountSerializableAttributes["division"].Value
                : "Web";
            var jnbBuyingGroup = accountSerializableAttributes.ContainsKey("primaryPricingBuyingGroup")
                ? accountSerializableAttributes["primaryPricingBuyingGroup"].Value
                : string.Empty;
            pstRequest.PricingHdrEff_Custom = new List<PSTRequest.PricingHdrEffCustom>
            {
                new PSTRequest.PricingHdrEffCustom
                {
                    HeaderId_Custom = 1,
                    jnbTier_Custom = jnbPricingTier,
                    buyingGroup_Custom = jnbBuyingGroup,
                },
            };
            #endregion JBM specific logic
            var i = 1;
            foreach (var item in factoryProductsAndContexts)
            {
                var uoms = new List<string>();
                if (forCart is false
                    && item.Item1?.SerializableAttributes is not null
                    && item.Item1.SerializableAttributes.Count > 1
                    && item.Item1.SerializableAttributes.TryGetValue("AvailableUOMs", out var uomObj))
                {
                    uoms = uomObj.Value.Split(',').ToList();
                }
                if (forCart is true
                    && item.Item1?.CartItemSerializableAttributes is not null
                    && item.Item1.CartItemSerializableAttributes!.TryGetValue("SelectedUOM", out var selected))
                {
                    uoms.Add(selected.Value);
                }
                if (uoms.Count > 0)
                {
                    foreach (var uom in uoms)
                    {
                        pstRequest.Line.Add(new PSTRequest.Line
                        {
                            HeaderId = 1,
                            LineId = i,
                            AllowPriceListUpdateFlag = true,
                            InventoryItemId = item.Item1!.SerializableAttributes?.ContainsKey("InventoryItemId") == true
                        && long.TryParse(item.Item1.SerializableAttributes["InventoryItemId"].Value, out long inventoryItemId1)
                        ? inventoryItemId1
                        : 0,
                            InventoryOrganizationCode = OraclePricingProviderConfig.InventoryOrganizationCode,
                            LineCategoryCode = "ORDER",
                            LineQuantityUOMCode = uom,
                            LineQuantity = new PSTRequest.LineQuantity
                            {
                                Value = (int)Math.Ceiling(item.Item2.Quantity),
                                UomCode = uom,
                            },
                            LineTypeCode = "ORA_BUY",
                            SkipShippingChargesFlag = true,
                            TransactionOn = transactionOn,
                        });
                        i++;
                    }
                }
                else
                {
                    pstRequest.Line.Add(new PSTRequest.Line
                    {
                        HeaderId = 1,
                        LineId = i,
                        AllowPriceListUpdateFlag = true,
                        InventoryItemId = item.Item1?.SerializableAttributes?.ContainsKey("InventoryItemId") == true
                        && long.TryParse(item.Item1.SerializableAttributes["InventoryItemId"].Value, out long inventoryItemId)
                        ? inventoryItemId
                        : 0,
                        InventoryOrganizationCode = OraclePricingProviderConfig.InventoryOrganizationCode,
                        LineCategoryCode = "ORDER",
                        LineQuantityUOMCode = item.Item1?.ProductUnitOfMeasure,
                        LineQuantity = new PSTRequest.LineQuantity
                        {
                            Value = (int)Math.Ceiling(item.Item2.Quantity),
                            UomCode = item.Item1?.ProductUnitOfMeasure,
                        },
                        LineTypeCode = "ORA_BUY",
                        SkipShippingChargesFlag = true,
                        TransactionOn = transactionOn,
                    });
                    i++;
                }
            }
            const string URI = "/fscmRestApi/priceExecution/documentPrices/priceSalesTransaction";
            var request = (HttpWebRequest)WebRequest.Create(OraclePricingProviderConfig.BaseUrl + URI);
            request.Method = "POST";
            request.ContentType = "application/json";
            string svcCredentials = Convert.ToBase64String(
                Encoding.ASCII.GetBytes(OraclePricingProviderConfig.Username
                + ":"
                + OraclePricingProviderConfig.Password));
            request.Headers.Add("Authorization", "Basic " + svcCredentials);
            var jsonContent = JsonConvert.SerializeObject(pstRequest);
            byte[] byteArray = Encoding.UTF8.GetBytes(jsonContent);
            request.ContentLength = byteArray.Length;
            using var reqStream = request.GetRequestStream();
            reqStream.Write(byteArray, 0, byteArray.Length);
            string json;
            try
            {
                using var response = (HttpWebResponse)await request.GetResponseAsync().ConfigureAwait(false);
                json = await ReadResponseStreamAsync(response.GetResponseStream() ?? throw new InvalidOperationException()).ConfigureAwait(false);
            }
            catch (WebException webEx)
            {
                json = await ReadResponseStreamAsync(webEx.Response!.GetResponseStream() ?? throw new InvalidOperationException()).ConfigureAwait(false);
                await Logger.LogErrorAsync(
                        name: $"{nameof(OraclePricingProvider)}.{nameof(PriceSalesTransactionAsync)}.WebError",
                        message: webEx.Message + "\r\n" + json,
                        ex: webEx,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(OraclePricingProvider)}.{nameof(PriceSalesTransactionAsync)}.Error",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return null;
            }
            return JsonConvert.DeserializeObject<PriceSalesTransactionResponse>(json);
        }

        private static async Task<string> ReadResponseStreamAsync(Stream stream)
        {
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }
    }
}
